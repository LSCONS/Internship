using UnityEngine;

public class Player : Character
{
    [field: SerializeField] public PlayerData Data { get; private set; }
    [field: SerializeField] public Rigidbody2D Rigid2D { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    public PlayerMove Move { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerUseItem UseItem { get; private set; }
    public PlayerAttack Attack { get; private set; }
    private ViewPresenter viewPresenter;


    private void Awake()
    {
        Init();
        Input.Init();
        Attack.Init();
    }

    private void FixedUpdate()
    {
        Move.FixedUpdate();
    }


    /// <summary>
    /// 클래스 첫 생성 시 초기화를 실행할 메서드 
    /// </summary>
    public override void Init()
    {
        base.Init();
        HpBarView viewHpBar = ManagerHub.Instance.PoolManager.GetPoolObject<HpBarView>("HpBar");
        Input = new PlayerInput(this);
        Move = new PlayerMove(this);
        Attack = new PlayerAttack(this);
        UseItem = new PlayerUseItem(this);
        Data.Init();
        viewPresenter = new ViewPresenter(viewHpBar, Data);
        viewPresenter.Init(transform);
        viewPresenter.UpdateHealthBar();
    }


    /// <summary>
    /// 플레이어가 데미지를 입었을 때 실행할 메서드.
    /// </summary>
    /// <param name="damage"></param>
    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        Data.CurHP = Mathf.Max(Data.CurHP - (int)damage, 0);
        viewPresenter.UpdateHealthBar();

        if (Data.CurHP == 0)
        {
            Dead();
        }
    }


    /// <summary>
    /// 플레이어가 사망할 시 실행할 메서드.
    /// </summary>
    public override void Dead()
    {
        base.Dead();
        Rigid2D.linearVelocity = Vector3.zero;
        Animator.SetBool(ReadonlyData.AnimatorHash_IsDie, true);
        Attack.StopAttack();
        Data.IsDie = true;
    }
}
