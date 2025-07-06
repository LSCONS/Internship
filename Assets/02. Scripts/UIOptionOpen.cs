using UnityEngine;
using UnityEngine.UI;

public class UIOptionOpen : MonoBehaviour
{
    [field: SerializeField] public Button BtnOptionOpen { get; private set; }

    private void Awake()
    {
        BtnOptionOpen.onClick.AddListener(() =>
        {
            ManagerHub.Instance.UIManager.UIEnemyGuide.gameObject.SetActive(true);
        });
    }
}
