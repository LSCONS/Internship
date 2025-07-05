using UnityEngine;

public class PlayerUseItem
{
    private Player player;

    public PlayerUseItem(Player player)
    {
        this.player = player;
    }


    public void UseItem(ItemData itemData)
    {
        PlayerData playerData = player.playerData;
        playerData.CurHP = Mathf.Min(playerData.MaxHP, playerData.CurHP + itemData.MaxHP);
        playerData.CurMP = Mathf.Min(playerData.MaxMP, playerData.CurMP + itemData.MaxMP);
        playerData.MaxAtk += itemData.MaxAtk;
        playerData.MaxAtkMul *= itemData.MaxAtkMul;
        playerData.MaxDef += itemData.MaxDef;
        playerData.MaxDefMul *= itemData.MaxDefMul;
    }
}
