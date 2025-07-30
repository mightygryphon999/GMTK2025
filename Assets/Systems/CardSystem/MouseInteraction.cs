using UnityEngine;
using UnityEngine.Rendering;

public class CardInteraction : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 worldPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            getMousePos();
            getMouseWorldPos();
            detectInteraction();
        }
    }

    public void detectInteraction()
    {
        Ray ray = new Ray(worldPos, Camera.main.transform.forward);
        
    }

    public void getMouseWorldPos()
    {
        worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
    }

    public void getMousePos()
    {
        screenPoint = Input.mousePosition;
    }
}
