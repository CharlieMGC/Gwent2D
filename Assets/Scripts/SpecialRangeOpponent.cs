using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRangeOpponent : MonoBehaviour
{
    private Player Owner { get; set; }

    void Start()
    {
        Owner = Gwent.Player2;
        Gwent.Player2.SpecialRange = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        Utility.Invocation(Owner, gameObject, Types.SpecialRange, 1);
    }
}
