using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PerkBuyer : MonoBehaviour
{
    public PerksController pc;
    public int type;
    public UnityEngine.UI.Image perk;
    public List<GameObject> otherGainers;

    void OnEnable()
    {
        gameObject.transform.DOScale(33.58229f, 0.25f);
        type = Random.Range(0, 13);
        perk.sprite = pc.perkImages[type];
    }

    public void buyPerk()
    {
        pc.Setup();
        foreach (GameObject gain in otherGainers)
        {
            gain.GetComponent<PerkBuyer>().delete();
        }
        pc.currentPerks.Add(type+1);
        delete();
    }

    public void delete()
    {
        gameObject.transform.DOScale(0.01f, 0.25f);
    }
}
