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
    // Start is called before the first frame update

    void Awake()
    {
        card.Owner = card.IsPlayer1 ? Gwent.Player1 : Gwent.Player2;
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

    }
}
