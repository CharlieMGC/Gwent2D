using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        Player Owner = Gwent.Player1.IsMyTurn ? Gwent.Player1 : Gwent.Player2;
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Weather, 3));
    }
}
