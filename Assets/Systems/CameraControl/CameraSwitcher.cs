using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public int currentCam;
    public List<GameObject> cams;
    public GameObject Camera;
    public float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentCam = 0;
        goToCam(currentCam);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            currentCam++;
            if (currentCam >= cams.Count)
            {
                currentCam = 0;
            }
            goToCam(currentCam);
        }
    }

    public void goToCam(int index)
    {
        DOTween.Kill(Camera.transform);
        Camera.transform.DOMove(cams[index].transform.position, moveSpeed);
        Camera.transform.DORotate(new Vector3(cams[index].transform.eulerAngles.x, cams[index].transform.eulerAngles.y, cams[index].transform.eulerAngles.z), moveSpeed);
    }
}
