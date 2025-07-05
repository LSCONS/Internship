using System.Collections;
using UnityEngine;

public class PlayerAttack
{
    private Player player;
    public Coroutine AttackCoroutine;
    private WaitForSeconds attackCoolTime;  //공격 쿨타임(몬스터를 공격했을 때의 지연)
    private WaitForSeconds attackDelay = new WaitForSeconds(0.05f);     //공격 지연 시간(몬스터를 공격하지 못했을 때의 지연)

    public PlayerAttack(Player player)
    {
        this.player = player;
    }


    public void Init()
    {
        attackCoolTime = new WaitForSeconds(player.playerData.attackCoolTime);
        AttackCoroutine = player.StartCoroutine(AttackRepeatCoroutine());
    }


    public bool TryAttackEnemy()
    {
        Vector2 startPoint = player.transform.position;
        float checkRange = player.playerData.playerRange;
        LayerMask enemyLayerMask = ReadonlyData.EnemyLayerMask;

        Collider2D enemy = Physics2D.OverlapCircle(startPoint, checkRange, enemyLayerMask);
        if (enemy == null) return false;

        Bullet bulletObj = ManagerHub.Instance.PoolManager.GetPoolObject<Bullet>("Bullet");
        if (bulletObj == null) return false;

        bulletObj.InitData(player.transform.position, enemy.transform.position, 5, 10, 10);
        return true;
    }


    public void Attack(Vector2 targetPosition)
    {

    }


    private IEnumerator AttackRepeatCoroutine()
    {
        while (true)
        {
            yield return TryAttackEnemy() ? attackCoolTime : attackDelay;
        }
    }
}
