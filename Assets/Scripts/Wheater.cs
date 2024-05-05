using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheater : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Gwent.Weather = gameObject;
    }
    private void OnMouseDown()
    {
        Player Owner = Gwent.Player1.IsMyTurn ? Gwent.Player1 : Gwent.Player2;
        Utility.Invocation(Owner, gameObject, Types.Weather, 3);
    }
}
