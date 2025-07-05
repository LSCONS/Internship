using UnityEngine;

public class UIManager
{
    public Canvas Canvas { get; private set; }

    public void Init()
    {
        Canvas = Object.Instantiate(ManagerHub.Instance.AddressableManager.CanvasPrefab);
    }
}
