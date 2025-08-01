using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening.Plugins.Options;
using System.Collections;

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
    public List<string> currentPerks; // 1 is green, 2 is red, 3 is orange, 4 is rainbow, 5 is no rice
    public int points;
    public UnityEngine.UI.Image sprite;
    public float flipTime;
    public GameObject canvas;
    public bool hidden;
    public float destroyTime;
    public float flipHeight;

    public bool inHand;
    public GameObject perkImage;
    public GameObject perkSlot;
    // Add the perk visual initialization

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hover = transform.position.y + raiseAmount;
        canClick = true;
        sprite.sprite = images[points];
        setupPerks();
    }

    public void setupPerks()
    {
        if (currentPerks[points] != null)
        {
            string[] parts = currentPerks[points].Split(':');
            for (int i = parts.Length - 1; i >= 0; i--)
            {
                GameObject perk = Instantiate(perkImage);
                perk.transform.SetParent(perkSlot.transform, worldPositionStays: false);
                perk.GetComponent<UnityEngine.UI.Image>().sprite = perks[int.Parse(parts[i])-1];
            }
        }
    }

    public void delete()
    {
        DOTween.Kill(gameObject.transform);
        if (cardSlot != null)
        {
            cardSlot.GetComponent<CardPlacement>().currentCard = null;
        }
        gameObject.transform.DOScale(0.01f, destroyTime).OnComplete(() => { Destroy(gameObject); });
    }

    public void show()
    {
        hidden = false;
        canvas.SetActive(true);
        canClick = true;
        gameObject.transform.DORotate(new Vector3(-90, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z), flipTime).SetEase(Ease.InOutSine);
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + flipHeight, gameObject.transform.position.z), flipTime / 2).SetEase(Ease.OutBounce).OnComplete(() => { gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - flipHeight, gameObject.transform.position.z), flipTime / 2).SetEase(Ease.InBounce); });
    }

    public void hide()
    {
        hidden = true;
        canvas.SetActive(false);
        gameObject.transform.DORotate(new Vector3(90, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z), flipTime).SetEase(Ease.InOutSine);
        gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + flipHeight, gameObject.transform.position.z), flipTime / 2).SetEase(Ease.OutBounce).OnComplete(() => { gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - flipHeight, gameObject.transform.position.z), flipTime / 2).SetEase(Ease.InBounce); });
    }

    public void interact()
    {
        if (!selected)
        {
            // if (hidden)
            // {
            //     show();
            // }
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
                placeDown(cardSlot, false, 0, true, false);
            }
        }
    }

    public void placeDown(GameObject target, bool hide, float watiTime, bool show, bool overideShow)
    {
        DOTween.Kill(gameObject.transform);
        selected = false;
        canClick = false;
        gameObject.transform.SetParent(target.transform);
        transform.DOMove(new Vector3(target.transform.position.x, target.transform.position.y + floatAmount, target.transform.position.z), transferSpeed).SetEase(Ease.OutSine).OnComplete(() => { hovering = false; canClick = true; if (hide) { StartCoroutine(flipAfterTime(watiTime)); } });
        if (show && target.CompareTag("Hand") || overideShow)
        {
            inHand = true;
            if (overideShow && !target.CompareTag("Hand"))
            {
                inHand = false;
            }
            canvas.SetActive(true);
            transform.DORotate(new Vector3(-90, target.transform.eulerAngles.y, 0), transferSpeed).SetEase(Ease.OutSine);
        }
        else
        {
            inHand = false;
            canvas.SetActive(false);
            transform.DORotate(new Vector3(90, target.transform.eulerAngles.y, 0), transferSpeed).SetEase(Ease.OutSine);
        }
    }

    IEnumerator flipAfterTime(float timer)
    {
        canClick = false;
        yield return new WaitForSeconds(timer);
        canClick = true;
        gameObject.GetComponent<CardObject>().hide();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
