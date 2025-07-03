using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Net;

public class ShowQabulQilingan : MonoBehaviour
{

    public static ShowQabulQilingan Instance;

    int gilam_soni = 0;
    int korpa_soni = 0;
    int yakandoz_soni = 0;
    int adyol_soni = 0;
    int parda_soni = 0;
    int daroshka_soni = 0;


    [Header("Input Fields Qabul")]
    public TMP_InputField inputNameQabul;
    public TMP_InputField inputPhoneQabul;
    public TMP_InputField inputAddressQabul;
    public TMP_InputField inputNoteQabul;
    public TMP_InputField kvadratQabul;
    public TMP_InputField gilamSoni;
    public TMP_InputField korpaSoni;
    public TMP_InputField yakandozSoni;
    public TMP_InputField adyolSoni;
    public TMP_InputField pardaSoni;
    public TMP_InputField daroshkaSoni;
    public TMP_InputField xizmatNarxi;

    [Header("Input Fields Yangi")]
    public TMP_InputField inputNameYangi;
    public TMP_InputField inputPhoneYangi;
    public TMP_InputField inputAddressYangi;
    public TMP_InputField inputNoteYangi;

    [Header("Prefabs & UI")]
    public GameObject yangiPanelPrefab;
    public GameObject qabulQilinganPrefab;
    public Transform gridContentYangi;
    public Transform gridContentQabul;
    public Transform gridContentYuvilmoqda;
    public Transform gridContentTayyor;
    public GameObject qabulInputUI;
    public GameObject yangiInputUI;
    public GameObject yangiBuyurtmaUI;

    public FirebaseDataWriter firebaseWriter;
    public Statistika statistika;


    public int totalBalance = 0;
    public int totalKvadrat = 0;
    public int totalGilam = 0;
    public int totalDaroshka = 0;
    public int totalKorpa = 0;
    public int totalYakandoz = 0;
    public int totalAdyol = 0;
    public int totalParda = 0;


    public List<string> holat = new List<string> { "Yangi", "Jarayonda", "Yuvilmoqda", "Tayyor" };

    public List<OrderDataQabul> orderListQabul = new List<OrderDataQabul>();

    private OrderDataQabul currentEditingOrder;

    private bool isEditing = false;
    private GameObject objectToEdit;

    private void Awake()
    {
        Instance = this;
    }

    // ShowQabulQilingan scriptidagi SaveQabulQilingan metodini o'zgartiring:
    public void SaveQabulQilingan()
    {
        if (isEditing)
        {
            ApplyEditedOrderQabul(objectToEdit);
            return;
        }

        // 1. Ma'lumotlarni olish va to'g'ri formatga o'girish
        string name = inputNameQabul.text;
        int phone = ParseInt(inputPhoneQabul.text);
        string address = inputAddressQabul.text;
        string note = inputNoteQabul.text;
        int kvadrat = ParseInt(kvadratQabul.text);
        int gilam = ParseInt(gilamSoni.text);
        int korpa = ParseInt(korpaSoni.text);
        int yakandoz = ParseInt(yakandozSoni.text);
        int adyol = ParseInt(adyolSoni.text);
        int parda = ParseInt(pardaSoni.text);
        int daroshka = ParseInt(daroshkaSoni.text);
        int narx = ParseInt(xizmatNarxi.text);

        string holati = holat[1];
        string saveTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // 2. Obyekt yaratish
        OrderDataQabul yangiBuyurtma = new OrderDataQabul(
            name, phone, address, note, kvadrat, gilam, korpa, yakandoz, adyol, parda, daroshka, narx, holati, saveTime
        );

        orderListQabul.Add(yangiBuyurtma);

        // 3. Firebase ga saqlash
        //FirebaseDataWriter firebaseWriter = FindObjectOfType<FirebaseDataWriter>();
        if (firebaseWriter != null)
        {
            firebaseWriter.SaveNewOrder(yangiBuyurtma);
        }

        // 4. UI element yaratish
        GameObject item = Instantiate(qabulQilinganPrefab, gridContentQabul);

        item.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = name;
        item.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = phone.ToString();
        item.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = address;
        item.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = note;
        item.transform.Find("Text (TMP)_kvadrat").GetComponent<TMP_Text>().text = kvadrat.ToString();
        item.transform.Find("Text (TMP)_gilam_soni").GetComponent<TMP_Text>().text = gilam.ToString();
        item.transform.Find("Text (TMP)_Ko'rpa_soni").GetComponent<TMP_Text>().text = korpa.ToString();
        item.transform.Find("Text (TMP)_yakandoz_soni").GetComponent<TMP_Text>().text = yakandoz.ToString();
        item.transform.Find("Text (TMP)_adyol_soni").GetComponent<TMP_Text>().text = adyol.ToString();
        item.transform.Find("Text (TMP)_parda_soni").GetComponent<TMP_Text>().text = parda.ToString();
        item.transform.Find("Text (TMP)_doroshka_soni").GetComponent<TMP_Text>().text = daroshka.ToString();
        item.transform.Find("Text (TMP)_sana_vaqt").GetComponent<TMP_Text>().text = saveTime;

        item.transform.GetComponent<QabulQilinganPrefab>().GetQabulInfo();
        item.transform.GetComponent<QabulQilinganPrefab>().xizmatNarxi = narx.ToString();


        ResetAll();
        yangiBuyurtmaUI.SetActive(false);
    }

    // SaveYangi metodini ham o'zgartiring:
    public void SaveYangi()
    {
        if (isEditing)
        {
            ApplyEditedOrderYangi(objectToEdit);
            return;
        }

        string name = inputNameYangi.text;
        int phone = ParseInt(inputPhoneYangi.text);
        string address = inputAddressYangi.text;
        string note = inputNoteYangi.text;
        string saveTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        OrderDataQabul yangiBuyurtma = new OrderDataQabul(
           name, phone, address, note, 0, 0, 0, 0, 0, 0, 0, 0, holat[0], saveTime
       );

        orderListQabul.Add(yangiBuyurtma);

        // Firebase ga saqlash
        //FirebaseDataWriter firebaseWriter = FindObjectOfType<FirebaseDataWriter>();
        if (firebaseWriter != null)
        {
            firebaseWriter.SaveNewOrder(yangiBuyurtma);
        }

        GameObject item = Instantiate(yangiPanelPrefab, gridContentYangi);

        item.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = name;
        item.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = phone.ToString();
        item.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = address;
        item.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = note;
        item.transform.Find("Text (TMP)_savetime").GetComponent<TMP_Text>().text = saveTime;    

        item.transform.GetComponent<YangiPrefab>().GetInfo();

        ResetAll();
    }

    // 🔧 Xatolik chiqmasligi uchun matndan int parse qilish yordamchi metod
    private int ParseInt(string input)
        {
            int.TryParse(input, out int result);
            return result;
        }

        public void AddGilam()
        {
            gilam_soni++;
            gilamSoni.text = $"{gilam_soni}";
        }
        public void AddKorpa()
        {
            korpa_soni++;
            korpaSoni.text = $"{korpa_soni}";
        }
        public void AddYakandoz()
        {
            yakandoz_soni++;
            yakandozSoni.text = $"{yakandoz_soni}";
        }
        public void AddAdyol()
        {
            adyol_soni++;
            adyolSoni.text = $"{adyol_soni}";
        }
        public void AddParda()
        {
            parda_soni++;
            pardaSoni.text = $"{parda_soni}";
        }
        public void AddDoroshka()
        {
            daroshka_soni++;
            daroshkaSoni.text = $"{daroshka_soni}";
        }
        public void RemoveGilam()
        {
            gilam_soni--;
            if (gilam_soni < 0)
            {
                gilam_soni = 0;
            }
            gilamSoni.text = $"{gilam_soni}";
        }
        public void RemoveKorpa()
        {
            korpa_soni--;
            if (korpa_soni < 0) { korpa_soni = 0; }
            korpaSoni.text = $"{korpa_soni}";
        }
        public void RemoveYakandoz()
        {
            yakandoz_soni--;
            if (yakandoz_soni < 0) { yakandoz_soni = 0; }
            yakandozSoni.text = $"{yakandoz_soni}";
        }
        public void RemoveAdyol()
        {
            adyol_soni--;
            if (adyol_soni < 0) {  adyol_soni = 0; }
            adyolSoni.text = $"{adyol_soni}";
        }
        public void RemoveParda()
        {
            parda_soni--;
            if (parda_soni < 0) {  parda_soni = 0; }
            pardaSoni.text = $"{parda_soni}";
        }
        public void RemoveDoroshka()
        {
            daroshka_soni--;
            if (daroshka_soni < 0) { daroshka_soni = 0; }
            daroshkaSoni.text = $"{daroshka_soni}";
        }


        public void ResetAll()
        {
            gilam_soni = 0;
            korpa_soni = 0;
            yakandoz_soni = 0;
            adyol_soni = 0;
            parda_soni = 0;
            daroshka_soni = 0;

            inputNameYangi.text = "";
            inputPhoneYangi.text = "";
            inputAddressYangi.text = "";
            inputNoteYangi.text = "";

            inputNameQabul.text = "";
            inputPhoneQabul.text = "";
            inputAddressQabul.text = "";
            inputNoteQabul.text = "";
            kvadratQabul.text = "";
            gilamSoni.text = "";
            korpaSoni.text = "";
            yakandozSoni.text = "";
            adyolSoni.text = "";
            pardaSoni.text = "";
            daroshkaSoni.text = "";
            xizmatNarxi.text = "";


        }

        public void DeletePanel(GameObject objectToDestroy)
        {
            Destroy(objectToDestroy);
        }

        public void EditOrderByPhoneQabul(int phoneNumber, GameObject editObject)
        {
            isEditing = true;
            objectToEdit = editObject;
            // 1. Find the order
            currentEditingOrder = orderListQabul.FirstOrDefault(o => o.phone == phoneNumber);

            if (currentEditingOrder != null)
            {
                // 2. Activate UI and fill current data
                qabulInputUI.SetActive(true);

                inputNameQabul.text = currentEditingOrder.name;
                inputPhoneQabul.text = currentEditingOrder.phone.ToString();
                inputAddressQabul.text = currentEditingOrder.address;
                inputNoteQabul.text = currentEditingOrder.note;
                kvadratQabul.text = currentEditingOrder.kvadrat.ToString();
                gilamSoni.text = currentEditingOrder.gilamSoni.ToString();
                korpaSoni.text = currentEditingOrder.korpaSoni.ToString();
                yakandozSoni.text = currentEditingOrder.yakandozSoni.ToString();
                adyolSoni.text = currentEditingOrder.adyolSoni.ToString();
                pardaSoni.text = currentEditingOrder.pardaSoni.ToString();
                daroshkaSoni.text = currentEditingOrder.daroshkaSoni.ToString();
                xizmatNarxi.text = currentEditingOrder.xizmatNarxi.ToString();
            }
            else
            {
                Debug.LogWarning("Buyurtma topilmadi.");
            }
        }
        public void EditOrderByPhoneYangi(int phoneNumber, GameObject editObject)
        {
            isEditing = true;
            objectToEdit = editObject;
            // 1. Find the order
            currentEditingOrder = orderListQabul.FirstOrDefault(o => o.phone == phoneNumber);

            if (currentEditingOrder != null)
            {
                // 2. Activate UI and fill current data
                yangiInputUI.SetActive(true);

                inputNameYangi.text = currentEditingOrder.name;
                inputPhoneYangi.text = currentEditingOrder.phone.ToString();
                inputAddressYangi.text = currentEditingOrder.address;
                inputNoteYangi.text = currentEditingOrder.note;
            }
            else
            {
                Debug.LogWarning("Buyurtma topilmadi.");
            }
        }


    // ShowQabulQilingan scriptidagi ApplyEditedOrderQabul metodini yangilang:

    public void ApplyEditedOrderQabul(GameObject item)
    {
        if (currentEditingOrder == null)
        {
            Debug.LogWarning("Hech qanday buyurtma tanlanmagan.");
            return;
        }

        // UniqueId ni saqlab qolish
        string editingUniqueId = currentEditingOrder.uniqueId;

        // Ma'lumotlarni yangilash
        currentEditingOrder.name = inputNameQabul.text;
        currentEditingOrder.phone = ParseInt(inputPhoneQabul.text);
        currentEditingOrder.address = inputAddressQabul.text;
        currentEditingOrder.note = inputNoteQabul.text;
        currentEditingOrder.kvadrat = ParseInt(kvadratQabul.text);
        currentEditingOrder.gilamSoni = ParseInt(gilamSoni.text);
        currentEditingOrder.korpaSoni = ParseInt(korpaSoni.text);
        currentEditingOrder.yakandozSoni = ParseInt(yakandozSoni.text);
        currentEditingOrder.adyolSoni = ParseInt(adyolSoni.text);
        currentEditingOrder.pardaSoni = ParseInt(pardaSoni.text);
        currentEditingOrder.daroshkaSoni = ParseInt(daroshkaSoni.text);
        currentEditingOrder.xizmatNarxi = ParseInt(xizmatNarxi.text);

        // Firebase'da yangilash - YANGI QISM!
        if (firebaseWriter != null && !string.IsNullOrEmpty(editingUniqueId))
        {
            firebaseWriter.EditOrderInFirebase(editingUniqueId, currentEditingOrder);
        }

        // UI elementlarini yangilash
        item.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = currentEditingOrder.name;
        item.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = currentEditingOrder.phone.ToString();
        item.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = currentEditingOrder.address;
        item.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = currentEditingOrder.note;
        item.transform.Find("Text (TMP)_kvadrat").GetComponent<TMP_Text>().text = currentEditingOrder.kvadrat.ToString();
        item.transform.Find("Text (TMP)_gilam_soni").GetComponent<TMP_Text>().text = currentEditingOrder.gilamSoni.ToString();
        item.transform.Find("Text (TMP)_Ko'rpa_soni").GetComponent<TMP_Text>().text = currentEditingOrder.korpaSoni.ToString();
        item.transform.Find("Text (TMP)_yakandoz_soni").GetComponent<TMP_Text>().text = currentEditingOrder.yakandozSoni.ToString();
        item.transform.Find("Text (TMP)_adyol_soni").GetComponent<TMP_Text>().text = currentEditingOrder.adyolSoni.ToString();
        item.transform.Find("Text (TMP)_parda_soni").GetComponent<TMP_Text>().text = currentEditingOrder.pardaSoni.ToString();
        item.transform.Find("Text (TMP)_doroshka_soni").GetComponent<TMP_Text>().text = currentEditingOrder.daroshkaSoni.ToString();

        item.transform.GetComponent<QabulQilinganPrefab>().xizmatNarxi = currentEditingOrder.xizmatNarxi.ToString();

        Debug.Log("Buyurtma yangilandi va Firebase'ga saqlandi.");

        isEditing = false;
        ResetAll();
    }

    // ApplyEditedOrderYangi metodini ham yangilang:
    public void ApplyEditedOrderYangi(GameObject item)
    {
        if (currentEditingOrder == null)
        {
            Debug.LogWarning("Hech qanday buyurtma tanlanmagan.");
            return;
        }

        // UniqueId ni saqlab qolish
        string editingUniqueId = currentEditingOrder.uniqueId;

        // Ma'lumotlarni yangilash
        currentEditingOrder.name = inputNameYangi.text;
        currentEditingOrder.phone = ParseInt(inputPhoneYangi.text);
        currentEditingOrder.address = inputAddressYangi.text;
        currentEditingOrder.note = inputNoteYangi.text;
        currentEditingOrder.kvadrat = 0;
        currentEditingOrder.gilamSoni = 0;
        currentEditingOrder.korpaSoni = 0;
        currentEditingOrder.yakandozSoni = 0;
        currentEditingOrder.adyolSoni = 0;
        currentEditingOrder.pardaSoni = 0;
        currentEditingOrder.daroshkaSoni = 0;
        currentEditingOrder.xizmatNarxi = 0;

        // Firebase'da yangilash - YANGI QISM!
        if (firebaseWriter != null && !string.IsNullOrEmpty(editingUniqueId))
        {
            firebaseWriter.EditOrderInFirebase(editingUniqueId, currentEditingOrder);
        }

        // UI elementlarini yangilash
        item.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = currentEditingOrder.name;
        item.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = currentEditingOrder.phone.ToString();
        item.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = currentEditingOrder.address;
        item.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = currentEditingOrder.note;

        item.transform.GetComponent<YangiPrefab>().GetInfo();

        Debug.Log("Buyurtma yangilandi va Firebase'ga saqlandi.");

        isEditing = false;
        ResetAll();
    }
    public void AddStatsToTotal(string narx, string kvadrat, string gilam, string daroshka, string korpa, string yakandoz, string adyol, string parda)
    {
        totalBalance += ParseSafe(narx);
        totalKvadrat += ParseSafe(kvadrat);
        totalGilam += ParseSafe(gilam);
        totalDaroshka += ParseSafe(daroshka);
        totalKorpa += ParseSafe(korpa);
        totalYakandoz += ParseSafe(yakandoz);
        totalAdyol += ParseSafe(adyol);
        totalParda += ParseSafe(parda);
        statistika.ShowTotalStats();
    }

    private int ParseSafe(string input)
    {
        if (int.TryParse(input, out int result))
            return result;
        else
        {
            Debug.LogWarning($"Invalid number input: \"{input}\"");
            return 0;
        }
    }


}
