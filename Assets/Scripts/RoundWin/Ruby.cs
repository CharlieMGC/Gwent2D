using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ruby : MonoBehaviour
{
    public Sprite Win;
    public Sprite Lose;
    public bool IsPlayer1;
    public int Round;
    private Player Owner;

    private void Start()
    {
        Owner = IsPlayer1 ? Gwent.Player1 : Gwent.Player2;
    }

    void Update()
    {

        if (Owner.RoundWin > Round)
        {
            gameObject.GetComponent<Image>().sprite = Win;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Lose;
        }
    }

}