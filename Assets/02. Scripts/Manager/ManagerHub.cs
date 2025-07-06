using UnityEngine;

public class ManagerHub : Singleton<ManagerHub>
{
    [field: SerializeField] public EnemySpawnerManager EnemySpawnerManager { get; private set; }
    public AddressableManager AddressableManager { get; private set; } = new();
    public PoolManager PoolManager { get; private set; } = new();
    public DataManager DataManager { get; private set; } = new();
    public PlayerManager PlayerManager { get; private set; } = new();
    public UIManager UIManager { get; private set; } = new();

    protected override async void Awake()
    {
        base.Awake();
        await AddressableManager.Init();
        UIManager.InitCanvas();
        DataManager.Init();
        PoolManager.Init();

        UIManager.Init();
        PlayerManager.Init();
        EnemySpawnerManager.Init();
    }

}
