using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using DG.Tweening.Plugins.Options;

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
    public List<Sprite> images;
    public List<Sprite> perks;
    public List<int> currentPerks; // 1 is green, 2 is red, 3 is orange, 4 is rainbow
    public int points;
    public UnityEngine.UI.Image sprite;
    public float flipTime;

    // Add the perk visual initialization

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hover = transform.position.y + raiseAmount;
        canClick = true;
        sprite.sprite = images[points];
    }

    public void setupPerks()
    {
        // put the perk setup code here
    }

    public void show()
    {
        gameObject.transform.DORotate(new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, 0), flipTime).SetEase(Ease.InBounce);
    }

    public void hide()
    {
        gameObject.transform.DORotate(new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, 180), flipTime).SetEase(Ease.InBounce);
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
