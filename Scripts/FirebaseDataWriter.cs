using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;

public class FirebaseDataWriter : MonoBehaviour
{
    //[SerializeField] private Button saveAllButton; // Barcha ma'lumotlarni saqlash tugmasi
    //[SerializeField] private Button loadDataButton; // Ma'lumotlarni yuklash tugmasi
    //[SerializeField] private Button syncButton; // Sinxronlash tugmasi

    private DatabaseReference dbReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;

                //// Tugmalar uchun listenerlar
                //saveAllButton.onClick.AddListener(SaveAllOrdersToFirebase);
                //loadDataButton.onClick.AddListener(LoadDataFromFirebase);
                //syncButton.onClick.AddListener(SyncDataWithFirebase);

                Debug.Log("Firebase tayyor!");
            }
            else
            {
                Debug.LogError("Firebase mavjud emas: " + task.Result.ToString());
            }
        });
    }

    // ShowQabulQilingan dan barcha buyurtmalarni Firebase ga saqlash
    public void SaveAllOrdersToFirebase()
    {
        if (ShowQabulQilingan.Instance == null)
        {
            Debug.LogError("ShowQabulQilingan Instance topilmadi!");
            return;
        }

        List<OrderDataQabul> orders = ShowQabulQilingan.Instance.orderListQabul;

        if (orders.Count == 0)
        {
            Debug.LogWarning("Saqlash uchun buyurtmalar yo'q!");
            return;
        }

        foreach (OrderDataQabul order in orders)
        {
            SaveSingleOrderToFirebase(order);
        }

        Debug.Log($"{orders.Count} ta buyurtma Firebase ga saqlandi!");
    }

    // Bitta buyurtmani Firebase ga saqlash
    private void SaveSingleOrderToFirebase(OrderDataQabul order)
    {
        // Unikal ID yaratish (agar bo'sh bo'lsa)
        if (string.IsNullOrEmpty(order.uniqueId))
        {
            order.uniqueId = System.Guid.NewGuid().ToString();
        }

        // Joriy sana va vaqtni olish
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        order.saveTime = saveTime;

        // OrderDataQabul obyektini JSON ga aylantirish
        string json = JsonUtility.ToJson(order);

        // Firebase ga yozish
        dbReference.Child("Orders").Child(order.uniqueId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Buyurtma saqlandi: ID = {order.uniqueId}, Ism = {order.name}");
            }
            else
            {
                Debug.LogError("Buyurtmani saqlashda xatolik: " + task.Exception);
            }
        });
    }

    // Firebase dan ma'lumotlarni yuklash
    public void LoadDataFromFirebase()
    {
        dbReference.Child("Orders").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                if (ShowQabulQilingan.Instance != null)
                {
                    ShowQabulQilingan.Instance.orderListQabul.Clear();
                }

                foreach (DataSnapshot orderSnapshot in snapshot.Children)
                {
                    string orderData = orderSnapshot.GetRawJsonValue();
                    OrderDataQabul loadedOrder = JsonUtility.FromJson<OrderDataQabul>(orderData);

                    if (ShowQabulQilingan.Instance != null)
                    {
                        ShowQabulQilingan.Instance.orderListQabul.Add(loadedOrder);
                    }

                    Debug.Log($"Yuklandi: ID: {loadedOrder.uniqueId}, Ism: {loadedOrder.name}, Telefon: {loadedOrder.phone}, Holati: {loadedOrder.holati}");
                }

                Debug.Log("Barcha ma'lumotlar Firebase dan yuklandi!");
                RefreshUI();
            }
            else
            {
                Debug.LogError("Ma'lumotlarni yuklashda xatolik: " + task.Exception);
            }
        });
    }

    // Ma'lumotlarni sinxronlash (yangi buyurtmalarni saqlash va o'zgarishlarni yangilash)
    public void SyncDataWithFirebase()
    {
        // Avval Firebase dan yuklash
        LoadDataFromFirebase();

        // Keyin local ma'lumotlarni saqlash
        SaveAllOrdersToFirebase();
    }

    // UI ni yangilash (bu metodda siz o'z UI yangilash logikangizni qo'shishingiz mumkin)
    private void RefreshUI()
    {
        // Bu yerda agar kerak bo'lsa UI elementlarini qayta yaratish logikasini qo'shing
        Debug.Log("UI yangilandi!");
    }

    // Yangi buyurtma qo'shilganda avtomatik saqlash
    public void SaveNewOrder(OrderDataQabul newOrder)
    {
        SaveSingleOrderToFirebase(newOrder);
    }

    // Buyurtmani o'chirish
    public void DeleteOrderFromFirebase(string uniqueId)
    {
        dbReference.Child("Orders").Child(uniqueId).RemoveValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Buyurtma o'chirildi: ID = {uniqueId}");
            }
            else
            {
                Debug.LogError("Buyurtmani o'chirishda xatolik: " + task.Exception);
            }
        });
    }

    // Buyurtmani yangilash
    public void UpdateOrderInFirebase(OrderDataQabul updatedOrder)
    {
        updatedOrder.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string json = JsonUtility.ToJson(updatedOrder);

        dbReference.Child("Orders").Child(updatedOrder.uniqueId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Buyurtma yangilandi: ID = {updatedOrder.uniqueId}");
            }
            else
            {
                Debug.LogError("Buyurtmani yangilashda xatolik: " + task.Exception);
            }
        });
    }
}