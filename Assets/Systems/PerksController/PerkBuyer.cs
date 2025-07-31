using System.Collections.Generic;
using DG.Tweening;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PerkBuyer : MonoBehaviour
{
    public PerksController pc;
    public int type;
    public UnityEngine.UI.Image perk;
    public List<GameObject> otherGainers;
    private float originalScale;

    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
    }

    void OnEnable()
    {
        gameObject.transform.DOScale(originalScale, 0.25f);
        perk.sprite = pc.perkImages[type];
        type = Random.Range(0, 13);
    }

    public void buyPerk()
    {
        pc.Setup();
        foreach (GameObject gain in otherGainers)
        {
            gain.GetComponent<PerkBuyer>().delete();
        }
        pc.currentPerks.Add(type);
        delete();
    }

    public void delete()
    {
        gameObject.transform.DOScale(0.01f, 0.25f);
    }
}
