using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class SearchAndDisplayOrders : MonoBehaviour
{
    [Header("Prefabs & UI Parents")]
    public GameObject orderItemPrefab; // Prefab for each order item
    public Transform parentContent;    // Parent to attach UI elements under (like ScrollView Content)



    [Header("Search Field")]
    public TMP_InputField searchInputField;

    public void OnSearchButtonClicked()
    {
        string searchText = searchInputField.text.Trim();

        if (searchText.Length != 4 || !int.TryParse(searchText, out _))
        {
            Debug.LogWarning("Please enter exactly 4 digits to search by phone number.");
            return;
        }

        // Clear old UI elements
        foreach (Transform child in parentContent)
        {
            Destroy(child.gameObject);
        }

        // Search matching orders
        List<OrderDataQabul> matchingOrders = ShowQabulQilingan.Instance.orderListQabul.FindAll(order =>
        {
            string phoneStr = order.phone.ToString();
            return phoneStr.Length >= 4 && phoneStr.EndsWith(searchText);
        });

        if (matchingOrders.Count == 0)
        {
            Debug.Log("No matching orders found.");
            return;
        }

        // Instantiate UI items for each matching order
        foreach (var order in matchingOrders)
        {
            GameObject orderItem = Instantiate(orderItemPrefab, parentContent);

            // Example: Assign data to TextMeshProUGUI elements inside prefab
            orderItem.transform.Find("Text (TMP)_ism").GetComponent<TMP_Text>().text = order.name;
            orderItem.transform.Find("Text (TMP)_tel").GetComponent<TMP_Text>().text = order.ToString();
            orderItem.transform.Find("Text (TMP)_manzil").GetComponent<TMP_Text>().text = order.address;
            orderItem.transform.Find("Text (TMP)_izoh").GetComponent<TMP_Text>().text = order.note;
            orderItem.transform.Find("Text (TMP)_kvadrat").GetComponent<TMP_Text>().text = order.kvadrat.ToString();
            orderItem.transform.Find("Text (TMP)_gilam_soni").GetComponent<TMP_Text>().text = order.gilamSoni.ToString();
            orderItem.transform.Find("Text (TMP)_Ko'rpa_soni").GetComponent<TMP_Text>().text = order.korpaSoni.ToString();
            orderItem.transform.Find("Text (TMP)_yakandoz_soni").GetComponent<TMP_Text>().text = order.yakandozSoni.ToString();
            orderItem.transform.Find("Text (TMP)_adyol_soni").GetComponent<TMP_Text>().text = order.adyolSoni.ToString();
            orderItem.transform.Find("Text (TMP)_parda_soni").GetComponent<TMP_Text>().text = order.pardaSoni.ToString();
            orderItem.transform.Find("Text (TMP)_doroshka_soni").GetComponent<TMP_Text>().text = order.daroshkaSoni.ToString();
            orderItem.transform.Find("Text (TMP)_holat").GetComponent<TMP_Text>().text = order.holati;



            // Add more if needed...
        }
    }

    public void ClearGridContent()
    {
        foreach (Transform child in parentContent)
        {
            Destroy(child.gameObject);
        }

        searchInputField.text = string.Empty;
    }
}

