using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalAtkOpponent : MonoBehaviour
{
    // Start is called before the first frame update    private int totalAtkLastFrame;
    public GameObject meleeAtk;
    public GameObject rangeAtk;
    public GameObject assaultAtk;
    public int totalAtkLastFrame;

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
        if (meleeAtk.GetComponent<Text>().text != "")
        {
            return int.Parse(meleeAtk.GetComponent<Text>().text) + int.Parse(rangeAtk.GetComponent<Text>().text) + int.Parse(assaultAtk.GetComponent<Text>().text);
        }
        else return 0;
    }
}
