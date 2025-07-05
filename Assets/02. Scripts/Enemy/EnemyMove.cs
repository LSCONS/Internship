using UnityEngine;

public class EnemyMove
{
    private Enemy enemy;
    public EnemyMove(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        Vector2 playerPoint = ManagerHub.Instance.PlayerManager.Player.transform.position;
        Vector2 enemyPoint = enemy.transform.position;
        Vector2 moveDir = (playerPoint - enemyPoint).normalized;
        enemy.Rigid2D.linearVelocity = enemy.enemyData.MoveSpeed * moveDir;
    }
}
