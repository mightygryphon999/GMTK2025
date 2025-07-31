using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string level;
    public void loadScene()
    {
        SceneManager.LoadScene(level);
    }
}
