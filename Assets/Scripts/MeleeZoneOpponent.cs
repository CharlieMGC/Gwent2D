
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeZoneOpponent : MonoBehaviour
{
    private Player Owner { get; set; }

    private void Awake()
    {
    }
    void Start()
    {
        Owner = Gwent.Player2;
        Gwent.Player2.MeleeZone = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Melee, 10, UltimateInvocation.MeleeZone));
    }
}
