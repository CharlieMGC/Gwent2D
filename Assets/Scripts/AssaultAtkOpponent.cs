using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssaultAtkOpponent : MonoBehaviour
{
    private int totalAtkLastFrame;
    public GameObject assaultZone; // Referencia al GameObject assault Zone

    void Start()
    {
        totalAtkLastFrame = GetTotalAtk();
    }

    void Update()
    {
        int totalAtkThisFrame = GetTotalAtk();
        if (totalAtkThisFrame != totalAtkLastFrame)
        {
            gameObject.GetComponent<Text>().text = totalAtkThisFrame.ToString();
            totalAtkLastFrame = totalAtkThisFrame;
        }
    }

    private int GetTotalAtk()
    {
        int totalAtk = 0;
        foreach (Transform child in assaultZone.transform) // Buscar en el GameObject assault Zone
        {
            DisplayCard displayCard = child.GetComponent<DisplayCard>();
            if (displayCard != null)
            {
                totalAtk += displayCard.card.Atk;
            }
        }
        return totalAtk;
    }
}
