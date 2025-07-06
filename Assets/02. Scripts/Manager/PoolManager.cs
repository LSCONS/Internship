using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public int poolSize = 10;
    public Dictionary<Type, PoolHub> dictTypeToPool = new();
    private List<string> listMonsterID = new();

    /// <summary>
    /// 풀 매니저 클래스 생성 후 실행할 초기화 메서드
    /// </summary>
    public void Init()
    {
        foreach (string id in ManagerHub.Instance.DataManager.DictStrToEnemy.Keys)
        {
            listMonsterID.Add(id);
        }

        dictTypeToPool[typeof(Item)] = new PoolHub();
        dictTypeToPool[typeof(Item)].InitDictStrToT<Item>(ManagerHub.Instance.AddressableManager.DictStrToItem);

        dictTypeToPool[typeof(Enemy)] = new PoolHub();
        dictTypeToPool[typeof(Enemy)].InitDictStrToT<Enemy>(ManagerHub.Instance.AddressableManager.DictStrToEnemy);

        dictTypeToPool[typeof(Bullet)] = new PoolHub();
        dictTypeToPool[typeof(Bullet)].InitDictStrToT<Bullet>(ManagerHub.Instance.AddressableManager.DictStrToBullet);

        dictTypeToPool[typeof(AttackDistance)] = new PoolHub();
        dictTypeToPool[typeof(AttackDistance)].InitDictStrToT(ManagerHub.Instance.AddressableManager.DictStrToAtk);

        dictTypeToPool[typeof(HpBarView)] = new PoolHub();
        dictTypeToPool[typeof(HpBarView)].InitDictStrToT<HpBarView>(ManagerHub.Instance.AddressableManager.DictStrToHpBar, true);
    }


    /// <summary>
    /// 타입에 맞는 풀 안에 해당 객체를 집어넣는 메서드
    /// </summary>
    /// <typeparam name="T">설정 타입</typeparam>
    /// <param name="key">입력할 key</param>
    /// <param name="obj">집어넣을 객체</param>
    public void ReturnPool<T>(string key, MonoBehaviour obj, bool setParent = false)
    {
        if(!dictTypeToPool.TryGetValue(typeof(T), out PoolHub poolHub))
        {
            return;
        }

        poolHub.EnQueueGameObject(key, obj);
        if (setParent) poolHub.SetParentPool(key, obj);
    }


    /// <summary>
    /// 타입에 맞는 풀Hub에서 key와 맞는 풀 중 하나를 반환하는 메서드.
    /// </summary>
    /// <typeparam name="T">설정 타입</typeparam>
    /// <param name="key">입력할 key</param>
    /// <param name="isCopy">풀에 없을 경우 복사 여부</param>
    public T GetPoolObject<T>(string key, bool isCopy = true) where T : MonoBehaviour
    {
        if(dictTypeToPool.TryGetValue(typeof(T), out PoolHub poolHub))
        {
            MonoBehaviour obj = poolHub.TryGetGameObject(key, isCopy);
            if (obj != null && obj is T result)
            {
                return result;
            }
        }
#if UNITY_EDITOR
        Debug.LogError($"PoolManager: '{typeof(T)}'에 해당하는 PoolHub를 찾을 수 없습니다.");
#endif
        return default;
    }


    /// <summary>
    /// 랜덤으로 모든 몬스터 풀 중 한 몬스터를 반환하는 메서드.
    /// </summary>
    public Enemy GetRandomEnemy()
    {
        ///설정해둔 몬스터ID를 가진 List에서 랜덤으로 한 몬스터를 지정.
        ///해당 몬스터의 풀이 비어있다면 List에서 제거 후 다른 랜덤 몬스터 지정.
        ///성공시 해당 몬스터 풀에서 들어있는 몬스터를 반환.
        ///실패시 null반환.
        ///모든 처리가 끝난 경우 제외했던 string을 다시 List에 추가.
        Enemy resultEnemy = null;
        List<string> listRemove = new();

        while(listMonsterID.Count > 0)
        {
            int randomMonsterIndex = GetRandomMonsterIndex();
            string monsterID = listMonsterID[randomMonsterIndex];
            resultEnemy = GetPoolObject<Enemy>(monsterID, false);

            if (resultEnemy == null)
            {
                listRemove.Add(monsterID);
                listMonsterID.RemoveListAt(randomMonsterIndex);
            }
            else break;
        }

        foreach(string monsterID in listRemove)
        {
            listMonsterID.Add(monsterID);
        }

        return resultEnemy;
    }


    /// <summary>
    /// 랜덤으로 몬스터ID를 반환하는 메서드
    /// </summary>
    private int GetRandomMonsterIndex()
    {
        int monsterCount = listMonsterID.Count;
        int randomIndex = UnityEngine.Random.Range(0, monsterCount);
        return randomIndex;
    }
}
