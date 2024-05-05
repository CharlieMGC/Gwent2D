using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Utility : MonoBehaviour
{
    public static void Invocation(Player Owner, GameObject zone, Types type, int amount = 10)
    {
        if (Owner.IsMyTurn && Owner.SelectedCards.Count > 0 && zone.GetComponentsInChildren<DisplayCard>().Count() < amount)
        {
            GameObject cardCurrent = Owner.SelectedCards[Owner.SelectedCards.Count - 1];
            if (cardCurrent != null && cardCurrent.GetComponent<DisplayCard>().card.Type.Contains(type) && Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(cardCurrent.GetComponent<DisplayCard>()))
            {
                cardCurrent.transform.SetParent(zone.transform, false);
                Owner.SelectedCards.Clear();
                Gwent.SwitchTurn();
            }
        }

    }

    public static void ResetPlayer(Player Owner)
    {
        ClearChildGameObject(Owner.Hand);
        ClearChildGameObject(Owner.MeleeZone);
        ClearChildGameObject(Owner.RangeZone);
        ClearChildGameObject(Owner.AssaultZone);
        ClearChildGameObject(Owner.SpecialMelee);
        ClearChildGameObject(Owner.SpecialRange);
        ClearChildGameObject(Owner.SpecialAssault);
    }
    public static void ClearChildGameObject(GameObject item)
    {
        foreach (Transform child in item.GetComponent<Transform>())
        {
            Destroy(child.gameObject);
        }
    }
}
