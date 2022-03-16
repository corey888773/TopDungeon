using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int money = 10;
    protected override void OnCollect()
    {
        if (!collected)
        {
            // collected = true;
            base.OnCollect();
            GetComponent<SpriteRenderer>().sprite = emptyChest;

            GameManager.instance.pesos += money;
            GameManager.instance.ShowText("+" + money + " pesos", 35, Color.yellow, transform.position, Vector3.up * 20,
                2.0f);
        }
    }
}
