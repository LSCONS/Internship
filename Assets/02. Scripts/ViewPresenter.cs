using UnityEngine;

public class ViewPresenter
{
    private IViewHpBar viewHpBar;
    private IHpModel HpModel;

    public ViewPresenter(IViewHpBar viewHpBar, IHpModel hpModel)
    {
        this.viewHpBar = viewHpBar;
        this.HpModel = hpModel;
    }

    public void Init(Transform trParent)
    {
        viewHpBar.Init(trParent);
    }

    public void UpdateHealthBar()
    {
        viewHpBar.ShowHealth((float)HpModel.ModelCurHp / HpModel.ModelMaxHp);
    }
}

public interface IHpModel
{
    public int ModelMaxHp { get;}
    public int ModelCurHp { get;}
}
