using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressableManager
{
    public Player PlayerPrefab { get; private set; }
    public Canvas CanvasPrefab { get; private set; }
    public UIEnemyGuide UIEnemyGuidePrefab { get; private set; }
    public Button BtnUIEnemyGuide { get; private set; }
    public UIOptionOpen UIOptionOpenPrefab { get; private set; }
    public Dictionary<string, Bullet> DictStrToBullet { get; private set; } = new();
    public Dictionary<string, Enemy> DictStrToEnemy { get; private set; } = new();
    public Dictionary<string, Item> DictStrToItem { get; private set; } = new();
    public Dictionary<string, HpBarView> DictStrToHpBar { get; private set; } = new();

    public async Task Init()
    {
        PlayerPrefab = await LoadObjectResource<Player>("Player");
        CanvasPrefab = await LoadObjectResource<Canvas>("Canvas");
        UIEnemyGuidePrefab = await LoadObjectResource<UIEnemyGuide>("EnemyGuide");
        BtnUIEnemyGuide = await LoadObjectResource<Button>("GuideSlot");
        UIOptionOpenPrefab = await LoadObjectResource<UIOptionOpen>("OptionOpen");
        await InitDictionary<Bullet, string>(DictStrToBullet, "Bullet", bullet => bullet.BulletID);
        await InitDictionary<Item, string>(DictStrToItem, "Item", item => item.ItemID);
        await InitDictionary<Enemy, string>(DictStrToEnemy, "Enemy", enemy => enemy.MonsterID);
        await InitDictionary<HpBarView, string>(DictStrToHpBar, "HpBar", hpBar => hpBar.ViewID);
    }


    private async Task InitDictionary<T, TKey>(Dictionary<TKey, T>dict, string addressableName, Func<T, TKey> key)
    {
        List<T> listData = await LoadArrayObjectResource<T>(addressableName);
        foreach(T data in listData)
        {
            TKey dataKey = key(data);
            if (!dict.ContainsKey(dataKey))
            {
                dict[dataKey] = data;
            }
        }
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T> LoadObjectResource<T>(string adress)
    {
        T data = default;
        var temp = Addressables.LoadAssetAsync<GameObject>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            data = temp.Result.GetComponent<T>();
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<List<T>> LoadArrayObjectResource<T>(string adress)
    {
        GameObject[] objArray = default;
        List<T> dataArray = new();
        var temp = Addressables.LoadAssetsAsync<GameObject>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            objArray = temp.Result.ToArray();
            if (typeof(T) == typeof(GameObject))
            {
                dataArray = objArray.Cast<T>().ToList();
            }
            else
            {
                foreach (GameObject obj in objArray)
                {
                    if (obj.TryGetComponent<T>(out T component))
                    {
                        dataArray.Add(component);
                    }
                    else
                    {
                        Debug.LogError($"어드레서블 로딩 실패: {obj.name}에 {typeof(T).Name} 컴포넌트가 없습니다.");
                    }
                }
            }
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return dataArray;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T> LoadTypeResource<T>(string adress)
    {
        T data = default;
        var temp = Addressables.LoadAssetAsync<T>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            data = temp.Result;
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }


    /// <summary>
    /// 데이터를 원하는 타입으로 어드레서블을 통해 불러오는 메서드
    /// </summary>
    /// <typeparam name="T">가져올 타입</typeparam>
    /// <param name="adress">가져올 주소</param>
    /// <returns>반환 받을 데이터</returns>
    private async Task<T[]> LoadTypeResources<T>(string adress)
    {
        T[] data = default;
        var temp = Addressables.LoadAssetsAsync<T>(adress);
        await temp.Task;
        if (temp.Status == AsyncOperationStatus.Succeeded)
        {
            data = temp.Result.ToArray();
        }
        else
        {
            Debug.LogError($"어드레서블 로딩 실패: {temp.OperationException?.Message}");
        }
        return data;
    }
}
