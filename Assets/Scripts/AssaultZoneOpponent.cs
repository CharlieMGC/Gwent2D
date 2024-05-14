
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AssaultZoneOpponent : MonoBehaviour
{
    private Player Owner { get; set; }

    private void Awake()
    {
    }
    void Start()
    {
        Owner = Gwent.Player2;
        Gwent.Player2.AssaultZone = gameObject;

    }
    void Update()
    {
        Utility.CheckColliderZone(gameObject, Owner);
    }


    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Assault, 10, UltimateInvocation.AssaultZone));
    }
}
