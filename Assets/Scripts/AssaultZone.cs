
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssaultZone : MonoBehaviour
{
    private Player Owner { get; set; }

    private void Awake()
    {
    }
    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.AssaultZone = gameObject;
    }


    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Assault, 10, UltimateInvocation.AssaultZone));
    }


}
