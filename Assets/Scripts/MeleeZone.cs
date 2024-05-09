
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MeleeZone : MonoBehaviour
{

    // Start is called before the first frame update
    private Player Owner { get; set; }
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.MeleeZone = gameObject;

    }

    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Melee, 10, UltimateInvocation.MeleeZone));
    }

}
