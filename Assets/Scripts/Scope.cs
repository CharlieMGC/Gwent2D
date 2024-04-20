using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scope : MonoBehaviour
{
    public static List<GameObject> CardsInDeck { get; set; }
    public static List<GameObject> CardsInHand { get; set; }
    public static List<GameObject> CardsInMeleeZone { get; set; }
    public static bool BelongToMelee { get; set; }
    public static List<GameObject> CardsInRangeZone { get; set; }
    public static bool BelongToRange { get; set; }
    public static List<GameObject> CardsInAssaultZone { get; set; }
    public static bool BelongToAssault { get; set; }
    public static GameObject ObjectSelect1 { get; set; }
    public static GameObject BoxInfo { get; set; }
    public static GameObject MeleeZone { get; set; }
    public static DisplayBoxText BoxTest { get; set; }
    public static bool BoxInfoShow { get; set; }
    public static bool Descart { get; set; }
    public static int AmountDescart { get; set; }
}