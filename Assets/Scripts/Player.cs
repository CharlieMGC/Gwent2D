using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public GameObject Hand { get; set; }
    public GameObject MeleeZone { get; set; }
    public GameObject RangeZone { get; set; }
    public GameObject AssaultZone { get; set; }
    public GameObject SpecialMelee { get; set; }
    public GameObject SpecialRange { get; set; }
    public GameObject SpecialAssault { get; set; }
    public List<GameObject> Deck { get; set; } = new List<GameObject>();
    public int RoundWin { get; set; }
    public bool IsMyTurn { get; set; }
    public bool EndTurn { get; set; }
    public GameObject CardLeader { get; set; }
    public List<GameObject> SelectedCards { get; set; } = new List<GameObject> { };

}