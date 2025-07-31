using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PerksController : MonoBehaviour
{
    public GameController gc;
    public List<int> currentPerks; /* 1 is +1 point for the base value, 2 is +1 card on disk, 3 is +1 for round time, 4 is green cards are doubled
    , 5 is each red has +1 base, 6 is each red has +2 base, 7 is each green has +1 base, 8 is each green has +2 base, 9 is each orange has +1 base, 10 is orange has +2 base
    , 11 is noRice get *4 total score, 12 is orange gets *2 to total score, 13 is red gets *2 for total score*/

    // 1 is green, 2 is red, 3 is orange, 4 is rainbow, 5 is no rice
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
    public IEnumerator pointAdditions()
    {
        yield return new WaitForEndOfFrame();
        foreach (int perk in currentPerks)
        {
            if (perk == 1)
            {
                gc.points += 1;
            }
            else if (perk == 2)
            {
                // fill in later
            }
            else if (perk == 3)
            {
                gc.bonusRoundsAmount += 1;
            }
            else if (perk == 4)
            {
                multiplePoints(1, 2, true);
            }
            else if (perk == 5)
            {
                addPoints(2, 1);
            }
            else if (perk == 6)
            {
                addPoints(2, 2);
            }
            else if (perk == 7)
            {
                addPoints(1, 1);
            }
            else if (perk == 8)
            {
                addPoints(1, 2);
            }
            else if (perk == 9)
            {
                addPoints(3, 1);
            }
            else if (perk == 10)
            {
                addPoints(3, 2);
            }
            else if (perk == 11)
            {
                multiplePoints(5, 4, false);
            }
            else if (perk == 12)
            {
                multiplePoints(3, 2, true);
            }
            else if (perk == 13)
            {
                multiplePoints(2, 2, true);
            }
        }
    }
    public void multiplePoints(int type, int multiply, bool wantType)
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
                        if (wantType)
                        {
                            if (perk == type.ToString() || perk == "4")
                            {
                                typeCount++;
                            }
                        }
                        else
                        {
                            if (perk != type.ToString() || perk == "4")
                            {
                                typeCount++;
                            }
                        }
                    }
                }
            }
        }
        gc.points = gc.points * (typeCount * multiply);
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
                        if (perk == type.ToString() || perk == "4")
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
