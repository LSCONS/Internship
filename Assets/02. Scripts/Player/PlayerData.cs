using System;

[Serializable]
public class PlayerData : IHpModel
{
    public float attackCoolTime = 0.5f; //공격 쿨타임
    public float playerRange = 5.0f;    //플레이어 공격 범위
    public string Name = "User Name";   //플레이어 이름
    public int Level = 1;               //플레이어 레벨

    public int MaxExp = 100;            //플레이어 최대 경험치

    public int MaxHP = 100;             //플레이어 최대 체력
    public float MaxHPMul = 1.0f;       //플레이어 최대 체력 배율

    public int MaxMP = 100;             //플레이어 최대 마나
    public float MaxMPMul = 1.0f;       //플레이어 최대 마나 배율

    public int MaxAtk = 10;             //플레이어 최대 공격력
    public float MaxAtkMul = 1.0f;      //플레이어 최대 공격력 배율

    public int MaxDef = 0;              //플레이어 최대 방어력
    public float MaxDefMul = 1.0f;      //플레이어 최대 방어력 배율

    public float Speed = 3.0f;          //플레이어 이동 속도

    public bool isPoison = false;       //플레이어 중독 여부

    public int CurHP;
    public int CurMP;
    public int CurExp;

    public bool IsDie = false;
    public int TotalAtk => (int)(MaxAtk * MaxAtkMul);
    public int TotalDef => (int)(MaxDef * MaxDefMul);

    public int ModelMaxHp { get => MaxHP;}
    public int ModelCurHp { get => CurHP;}

    public void Init()
    {
        CurHP = MaxHP;
        CurMP = MaxMP;
        CurExp = 0;
    }
}
