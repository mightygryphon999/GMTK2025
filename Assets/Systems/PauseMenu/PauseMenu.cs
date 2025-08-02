using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
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
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseButton.SetActive(true);
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
                pauseButton.SetActive(false);
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseButton.SetActive(true);
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }
}
