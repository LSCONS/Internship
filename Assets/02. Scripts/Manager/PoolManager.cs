using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public int poolSize = 10;
    public Dictionary<Type, PoolHub> dictTypeToPool = new();

    public void Init()
    {
        dictTypeToPool[typeof(Item)] = new PoolHub();
        dictTypeToPool[typeof(Item)].InitDictStrToT<Item>(ManagerHub.Instance.AddressableManager.DictStrToItem);

        dictTypeToPool[typeof(Enemy)] = new PoolHub();
        dictTypeToPool[typeof(Enemy)].InitDictStrToT<Enemy>(ManagerHub.Instance.AddressableManager.DictStrToEnemy);

        dictTypeToPool[typeof(Bullet)] = new PoolHub();
        dictTypeToPool[typeof(Bullet)].InitDictStrToT<Bullet>(ManagerHub.Instance.AddressableManager.DictStrToBullet);

        dictTypeToPool[typeof(HpBarView)] = new PoolHub();
        dictTypeToPool[typeof(HpBarView)].InitDictStrToT<HpBarView>(ManagerHub.Instance.AddressableManager.DictStrToHpBar, true);
    }


    public void ReturnPool<T>(string key, MonoBehaviour obj)
    {
        if(!dictTypeToPool.TryGetValue(typeof(T), out PoolHub poolHub))
        {
            return;
        }

        poolHub.EnQueueGameObject(key, obj);
    }


    public T GetPoolObject<T>(string key) where T : MonoBehaviour
    {
        if(dictTypeToPool.TryGetValue(typeof(T), out PoolHub poolHub))
        {
            MonoBehaviour obj = poolHub.TryGetGameObject(key);
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
}
