
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    void Update()
    {
        Utility.CheckColliderZone(gameObject, Owner);
    }


    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Range, 10, UltimateInvocation.RangeZone));
    }
}
