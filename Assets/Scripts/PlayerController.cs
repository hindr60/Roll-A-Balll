using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1f;
    private int pickupCount;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        //Get ther number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Run the CheckPickups function
        CheckPickups();
        //Gets the timer object
        timer = FindObjectOfType<Timer>();
        //Starts the timer
        timer.StartTimer(); 
    }
    void FixedUpdate()
    {
        //Store the horizontal axis value in a float
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Store the vertical axis in a float
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Adds force to the rigidbody component
        rb.AddForce(movement * speed);

       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup") 
        {
            //Destroy the collided object
            Destroy(other.gameObject);
            //Determines the pickup count
            pickupCount--;
            //Run the CheckPickups function again
            CheckPickups();
        }
    }

    private void CheckPickups()
    {
        print("Pickups left: " + pickupCount);
        if(pickupCount == 0)
        {
            WinGame();
        }
    }
    private void WinGame()
    {
        timer.StopTimer();
        print("Yipee! You win. Your time was: " + timer.GetTime().ToString("F2"));
    }
}
