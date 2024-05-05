using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPlayer2 : MonoBehaviour
{
    // Start is called before the first frame update
    private Player owner;
    void Start()
    {
        owner = Gwent.Player2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (owner.IsMyTurn)
        {
            Gwent.Player2.EndTurn = true;
            Gwent.SwitchTurn();
        }
    }
}
