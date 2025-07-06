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


    /// <summary>
    /// 클래스 첫 생성시 실행할 초기화 메서드
    /// </summary>
    public void Init()
    {
        attackCoolTime = new WaitForSeconds(player.Data.attackCoolTime);
        AttackCoroutine = player.StartCoroutine(AttackRepeatCoroutine());
    }


    /// <summary>
    /// 공격을 시도하고 성공 여부를 반환하는 메서드
    /// </summary>
    /// <returns></returns>
    public bool TryAttackEnemy()
    {
        Vector2 startPoint = player.transform.position;
        float checkRange = player.Data.playerRange;
        LayerMask enemyLayerMask = ReadonlyData.EnemyLayerMask;

        Collider2D enemy = Physics2D.OverlapCircle(startPoint, checkRange, enemyLayerMask);
        if (enemy == null) return false;

        Bullet bulletObj = ManagerHub.Instance.PoolManager.GetPoolObject<Bullet>("Bullet");
        if (bulletObj == null) return false;

        bulletObj.InitData(player.transform.position, enemy.transform.position, 5, 10, 10);
        return true;
    }


    /// <summary>
    /// 공격을 멈추는 메서드
    /// </summary>
    public void StopAttack()
    {
        player.StopCoroutine(AttackCoroutine);
        AttackCoroutine = null;
    }


    /// <summary>
    /// 공격을 반복적으로 실행하는 메서드
    /// </summary>
    /// <returns></returns>
    private IEnumerator AttackRepeatCoroutine()
    {
        yield return attackCoolTime;
        while (true)
        {
            if (player.Input.MoveDir == Vector2.zero)
            {
                yield return TryAttackEnemy() ? attackCoolTime : attackDelay;
            }
            else yield return attackDelay;
        }
    }
}
