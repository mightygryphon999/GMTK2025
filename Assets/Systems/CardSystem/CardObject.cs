using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;

public class CardObject : MonoBehaviour
{
    public float raiseAmount;
    public float raiseSpeed;
    public float floatAmount;
    public float transferSpeed;
    public GameObject cardSlot;
    public bool placed;
    public bool selected;
    private bool hovering;
    private float hover;
    public bool canMove;
    public bool canClick;
    public List<Texture2D> images;
    public List<Texture2D> perks;
    public List<int> currentPerks; // 1 is green, 2 is red, 3 is orange, 4 is rainbow
    public int points;
    public RawImage sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hover = transform.position.y + raiseAmount;
        canClick = true;
        sprite.texture = images[points];
    }

    public void interact()
    {
        if (!selected)
        {
            canMove = false;
            canClick = false;
            transform.DOMoveY(transform.position.y + raiseAmount, raiseSpeed)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                canClick = true;
                hovering = true;
                canMove = true;
                selected = true;
            });
        }
        else
        {
            if (!placed)
            {
                placeDown(cardSlot);
            }
        }
    }

    public void placeDown(GameObject target)
    {
        selected = false;
        canClick = false;
        gameObject.transform.SetParent(target.transform);
        transform.DOMove(new Vector3(target.transform.position.x, target.transform.position.y + floatAmount, target.transform.position.z), transferSpeed).SetEase(Ease.OutSine).OnComplete(() => {hovering = false; canClick = true; });
        transform.DORotate(new Vector3(-90, target.transform.eulerAngles.y, 0), transferSpeed).SetEase(Ease.OutSine);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
