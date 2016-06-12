using UnityEngine;
using System.Collections;
using System;

public class SpawnerItem : MonoBehaviour {
    public Transform item;
    Color[] colors;
    string[] items;
    float spawnItemCooldown;
    float spawnItemTimer;
    // Use this for initialization
    void Start () {
        spawnItemCooldown = 8f;
        setUpColors();
        setItems();
	}
	
	// Update is called once per frame
	void Update () {
	    if(spawnItemTimer > 0)
        {
            spawnItemTimer -= Time.deltaTime;
        }
        else
        {
            instantiateItem(UnityEngine.Random.Range(0, items.Length));
            spawnItemTimer = spawnItemCooldown;
        }
    }

    private void instantiateItem(int itemPosition)
    {
        Transform itemTransform = Instantiate(item) as Transform;
        Vector3 newPos = new Vector3(UnityEngine.Random.Range(4f, 5f), UnityEngine.Random.Range(1f, 3.5f), 0f);
        itemTransform.position = newPos;
        itemTransform.SetParent(this.transform);
        Component script = itemTransform.gameObject.AddComponent(Type.GetType(items[itemPosition]));
        Item scriptItem = itemTransform.gameObject.GetComponent(Type.GetType(items[itemPosition])) as Item;
        scriptItem.changeColor(colors[itemPosition]);
    }

    void shuffleArray()
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int t = 0; t < colors.Length; t++)
        {
            Color tmp = colors[t];
            int r = UnityEngine.Random.Range(t, colors.Length);
            colors[t] = colors[r];
            colors[r] = tmp;
        }
    }

    private void setUpColors()
    {
        colors = new Color[10];
        colors[0] = UnityEngine.Color.blue;
        colors[1] = UnityEngine.Color.black;
        colors[2] = UnityEngine.Color.cyan;
        colors[3] = UnityEngine.Color.gray;
        colors[4] = UnityEngine.Color.green;
        colors[5] = UnityEngine.Color.grey;
        colors[6] = UnityEngine.Color.magenta;
        colors[7] = UnityEngine.Color.red;
        colors[8] = UnityEngine.Color.white;
        colors[9] = UnityEngine.Color.yellow;
        shuffleArray();
    }

    private void setItems()
    {
        items = new string[2];
        items[0] = "Item_Life_Bonus";
        items[1] = "Item_Life_Malus";
    }
}
