using UnityEngine;

public static class ReadonlyData
{
    public readonly static LayerMask EnemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    public readonly static LayerMask PlayerLayerMask = 1 << LayerMask.NameToLayer("Player");

    public readonly static int AnimatorHash_IsDie = Animator.StringToHash("IsDie");
    public readonly static int AnimatorHash_IsMove = Animator.StringToHash("IsMove");
    public readonly static int AnimatorHash_IsIdle = Animator.StringToHash("IsIdle");
}
