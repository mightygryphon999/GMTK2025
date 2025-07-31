using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<CardPlacement> cards;
    public float points;
    public List<CardPlacement> hand;
    public GameObject cardPrefab;
    public Vector3 startingPos;
    public Quaternion startingRot;
    public float cardSpawnRate;
    public float preFlipTime;

    [ContextMenu("Start Game")]
    public void startGame()
    {
        StartCoroutine(setupCards());
    }

    IEnumerator setupCards()
    {
        foreach (CardPlacement card in cards)
        {
            GameObject newCard = Instantiate(cardPrefab, startingPos, startingRot);
            card.currentCard = newCard;
            newCard.GetComponent<CardObject>().placeDown(card.gameObject, true);
            newCard.GetComponent<CardObject>().placed = true;
            newCard.GetComponent<CardObject>().selected = false;
            newCard.GetComponent<CardObject>().cardSlot = card.gameObject;
            newCard.GetComponent<CardObject>().canMove = true;
            newCard.GetComponent<CardObject>().points = Random.Range(0, newCard.GetComponent<CardObject>().images.Count - 1);
            newCard.GetComponent<CardObject>().hide();
            newCard = null;
            yield return new WaitForSeconds(cardSpawnRate);
        }
    }

    public void countPoints()
    {
        points = 0;
        for (int i = 0; i < hand.Count; i++)
        {
            CardPlacement card = hand[i];
            int neighboringCards = checkNextInList(i, hand[i].currentCard.GetComponent<CardObject>().points);
            points += (1 + neighboringCards) * neighboringCards;
            i += neighboringCards - 1;
        }
        foreach (CardPlacement handI in hand)
        {
            handI.currentCard.GetComponent<CardObject>().delete();
        }
    }
    public int checkNextInList(int index, int matchPoints)
    {
        int count = 1;
        for (int i = index + 1; i < hand.Count; i++)
        {
            var nextCard = hand[i].currentCard.GetComponent<CardObject>();
            if (nextCard.points == matchPoints)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count; // fill in;
    }
}
