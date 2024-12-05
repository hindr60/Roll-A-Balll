using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public CameraController cameraScript;
    public CursorController cursorController;
    // By default sets the isPaused action to false.
    bool isPaused = false;

    private void Start()
    {
        // When the game is started the pause screen will be off.
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    
    private void Update()
    {
        // Escape key is entered the pause function will happen.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Pause action
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Sets to show the pause panel an stop time completely in the game, pausing everything.
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            cameraScript.enabled = false;
        }
        else
        {
            // If it hasnt been pressed it will resume as normal, pause panel off and the time running normal.
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            cameraScript.enabled = true;
        }
    }


}
