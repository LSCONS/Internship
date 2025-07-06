[System.Serializable]
public class ItemData
{
    public string   ItemID;
    public string   Name;
    public string   Description;
    public int      UnlockLev;
    public int      MaxHP;
    public float    MaxHPMul;
    public int      MaxMP;
    public float    MaxMPMul;
    public int      MaxAtk;
    public float    MaxAtkMul;
    public int      MaxDef;
    public float    MaxDefMul;
    public int      Status;

    public ItemData() { }
    public ItemData(ItemData itemData)
    {
        this.ItemID = itemData.ItemID;
        this.Name = itemData.Name;
        this.Description = itemData.Description;
        this.UnlockLev = itemData.UnlockLev;
        this.MaxHP = itemData.MaxHP;
        this.MaxHPMul = itemData.MaxHPMul;
        this.MaxMP = itemData.MaxMP;
        this.MaxMPMul = itemData.MaxMPMul;
        this.MaxAtk = itemData.MaxAtk;
        this.MaxAtkMul = itemData.MaxAtkMul;
        this.MaxDef = itemData.MaxDef;
        this.MaxDefMul  = itemData.MaxDefMul;
        this.Status = itemData.Status;
    }
}
