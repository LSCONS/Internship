using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid2D;
    public string BulletID;
    private float speed = 5f;
    private float damage = 10f;
    private float range = 10f;
    private Vector2 startPoint;
    private Vector2 targetDir;


    public void FixedUpdate()
    {
        Move();
        CheckRange();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (1 << collider.gameObject.layer == ReadonlyData.EnemyLayerMask)
        {
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.OnDamage(damage);

            ReturnPool();
        }
    }


    /// <summary>
    /// 총알의 데이터를 초기화하는 메서드
    /// </summary>
    /// <param name="startPoint">시작 지점</param>
    /// <param name="targetPoint">목표 지점</param>
    /// <param name="speed">총알 속도</param>
    /// <param name="damage">총알 데미지</param>
    /// <param name="range">총알 사정거리</param>
    public void InitData(Vector2 startPoint, Vector2 targetPoint, float speed, float damage, float range)
    {
        this.startPoint = startPoint;
        this.targetDir = (targetPoint - startPoint).normalized;
        this.speed = speed;
        this.damage = damage;
        this.range = range;

        transform.position = startPoint;
        gameObject.SetActive(true);
    }


    /// <summary>
    /// 총알을 이동시키는 메서드
    /// </summary>
    private void Move()
    {
        rigid2D.linearVelocity = speed * targetDir;
    }


    /// <summary>
    /// 총알 발사 후 일정 거리를 이동하면 비활성화하는 메서드
    /// </summary>
    private void CheckRange()
    {
        if (Vector2.Distance(transform.position, startPoint) > range)
        {
            ReturnPool();
        }
    }


    /// <summary>
    /// 총알을 풀로 되돌리는 메서드
    /// </summary>
    private void ReturnPool()
    {
        gameObject.SetActive(false);
        ManagerHub.Instance.PoolManager.ReturnPool<Bullet>("Bullet", this);
    }
}
