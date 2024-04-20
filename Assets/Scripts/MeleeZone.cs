using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeZone : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Scope.CardsInMeleeZone = new List<GameObject>();
        Scope.MeleeZone = gameObject;
    }

    private void Update()
    {
        Scope.MeleeZone.GetComponent<Collider2D>().enabled = Scope.BoxInfoShow ? false : true;
    }
    private void OnMouseDown()
    {
        if (Scope.ObjectSelect1 != null && Scope.CardsInMeleeZone.Count < 10)
        {
            GameObject cardMelee = Scope.ObjectSelect1;
            cardMelee.transform.SetParent(Scope.MeleeZone.transform, false);
            Scope.CardsInMeleeZone.Add(cardMelee);
            Scope.CardsInHand.Remove(cardMelee);
        }

    }
}

/* cardMelee.transform.SetParent(MeleeZoneVar.transform, false); */ 