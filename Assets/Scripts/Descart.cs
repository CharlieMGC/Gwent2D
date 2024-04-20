using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descart : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (Scope.Descart && Scope.AmountDescart > 0)
        {
            Scope.AmountDescart--;
            Scope.CardsInHand.Remove(Scope.ObjectSelect1);
            Destroy(Scope.ObjectSelect1);
        }
        if (Scope.AmountDescart == 0)
        {
            Scope.BoxInfo.SetActive(false);
            Scope.BoxInfoShow = false;
            Scope.Descart = false;
        }
    }
}
