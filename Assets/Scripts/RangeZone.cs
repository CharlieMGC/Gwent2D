
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeZone : MonoBehaviour
{
    private Player Owner { get; set; }

    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.RangeZone = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        Utility.Invocation(Owner, gameObject, Types.Range);
    }
}
