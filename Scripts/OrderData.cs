using System;

[Serializable]
public class OrderData
{
    public string name;
    public string phone;
    public string address;
    public string note;

    public OrderData(string name, string phone, string address, string note)
    {
        this.name = name;
        this.phone = phone;
        this.address = address;
        this.note = note;
    }
}

[Serializable]
public class OrderDataQabul
{
    public string name;
    public int phone;
    public string address;
    public string note;
    public int gilamSoni;
    public int korpaSoni;
    public int yakandozSoni;
    public int adyolSoni;
    public int pardaSoni;
    public int daroshkaSoni;
    public int xizmatNarxi;
    public string holati;

    public OrderDataQabul(string name, int phone, string address, string note, int gilamSoni, int korpaSoni, int yakandozSoni, int adyolSoni, int pardaSoni, int daroshkaSoni, int xizmatNarxi, string holati)
    {
        this.name = name;
        this.phone = phone;
        this.address = address;
        this.note = note;
        this.gilamSoni = gilamSoni;
        this.korpaSoni = korpaSoni;
        this.yakandozSoni = yakandozSoni;
        this.adyolSoni = adyolSoni;
        this.pardaSoni = pardaSoni;
        this.daroshkaSoni = daroshkaSoni;
        this.xizmatNarxi = xizmatNarxi;
        this.holati = holati;
    }
}