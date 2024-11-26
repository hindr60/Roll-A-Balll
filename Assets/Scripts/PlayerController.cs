using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1f;
    private int pickupCount;
    private int totalPickups;
    public Timer timer;

    [Header("UI")]
    public GameObject inGamePanel;
    public TMP_Text pickupText;
    public TMP_Text timerText;
    public GameObject winPanel;
    public TMP_Text winTimeText;

    // Start is called before the first frame update
    private void Start()
    {
        //Gets the rigidbody component attached to this game object
        rb = GetComponent<Rigidbody>();
        //Get ther number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Assign the total amount of pickups
        totalPickups = pickupCount;
        //Run the CheckPickups function
        CheckPickups();
        //Gets the timer object
        timer = gameObject.GetComponent<Timer>();
       //timer = FindObjectOfType<Timer>();
        //Starts the timer
        timer.StartTimer();

        //Turn off our win panel
        winPanel.SetActive(false);
        //Turn on our in game panel
        inGamePanel.SetActive(true);
    }

    private void Update()
    {
        if (timerText != null)
        {
            timerText.text = "Timer: " + timer.GetTime().ToString("F2");
        }
        else
        {
            Debug.Log("Timer not found");
        }
            
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
        pickupText.text = "Pickups left: " + pickupCount.ToString();
        pickupText.color = Color.white;
        pickupText.fontSize += 7;
        if(pickupCount == 0)
        {
            WinGame();
        }
    }
    private void WinGame()
    {
        //Stops the timer
        timer.StopTimer();

        //Display the timer on our win time text
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Turn off the in game panel
        inGamePanel.SetActive(false);
        //Turn on the win panel
        winPanel.SetActive(true);

        //Stop the ball from rolling
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
 
    }
    
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
