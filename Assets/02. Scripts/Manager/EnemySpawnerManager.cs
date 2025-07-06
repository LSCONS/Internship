using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class EnemySpawnerManager
{
    private WaitForSeconds waitEnemySpawn;
    public float SpawnCooldown = 10;        //몬스터 스폰 쿨타임
    public float SpawnDistance = 5;         //플레이어로부터 몬스터 스폰 거리
    private Coroutine enemySpawnCoroutine;

    public void Init()
    {
        waitEnemySpawn = new WaitForSeconds(SpawnCooldown);
        enemySpawnCoroutine = ManagerHub.Instance.StartCoroutine(RepeatSpawnCoroutine());
    }


    /// <summary>
    /// 특정 시간마다 랜덤으로 몬스터를 특정 위치에 스폰하는 메서드
    /// </summary>
    private IEnumerator RepeatSpawnCoroutine()
    {
        while (true)
        {
            Enemy spawnEnemy = ManagerHub.Instance.PoolManager.GetRandomEnemy();
            if(spawnEnemy != null)
            {
                Transform trPlayer = ManagerHub.Instance.PlayerManager.Player.transform;
                Vector2 spawnPosition = trPlayer.position.RandomPointOnCircle(SpawnDistance);
                spawnEnemy.Init();
                spawnEnemy.SetPosition(spawnPosition);
            }
            yield return waitEnemySpawn;
        }
    }
}
