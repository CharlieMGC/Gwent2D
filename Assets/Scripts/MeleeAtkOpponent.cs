using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeAtkOpponent : MonoBehaviour
{
    // Start is called before the first frame update
    private int totalAtkLastFrame;
    public GameObject meleeZone; 
    void Start()
    {
        Gwent.Weather = gameObject;
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
        foreach (Transform child in meleeZone.transform) 
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
