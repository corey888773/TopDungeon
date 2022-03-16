using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class cant be used within an object. It can only be inherited  
public abstract class Mover : Fighter
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;
    
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset movedelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        
        //swap player direction
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1, 1, 1); // or Vector3.one
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //add push vector if any
        moveDelta += pushDirections;
        //reduce push every frame based on recovery speed
        pushDirections = Vector3.Lerp(pushDirections, Vector3.zero, pushRecoverySpeed); // lerp changes the value from starting to ending during the time (start, end , time) 
        
        //check collision with objectects of actor and blocking layers
        //(checks if the destination box is covered with some blocking structures. If not allows player to move there)
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make this thing move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //make this thing move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }



        // Debug.Log(x);
        // Debug.Log(y);
    }
}
