using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMeleeOpponent : MonoBehaviour
{
    private Player Owner { get; set; }

    void Start()
    {
        Owner = Gwent.Player2;
        Gwent.Player2.SpecialMelee = gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Utility.CheckColliderZone(gameObject, Owner);
    }
    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.SpecialMelee, 1, UltimateInvocation.SpecialMelee));
    }
}
