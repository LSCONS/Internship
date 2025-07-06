using UnityEngine;

public class AttackDistance : MonoBehaviour
{
    public string AttackDistanceID;
    private Enemy enemy;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        Vector3 temp = transform.localScale;
        temp.x = enemy.EnemyData.TotalRange;
        transform.localScale = temp;

        transform.parent = enemy.transform;
        transform.localPosition = Vector3.zero;
    }


    private void LateUpdate()
    {
        if (enemy == null) return;
        if (ManagerHub.Instance.PlayerManager.Player == null) return;

        float angle = enemy.transform.position.ComputeAngle2D(ManagerHub.Instance.PlayerManager.Player.transform.position);
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }


    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ManagerHub.Instance.PoolManager.ReturnPool<AttackDistance>(AttackDistanceID, this, true);
    }
}
