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
                if (ultimateInvocation == UltimateInvocation.MeleeZone || ultimateInvocation == UltimateInvocation.RangeZone || ultimateInvocation == UltimateInvocation.AssaultZone)
                {
                    Owner.InvocationMonster++;
                }
                if (ultimateInvocation == UltimateInvocation.SpecialMelee || ultimateInvocation == UltimateInvocation.SpecialAssault || ultimateInvocation == UltimateInvocation.SpecialRange)
                {
                    Owner.InvocationMagic++;
                }


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
                Gwent.Player1.SelectdMonster = false;
                Gwent.Player2.SelectdMonster = false;
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
        Owner.InvocationMagic = 0;
        Owner.InvocationMonster = 0;
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

    public static void CheckColliderZone(GameObject gameObject, Player Owner)
    {
        try
        {
            if (Gwent.BoxInfo.GetComponent<BoxInfo>().Active)
                gameObject.GetComponent<Collider2D>().enabled = false;
            else if (gameObject.GetComponentsInChildren<DisplayCard>() != null && gameObject.GetComponentsInChildren<DisplayCard>().Count() > 0)
            {
                if (!Gwent.InitialDraw && Owner.SelectedCards != null && Owner.SelectedCards.Count > 0 && Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(Owner.SelectedCards.Last().GetComponent<DisplayCard>()))
                {
                    gameObject.GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    gameObject.GetComponent<Collider2D>().enabled = false;
                }
            }
            else
                gameObject.GetComponent<Collider2D>().enabled = true;
        }
        catch
        {
        }

    }

}
