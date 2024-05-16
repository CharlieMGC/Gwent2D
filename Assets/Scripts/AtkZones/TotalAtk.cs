using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalAtk : MonoBehaviour
{
    public GameObject meleeAtk;
    public GameObject rangeAtk;
    public GameObject assaultAtk;
    public int totalAtkLastFrame;
    public bool IsPlayer1;

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

        if (meleeAtk.GetComponent<Text>().text != null)
        {
            return int.Parse(meleeAtk.GetComponent<Text>().text) + int.Parse(rangeAtk.GetComponent<Text>().text) + int.Parse(assaultAtk.GetComponent<Text>().text) + (IsPlayer1 ? Gwent.Player1.InvocationMonster : Gwent.Player2.InvocationMagic);
        }
        else return 0;
    }
}
