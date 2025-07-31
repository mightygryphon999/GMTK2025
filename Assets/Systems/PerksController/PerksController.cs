using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PerksController : MonoBehaviour
{
    public GameController gc;
    public List<int> currentPerks; /* 1 is +1 point for the base value, 2 is +1 card on disk, 3 is +1 for round time, 4 is green cards are doubled
    , 5 is each red has +1 base, 6 is each red has +2 base, 7 is each green has +1 base, 8 is each green has +2 base, 9 is each orange has +1 base, 10 is orange has +2 base
    , 11 is noRice get *4 total score, 12 is orange gets *2 to total score, 13 is red gets *2 for total score*/
    public List<Sprite> perkImages;
    public GameObject perkGroup;
    public GameObject perkImagePrefab;
    public void Setup()
    {
        foreach (int perk in currentPerks)
        {
            GameObject perkGameobject = Instantiate(perkImagePrefab);
            perkGameobject.transform.SetParent(perkImagePrefab.transform);
            perkGameobject.GetComponent<UnityEngine.UI.Image>().sprite = perkImages[perk];
        }
    }
    public void pointAdditions()
    {
        foreach (int perk in currentPerks)
        {
            
        }
    }
    public void addPoints(int type, int addition)
    {
        int typeCount = 0;
        for (int i = 0; i < gc.hand.Count; i++)
        {
            for (int x = 0; x < gc.hand[i].currentCard.GetComponent<CardObject>().currentPerks.Count; i++)
            {
                if (!string.IsNullOrEmpty(gc.hand[i].currentCard.GetComponent<CardObject>().currentPerks[x]))
                {
                    string[] list = gc.hand[i].currentCard.GetComponent<CardObject>().currentPerks[x].Split(':');
                    foreach (string perk in list)
                    {
                        if (perk == type.ToString())
                        {
                            typeCount++;
                        }
                    }
                }
            }
        }
        gc.points += typeCount * addition;
    }
}
