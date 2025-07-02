[System.Serializable]
public class OrderDataQabul
{
    public string name;
    public int phone;
    public string address;
    public string note;
    public int kvadrat;
    public int gilamSoni;
    public int korpaSoni;
    public int yakandozSoni;
    public int adyolSoni;
    public int pardaSoni;
    public int daroshkaSoni;
    public int xizmatNarxi;
    public string holati;
    public string saveTime; // Firebase uchun saqlangan vaqt
    public string uniqueId; // Firebase uchun unikal ID

    // Constructor
    public OrderDataQabul(string name, int phone, string address, string note, int kvadrat,
                         int gilamSoni, int korpaSoni, int yakandozSoni, int adyolSoni,
                         int pardaSoni, int daroshkaSoni, int xizmatNarxi, string holati,
                         string saveTime, string uniqueId = "")
    {
        this.name = name;
        this.phone = phone;
        this.address = address;
        this.note = note;
        this.kvadrat = kvadrat;
        this.gilamSoni = gilamSoni;
        this.korpaSoni = korpaSoni;
        this.yakandozSoni = yakandozSoni;
        this.adyolSoni = adyolSoni;
        this.pardaSoni = pardaSoni;
        this.daroshkaSoni = daroshkaSoni;
        this.xizmatNarxi = xizmatNarxi;
        this.holati = holati;
        this.saveTime = saveTime;
        this.uniqueId = string.IsNullOrEmpty(uniqueId) ? System.Guid.NewGuid().ToString() : uniqueId;
    }

    // Default constructor JSON uchun
    public OrderDataQabul() { }
}   