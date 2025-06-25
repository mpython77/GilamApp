using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowQabulQilingan : MonoBehaviour
{
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

    public List<string> holat = new List<string> { "Yangi", "Jarayonda", "Yuvilmoqda", "Tayyor" };

    private List<OrderDataQabul> orderListQabul = new List<OrderDataQabul>();

    public void SaveQabulQilingan()
    {
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

        // 2. Obyekt yaratish
        OrderDataQabul yangiBuyurtma = new OrderDataQabul(
            name, phone, address, note, kvadrat, gilam, korpa, yakandoz, adyol, parda, daroshka, narx, holati
        );

        orderListQabul.Add(yangiBuyurtma);

        // 3. UI element yaratish
        GameObject item = Instantiate(qabulQilinganPrefab, gridContentQabul);

        item.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = name;
        item.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = phone.ToString();
        item.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = address;
        item.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = note;
        item.transform.Find("Text (TMP)_kvadrat").GetComponent <TMP_Text>().text = kvadrat.ToString();
        item.transform.Find("Text (TMP)_gilam_soni").GetComponent<TMP_Text>().text = gilam.ToString();
        item.transform.Find("Text (TMP)_Ko'rpa_soni").GetComponent<TMP_Text>().text = korpa.ToString();
        item.transform.Find("Text (TMP)_yakandoz_soni").GetComponent<TMP_Text>().text = yakandoz.ToString();
        item.transform.Find("Text (TMP)_adyol_soni").GetComponent<TMP_Text>().text = adyol.ToString();
        item.transform.Find("Text (TMP)_parda_soni").GetComponent<TMP_Text>().text = parda.ToString();
        item.transform.Find("Text (TMP)_doroshka_soni").GetComponent<TMP_Text>().text = daroshka.ToString();

        // 4. Inputlarni tozalash

        ResetAll();
    }

    public void SaveYangi()
    {
        string name = inputNameYangi.text; ;
        int phone = ParseInt(inputPhoneYangi.text);
        string address = inputAddressYangi.text;
        string note = inputNoteYangi.text;


        OrderDataQabul yangiBuyurtma = new OrderDataQabul(
           name, phone, address, note, 0, 0, 0, 0, 0, 0, 0,0, holat[0]
       );

        orderListQabul.Add(yangiBuyurtma);

        GameObject item = Instantiate(yangiPanelPrefab, gridContentYangi);

        item.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = name;
        item.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = phone.ToString();
        item.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = address;
        item.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = note;



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

}
