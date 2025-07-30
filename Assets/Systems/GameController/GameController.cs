using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<CardPlacement> cards;
    public float points;
    public List<CardPlacement> hand;
    public void setupCards()
    {
        // cards setup
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
