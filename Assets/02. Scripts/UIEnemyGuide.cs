using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyGuide : MonoBehaviour
{
    [field: SerializeField] public TextMeshProUGUI TextName         { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextDescription  { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextAttack       { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextHP           { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextRange        { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextAttackSpeed  { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextMoveSpeed    { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextDropExp      { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextDropItem     { get; private set; }

    [field: SerializeField] public Transform TrContent              { get; private set; }
    private List<Button> listBtnEnemySlot = new();

    private void Awake()
    {
        foreach (EnemyData enemyData in ManagerHub.Instance.DataManager.DictStrToEnemy.Values)
        {
            Button btn = Instantiate(ManagerHub.Instance.AddressableManager.BtnUIEnemyGuide, TrContent);
            listBtnEnemySlot.Add(btn);
            btn.onClick.AddListener(() => SetData(enemyData));
        }
        gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        Init();
        Time.timeScale = 0f;
        ManagerHub.Instance.UIManager.SetActiveUI(false);
    }


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }


    private void OnDisable()
    {
        Time.timeScale = 1f;
        ManagerHub.Instance.UIManager.SetActiveUI(true);
    }


    /// <summary>
    /// 텍스트의 출력을 빈 문자열로 초기화해주는 메서드 
    /// </summary>
    public void Init()
    {
        TextName          .text = string.Empty;
        TextDescription   .text = string.Empty;
        TextAttack        .text = string.Empty;
        TextHP            .text = string.Empty;
        TextRange         .text = string.Empty;
        TextAttackSpeed   .text = string.Empty;
        TextMoveSpeed     .text = string.Empty;
        TextDropExp       .text = string.Empty;
        TextDropItem      .text = string.Empty;
    }


    /// <summary>
    /// 텍스트의 문자열을 몬스터 정보로 설정하는 메서드
    /// </summary>
    /// <param name="enemyData">설정할 몬스터 정보</param>
    public void SetData(EnemyData enemyData)
    {
        TextName.text = enemyData.Name;
        TextDescription.text = enemyData.Description;
        TextAttack.text = (enemyData.Attack * enemyData.AttackMul).ToString();
        TextHP.text = (enemyData.MaxHP * enemyData.MaxHPMul).ToString();
        TextRange.text = (enemyData.AttackRange * enemyData.AttackRangeMul).ToString();
        TextAttackSpeed.text = enemyData.AttackSpeed.ToString();
        TextMoveSpeed.text = enemyData.MoveSpeed.ToString();
        TextDropExp.text = $"{enemyData.MinExp} ~ {enemyData.MaxExp}";

        StringBuilder sb = new StringBuilder();
        bool isFirst = true;
        foreach(int itemId in enemyData.DropItem)
        {
            if (isFirst) isFirst = false;
            else sb.Append(" / ");

            if (ManagerHub.Instance.DataManager.DictStrToItem.TryGetValue(itemId.ToString(), out ItemData itemData))
            {
                sb.Append(itemData.Name);
            }
        }
        TextDropItem.text = sb.ToString();
    }
}
