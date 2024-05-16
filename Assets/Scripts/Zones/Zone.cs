
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Zone : MonoBehaviour
{
    public bool IsPlayer1;
    public bool IsSpecial;
    public UltimateInvocation InvocationZone;
    public Types Type;
    private Player Owner { get; set; }

    void Start()
    {
        Owner = IsPlayer1 ? Gwent.Player1 : Gwent.Player2;
        switch (InvocationZone)
        {
            case UltimateInvocation.MeleeZone:
                Owner.MeleeZone = gameObject;
                break;
            case UltimateInvocation.RangeZone:
                Owner.RangeZone = gameObject;
                break;
            case UltimateInvocation.AssaultZone:
                Owner.AssaultZone = gameObject;
                break;
            case UltimateInvocation.SpecialMelee:
                Owner.SpecialMelee = gameObject;
                break;
            case UltimateInvocation.SpecialRange:
                Owner.SpecialRange = gameObject;
                break;
            case UltimateInvocation.SpecialAssault:
                Owner.SpecialAssault = gameObject;
                break;
        }
    }

    void Update()
    {
        Utility.CheckColliderZone(gameObject, Owner);
    }


    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Type, IsSpecial ? 1 : 10, InvocationZone));
    }


}
