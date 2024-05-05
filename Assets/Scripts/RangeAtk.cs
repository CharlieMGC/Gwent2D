using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeAtk : MonoBehaviour
{
    private int totalAtkLastFrame;
    public GameObject rangeZone; // Referencia al GameObject range Zone

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
        foreach (Transform child in rangeZone.transform) // Buscar en el GameObject range Zone
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
