using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMechanic : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody rb;
    public bool playerIsOnGround = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

 
    void Update()
    {
        //When the space bar is pressed, the force will push the ball to move up into the air.
        if (Input.GetButtonDown("Jump") && playerIsOnGround)
        {
            //This code will only work if the player is off the ground.
            rb.AddForce(new Vector3(0, 3, 0), ForceMode.Impulse);
            playerIsOnGround = false;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        ///This logic prevents the player from jumping repeatedly into the air. It only works once. The variable of playerIsOnGround 
        ///determines the state of the player, whether it is in the air or not and applies the force only when the condition is true. 

        if(collision.gameObject.name == "Floor")
        {
            playerIsOnGround = true;
        }
    }
}
