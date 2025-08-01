using UnityEngine;
using UnityEngine.Video;

public class BegenningVideoController : MonoBehaviour
{
    public VideoPlayer vp;
    public GameController gc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp.loopPointReached += videoFinished;
    }
    void videoFinished(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
        gc.startGame();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vp.Stop();
        }
    }
}
