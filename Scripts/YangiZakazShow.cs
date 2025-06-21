using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class FormManager : MonoBehaviour
{
    [Header("Input Fields")]
    public TMP_InputField inputName;
    public TMP_InputField inputPhone;
    public TMP_InputField inputAddress;
    public TMP_InputField inputNote;

    [Header("Prefabs & UI")]
    public GameObject entryPrefab;
    public Transform gridContent;

    [Header("Button")]
    public Button saveButton;

    // ?? Buyurtmalar ro'yxati
    private List<OrderData> orderList = new List<OrderData>();

    void Start()
    {
        saveButton.onClick.AddListener(OnSaveClicked);
    }

    void OnSaveClicked()
    {
        // ?? Inputlardan matnlarni olish
        string name = inputName.text.Trim();
        string phone = inputPhone.text.Trim();
        string address = inputAddress.text.Trim();
        string note = inputNote.text.Trim();

        // ?? Bo'sh qiymatlarni tekshirish (optional, lekin foydali)
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone))
        {
            Debug.LogWarning("Ism va telefon bo‘sh bo‘lmasligi kerak.");
            return;
        }

        // ? OrderData obyektini yaratamiz
        OrderData newOrder = new OrderData(name, phone, address, note);
        orderList.Add(newOrder);

        // ?? Prefabni UI ga qo‘shamiz
        GameObject newEntry = Instantiate(entryPrefab, gridContent);

        // ?? Prefab ichidagi textlarni to‘ldiramiz
        TMP_Text nameText = newEntry.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>();
        TMP_Text phoneText = newEntry.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>();
        TMP_Text addressText = newEntry.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>();
        TMP_Text noteText = newEntry.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>();

        nameText.text = name;
        phoneText.text = phone;
        addressText.text = address;
        noteText.text = note;

        // ?? Inputlarni tozalash
        inputName.text = "";
        inputPhone.text = "";
        inputAddress.text = "";
        inputNote.text = "";

        Debug.Log("Buyurtma saqlandi. Jami: " + orderList.Count);
    }

    // ?? Barcha buyurtmalar ro'yxatini olish (agar kerak bo‘lsa boshqa joyda ishlatish uchun)
    public List<OrderData> GetAllOrders()
    {
        return orderList;
    }
}
