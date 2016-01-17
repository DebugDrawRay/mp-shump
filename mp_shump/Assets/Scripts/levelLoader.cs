using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class levelLoader : MonoBehaviour
{
    public Scene level;

    void Start()
    {
        SceneManager.LoadSceneAsync(level.name);
    }
}
