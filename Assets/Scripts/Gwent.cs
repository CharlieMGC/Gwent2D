

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Gwent : MonoBehaviour
{
    public static GameObject zoneSelected;
    public static GameObject gwent;
    public static GameObject Weather;
    private static int roundWinPlayer1;
    private static int roundWinPlayer2;
    public static GameObject BoxInfo;
    public static Player Player1 { get; set; }
    private static Text totalAtkPlayer1;
    public List<GameObject> CardInDeckPlayer1;
    public static Player Player2 { get; set; }
    private static Text totalAtkPlayer2;
    public List<GameObject> CardInDeckPlayer2;
    public List<GameObject> ResetGameObjects;
    public static Player SelectPlayer;

    private void Awake()
    {
        Player1 = new Player();
        Player2 = new Player();
        Player1.IsMyTurn = true;
        Player2.IsMyTurn = false;
    }

    private void Start()
    {
        gwent = gameObject;
        Weather = GameObject.Find("Weather");
        InitializeGame(Player1, CardInDeckPlayer1);
        InitializeGame(Player2, CardInDeckPlayer2);
        totalAtkPlayer1 = GameObject.Find("Total Text").GetComponent<Text>();
        totalAtkPlayer2 = GameObject.Find("Total Oponent Text").GetComponent<Text>();

        StartCoroutine(GameFlow());
    }

    private void InitializeGame(Player player, List<GameObject> cards)
    {
        foreach (GameObject card in cards)
        {
            player.Deck.Add(card);
        }
    }

    private IEnumerator DrawCards(Player player, List<GameObject> cards, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DrawCard(player, cards);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void DrawCard(Player player, List<GameObject> cards)
    {
        if (cards.Count > 0)
        {
            int cardIndex = UnityEngine.Random.Range(0, cards.Count);
            GameObject drawCard = Instantiate(cards[cardIndex], Vector3.zero, Quaternion.identity);
            drawCard.transform.SetParent(player.Hand.transform, false);
            cards.RemoveAt(cardIndex);
        }

    }

    private IEnumerator GameFlow()
    {

        yield return StartCoroutine(DrawCards(Player1, CardInDeckPlayer1, 10));
        yield return StartCoroutine(DrawCards(Player2, CardInDeckPlayer2, 10));

        yield return StartCoroutine(ShowDialogAndReturnCards(Player1, "Player1", CardInDeckPlayer1));
        Player1.IsMyTurn = false;
        Player2.IsMyTurn = true;
        yield return StartCoroutine(ShowDialogAndReturnCards(Player2, "Player2", CardInDeckPlayer2));
        Player1.IsMyTurn = true;
        Player2.IsMyTurn = false;


    }

    private IEnumerator ShowDialogAndReturnCards(Player player, string playerName, List<GameObject> cards)
    {
        BoxInfo boxInfo = BoxInfo.GetComponent<BoxInfo>();

        boxInfo.Show($"Desea enviar cartas al deck {playerName}", "No", "1 carta", "2 cartas");
        yield return new WaitUntil(() => boxInfo.GetResult() != -1);
        int numCardsToReturn = boxInfo.GetResult();

        yield return StartCoroutine(ReturnCardsToDeck(player, numCardsToReturn));
        yield return StartCoroutine(DrawCards(player, cards, numCardsToReturn));

        boxInfo.Hide();
    }

    private IEnumerator ReturnCardsToDeck(Player player, int numCardsToReturn)
    {

        player.SelectedCards.Clear();
        yield return new WaitUntil(() => player.SelectedCards.Count == numCardsToReturn);
        foreach (var item in player.SelectedCards)
        {
            player.Deck.Add(item);
            Destroy(item);
        }
    }

    public static void SwitchTurn()
    {
        if (Player1.EndTurn & Player2.EndTurn)
        {
            if (int.Parse(totalAtkPlayer1.GetComponent<Text>().text) > int.Parse(totalAtkPlayer2.GetComponent<Text>().text))
            {
                Debug.Log("Gana Player 1");
                Player1.RoundWin++;
            }
            else if (int.Parse(totalAtkPlayer1.GetComponent<Text>().text) < int.Parse(totalAtkPlayer2.GetComponent<Text>().text))
            {
                Debug.Log("Gana Player 2");
                Player2.RoundWin++;
            }
            else
            {
                Debug.Log("Empate");
                Player1.RoundWin++;
                Player2.RoundWin++;
            }
            Player1.IsMyTurn = false;
            Player2.IsMyTurn = false;

            gwent.GetComponent<Gwent>().ResetRound();
        }
        else if (Player1.EndTurn)
        {
            Player1.IsMyTurn = false;
            Player2.IsMyTurn = true;
        }
        else if (Player2.EndTurn)
        {
            Player2.IsMyTurn = false;
            Player1.IsMyTurn = true;
        }
        else
        {
            Player1.IsMyTurn = !Player1.IsMyTurn;
            Player2.IsMyTurn = !Player2.IsMyTurn;
        }
    }

    private static void SaveRoundWin()
    {
        roundWinPlayer1 = Player1.RoundWin;
        roundWinPlayer2 = Player2.RoundWin;
    }
    public void ResetRound()
    {
        Utility.ResetPlayer(Player1);
        Utility.ResetPlayer(Player2);
        Utility.ClearChildGameObject(Weather);

        SaveRoundWin();
        Player1.IsMyTurn = true;
        Player2.IsMyTurn = false;
        Start();

        Player1.RoundWin = roundWinPlayer1;
        Player2.RoundWin = roundWinPlayer2;

        Player1.EndTurn = false;
        Player2.EndTurn = false;
    }

    public IEnumerator ActivateEffect(DisplayCard displayCard)
    {
        Player Owner = displayCard.card.Owner;
        BoxInfo boxInfo = BoxInfo.GetComponent<BoxInfo>();
        bool EndTurn = true;

        switch (displayCard.card.Effect)
        {
            case Effects.None:
                break;
            case Effects.SetCardSpecialInOwnRow:
                yield return SetCardSpecialInOwnRowEffect(Owner);
                EndTurn = false;
                break;
            case Effects.SetWeather:
                yield return StartCoroutine(SetWeatherEffect(Owner));
                EndTurn = false;
                break;
            case Effects.EliminateBiggestAtkCard:
                yield return DestroyMonsterWithHigherOrLessAtk(true);
                break;
            case Effects.EliminateLeastAtkCard:
                yield return DestroyMonsterWithHigherOrLessAtk(false);
                break;
            case Effects.Draw:
                DrawEffect(Owner);
                break;
            case Effects.AtkUpByEqualsNames:
                Debug.Log("test");
                break;
            case Effects.ClearRowLessUnit:
                Debug.Log("test");
                break;
            case Effects.BalanceAtkField:
                yield return StartCoroutine(BalanceEffect(boxInfo));
                break;
            case Effects.Decoy:
                Debug.Log("test");
                break;
            case Effects.WeatherOff:
                Debug.Log("test");
                break;
            case Effects.DestroyCard:
                Debug.Log("test");
                break;
            case Effects.Invocation:
                Debug.Log("test");
                break;
            case Effects.InvocationTwo:
                Debug.Log("test");
                break;
            case Effects.PowerUpSanctuary:
                Debug.Log("test");
                break;
            case Effects.PowerUpEvocation:
                Debug.Log("test");
                break;
            case Effects.WeatherRainbow:
                Debug.Log("test");
                break;
            case Effects.WeatherStonehedge:
                Debug.Log("test");
                break;
            case Effects.WeatherSmoke:
                Debug.Log("test");
                break;
            case Effects.WeatherEruption:
                Debug.Log("test");
                break;
            case Effects.IncreaseAtkDraco:
                Debug.Log("test");
                break;
            case Effects.ActivateTwoSpecial:
                Debug.Log("test");
                break;
        }
        if (EndTurn)
            Gwent.SwitchTurn();
    }
    private IEnumerator SelectPlayerBox(BoxInfo boxInfo)
    {

        boxInfo.Show("Seleccione el Player", "Player 1", "Player2 ");
        yield return new WaitUntil(() => boxInfo.GetResult() != -1);
        int tempSelect = boxInfo.GetResult();
        SelectPlayer = tempSelect == 0 ? Player1 : Player2;
        boxInfo.Hide();
        boxInfo.result = -1;
    }
    private void DrawEffect(Player Owner)
    {
        if (Owner == Player1)
        {
            DrawCard(Player1, CardInDeckPlayer1);
        }
        else
        {
            DrawCard(Player2, CardInDeckPlayer2);
        }
    }
    private IEnumerator BalanceEffect(BoxInfo boxInfo)
    {
        SelectPlayer = null;
        yield return StartCoroutine(SelectPlayerBox(boxInfo));
        int totalAtk = int.Parse(SelectPlayer == Player1 ? totalAtkPlayer1.text : totalAtkPlayer2.text);
        Debug.Log(SelectPlayer);
        if (totalAtk != 0)
        {

            int cardCount = SelectPlayer.MeleeZone.GetComponentsInChildren<DisplayCard>().Count() + SelectPlayer.RangeZone.GetComponentsInChildren<DisplayCard>().Count() + SelectPlayer.AssaultZone.GetComponentsInChildren<DisplayCard>().Count();
            int average = (int)Math.Round((double)totalAtk / cardCount);

            // Asegurarse de que el promedio sea un número entero positivo
            average = Math.Abs(average);
            Debug.Log("Wiii eso " + average);
            SetAtkInRow(SelectPlayer.MeleeZone, average);
            SetAtkInRow(SelectPlayer.RangeZone, average);
            SetAtkInRow(SelectPlayer.AssaultZone, average);
        }
    }
    private void SetAtkInRow(GameObject zone, int atk)
    {
        foreach (var item in zone.GetComponentsInChildren<DisplayCard>())
        {
            item.card.Atk = atk;
        }
    }
    private IEnumerator SetWeatherEffect(Player Owner)
    {
        bool ContainWeather = false;
        foreach (var item in Owner.Hand.GetComponentsInChildren<DisplayCard>())
        {
            if (item.card.Type.Contains(Types.Weather))
            {
                ContainWeather = true;
                break;
            }
        }
        if (ContainWeather)
        {
            Owner.SelectedCards.Clear();
            Debug.Log(Owner.Hand.GetComponentsInChildren<DisplayCard>().Count());
            yield return new WaitUntil(() => Owner.SelectedCards.Count() > 0 && Owner.SelectedCards[Owner.SelectedCards.Count() - 1].GetComponent<DisplayCard>().card.Type.Contains(Types.Weather));
            StartCoroutine(Utility.Invocation(Owner, Weather, Types.Weather, 3));
        }
        else SwitchTurn();

    }
    private IEnumerator SetCardSpecialInOwnRowEffect(Player Owner)
    {
        bool ContainSpecial = false;
        foreach (var item in Owner.Hand.GetComponentsInChildren<DisplayCard>())
        {
            if (item.card.Type.Contains(Types.SpecialMelee) || item.card.Type.Contains(Types.SpecialRange) || item.card.Type.Contains(Types.SpecialAssault))
            {
                ContainSpecial = true;
                break;
            }
        }
        if (ContainSpecial)
        {
            Owner.SelectedCards.Clear();
            switch (Owner.LastInvocation)
            {
                case UltimateInvocation.MeleeZone:
                    yield return new WaitUntil(() => Owner.SelectedCards.Count() > 0 && Owner.SelectedCards[Owner.SelectedCards.Count() - 1].GetComponent<DisplayCard>().card.Type.Contains(Types.SpecialMelee));
                    zoneSelected = Player1.IsMyTurn ? GameObject.Find("Special Melee") : GameObject.Find("Special Melee 2");
                    StartCoroutine(Utility.Invocation(Owner, zoneSelected, Types.SpecialMelee, 1));
                    break;
                case UltimateInvocation.RangeZone:
                    yield return new WaitUntil(() => Owner.SelectedCards.Count() > 0 && Owner.SelectedCards[Owner.SelectedCards.Count() - 1].GetComponent<DisplayCard>().card.Type.Contains(Types.SpecialRange));
                    zoneSelected = Player1.IsMyTurn ? GameObject.Find("Special Range") : GameObject.Find("Special Range 2");
                    StartCoroutine(Utility.Invocation(Owner, zoneSelected, Types.SpecialRange, 1));
                    break;
                case UltimateInvocation.AssaultZone:
                    yield return new WaitUntil(() => Owner.SelectedCards.Count() > 0 && Owner.SelectedCards[Owner.SelectedCards.Count() - 1].GetComponent<DisplayCard>().card.Type.Contains(Types.SpecialAssault));
                    zoneSelected = Player1.IsMyTurn ? GameObject.Find("Special Assault") : GameObject.Find("Special Assault 2");
                    StartCoroutine(Utility.Invocation(Owner, zoneSelected, Types.SpecialAssault, 1));
                    break;
                case UltimateInvocation.SpecialOrClima:
                    break;
            }
        }
        else SwitchTurn();

    }

    private IEnumerator DestroyMonsterWithHigherOrLessAtk(bool bigger)
    {
        List<GameObject> listOfMonster = new List<GameObject>();
        int extremeAtk = bigger ? int.MinValue : int.MaxValue;

        foreach (var zone in new GameObject[] { Player1.MeleeZone, Player1.RangeZone, Player1.AssaultZone, Player2.MeleeZone, Player2.RangeZone, Player2.AssaultZone })
        {
            foreach (Transform child in zone.transform)
            {
                listOfMonster.Add(child.gameObject);
            }
        }

        List<GameObject> extremeAtkMonsters = new List<GameObject>();
        foreach (var item in listOfMonster)
        {
            int itemAtk = item.GetComponent<DisplayCard>().card.Atk;
            if (bigger ? itemAtk > extremeAtk : itemAtk < extremeAtk)
            {
                extremeAtk = itemAtk;
                extremeAtkMonsters.Clear();
                extremeAtkMonsters.Add(item);
            }
            else if (itemAtk == extremeAtk)
            {
                extremeAtkMonsters.Add(item);
            }
        }

        if (extremeAtkMonsters.Count > 1)
        {
            foreach (var item in extremeAtkMonsters)
            {
                Destroy(item);
            }
        }
        else
        {
            Destroy(extremeAtkMonsters[0]);
        }
        yield return new WaitForSeconds(0.5f);
    }

}