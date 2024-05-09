using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMelee : MonoBehaviour
{
    private Player Owner { get; set; }

    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.SpecialMelee = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.SpecialMelee, 1, UltimateInvocation.SpecialMelee));
    }
}
