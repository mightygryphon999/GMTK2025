using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public bool stackMode;
    private bool playing;
    public float quota;
    public float round;
    public TextMeshProUGUI quotaText;
    public GameObject roundWon;
    private bool roundWonShowing;
    public TextMeshProUGUI inGameQuotaText;
    public TextMeshProUGUI inGameRoundCounter;
    private int roundTillDeath;
    private float totalPoints;
    public TextMeshProUGUI currentPointCounter;

    void Start()
    {
        roundTillDeath = 5;
        currentPointCounter.text = "Points: 0";
        round = 0;
        quota = 15;
        roundWonShowing = false;
        startGame();
    }

    public void startGame()
    {
        if (!playing)
        {
            inGameRoundCounter.text = "Round: " + roundTillDeath.ToString();
            inGameQuotaText.text = "Quota: " + quota.ToString();
            if (roundWonShowing)
            {
                roundWon.GetComponent<CanvasGroup>().DOFade(0f, 0.5f).OnComplete(() => { roundWon.SetActive(false); roundWonShowing = false; });
            }
            playing = true;
            StartCoroutine(setupCards());
        }
    }

    IEnumerator setupCards()
    {
        foreach (CardPlacement card in cards)
        {
            GameObject newCard = Instantiate(cardPrefab, startingPos, startingRot);
            card.currentCard = newCard;
            card.allCards.Add(newCard);
            newCard.GetComponent<CardObject>().placeDown(card.gameObject, true, preFlipTime, true, true);
            newCard.GetComponent<CardObject>().placed = true;
            newCard.GetComponent<CardObject>().selected = false;
            newCard.GetComponent<CardObject>().cardSlot = card.gameObject;
            newCard.GetComponent<CardObject>().canMove = true;
            newCard.GetComponent<CardObject>().points = Random.Range(0, newCard.GetComponent<CardObject>().images.Count - 1);
            newCard = null;
            yield return new WaitForSeconds(cardSpawnRate);
        }
    }

    public void countPoints()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            if (hand[i].currentCard == null)
            {
                return;
            }
        }
        for (int i = 0; i < hand.Count; i++)
        {
            CardPlacement card = hand[i];
            if (card.currentCard != null)
            {
                int neighboringCards = checkNextInList(i, hand[i].currentCard.GetComponent<CardObject>().points);
                points += (1 + (neighboringCards - 1)) * neighboringCards;
                i += neighboringCards - 1;
            }
        }
        foreach (CardPlacement handI in hand)
        {
            if (handI.currentCard != null)
            {
                handI.currentCard.GetComponent<CardObject>().delete();
            }
        }
        if (!stackMode)
        {
            foreach (CardPlacement handI in cards)
            {
                if (handI.currentCard != null)
                {
                    handI.currentCard.GetComponent<CardObject>().delete();
                }
            } // take out for stack mode
        }
        totalPoints += points;
        currentPointCounter.text = "Points: " + points.ToString();
        playing = false;
        if (roundTillDeath == 1)
        {
            if (points >= quota)
            {
                roundTillDeath = 5;
                roundWonShowing = true;
                round++;
                quotaText.text = points + "/" + quota;
                quota = Mathf.Ceil(quota + (points / 2));
                points = 0;
                currentPointCounter.text = "Points: " + points.ToString();
                roundWon.GetComponent<CanvasGroup>().alpha = 0;
                roundWon.SetActive(true);
                roundWon.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
            }
            else
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
        else
        {
            roundTillDeath--;
            roundWonShowing = true;
            round++;
            quotaText.text = points + "/" + quota;
            roundWon.GetComponent<CanvasGroup>().alpha = 0;
            roundWon.SetActive(true);
            roundWon.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        }
    }
    public void calculateCurrentSize()
    {
        foreach (CardPlacement handI in cards)
        {
            foreach (GameObject card in handI.allCards)
            {
                card.GetComponent<CardObject>().delete();
            }
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
