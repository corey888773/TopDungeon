using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenmyHitbox : Collidable
{
   //Damage
   public int damagePoint = 1;
   public float pushForce = 5;

   protected override void OnCollide(Collider2D coll)
   {
      if (coll.tag == "Fighter" && coll.name == "Player")
      {
         //create dmg object
         Damage dmg = new Damage
         {
            damageAmount = damagePoint,
            origin = transform.position,
            pushForce = pushForce
         };
         
         coll.SendMessage("ReceiveDamage", dmg);
      }
   }
}
