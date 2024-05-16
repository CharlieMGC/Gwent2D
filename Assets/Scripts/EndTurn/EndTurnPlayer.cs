using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPlayer : MonoBehaviour
{
    public bool IsPlayer1;
    private Player owner;
    void Start()
    {
        owner = IsPlayer1 ? Gwent.Player1 : Gwent.Player2;
    }

    private void OnMouseDown()
    {
        if (owner.IsMyTurn)
        {
            owner.EndTurn = true;
            Gwent.SwitchTurn();
        }
    }
}
