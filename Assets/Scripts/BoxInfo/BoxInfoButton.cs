using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class BoxInfoButton : MonoBehaviour
{
    public int ChangeResult;
    private void OnMouseDown()
    {
        Gwent.BoxInfo.GetComponent<BoxInfo>().result = ChangeResult;
    }
}
