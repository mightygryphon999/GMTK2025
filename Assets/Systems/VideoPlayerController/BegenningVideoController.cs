using UnityEngine;
using UnityEngine.Video;

public class BegenningVideoController : MonoBehaviour
{
    public VideoPlayer vp;
    public GameController gc;
    public GameObject text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp.loopPointReached += videoFinished;
        text.SetActive(true);
    }
    void videoFinished(VideoPlayer vp)
    {
        text.SetActive(false);
        vp.gameObject.SetActive(false);
        gc.startGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            text.SetActive(false);
            vp.Stop();
            vp.gameObject.SetActive(false);
            gc.startGame();
        }
    }
}
