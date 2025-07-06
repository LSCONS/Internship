using UnityEngine;

public class Enemy : Character
{
    [field: SerializeField] public Rigidbody2D Rigid2D { get; private set; }
    public string MonsterID;
    public EnemyData EnemyData { get; private set; }
    public EnemyMove EnemyMove { get; private set; }
    public EnemyAttack EnemyAttack { get; private set; }
    public AttackDistance AttackDistance { get; private set; }


    private ViewPresenter viewPresenter;


    private void Awake()
    {
        EnemyData = ManagerHub.Instance.DataManager.GetEnemyData(MonsterID);
        EnemyMove = new EnemyMove(this);
        EnemyAttack = new EnemyAttack(this);
    }


    private void OnDisable()
    {
        EnemyAttack?.OnDisable();
    }


    public override void Init()
    {
        base.Init();
        HpBarView viewHpBar = ManagerHub.Instance.PoolManager.GetPoolObject<HpBarView>("HpBar");
        AttackDistance = ManagerHub.Instance.PoolManager.GetPoolObject<AttackDistance>("AtkDis");
        EnemyData.Init();

        viewPresenter = new ViewPresenter(viewHpBar, EnemyData);
        viewPresenter.Init(transform);
        viewPresenter.UpdateHealthBar();

        AttackDistance.Init(this);

        deadAction += DropItem;
        deadAction += ReturnPool;
        deadAction += viewHpBar.ReturnPool;
        deadAction += AttackDistance.ReturnPool;

        gameObject.SetActive(true);
        EnemyAttack.Init();
    }


    private void FixedUpdate()
    {
        EnemyMove?.FixedUpdate();
    }


    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);

        EnemyData.CurHp = Mathf.Max(EnemyData.CurHp - (int)damage, 0);

        viewPresenter.UpdateHealthBar();
        if (EnemyData.CurHp == 0)
        {
            Dead();
        }
    }


    public override void Dead()
    {
        base.Dead();
    }


    private void DropItem()
    {
        Vector2 itemPosition = transform.position;
        foreach (int itemID in EnemyData.DropItem)
        {
            Item item = ManagerHub.Instance.PoolManager.GetPoolObject<Item>(itemID.ToString());
            if (item != null)
            {
                item.Init(itemPosition);
                itemPosition += Vector2.down;
            }
        }
    }


    private void ReturnPool()
    {
        gameObject.SetActive(false);
        ManagerHub.Instance.PoolManager.ReturnPool<Enemy>(MonsterID, this);
        AttackDistance = null;
    }
}
