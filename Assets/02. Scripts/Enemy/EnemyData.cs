[System.Serializable]
public class EnemyData : IHpModel
{
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxHPMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public int[] DropItem;

    public int CurHp;

    public void Init()
    {
        CurHp = MaxHP;
    }

    public int ModelMaxHp => MaxHP;
    public int ModelCurHp => CurHp;
}
