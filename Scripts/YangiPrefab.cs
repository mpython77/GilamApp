using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using System.ComponentModel;
using System;

public class YangiPrefab: MonoBehaviour
{
    public string ism;
    public string tel;
    public string manzil;
    public string izoh;

    int telNomer = 0;







    public void GetInfo() 
    {
        ism = transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text;
        tel = transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text;
        manzil = transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text;
        izoh = transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text;

        telNomer = int.Parse(tel);
    }


    public void GiveInfo()
    {
        

        ShowQabulQilingan.Instance.qabulInputUI.SetActive(true);
        ShowQabulQilingan.Instance.inputNameQabul.text = ism;
        ShowQabulQilingan.Instance.inputPhoneQabul.text = tel;
        ShowQabulQilingan.Instance.inputAddressQabul.text = manzil;
        ShowQabulQilingan.Instance.inputNoteQabul.text = izoh;
        DeleteInfo(ShowQabulQilingan.Instance.orderListQabul, telNomer );

//            ShowQabulQilingan.Instance.yangiBuyurtmaUI.SetActive(false);
    }
    public void CallDelete()
    {
        DeleteInfo(ShowQabulQilingan.Instance.orderListQabul, telNomer);
    }
    public void DeleteInfo(List<OrderDataQabul> orders, int phoneNumber)
    {
        OrderDataQabul orderToRemove = orders.FirstOrDefault(o => o.phone == phoneNumber);

        if (orderToRemove != null)
        {
            string idToDelete = orderToRemove.uniqueId;
            orders.Remove(orderToRemove);
            ShowQabulQilingan.Instance.DeletePanel(gameObject, idToDelete);
            Debug.Log("Ma'lumot O'chdi");

        }
        else { Debug.Log("Xatolik yuz berdi");  }
    }


    public void ActivteEditMode()
    {
        ShowQabulQilingan.Instance.EditOrderByPhoneYangi(telNomer, gameObject);
    }


}
