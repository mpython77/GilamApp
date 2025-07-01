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


    public void ChangeStatus()
    {
        OrderDataQabul orderToChange = ShowQabulQilingan.Instance.orderListQabul.FirstOrDefault(o => o.phone == telNomerQabul);

        if (orderToChange != null)
        {
            string status = orderToChange.holati;
            if (status == ShowQabulQilingan.Instance.holat[1])
            {
                orderToChange.holati = ShowQabulQilingan.Instance.holat[2];
                gameObject.transform.SetParent(ShowQabulQilingan.Instance.gridContentYuvilmoqda);
            }
            else if (status == ShowQabulQilingan.Instance.holat[2])
            {
                orderToChange.holati = ShowQabulQilingan.Instance.holat[3];
                gameObject.transform.SetParent(ShowQabulQilingan.Instance.gridContentTayyor);
            }
           
        }

    }
}
