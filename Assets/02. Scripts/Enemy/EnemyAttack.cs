using System.Collections;
using UnityEngine;

public class EnemyAttack
{
    private Enemy enemy;
    private WaitForSeconds attackCooldown;
    private WaitForSeconds attackTimeDelay = new WaitForSeconds(0.1f);
    private WaitForSeconds attackStartDelay = new WaitForSeconds(0.5f);
    private Coroutine coroAttack;

    public EnemyAttack(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void Init()
    {
        attackCooldown = new WaitForSeconds(1 / enemy.EnemyData.AttackSpeed);
        coroAttack = enemy.StartCoroutine(AttackCoroutine());
    }


    private IEnumerator AttackCoroutine()
    {
        yield return attackTimeDelay;
        while (true)
        {
            Vector2 enemyPosition = enemy.transform.position;
            Vector2 playerPosition = ManagerHub.Instance.PlayerManager.Player.transform.position;
            float enemyRange = enemy.EnemyData.TotalRange;
            if(Vector2.Distance(enemyPosition, playerPosition) <= enemyRange)
            {
                Vector2 dir = playerPosition - enemyPosition;
                //몬스터 움직임 멈춤
                enemy.EnemyMove.SetMove(false);
                enemy.AttackDistance.gameObject.SetActive(true);
                yield return attackStartDelay;

                //공격 시도
                RaycastHit2D ray = Physics2D.Raycast(enemyPosition, dir, enemyRange, ReadonlyData.PlayerLayerMask);
                enemy.AttackDistance.gameObject.SetActive(false);
                if (ray.collider != null)
                {
                    ManagerHub.Instance.PlayerManager.Player.OnDamage(enemy.EnemyData.TotalAttack);
                }

                //움직임 재시작
                enemy.EnemyMove.SetMove(true);
                yield return attackCooldown;
            }
            else
            {
                yield return attackTimeDelay;
            }
        }
    }

    public void OnDisable()
    {
        if(coroAttack != null) enemy.StopCoroutine(coroAttack);
        coroAttack = null;
    }
}
