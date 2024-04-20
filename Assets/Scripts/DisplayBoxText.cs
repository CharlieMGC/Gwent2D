using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayBoxText : MonoBehaviour
{
    // Start is called before the first frame update
    public Text textTitle;
    public Text textButton1;
    public Text textButton2;
    public bool Discart;
    public int NumberDiscart;

    void Start()
    {
        textTitle.text = "Desea descartar cartas?";
        textButton1.text = "Yes";
        textButton2.text = "No";

        Scope.BoxTest = this;

        Scope.BoxInfo = gameObject;
    }
}
