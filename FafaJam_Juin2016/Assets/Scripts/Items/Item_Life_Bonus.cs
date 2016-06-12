using UnityEngine;
using System.Collections;

public class Item_Life_Bonus : Item {

    public override void Awake()
    {
        base.Awake();
        this.itemName = "life_bonus";
        this.bonus = true;
    }

    public override void itemEffect()
    {
        this.playerManager.pvRegen(1);
    }
}
