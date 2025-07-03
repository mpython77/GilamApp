using UnityEngine;
using TMPro;

public class Statistika: MonoBehaviour
{
    public TMP_Text totalBalanceText;
    public TMP_Text totalKvadratText;
    public TMP_Text totalGilamText;
    public TMP_Text totalDaroshkaText;
    public TMP_Text totalKorpaText;
    public TMP_Text totalYakandozText;
    public TMP_Text totalAdyolText;
    public TMP_Text totalPardaText;






    public void ShowTotalStats()
    {
        totalBalanceText.text = ShowQabulQilingan.Instance.totalBalance.ToString();
        totalKvadratText.text = ShowQabulQilingan.Instance.totalKvadrat.ToString();
        totalGilamText.text = ShowQabulQilingan.Instance.totalGilam.ToString();
        totalDaroshkaText.text = ShowQabulQilingan.Instance.totalDaroshka.ToString();
        totalKorpaText.text = ShowQabulQilingan.Instance.totalKorpa.ToString();
        totalYakandozText.text = ShowQabulQilingan.Instance.totalYakandoz.ToString();
        totalAdyolText.text = ShowQabulQilingan.Instance.totalAdyol.ToString();
        totalPardaText.text = ShowQabulQilingan.Instance.totalParda.ToString();
    }


}
