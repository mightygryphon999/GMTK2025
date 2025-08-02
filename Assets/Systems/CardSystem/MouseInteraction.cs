using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardInteraction : MonoBehaviour
{
    // private Vector3 screenPoint;
    // private Vector3 worldPos;
    public GameObject currentSelected;
    public GameController gc;
    public int layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = ~LayerMask.GetMask("coinBarrier");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentSelected == null)
        {
            // getMousePos();
            // getMouseWorldPos();
            detectInteraction();
            detectRuleBook();
        }
        else if (Input.GetMouseButtonDown(0) && currentSelected != null)
        {
            // getMousePos();
            // getMouseWorldPos();
            detectPlacement();
            detectRuleBook();
        }
    }
    public void detectRuleBook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<RuleBook>())
            {
                if (hit.collider.gameObject.GetComponent<RuleBook>().shouldMove)
                {
                    hit.collider.gameObject.GetComponent<RuleBook>().shouldMove = false;
                }
                else
                {
                    hit.collider.gameObject.GetComponent<RuleBook>().shouldMove = true;
                }
            }
        }
    }

    public void detectPlacement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<CardPlacement>() && gc.playing)
            {
                if (hit.collider.gameObject.GetComponent<CardPlacement>().currentCard == null)
                {
                    if (currentSelected.GetComponent<CardObject>().canMove && !currentSelected.GetComponent<CardObject>().inHand || currentSelected.GetComponent<CardObject>().canMove && hit.collider.gameObject.GetComponent<CardPlacement>().CompareTag("Hand"))
                    {
                        hit.collider.gameObject.GetComponent<CardPlacement>().currentCard = currentSelected;
                        hit.collider.gameObject.GetComponent<CardPlacement>().allCards.Add(currentSelected);
                        currentSelected.GetComponent<CardObject>().placeDown(hit.collider.gameObject, false, 0, true, false);
                        currentSelected.GetComponent<CardObject>().placed = true;
                        currentSelected.GetComponent<CardObject>().selected = false;
                        currentSelected.GetComponent<CardObject>().cardSlot = hit.collider.gameObject;
                        currentSelected.GetComponent<CardObject>().canMove = true;
                        currentSelected = null;
                    }
                }
            }
        }
    }

    public void detectInteraction()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<CardObject>() && gc.playing)
            {
                Debug.Log("Card");
                if (!hit.collider.gameObject.GetComponent<CardObject>().selected && hit.collider.gameObject.GetComponent<CardObject>().canClick)
                {
                    if (hit.collider.gameObject.GetComponent<CardObject>().cardSlot != null)
                    {
                        hit.collider.gameObject.GetComponent<CardObject>().cardSlot.GetComponent<CardPlacement>().currentCard = null;
                        hit.collider.gameObject.GetComponent<CardObject>().cardSlot.GetComponent<CardPlacement>().allCards.Remove(hit.collider.gameObject);
                    }
                    hit.collider.gameObject.GetComponent<CardObject>().interact();
                    currentSelected = hit.collider.gameObject;
                    currentSelected.GetComponent<CardObject>().placed = false;
                    currentSelected.GetComponent<CardObject>().selected = true;
                    currentSelected.GetComponent<CardObject>().cardSlot = null;
                }
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
