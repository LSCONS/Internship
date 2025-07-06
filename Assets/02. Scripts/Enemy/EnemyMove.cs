using UnityEngine;

public class EnemyMove
{
    private Enemy enemy;
    private bool canMove { get; set; } = true;


    public EnemyMove(Enemy enemy)
    {
        this.enemy = enemy;
    }


    public void FixedUpdate()
    {
        if (canMove) Move();
        else enemy.Rigid2D.linearVelocity = Vector3.zero;
    }


    /// <summary>
    /// 몬스터가 플레이어를 향해 움직이게 하는 메서드
    /// </summary>
    private void Move()
    {
        Vector2 playerPoint = ManagerHub.Instance.PlayerManager.Player.transform.position;
        Vector2 enemyPoint = enemy.transform.position;
        Vector2 moveDir = (playerPoint - enemyPoint).normalized;
        enemy.Rigid2D.linearVelocity = enemy.EnemyData.MoveSpeed * moveDir;
    }


    /// <summary>
    /// 몬스터의 움직임을 결정하는 메서드
    /// </summary>
    /// <param name="isMove">몬스터 움직임 여부</param>
    public void SetMove(bool isMove)
    {
        canMove = isMove;
    }
}
