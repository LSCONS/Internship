using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
    protected Action deadAction;


    public virtual void Init()
    {
        deadAction = null;
    }

    /// <summary>
    /// 데미지 처리를 받는 메서드
    /// </summary>
    /// <param name="damage"></param>
    public virtual void OnDamage(float damage)
    {

    }


    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }


    /// <summary>
    /// 사망시 실행할 메서드
    /// </summary>
    public virtual void Dead()
    {
        deadAction?.Invoke();
        deadAction = null;
    }


    /// <summary>
    /// 매개변수의 움직임이 (-)일 경우 flip을 활성화, (+)일 경우 비활성화 해주는 메서드.
    /// </summary>
    /// <param name="MoveX">움직이는 방향의 X값</param>
    public void FlipX(float MoveX)
    {
        if (MoveX == 0) return;
        bool isFlipX = MoveX < 0 ? true : false;
        if(SpriteRenderer.flipX != isFlipX) 
            SpriteRenderer.flipX = isFlipX;
    }
}
