
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    void Update()
    {
        Utility.CheckColliderZone(gameObject,Owner);
    }

    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.Range, 10, UltimateInvocation.RangeZone));

    }
}
