using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Utility : MonoBehaviour
{
    public static IEnumerator Invocation(Player Owner, GameObject zone, Types type, int amount = 10, UltimateInvocation ultimateInvocation = UltimateInvocation.Another)
    {
        /* Debug.Log(zone.GetComponentsInChildren<GameObject>()); */

        if (Owner.IsMyTurn && Owner.SelectedCards.Count > 0 && zone.GetComponentsInChildren<DisplayCard>().Count() < amount)
        {
            GameObject cardCurrent = Owner.SelectedCards[Owner.SelectedCards.Count - 1];
            if (cardCurrent != null && cardCurrent.GetComponent<DisplayCard>().card.Type.Contains(type) && Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(cardCurrent.GetComponent<DisplayCard>()))
            {
                Owner.LastInvocation = ultimateInvocation;
                cardCurrent.transform.SetParent(zone.transform, false);
                Owner.SelectedCards.Clear();
                //Gwent.SwitchTurn();
                yield return new WaitForSeconds(0.5f);
                Gwent tempGwent = Gwent.gwent.GetComponent<Gwent>();
                List<Types> typesCard = cardCurrent.GetComponent<DisplayCard>().card.Type;
                if (typesCard.Contains(Types.SpecialMelee) || typesCard.Contains(Types.SpecialRange) || typesCard.Contains(Types.SpecialAssault))
                {
                    Destroy(cardCurrent);
                }
                if (Gwent.zonesSelectedNegate == null || !Gwent.zonesSelectedNegate.Contains(zone) || cardCurrent.GetComponent<DisplayCard>().card.IsHero)
                {
                    yield return tempGwent.StartCoroutine(tempGwent.ActivateEffect(cardCurrent.GetComponent<DisplayCard>()));
                }
                else
                {
                    Gwent.SwitchTurn();
                }
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
        if (item != null)
        {
            foreach (Transform child in item.GetComponent<Transform>())
            {
                Destroy(child.gameObject);
            }
        }
    }

}
