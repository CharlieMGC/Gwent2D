using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPlayer1 : MonoBehaviour
{
    void Start()
    {
        Gwent.Player1.Hand = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
