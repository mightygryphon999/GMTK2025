using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }
    public void togglePause()
    {
        paused = !paused;
        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }
}
