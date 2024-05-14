using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCard : MonoBehaviour
{
    public Sprite genericSprite;
    public Sprite sprite;

    public Card card;
    public GameObject cardView;
    // Start is called before the first frame update

    void Awake()
    {
        card = Instantiate(card);
        card.Owner = card.IsPlayer1 ? Gwent.Player1 : Gwent.Player2;
    }
    void Start()
    {
        cardView = GameObject.Find("Card View");
    }
    void Update()
    {
        if (card.Owner.IsMyTurn || !card.Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(this))
        {
            gameObject.GetComponent<Image>().sprite = sprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = genericSprite;
        }

    }


    private void OnMouseDown()
    {
        if (card.Owner.IsMyTurn)
        {
            if (card.Owner.SelectedCards.Contains(gameObject))
            {
                card.Owner.SelectedCards.Remove(gameObject);
            }
            else
            {
                card.Owner.SelectedCards.Add(gameObject);
            }
        }
        if (!Gwent.Player1.Hand.GetComponentsInChildren<DisplayCard>().Contains(this) && !Gwent.Player2.Hand.GetComponentsInChildren<DisplayCard>().Contains(this))
        {
            if (card.Owner.SelectedCardsBoard.Contains(gameObject))
            {
                Debug.Log("Me quito");
                card.Owner.SelectedCardsBoard.Remove(gameObject);
                card.Owner.SelectedCardsBoard.Remove(gameObject);
            }
            else
            {
                Debug.Log("Me agrego");
                card.Owner.SelectedCardsBoard.Add(gameObject);
                card.Owner.SelectedCardsBoard.Add(gameObject);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (card.Owner.IsMyTurn || !card.Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(this))
        {
            cardView.GetComponent<Image>().sprite = sprite;
            if (card.Atk.ToString() == "0")
            {
                cardView.GetComponent<CardView>().Atk.GetComponent<Text>().text = "";
            }
            else
            {
                cardView.GetComponent<CardView>().Atk.GetComponent<Text>().text = card.Atk.ToString();
            }
            cardView.GetComponent<CardView>().Effect.GetComponent<Text>().text = card.Description;
        }

        if (card.Owner.IsMyTurn && card.Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(this))
        {
            Vector3 originalPosition = transform.position;
            if (card.Owner == Gwent.Player1)
            {
                transform.position = new Vector3(originalPosition.x, originalPosition.y + 2f, originalPosition.z);
            }
            else
            {
                transform.position = new Vector3(originalPosition.x, originalPosition.y - 2f, originalPosition.z);
            }
        }
    }
    private void OnMouseExit()
    {
        cardView.GetComponent<Image>().sprite = cardView.GetComponent<CardView>().Image;
        cardView.GetComponent<CardView>().Atk.GetComponent<Text>().text = "";
        cardView.GetComponent<CardView>().Effect.GetComponent<Text>().text = "";

        if (card.Owner.IsMyTurn && card.Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(this))
        {
            Vector3 originalPosition = transform.position;
            if (card.Owner == Gwent.Player1)
            {
                transform.position = new Vector3(originalPosition.x, originalPosition.y - 2f, originalPosition.z);
            }
            else
            {
                transform.position = new Vector3(originalPosition.x, originalPosition.y + 2f, originalPosition.z);
            }
        }
    }

}
