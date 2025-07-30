using UnityEngine;
using UnityEngine.Rendering;

public class CardInteraction : MonoBehaviour
{
    // private Vector3 screenPoint;
    // private Vector3 worldPos;
    private GameObject currentSelected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentSelected == null)
        {
            // getMousePos();
            // getMouseWorldPos();
            detectInteraction();
        }
        else if (Input.GetMouseButtonDown(0) && currentSelected != null)
        {
            // getMousePos();
            // getMouseWorldPos();
            detectPlacement();
        }
        
    }

    public void detectPlacement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.gameObject.GetComponent<CardPlacement>())
            {
                hit.collider.gameObject.GetComponent<CardPlacement>().currentCard = currentSelected;
                currentSelected.GetComponent<CardObject>().placeDown(hit.collider.gameObject);
                currentSelected.GetComponent<CardObject>().placed = true;
                currentSelected.GetComponent<CardObject>().cardSlot = hit.collider.gameObject;
                currentSelected = null;
            }
        }
    }

    public void detectInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.gameObject.GetComponent<CardObject>())
            {
                hit.collider.gameObject.GetComponent<CardObject>().interact();
                currentSelected = hit.collider.gameObject;
                currentSelected.GetComponent<CardObject>().placed = false;
                currentSelected.GetComponent<CardObject>().cardSlot = null;
            }
        }
    }

    // public void getMouseWorldPos()
    // {
    //     worldPos = Camera.main.ScreenToWorldPoint(screenPoint);
    // }

    // public void getMousePos()
    // {
    //     screenPoint = Input.mousePosition;
    // }
}
