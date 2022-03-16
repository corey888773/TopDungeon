using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage structure
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14} ;
    public float[] pushForce = {2.0f, 2.2f, 2.5f, 2.7f, 2.85f, 3.0f, 3.15f, 3.3f, 3.4f, 3.5f, 3.6f, 3.75f, 4.0f, 4.5f};
    
    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    public KeyCode swingKey;
    private Animator animator;
    private float cooldown = 0.5f;
    private float lastSwing = 0.0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        //spriteRenderer = GetComponent<SpriteRenderer>(); it didnt work because start is forced after awake fun
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(swingKey))
        {
            if (Time.time - lastSwing > cooldown)
            {
                Swing();
                lastSwing = Time.time;
                
            }
        }
    }
    private void Swing()
        {
            animator.SetTrigger("Swing");
        }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
                return;
            
            //Creates a dmg container

            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            
            coll.SendMessage("ReceiveDamage", dmg);
            
            Debug.Log(coll.name);
        }
            
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        
        //Change stats
        
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
