using UnityEngine;

public class Item : MonoBehaviour
{
    public string ItemID;
    private ItemData itemData;

    private void Awake()
    {
        itemData = ManagerHub.Instance.DataManager.GetItemData(ItemID);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == ReadonlyData.PlayerLayerMask)
        {
            ManagerHub.Instance.PlayerManager.Player.UseItem.UseItem(itemData);
            ReturnPool();
        }
    }


    /// <summary>
    /// 풀로 데이터를 반환하는 메서드.
    /// </summary>
    private void ReturnPool()
    {
        gameObject.SetActive(false);
        ManagerHub.Instance.PoolManager.ReturnPool<Item>(ItemID, this);
    }


    public void Init(Vector2 startPosition)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }
}
