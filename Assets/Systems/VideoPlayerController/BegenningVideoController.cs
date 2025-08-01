using UnityEngine;
using UnityEngine.Video;

public class BegenningVideoController : MonoBehaviour
{
    public VideoPlayer vp;
    public GameController gc;
    public GameObject text;
    public bool playing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vp.prepareCompleted += vp => playVideo();
        vp.Prepare();
    }
    public void playVideo()
    {
        playing = true;
        vp.Play();
        vp.loopPointReached += videoFinished;
        text.SetActive(true);
    }
    void videoFinished(VideoPlayer vp)
    {
        text.SetActive(false);
        vp.gameObject.SetActive(false);
        gc.startGame();
        playing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && playing)
        {
            playing = false;
            text.SetActive(false);
            vp.Stop();
            vp.gameObject.SetActive(false);
            gc.startGame();
        }
    }
}
