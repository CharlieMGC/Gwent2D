using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{

    public int Atk;
    public string Name;
    public List<Types> Type;
    public bool IsPlayer1;
    public Player Owner;

    // Update is called once per frame

}

public enum Types
{
    Melee,
    Range,
    Assault,
    SpecialMelee,
    SpecialRange,
    SpecialAssault,
    Weather,
}