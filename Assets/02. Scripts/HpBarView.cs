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


    /// <summary>
    /// HpBar의 상태를 입력 값에 따라 초기화하는 메서드.
    /// </summary>
    /// <param name="amount">입력할 수치</param>
    public void ShowHealth(float amount)
    {
        ImgHpBar.fillAmount = amount;
    }


    /// <summary>
    /// HpBar오브젝트를 풀로 되돌리는 메서드.
    /// </summary>
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
