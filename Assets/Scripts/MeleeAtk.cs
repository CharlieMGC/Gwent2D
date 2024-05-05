using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAtk : MonoBehaviour
{
    private int totalAtkLastFrame;
    public GameObject meleeZone; // Referencia al GameObject Melee Zone

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
        foreach (Transform child in meleeZone.transform) // Buscar en el GameObject Melee Zone
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