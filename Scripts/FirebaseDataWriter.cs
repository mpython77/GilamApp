   using UnityEngine;
using UnityEngine.UI; // Button uchun
using TMPro; // TextMeshPro uchun
using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System;

public class FirebaseDataWriter : MonoBehaviour
{
    [SerializeField] private TMP_InputField ismInput; // TextMeshPro InputField lari
    [SerializeField] private TMP_InputField telefonInput;
    [SerializeField] private TMP_InputField manzilInput;
    [SerializeField] private TMP_InputField commentInput;
    [SerializeField] private Button saveButton; // Saqlash tugmasi
    [SerializeField] private Button outputButton; // Output tugmasi

    private DatabaseReference dbReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                dbReference = FirebaseDatabase.DefaultInstance.RootReference;

                // Tugmalar uchun listenerlar
                saveButton.onClick.AddListener(SaveData);
                outputButton.onClick.AddListener(DisplayData);

                // Bu yerda chaqiring:
                DisplayData();
            }
            else
            {
                Debug.LogError("Firebase mavjud emas: " + task.Result.ToString());
            }
        });

        // BU YERDA QO‘YMANG: DisplayData();  <-- bu xato
    }


    // Saqlash tugmasi bosilganda ishlaydi
    void SaveData()
    {
        string ism = ismInput.text;
        string telefon = telefonInput.text;
        string manzil = manzilInput.text;
        string comment = commentInput.text;

        // Inputlar bo‘sh emasligini tekshirish
        if (string.IsNullOrEmpty(ism) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(manzil) || string.IsNullOrEmpty(comment))
        {
            Debug.LogWarning("Barcha maydonlarni to‘ldiring!");
            return;
        }

        // Unikal ID yaratish
        string uniqueId = System.Guid.NewGuid().ToString();

        // Joriy sana va vaqtni olish
        string saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // User obyektini yaratish
        User user = new User(ism, telefon, manzil, comment, saveTime, uniqueId);
        string json = JsonUtility.ToJson(user);

        // Firebase’ga ma'lumotlarni yozish
        dbReference.Child("Users").Child(uniqueId).SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log($"Ma'lumot yozildi: ID = {uniqueId}, Vaqt = {saveTime}");
                // Input maydonlarini tozalash
                ismInput.text = "";
                telefonInput.text = "";
                manzilInput.text = "";
                commentInput.text = "";
            }
            else
            {
                Debug.LogError("Xatolik yuz berdi: " + task.Exception);
            }
        });
    }

    // Output tugmasi bosilganda ishlaydi
    void DisplayData()
    {
        dbReference.Child("Users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    string userData = user.GetRawJsonValue();
                    User loadedUser = JsonUtility.FromJson<User>(userData);
                    Debug.Log($"ID: {loadedUser.uniqueId}, Ism: {loadedUser.ism}, Telefon: {loadedUser.telefon}, Manzil: {loadedUser.manzil}, Komment: {loadedUser.comment}, Saqlangan vaqt: {loadedUser.saveTime}");
                }
            }
            else
            {
                Debug.LogError("Ma'lumotlarni o‘qishda xatolik: " + task.Exception);
            }
        });
    }
}

// User ma'lumotlarini saqlash uchun class
[System.Serializable]
public class User
{
    public string ism;
    public string telefon;
    public string manzil;
    public string comment;
    public string saveTime; // Saqlangan vaqt
    public string uniqueId; // Unikal ID

    public User(string ism, string telefon, string manzil, string comment, string saveTime, string uniqueId)
    {
        this.ism = ism;
        this.telefon = telefon;
        this.manzil = manzil;
        this.comment = comment;
        this.saveTime = saveTime;
        this.uniqueId = uniqueId;
    }
}