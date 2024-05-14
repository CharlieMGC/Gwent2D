using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class BoxInfoButton1 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Gwent.BoxInfo.GetComponent<BoxInfo>().result = 0;
    }
}
