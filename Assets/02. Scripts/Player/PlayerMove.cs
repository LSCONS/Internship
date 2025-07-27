using UnityEngine;

public class PlayerMove
{
    private Player player;

    public PlayerMove(Player player)
    {
        this.player = player;
    }


    public void FixedUpdate()
    {
        if (player.Data.IsDie) return;
        Move();
    }


    /// <summary>
    /// 플레이어의 이동 속도에 프레임 시간을 곱해 이동시키는 메서드
    /// </summary>
    private void Move()
    {
        Vector2 MoveVector2 = MoveVelocity();
        player.Rigid2D.linearVelocity = MoveVector2;
        player.FlipX(MoveVector2.x);
    }


    /// <summary>
    /// 플레이어의 입력 값과 플레이어의 속도를 곱한 이동 속도를 반환하는 메서드
    /// </summary>
    private Vector2 MoveVelocity()
    {
        Vector2 moveDir = player.Input.MoveDir;
        float speed = player.Data.Speed;
        return moveDir * speed;
    }
}
