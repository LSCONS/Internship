[System.Serializable]
public class EnemyData : IHpModel
{
    public string   MonsterID;
    public string   Name;
    public string   Description;
    public int      Attack;
    public float    AttackMul;
    public int      MaxHP;
    public float    MaxHPMul;
    public int      AttackRange;
    public float    AttackRangeMul;
    public float    AttackSpeed;
    public float    MoveSpeed;
    public int      MinExp;
    public int      MaxExp;
    public int[]    DropItem;

    public int CurHp;
    public int ModelMaxHp => MaxHP;
    public int ModelCurHp => CurHp;
    public float TotalRange => AttackRange * AttackRangeMul + 1;
    public float TotalAttack => Attack * AttackMul;

    public void Init()
    {
        CurHp = MaxHP;
    }

    public EnemyData() { }
    public EnemyData(EnemyData other)
    {
        this.MonsterID = other.MonsterID;
        this.Name = other.Name;
        this.Description = other.Description;
        this.Attack = other.Attack;
        this.AttackMul = other.AttackMul;
        this.MaxHP = other.MaxHP;
        this.MaxHPMul = other.MaxHPMul;
        this.AttackRange = other.AttackRange;
        this.AttackRangeMul = other.AttackRangeMul;
        this.AttackSpeed = other.AttackSpeed;
        this.MoveSpeed = other.MoveSpeed;
        this.MinExp = other.MinExp;
        this.MaxExp = other.MaxExp;
        this.DropItem = other.DropItem;
    }
}
