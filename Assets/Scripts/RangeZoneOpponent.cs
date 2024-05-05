
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangeZoneOpponent : MonoBehaviour
{
    // Start is called before the first frame update
    private Player Owner { get; set; }

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Owner = Gwent.Player2;
        Gwent.Player2.RangeZone = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        Utility.Invocation(Owner, gameObject, Types.Range);
    }
}
