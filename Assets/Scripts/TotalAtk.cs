using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalAtk : MonoBehaviour
{
    // Start is called before the first frame update    private int totalAtkLastFrame;
    public GameObject meleeAtk; // Referencia al GameObject Melee Zone
    public GameObject rangeAtk; // Referencia al GameObject Melee Zone
    public GameObject assaultAtk; // Referencia al GameObject Melee Zone
    public int totalAtkLastFrame; // Referencia al GameObject Melee Zone

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
            return int.Parse(meleeAtk.GetComponent<Text>().text) + int.Parse(rangeAtk.GetComponent<Text>().text) + int.Parse(assaultAtk.GetComponent<Text>().text);
        }
        else return 0;
    }
}
