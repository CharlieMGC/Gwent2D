using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "CardBase")]
public class Card : ScriptableObject
{

    public int Atk;
    public string Name;
    public List<Types> Type;
    public Effects Effect;
    public bool IsPlayer1;
    public bool IsHero;
    public Player Owner;
    public string Description;


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
public enum Effects
{
    None,
    SetCardSpecialInOwnRow,
    SetWeather,
    EliminateBiggestAtkCard,
    EliminateLeastAtkCard,
    Draw,
    AtkUpByEqualsNames,
    ClearRowLessUnit,
    BalanceAtkField,
    Decoy,
    Decoy2,
    WeatherOff,
    DestroyCard,
    Invocation,
    PowerUpSanctuary,
    PowerUpEvocation,
    WeatherStonehedge,
    WeatherSmoke,
}
