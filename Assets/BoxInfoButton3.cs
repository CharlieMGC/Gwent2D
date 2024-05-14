using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInfoButton3 : MonoBehaviour
{
private void OnMouseDown()
    {
        Gwent.BoxInfo.GetComponent<BoxInfo>().result = 2;
    }
}
