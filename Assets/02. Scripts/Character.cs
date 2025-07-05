using System;
using UnityEngine;

public class Character : MonoBehaviour
{
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
}
