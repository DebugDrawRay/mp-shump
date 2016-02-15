using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SelectLevelController : MonoBehaviour
{
    [SerializeField]
    public levelAsset[] availableLevels;

    public string nextScene;

    public void SelectLevel(int levelNumber)
    {
        if(GameSettings.instance)
        {
            GameSettings.instance.selectedLevel = availableLevels[levelNumber];
            SceneManager.LoadScene(nextScene);
        }
    }
}
