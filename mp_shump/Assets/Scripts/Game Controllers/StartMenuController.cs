using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public string startScene;

    public void startGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
