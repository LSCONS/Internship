using System.Collections.Generic;
using UnityEngine;

public class PoolHub
{
    private Dictionary<string, MonoBehaviour> DictStrToT = new();
    private Dictionary<string, Transform> DictStrToTrParent = new();
    public Dictionary<string, Queue<MonoBehaviour>> DictStrToQueue { get; private set; } = new();
    private bool isUIPool = false;


    public void InitDictStrToT<T>(Dictionary<string, T> dict, bool isUIPool = false) where T : MonoBehaviour
    {
        this.isUIPool = isUIPool;
        foreach(var kvp in dict)
        {
            DictStrToT[kvp.Key] = kvp.Value;
        }
        InitPool();
    }


    private void InitPool()
    {
        foreach (var kvp in DictStrToT)
        {
            InstantiatePoolObject(kvp.Key);
        }
    }


    /// <summary>
    /// 풀에 오브젝트가 있으면 해당 오브젝트를 반환, 없다면 새로 생성 후 반환하는 메서드
    /// </summary>
    /// <param name="key">입력할 키</param>
    /// <returns></returns>
    public MonoBehaviour TryGetGameObject(string key)
    {
        if (DictStrToQueue.TryGetValue(key, out Queue<MonoBehaviour> queue) && queue.Count > 0)
        {
            return queue.Dequeue();
        }
        return AddGameObject(key);
    }


    /// <summary>
    /// 첫 실행 시 풀에 오브젝트를 생성하는 메서드.
    /// </summary>
    /// <param name="key">입력할 키</param>
    private void InstantiatePoolObject(string key)
    {
        if (!DictStrToQueue.ContainsKey(key))
        {
            DictStrToQueue[key] = new Queue<MonoBehaviour>();
        }
        AddPoolObject(key, ManagerHub.Instance.PoolManager.poolSize);
    }


    /// <summary>
    /// 다시 풀로 오브젝트를 반환하는 메서드
    /// </summary>
    /// <param name="key">입력할 키</param>
    /// <param name="obj">반환할 오브젝트</param>
    public void EnQueueGameObject(string key, MonoBehaviour obj)
    {
        if(!DictStrToQueue.TryGetValue(key, out Queue<MonoBehaviour> queue))
        {
#if UNITY_EDITOR
            Debug.LogError($"PoolHub: '{key}'에 해당하는 Queue를 찾을 수 없습니다.");
#endif
        }
        obj.gameObject.SetActive(false);
        queue.Enqueue(obj);
    }


    /// <summary>
    /// 특정 수 만큼 오브젝트를 복사해서 생성하는 메서드.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="count"></param>
    private void AddPoolObject(string key, int count)
    {
        if (DictStrToT.TryGetValue(key, out MonoBehaviour prefab))
        {
            for (int i = 0; i < count; i++)
            {
                MonoBehaviour obj = Object.Instantiate(prefab, GetParentTransform(key));
                obj.gameObject.SetActive(false);
                DictStrToQueue[key].Enqueue(obj);
            }
        }
    }


    /// <summary>
    /// 풀에 오브젝트가 없을 경우 새로 생성하는 메서드.
    /// </summary>
    /// <param name="key">입력할 키</param>
    private MonoBehaviour AddGameObject(string key)
    {
        if (DictStrToT.TryGetValue(key, out MonoBehaviour prefab))
        {
            MonoBehaviour obj = Object.Instantiate(prefab, GetParentTransform(key));
            obj.gameObject.SetActive(false);
            return obj;
        }
        return null;
    }


    /// <summary>
    /// key값에 해당하는 부모 오브젝트를 가져오는 메서드.
    /// </summary>
    /// <param name="key">입력할 키</param>
    private Transform GetParentTransform(string key)
    {
        if (!DictStrToTrParent.TryGetValue(key, out Transform trParent))
        {
            GameObject parentObject = new GameObject($"{key} Pool");
            if (isUIPool)
            {
                parentObject.AddComponent<RectTransform>();
                parentObject.transform.SetParent(ManagerHub.Instance.UIManager.Canvas.transform);

            }
            else
            {
                parentObject.transform.SetParent(ManagerHub.Instance.transform);
            }
            trParent = parentObject.transform;
            DictStrToTrParent[key] = trParent;
        }
        return trParent;
    }
}
