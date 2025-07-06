using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public Vector2 MoveDir { get; private set; }
    private Input_Player input_Player = new();
    private Player player;

    public PlayerInput(Player player)
    {
        this.player = player;
    }


    /// <summary>
    /// 클래스 첫 생성 시 초기화를 실행할 메서드
    /// </summary>
    public void Init()
    {
        AddEvent();
    }


    /// <summary>
    /// 플레이어 입력 이벤트를 추가하는 메서드
    /// </summary>
    public void AddEvent()
    {
        input_Player.Enable(); 
        input_Player.Player.Move.performed  += OnMove;
        input_Player.Player.Move.canceled   += StopMove;
    }


    /// <summary>
    /// 플레이어가 이동 키를 입력했을 때 실행할 메서드
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        MoveDir = context.ReadValue<Vector2>().normalized;
        player.Animator.SetBool(ReadonlyData.AnimatorHash_IsMove, true);
        player.Animator.SetBool(ReadonlyData.AnimatorHash_IsIdle, false);
    }


    /// <summary>
    /// 플레이어가 모든 입력키를 땠을 때 실행할 메서드
    /// </summary>
    private void StopMove(InputAction.CallbackContext context)
    {
        MoveDir = Vector2.zero;
        player.Animator.SetBool(ReadonlyData.AnimatorHash_IsMove, false);
        player.Animator.SetBool(ReadonlyData.AnimatorHash_IsIdle, true);
    }
}
