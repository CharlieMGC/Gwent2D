using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2Script : MonoBehaviour
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
                Scope.BoxInfo.SetActive(false);
                Scope.BoxInfoShow = false;
                break;
            case "Cantidad a descartar":
                Scope.Descart = true;
                Scope.AmountDescart = 2;
                break;
        }

    }
}
