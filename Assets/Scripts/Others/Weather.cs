using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    private void OnMouseDown()
    {
        Player Owner = Gwent.Player1.IsMyTurn ? Gwent.Player1 : Gwent.Player2;
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Weather, 3));
    }
    void Update()
    {
        Player Owner = Gwent.Player1.IsMyTurn ? Gwent.Player1 : Gwent.Player2.IsMyTurn ? Gwent.Player2 : null;
        if (Owner != null)
        {
            Utility.CheckColliderZone(gameObject, Owner);
        }
    }
}
