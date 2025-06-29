using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class QabulQilinganPrefab: MonoBehaviour
{
    public string nameQabul;
    public string phoneQabul;
    public string addressQabul;
    public string noteQabul;
    public string kvadratQabul;
    public string gilamSoni;
    public string korpaSoni;
    public string yakandozSoni;
    public string adyolSoni;
    public string pardaSoni;
    public string daroshkaSoni;
    public string xizmatNarxi;

    public int telNomerQabul = 0;

    public void GetQabulInfo()
    {
        nameQabul = transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text;
        phoneQabul = transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text;
        addressQabul = transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text;
        noteQabul = transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text;
        kvadratQabul = transform.Find("Text (TMP)_kvadrat").GetComponent<TMP_Text>().text;
        gilamSoni = transform.Find("Text (TMP)_gilam_soni").GetComponent<TMP_Text>().text;
        korpaSoni = transform.Find("Text (TMP)_Ko'rpa_soni").GetComponent<TMP_Text>().text;
        yakandozSoni = transform.Find("Text (TMP)_yakandoz_soni").GetComponent<TMP_Text>().text;
        adyolSoni = transform.Find("Text (TMP)_adyol_soni").GetComponent<TMP_Text>().text;
        pardaSoni = transform.Find("Text (TMP)_parda_soni").GetComponent<TMP_Text>().text;
        daroshkaSoni = transform.Find("Text (TMP)_doroshka_soni").GetComponent<TMP_Text>().text;

        telNomerQabul = int.Parse(phoneQabul);
    }


    public void ActivteEditModeQabul()
    {
        ShowQabulQilingan.Instance.EditOrderByPhoneQabul(telNomerQabul, gameObject);
    }

    public void CallDeleteQabul()
    {
        DeleteInfoQabul(ShowQabulQilingan.Instance.orderListQabul, telNomerQabul);
    }
    public void DeleteInfoQabul(List<OrderDataQabul> orders, int phoneNumber)
    {
        OrderDataQabul orderToRemove = orders.FirstOrDefault(o => o.phone == phoneNumber);

        if (orderToRemove != null)
        {
            orders.Remove(orderToRemove);
            ShowQabulQilingan.Instance.DeletePanel(gameObject);
            Debug.Log("Ma'lumot O'chdi");

        }
        else { Debug.Log("Xatolik yuz berdi"); }
    }

}
