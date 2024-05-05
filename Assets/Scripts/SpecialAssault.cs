using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAssault : MonoBehaviour
{
    private Player Owner { get; set; }

    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.SpecialAssault = gameObject;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        StartCoroutine(Utility.Invocation(Owner, gameObject, Types.SpecialAssault, 1));
        
    }
}
