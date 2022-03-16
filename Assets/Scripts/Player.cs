using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    public int currentCharacterSelection = 0;

    protected override void ReceiveDamage(Damage dmg)
    {
        {
            base.ReceiveDamage(dmg);
            GameManager.instance.OnHitpointChange();
        }
    }
   
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        UpdateMotor(new Vector3(x, y, 0));

    }
    public void SwapSprite(int spriteId)
    {
        currentCharacterSelection = spriteId;
        spriteRenderer.sprite = GameManager.instance.playerSprites[spriteId];
    }
    public void OnLevelUp(bool gameStart)
    {
        Debug.Log("lvled");
        maxHitpoint++;
        hitpoint = maxHitpoint;
        GameManager.instance.OnHitpointChange();
        
        if(!gameStart)
            GameManager.instance.ShowText("Level Up!", 25, Color.green, transform.position, Vector3.up * 10, 1.0f);
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp(true);
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitpoint != maxHitpoint)
        {
            if (maxHitpoint - hitpoint > healingAmount)
            {
                hitpoint += healingAmount;
                GameManager.instance.ShowText("+ " + healingAmount + "hp", 30, Color.red, transform.position, Vector3.zero, 1.0f);
                GameManager.instance.OnHitpointChange();
                return;
            }
            else
                hitpoint = maxHitpoint;
                GameManager.instance.ShowText("+ " + healingAmount + "hp", 30, Color.red, transform.position, Vector3.zero, 1.0f);
                GameManager.instance.OnHitpointChange();
                return;

        }
        GameManager.instance.ShowText("max hp", 30, Color.red, transform.position, Vector3.zero, 1.0f);
    }

    
}
