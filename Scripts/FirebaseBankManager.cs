using System;
using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

[System.Serializable]
public class BankData
{
    public int totalBalance;
    public int totalKvadrat;
    public int totalGilam;
    public int totalDaroshka;
    public int totalKorpa;
    public int totalYakandoz;
    public int totalAdyol;
    public int totalParda;
    public string lastUpdateTime;

    public BankData()
    {
        totalBalance = 0;
        totalKvadrat = 0;
        totalGilam = 0;
        totalDaroshka = 0;
        totalKorpa = 0;
        totalYakandoz = 0;
        totalAdyol = 0;
        totalParda = 0;
        lastUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public BankData(int balance, int kvadrat, int gilam, int daroshka, int korpa, int yakandoz, int adyol, int parda)
    {
        totalBalance = balance;
        totalKvadrat = kvadrat;
        totalGilam = gilam;
        totalDaroshka = daroshka;
        totalKorpa = korpa;
        totalYakandoz = yakandoz;
        totalAdyol = adyol;
        totalParda = parda;
        lastUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

public class FirebaseBankManager : MonoBehaviour
{
    public static FirebaseBankManager Instance;

    private DatabaseReference databaseRef;
    private ShowQabulQilingan showQabulQilingan;

    [Header("Bank Data")]
    public BankData currentBankData;

    [Header("Auto Save Settings")]
    public bool autoSaveEnabled = true;
    public float autoSaveInterval = 30f; // 30 soniya
    private float lastSaveTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeFirebase();

        // ShowQabulQilingan Instance ni olish
        if (ShowQabulQilingan.Instance != null)
        {
            showQabulQilingan = ShowQabulQilingan.Instance;
        }
        else
        {
            showQabulQilingan = FindObjectOfType<ShowQabulQilingan>();
        }

        currentBankData = new BankData();

        // Firebase dan ma'lumotlarni yuklash
        Invoke("LoadBankData", 1f); // 1 soniya kechikish bilan yuklash
    }

    private void Update()
    {
        // Auto save funksiyasi
        if (autoSaveEnabled && Time.time - lastSaveTime > autoSaveInterval)
        {
            SaveBankDataFromShowQabul();
            lastSaveTime = Time.time;
        }
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;
                databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
                Debug.Log("Firebase Bank Manager initialized successfully!");
            }
            else
            {
                Debug.LogError("Firebase Bank Manager initialization failed: " + task.Result);
            }
        });
    }

    // ShowQabulQilingan scriptidagi ma'lumotlarni olish va saqlash
    public void SaveBankDataFromShowQabul()
    {
        if (showQabulQilingan == null)
        {
            showQabulQilingan = ShowQabulQilingan.Instance;
            if (showQabulQilingan == null)
            {
                Debug.LogWarning("ShowQabulQilingan Instance topilmadi!");
                return;
            }
        }

        // ShowQabulQilingan dan to'g'ridan-to'g'ri ma'lumotlarni olish
        currentBankData = new BankData(
            showQabulQilingan.totalBalance,
            showQabulQilingan.totalKvadrat,
            showQabulQilingan.totalGilam,
            showQabulQilingan.totalDaroshka,
            showQabulQilingan.totalKorpa,
            showQabulQilingan.totalYakandoz,
            showQabulQilingan.totalAdyol,
            showQabulQilingan.totalParda
        );

        SaveBankData();
        Debug.Log($"ShowQabulQilingan dan ma'lumotlar olindi: Balance={showQabulQilingan.totalBalance}, Kvadrat={showQabulQilingan.totalKvadrat}");
    }

    // Firebase ga saqlash
    public void SaveBankData()
    {
        if (databaseRef == null)
        {
            Debug.LogError("Database reference null!");
            return;
        }

        string json = JsonUtility.ToJson(currentBankData);

        databaseRef.Child("Bank").Child("BankData").SetRawJsonValueAsync(json).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Bank ma'lumotlari muvaffaqiyatli saqlandi!");
                Debug.Log($"Saqlangan ma'lumotlar: Balance={currentBankData.totalBalance}, Kvadrat={currentBankData.totalKvadrat}");
            }
            else
            {
                Debug.LogError("Bank ma'lumotlarini saqlashda xatolik: " + task.Exception);
            }
        });
    }

    // Firebase dan yuklash
    public void LoadBankData()
    {
        if (databaseRef == null)
        {
            Debug.LogError("Database reference null!");
            return;
        }

        databaseRef.Child("Bank").Child("BankData").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string json = snapshot.GetRawJsonValue();
                    currentBankData = JsonUtility.FromJson<BankData>(json);

                    // ShowQabulQilingan scriptiga ma'lumotlarni o'tkazish
                    ApplyBankDataToShowQabul();

                    Debug.Log("Bank ma'lumotlari muvaffaqiyatli yuklandi!");
                    Debug.Log($"Yuklangan ma'lumotlar: Balance={currentBankData.totalBalance}, Kvadrat={currentBankData.totalKvadrat}");
                }
                else
                {
                    Debug.Log("Bank ma'lumotlari topilmadi, yangi ma'lumotlar yaratildi.");
                    currentBankData = new BankData();
                    SaveBankData(); // Yangi ma'lumotlarni saqlash
                }
            }
            else
            {
                Debug.LogError("Bank ma'lumotlarini yuklashda xatolik: " + task.Exception);
            }
        });
    }

    // Yuklangan ma'lumotlarni ShowQabulQilingan scriptiga o'tkazish
    private void ApplyBankDataToShowQabul()
    {
        if (showQabulQilingan == null)
        {
            showQabulQilingan = ShowQabulQilingan.Instance;
            if (showQabulQilingan == null)
            {
                Debug.LogWarning("ShowQabulQilingan Instance topilmadi!");
                return;
            }
        }

        // ShowQabulQilingan dagi o'zgaruvchilarga to'g'ridan-to'g'ri qiymat berish
        showQabulQilingan.totalBalance = currentBankData.totalBalance;
        showQabulQilingan.totalKvadrat = currentBankData.totalKvadrat;
        showQabulQilingan.totalGilam = currentBankData.totalGilam;
        showQabulQilingan.totalDaroshka = currentBankData.totalDaroshka;
        showQabulQilingan.totalKorpa = currentBankData.totalKorpa;
        showQabulQilingan.totalYakandoz = currentBankData.totalYakandoz;
        showQabulQilingan.totalAdyol = currentBankData.totalAdyol;
        showQabulQilingan.totalParda = currentBankData.totalParda;

        Debug.Log($"ShowQabulQilingan ga ma'lumotlar o'tkazildi: Balance={showQabulQilingan.totalBalance}, Kvadrat={showQabulQilingan.totalKvadrat}");

        // Statistika yangilash
        if (showQabulQilingan.statistika != null)
        {
            showQabulQilingan.statistika.ShowTotalStats();
        }
    }

    // Ma'lumotlarni qayta tiklash (Reset)
    public void ResetBankData()
    {
        currentBankData = new BankData();
        SaveBankData();
        ApplyBankDataToShowQabul();
        Debug.Log("Bank ma'lumotlari qayta tiklandi!");
    }

    // Maxsus ma'lumotlarni qo'shish
    public void AddToBankData(int balance, int kvadrat, int gilam, int daroshka, int korpa, int yakandoz, int adyol, int parda)
    {
        currentBankData.totalBalance += balance;
        currentBankData.totalKvadrat += kvadrat;
        currentBankData.totalGilam += gilam;
        currentBankData.totalDaroshka += daroshka;
        currentBankData.totalKorpa += korpa;
        currentBankData.totalYakandoz += yakandoz;
        currentBankData.totalAdyol += adyol;
        currentBankData.totalParda += parda;
        currentBankData.lastUpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        SaveBankData();
        ApplyBankDataToShowQabul();
    }

    // Manual Save tugmasi uchun
    public void ManualSave()
    {
        SaveBankDataFromShowQabul();
    }

    // Manual Load tugmasi uchun  
    public void ManualLoad()
    {
        LoadBankData();
    }

    // Bank ma'lumotlarini ko'rsatish
    public void ShowBankInfo()
    {
        Debug.Log("=== BANK MA'LUMOTLARI ===");
        Debug.Log($"Umumiy balans: {currentBankData.totalBalance}");
        Debug.Log($"Umumiy kvadrat: {currentBankData.totalKvadrat}");
        Debug.Log($"Umumiy gilam: {currentBankData.totalGilam}");
        Debug.Log($"Umumiy daroshka: {currentBankData.totalDaroshka}");
        Debug.Log($"Umumiy korpa: {currentBankData.totalKorpa}");
        Debug.Log($"Umumiy yakandoz: {currentBankData.totalYakandoz}");
        Debug.Log($"Umumiy adyol: {currentBankData.totalAdyol}");
        Debug.Log($"Umumiy parda: {currentBankData.totalParda}");
        Debug.Log($"Oxirgi yangilanish: {currentBankData.lastUpdateTime}");
        Debug.Log("========================");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveBankDataFromShowQabul();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveBankDataFromShowQabul();
        }
    }

    private void OnDestroy()
    {
        SaveBankDataFromShowQabul();
    }
}