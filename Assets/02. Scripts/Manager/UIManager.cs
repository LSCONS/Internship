using UnityEngine;

public class UIManager
{
    public Canvas Canvas { get; private set; }
    public UIEnemyGuide UIEnemyGuide { get; private set; }
    public UIOptionOpen OptionOpen { get; private set; }
    public bool isOpenUI = false;

    public void InitCanvas()
    {
        Canvas = Object.Instantiate(ManagerHub.Instance.AddressableManager.CanvasPrefab);
    }

    public void Init()
    {
        UIEnemyGuide = Object.Instantiate(ManagerHub.Instance.AddressableManager.UIEnemyGuidePrefab, Canvas.transform);
        OptionOpen = Object.Instantiate(ManagerHub.Instance.AddressableManager.UIOptionOpenPrefab, Canvas.transform);
    }


    public void SetActiveUI(bool isActive)
    {
        PoolHub hpBarHub = ManagerHub.Instance.PoolManager.dictTypeToPool[typeof(HpBarView)];
        hpBarHub?.SetActiveParentObj(isActive);
    }
}
