 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button1Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        switch (Scope.BoxTest.textTitle.text)
        {
            case "Desea descartar cartas?":
                Scope.BoxTest.textTitle.text = "Cantidad a descartar";
                Scope.BoxTest.textButton1.text = "1";
                Scope.BoxTest.textButton2.text = "2";
                break;
            case ("Cantidad a descartar"):
                    Scope.Descart = true;
                    Scope.AmountDescart = 1;
                break;
            default:
                break;
        }

    }
}
 