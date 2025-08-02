using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NarrativeText : MonoBehaviour
{
    public List<GameObject> text;
    public List<int> delay;
    public GameObject canvas;
    private int count;
    public string sceneAfterLoad;
    public bool playing;
    private Coroutine textI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        count = 0;
    }
    void Update()
    {
        if (Input.GetMouseButton(0) && playing)
        {
            StopCoroutine(textI);
            SceneManager.LoadScene(sceneAfterLoad);
        }
    }

    public void startNarrativeText()
    {
        canvas.SetActive(true);
        count = 0;
        for (int i = 1; i < text.Count; i++)
        {
            text[i].SetActive(false);
        }
        textI = StartCoroutine(textDisplay());
    }

    IEnumerator textDisplay()
    {
        playing = true;
        for (int i = 0; i < text.Count; i++)
        {
            text[i].SetActive(true);
            text[i].GetComponent<CanvasGroup>().DOFade(1, 0.1f);
            yield return new WaitForSeconds(delay[i]);
            text[i].GetComponent<CanvasGroup>().DOFade(0, 0.1f).OnComplete(() => {text[i].SetActive(false);});
        }
        canvas.SetActive(false);
        playing = false;
        SceneManager.LoadScene(sceneAfterLoad);
    }
}
