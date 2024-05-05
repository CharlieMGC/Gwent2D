
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
    // Start is called before the first frame update
    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.AssaultZone = gameObject;
    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        Utility.Invocation(Owner, gameObject, Types.Assault);
    }


}
