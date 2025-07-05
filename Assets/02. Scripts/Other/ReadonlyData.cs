using UnityEngine;

public static class ReadonlyData
{
    public readonly static LayerMask EnemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");
    public readonly static LayerMask PlayerLayerMask = 1 << LayerMask.NameToLayer("Player");
}
