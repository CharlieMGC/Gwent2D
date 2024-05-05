using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAssaultOpponent : MonoBehaviour
{
    private Player Owner { get; set; }

    void Start()
    {
        Owner = Gwent.Player2;
        Gwent.Player2.SpecialAssault = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.SpecialAssault, 1));
    }
}
