using UnityEngine;

public class Player : Character
{
    [field: SerializeField] public PlayerData playerData { get; private set; }
    [field: SerializeField] public Rigidbody2D playerRigid2D { get; private set; }
    public PlayerMove playerMove { get; private set; }
    public PlayerInput playerInput { get; private set; }
    public PlayerUseItem PlayerUseItem { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    private ViewPresenter viewPresenter;


    private void Awake()
    {
        Init();
        playerInput.Init();
        PlayerAttack.Init();
    }

    private void FixedUpdate()
    {
        playerMove.FixedUpdate();
    }


    /// <summary>
    /// 클래스 첫 생성 시 초기화를 실행할 메서드 
    /// </summary>
    public override void Init()
    {
        base.Init();
        HpBarView viewHpBar = ManagerHub.Instance.PoolManager.GetPoolObject<HpBarView>("HpBar");
        playerInput = new PlayerInput();
        playerMove = new PlayerMove(this);
        PlayerAttack = new PlayerAttack(this);
        PlayerUseItem = new PlayerUseItem(this);
        viewPresenter = new ViewPresenter(viewHpBar, playerData);
        viewPresenter.Init(transform);
        viewPresenter.UpdateHealthBar();
        playerData.Init();
    }


    /// <summary>
    /// 플레이어가 데미지를 입었을 때 실행할 메서드.
    /// </summary>
    /// <param name="damage"></param>
    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
        playerData.CurHP = Mathf.Max(playerData.CurHP - (int)damage, 0);
        viewPresenter.UpdateHealthBar();

        if (playerData.CurHP == 0)
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
    }
}
