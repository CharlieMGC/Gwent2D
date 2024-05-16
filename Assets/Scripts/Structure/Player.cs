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
    public List<GameObject> SelectedCardsBoard { get; set; } = new List<GameObject> { };
    public UltimateInvocation LastInvocation { get; set; }
    public bool SelectdMonster { get; set; }
    public Dictionary<string, int[]> AtkUpByEqualsNames { get; set; } = new Dictionary<string, int[]>();
    public int InvocationMagic { get; set; } = 0;
    public int InvocationMonster { get; set; } = 0;
}

public enum UltimateInvocation
{
    MeleeZone,
    RangeZone,
    AssaultZone,
    SpecialMelee,
    SpecialRange,
    SpecialAssault,
    Weather,
    Another
}