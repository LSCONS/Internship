using UnityEngine;
using UnityEngine.UI;

public class HpBarView : MonoBehaviour, IViewHpBar
{
    public string ViewID;

    [field: SerializeField] private RectTransform RectTrObj { get; set;}
    [field: SerializeField] private Image ImgHpBar { get; set; }
    private Transform trParent;
    private Vector3 offset = new Vector2(0, -1);

    public void Init(Transform trParent)
    {
        this.trParent = trParent;
        gameObject.SetActive(true);
    }


    private void LateUpdate()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(trParent.position + offset);
        RectTrObj.position = screenPos;
    }


    public void ShowHealth(float amount)
    {
        ImgHpBar.fillAmount = amount;
    }


    public void ReturnPool()
    {
        gameObject.SetActive(false);
        ManagerHub.Instance.PoolManager.ReturnPool<HpBarView>(ViewID, this);
    }
}


public interface IViewHpBar
{
    public void Init(Transform trParent);
    public void ShowHealth(float amount);
}
