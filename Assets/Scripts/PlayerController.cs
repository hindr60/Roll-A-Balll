using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 1f;
    private int pickupCount;
    private int totalPickups;
    GameObject resetPoint;
    bool resetting = false;
    Color originalColour;
    public Transform _camTransform;
    public CursorController cursorController;

    // All controllers.
    Timer timer;
    CameraController cameraController;
    SoundController soundController;
    GameController gameController;

    [Header("UI")]
    public GameObject inGamePanel;
    public TMP_Text pickupText;
    public TMP_Text timerText;
    public GameObject winPanel;
    public TMP_Text winTimeText;


    private void Start()
    {
        //Starts the timer.
        timer.StartTimer();

        //Gets the rigidbody component attached to this game object.
        rb = GetComponent<Rigidbody>();
        //Get ther number of pickups in our scene.
        pickupCount = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //Assign the total amount of pickups.
        totalPickups = pickupCount;
        //Run the CheckPickups function.
        CheckPickups();
        //Gets the timer object.
        timer = gameObject.GetComponent<Timer>();

        

        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;

        cameraController = FindObjectOfType<CameraController>();
        soundController = FindObjectOfType<SoundController>();

        gameController = FindObjectOfType<GameController>();
        timer = FindObjectOfType<Timer>();

        if (gameController.gameType == GameType.SpeedRun)
            StartCoroutine(timer.StartCountdown());

        //Turn off our win panel.
        winPanel.SetActive(false);
        //Turn on our in game panel.
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
        if (resetting)
            return;


        //Store the horizontal axis value in a float.
        float moveHorizontal = Input.GetAxis("Horizontal");
      

        //Store the vertical axis in a float.
        float moveVertical = Input.GetAxis("Vertical");

        // Allows for the player to move depending on the rotation of the camera.
        Vector3 right = _camTransform.right * moveHorizontal;
        Vector3 forward = _camTransform.forward * moveVertical;

        Vector3 movement = right + forward;
        

        //Adds force to the rigidbody component.
        rb.AddForce(movement * speed);

        if (cameraController.cameraStyle == CameraStyle.Free)
        {
            transform.eulerAngles = Camera.main.transform.eulerAngles;
            movement = transform.TransformDirection(movement);
        }
        if (!(gameController.gameType != GameType.SpeedRun && timer.IsTiming()))
            return;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup") 
        {
            //Destroy the collided object.
            Destroy(other.gameObject);
            //Determines the pickup count.
            pickupCount--;
            //Run the CheckPickups function again.
            CheckPickups();
            soundController.PlayPickupSound();
        }
    }

    // This function allows the reset surface to be recognised and play the ResetPlayer function. 
    private void OnCollisionEnter(Collision collision)
    {
        // If the player is in collision with an object with the respawn tag, the reset player function will start.
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        // Darkens the ball's material as a signal of a reset process.
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        // Stops the movement of the ball completely.
        rb.velocity = Vector3.zero;
        // This section moves the player back to the reset point.
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        // Once the ball has been reset the ball is restored to its original colour.
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;
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
        
        //Stops the timer.
        timer.StopTimer();

        //Display the timer on our win time text.
        winTimeText.text = "Your time was: " + timer.GetTime().ToString("F2");

        //Turn off the in game panel.
        inGamePanel.SetActive(false);
        //Turn on the win panel.
        winPanel.SetActive(true);

        //Stop the ball from rolling.
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        cameraController.enabled = false;
        soundController.PlayWinSound();

        if (gameController.gameType == GameType.SpeedRun)
            timer.StopTimer();

    }
    
    public void ResetGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
