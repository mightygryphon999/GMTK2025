using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    public List<RectTransform> pauses;
    public List<float> pauseLength;
    public List<bool> shown;
    public float speed;
    public int index = 0;
    public bool paused;
    private RectTransform rt;
    public float threshold;
    private int currentPause;
    public AudioSource audioS;
    public AudioClip firstAudioClip;
    void Start()
    {
        audioS.PlayOneShot(firstAudioClip);
        float length = firstAudioClip.length;
        Debug.Log(length);
        Invoke("playSecondSound", length);
        rt = gameObject.GetComponent<RectTransform>();
        shown = new List<bool>();
        foreach (RectTransform transform in pauses)
        {
            shown.Add(false);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector2(-100, threshold), new Vector2(100, threshold));
    }

    // Update is called once per frame
    void Update()
    {
        int speedboost = Input.GetKeyDown(KeyCode.Escape) ? 1 : 0;
        if (!paused) { rt.anchoredPosition += new Vector2(0, (speed + 300 * speedboost) * Time.deltaTime); }
        ;
        for (int i = 0; i < pauses.Count; i++)
        {
            if (pauses[i].position.y >= threshold && !shown[i] && speedboost == 0)
            {
                shown[i] = true;
                currentPause = i;
                StartCoroutine(pauseForTime(pauseLength[i]));
                break;
            }
        }
        if (!paused && currentPause+1 == pauses.Count)
        {
            SceneManager.LoadScene("MenuScene");
        }
    }

    IEnumerator pauseForTime(float time)
    {
        Debug.Log("paused");
        paused = true;
        yield return new WaitForSeconds(time);
        paused = false;
        Debug.Log("unpaused");
    }
}
