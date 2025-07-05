using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    [field: SerializeField] public Rigidbody2D Rigid2D { get; private set; }
    public string MonsterID;
    public EnemyData enemyData { get; private set; }
    private EnemyMove enemyMove;
    private ViewPresenter viewPresenter;

    private void Awake()
    {
        enemyData = ManagerHub.Instance.DataManager.GetEnemyData(MonsterID);
        enemyMove = new EnemyMove(this);
    }


    public override void Init()
    {
        base.Init();
        HpBarView viewHpBar = ManagerHub.Instance.PoolManager.GetPoolObject<HpBarView>("HpBar");
        enemyData.Init();
        deadAction += DropItem;
        deadAction += ReturnPool;
        deadAction += viewHpBar.ReturnPool;
        viewPresenter = new ViewPresenter(viewHpBar, enemyData);
        viewPresenter.Init(transform);
        viewPresenter.UpdateHealthBar();
        gameObject.SetActive(true);
    }


    private void FixedUpdate()
    {
        enemyMove?.FixedUpdate();
    }


    public override void OnDamage(float damage)
    {
        if (enemyData.CurHp == 0) return;
        base.OnDamage(damage);
        enemyData.CurHp = Mathf.Max(enemyData.CurHp - (int)damage, 0);
        viewPresenter.UpdateHealthBar();
        if (enemyData.CurHp == 0)
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
        foreach (int itemID in enemyData.DropItem)
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
    }
}
