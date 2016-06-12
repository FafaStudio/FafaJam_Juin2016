using UnityEngine;
using System.Collections;

public class Item_Life_Malus : Item {

    public override void Awake()
    {
        base.Awake();
        this.itemName = "life_malus";
        this.bonus = false;
    }

    public override void itemEffect()
    {
        this.playerManager.takeDamage(1);
    }
}
