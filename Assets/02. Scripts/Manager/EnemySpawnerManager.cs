using UnityEngine;

public class EnemySpawnerManager
{
    public void Init()
    {
        SpawnEnemy("M0010", new Vector2(-5, 0));
    }


    private void SpawnEnemy(string monsterID, Vector2 position)
    {
        Enemy enemy = ManagerHub.Instance.PoolManager.GetPoolObject<Enemy>(monsterID);

        if(enemy == null)
        {
            Debug.LogError($"Enemy 클래스를 찾을 수 없습니다. ID: {monsterID}");
            return;
        }

        enemy.Init();
        enemy.SetPosition(position);
    }
}
