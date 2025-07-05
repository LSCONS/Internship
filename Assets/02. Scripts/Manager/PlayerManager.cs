using UnityEngine;

public class PlayerManager
{
    public Player Player { get; private set; }

    public void Init()
    {
        Player = Object.Instantiate(ManagerHub.Instance.AddressableManager.PlayerPrefab);
    }
}
