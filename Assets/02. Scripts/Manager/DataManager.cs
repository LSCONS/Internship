using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager
{
    public Dictionary<string, EnemyData> DictStrToEnemy { get; private set; } = new();
    public Dictionary<string, ItemData> DictStrToItem { get; private set; } = new();

    public void Init()
    {
        InitDictionary<EnemyData, string>(DictStrToEnemy, "Monster", enemy => enemy.MonsterID);
        InitDictionary<ItemData, string>(DictStrToItem, "Item", item => item.ItemID);
    }


    private void InitDictionary<T, TKey>(Dictionary<TKey, T> dict, string fileName, Func<T, TKey> key)
    {
        List<T> listData = LoadListData<T>(fileName);
        foreach (T data in listData)
        {
            TKey dataKey = key(data);
            if (!dict.ContainsKey(dataKey))
            {
                dict[dataKey] = data;
            }
        }
    }


    /// <summary>
    /// Json파일의 데이터를 List로 반환하는 메서드
    /// </summary>
    /// <typeparam name="T">반환받을 타입</typeparam>
    /// <param name="fileName">Resources의 주소/파일 값</param>
    private List<T> LoadListData<T>(string fileName)
    {
        List<T> listData = new List<T>();

        TextAsset enemyJsonFile = Resources.Load<TextAsset>(fileName);
        if (enemyJsonFile == null)
        {
            Debug.LogError("EnemyJson을 찾을 수 없습니다.");
            return listData;
        }

        ListDataClass<T> listDataClass = JsonUtility.FromJson<ListDataClass<T>>(enemyJsonFile.text);
        listData = listDataClass.ListData;

        return listData;
    }


    /// <summary>
    /// List에서 enemyData를 찾아 반환하는 메서드
    /// </summary>
    /// <param name="monsterID">찾을 몬스터ID</param>
    public EnemyData GetEnemyData(string monsterID)
    {
        if(DictStrToEnemy.TryGetValue(monsterID, out EnemyData enemyData))
        {
            EnemyData result = new EnemyData(enemyData);
            return result;
        }
#if UNITY_EDITOR
        Debug.LogError($"몬스터 ID {monsterID}를 찾을 수 없습니다.");
#endif
        return null;
    }


    /// <summary>
    /// List에서 itemData를 찾아 반환하는 메서드
    /// </summary>
    /// <param name="itemID">찾을 아이템ID</param>
    public ItemData GetItemData(string itemID)
    {
        if (DictStrToItem.TryGetValue(itemID, out ItemData itemData))
        {
            ItemData result = new ItemData(itemData);
            return result;
        }
#if UNITY_EDITOR
        Debug.LogError($"아이템 ID {itemID}를 찾을 수 없습니다.");
#endif
        return null;
    }
}


public class ListDataClass<T>
{
    public List<T> ListData;
}
