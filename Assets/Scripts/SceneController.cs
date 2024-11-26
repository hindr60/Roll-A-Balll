using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Will change the scene to the string passed in
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Reloads the current scene that the screen is on
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // This loads out the title scene
    public void ToTitleScene()
    {
        SceneManager.LoadScene("Title");
    }

    //Gets our active scene's name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
