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
    private GameObject[] zonesSelect;
    public static bool InitialDraw;
    public static bool UltimateVictoryPlayer2;
    public static List<GameObject> zonesSelectedNegate = new();
    public static List<GameObject> zonesSelectedInmune = new();
    public static GameObject gwent;
    public static GameObject Weather;
    private static int roundWinPlayer1;
    private static int roundWinPlayer2;
    public static GameObject BoxInfo;
    public static Player Player1 { get; set; }
    private static Text totalAtkPlayer1;
    public List<GameObject> CardInDeckPlayer1;
    public GameObject DecoyCard;
    public static Player Player2 { get; set; }
    private static Text totalAtkPlayer2;
    public List<GameObject> CardInDeckPlayer2;
    public static Player SelectPlayer;
    public GameObject DecoyCard2;

    private void Awake()
    {
        Player1 = new Player();
        Player2 = new Player();
        Player1.IsMyTurn = true;
        Player2.IsMyTurn = false;
        Player1.Hand = GameObject.Find("Hand");
        Player2.Hand = GameObject.Find("Oponent Hand");
    }
    private void Update()
    {
        Player Owner = Player1.IsMyTurn ? Player1 : Player2.IsMyTurn ? Player2 : null;
        if (Owner != null)
        {
            if (Owner.SelectdMonster && Owner.SelectedCards.Count() > 0)
            {
                var lastCardType = Owner.SelectedCards.Last().GetComponent<DisplayCard>().card;
                if (!lastCardType.Type.Contains(Types.Melee) && !lastCardType.Type.Contains(Types.Range) && !lastCardType.Type.Contains(Types.Assault))
                {
                    Owner.SelectedCards.Clear();
                }
            }
        }
        AtkUpByEqualsNamesEffect(Player1);
        AtkUpByEqualsNamesEffect(Player2);
        if (InitialDraw)
        {
            Player1.SelectedCards.Clear();
            Player2.SelectedCards.Clear();
        }
    }
    private void Start()
    {
        InitialDraw = true;
        gwent = gameObject;
        Weather = GameObject.Find("Weather");
        zonesSelect = new GameObject[2];
        InitializeGame(Player1, CardInDeckPlayer1);
        InitializeGame(Player2, CardInDeckPlayer2);
        totalAtkPlayer1 = GameObject.Find("Total Text").GetComponent<Text>();
        totalAtkPlayer2 = GameObject.Find("Total Oponent Text").GetComponent<Text>();

        StartCoroutine(GameFlow());
    }

    private void InitializeGame(Player player, List<GameObject> cards)
    {
        player.Deck = new(cards);
    }

    private IEnumerator DrawCards(Player player, List<GameObject> cards, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            DrawCard(player, cards);
            yield return new WaitForSeconds(0.10f);
        }
    }

    public void DrawCard(Player player, List<GameObject> cards)
    {
        if (cards.Count > 0 && player.Hand.GetComponentsInChildren<DisplayCard>().Count() < 10)
        {
            int cardIndex = UnityEngine.Random.Range(0, cards.Count);
            if (cards[cardIndex] != null)
            {
                GameObject drawCard = Instantiate(cards[cardIndex], Vector3.zero, Quaternion.identity);
                drawCard.transform.SetParent(player.Hand.transform, false);
                cards.RemoveAt(cardIndex);
            }
        }
    }

    private IEnumerator GameFlow()
    {
        if (!UltimateVictoryPlayer2)
        {
            yield return StartCoroutine(DrawCards(Player1, Player1.Deck, 10));
            yield return StartCoroutine(DrawCards(Player2, Player2.Deck, 10));

            yield return StartCoroutine(ShowDialogAndReturnCards(Player1, "Player1", Player1.Deck));
            Player1.IsMyTurn = false;
            Player2.IsMyTurn = true;
            yield return StartCoroutine(ShowDialogAndReturnCards(Player2, "Player2", Player2.Deck));
            Player1.IsMyTurn = true;
            Player2.IsMyTurn = false;
        }
        else
        {
            Player1.IsMyTurn = false;
            Player2.IsMyTurn = true;
            yield return StartCoroutine(DrawCards(Player2, Player2.Deck, 10));
            yield return StartCoroutine(DrawCards(Player1, Player1.Deck, 10));

            yield return StartCoroutine(ShowDialogAndReturnCards(Player2, "Player2", Player2.Deck));
            Player1.IsMyTurn = true;
            Player2.IsMyTurn = false;
            yield return StartCoroutine(ShowDialogAndReturnCards(Player1, "Player1", Player1.Deck));
            Player1.IsMyTurn = false;
            Player2.IsMyTurn = true;
        }

    }

    private IEnumerator ShowDialogAndReturnCards(Player player, string playerName, List<GameObject> cards)
    {
        InitialDraw = false;
        BoxInfo boxInfo = BoxInfo.GetComponent<BoxInfo>();

        boxInfo.Show($"Desea enviar cartas al deck {playerName}", "No", "1 carta", "2 cartas");

        yield return new WaitUntil(() => boxInfo.GetResult() != -1);
        boxInfo.Hide();
        int numCardsToReturn = boxInfo.GetResult();

        yield return StartCoroutine(ReturnCardsToDeck(player, numCardsToReturn));
        yield return StartCoroutine(DrawCards(player, cards, numCardsToReturn));
    }

    private IEnumerator ReturnCardsToDeck(Player player, int numCardsToReturn)
    {
        player.SelectedCards.Clear();
        yield return new WaitUntil(() => player.SelectedCards.Count == numCardsToReturn);
        List<GameObject> cardsToDestroy = new();

        foreach (var item in player.SelectedCards)
        {
            player.Deck.Add(item);
            cardsToDestroy.Add(item);
        }

        foreach (var item in cardsToDestroy)
        {
            Destroy(item);
        }
        yield return new WaitForSeconds(0.5f);
    }


    public static void SwitchTurn()
    {
        BoxInfo boxInfo = BoxInfo.GetComponent<BoxInfo>();
        boxInfo.Hide();
        if (Player1.EndTurn & Player2.EndTurn)
        {
            if (int.Parse(totalAtkPlayer1.GetComponent<Text>().text) > int.Parse(totalAtkPlayer2.GetComponent<Text>().text))
            {
                Player1.RoundWin++;
                if (Player1.RoundWin == 2)
                {
                    boxInfo.Show("Player 1 ha ganado el juego");
                }
                else
                {
                    boxInfo.Show("Player 1 ha ganado la ronda");
                }
                UltimateVictoryPlayer2 = false;
            }
            else if (int.Parse(totalAtkPlayer1.GetComponent<Text>().text) < int.Parse(totalAtkPlayer2.GetComponent<Text>().text))
            {
                Player2.RoundWin++;
                if (Player2.RoundWin == 2)
                {
                    boxInfo.Show("Player 2 ha ganado el juego");

                }
                else
                {
                    boxInfo.Show("Player 2 ha ganado la ronda");
                }
                UltimateVictoryPlayer2 = true;
            }
            else
            {
                Player1.RoundWin++;
                Player2.RoundWin++;
                if (Player1.RoundWin > Player2.RoundWin)
                {
                    boxInfo.Show("Player 1 ha ganado el juego");
                    Player1.IsMyTurn = false;
                    Player2.IsMyTurn = false;
                }
                else if (Player1.RoundWin < Player2.RoundWin)
                {
                    boxInfo.Show("Player 2 ha ganado el juego");
                    Player1.IsMyTurn = false;
                    Player2.IsMyTurn = false;
                }
                else
                {
                    boxInfo.Show(" Empate ");
                    UltimateVictoryPlayer2 = !UltimateVictoryPlayer2;
                }
            }
            Player1.IsMyTurn = false;
            Player2.IsMyTurn = false;
            if (Player1.RoundWin < 2 & Player2.RoundWin < 2)
            {
                gwent.GetComponent<Gwent>().StartCoroutine(gwent.GetComponent<Gwent>().ResetRound());
            }
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
    public IEnumerator ResetRound()
    {
        Utility.ResetPlayer(Player1);
        Utility.ResetPlayer(Player2);
        Utility.ClearChildGameObject(Weather);
        zonesSelectedNegate.Clear();
        zonesSelectedInmune.Clear();

        SaveRoundWin();
        Player1.IsMyTurn = true;
        Player2.IsMyTurn = false;

        yield return new WaitForSeconds(1.5f);
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
            case Effects.ClearRowLessUnit:
                ClearRowLessUnitEffect(Owner);
                break;
            case Effects.BalanceAtkField:
                yield return StartCoroutine(BalanceEffect(boxInfo));
                break;
            case Effects.Decoy:
                yield return StartCoroutine(DecoyEffect(Owner, DecoyCard));
                EndTurn = false;
                break;
            case Effects.Decoy2:
                yield return StartCoroutine(DecoyEffect(Owner, DecoyCard2));
                EndTurn = false;
                break;
            case Effects.WeatherOff:
                WeatherOffEffect();
                break;
            case Effects.DestroyCard:
                yield return StartCoroutine(DestroyEffect(Owner));
                EndTurn = false;
                break;
            case Effects.Invocation:
                InvocationEffect(Owner);
                EndTurn = false;
                break;
            case Effects.PowerUpSanctuary:
                PowerUpSanctuaryEffect(Owner, 2);
                break;
            case Effects.PowerUpEvocation:
                PowerUpSanctuaryEffect(Owner, 4);
                break;
            case Effects.WeatherStonehedge:
                yield return StartCoroutine(ZoneSelectedInmune(boxInfo));
                break;
            case Effects.WeatherSmoke:
                yield return StartCoroutine(ZoneSelectedNegateEffect(boxInfo));
                break;
        }
        if (EndTurn)
            SwitchTurn();
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
    private IEnumerator SelectZoneBox(BoxInfo boxInfo)
    {
        boxInfo.Show("Seleccione la zona", "Melee", "Range", "Assault");
        yield return new WaitUntil(() => boxInfo.GetResult() != -1);
        int tempSelect = boxInfo.GetResult();
        switch (tempSelect)
        {
            case 0:
                zonesSelect[0] = Player1.MeleeZone;
                zonesSelect[1] = Player2.MeleeZone;
                break;
            case 1:
                zonesSelect[0] = Player1.RangeZone;
                zonesSelect[1] = Player2.RangeZone;
                break;
            case 2:
                zonesSelect[0] = Player1.AssaultZone;
                zonesSelect[1] = Player2.AssaultZone;
                break;
        }
        boxInfo.Hide();
        boxInfo.result = -1;

    }
    private void DrawEffect(Player Owner)
    {
        if (Owner == Player1)
        {
            DrawCard(Player1, Owner.Deck);
        }
        else
        {
            DrawCard(Player2, Owner.Deck);
        }
    }
    private IEnumerator BalanceEffect(BoxInfo boxInfo)
    {
        SelectPlayer = null;
        yield return StartCoroutine(SelectPlayerBox(boxInfo));
        int totalAtk = int.Parse(SelectPlayer == Player1 ? totalAtkPlayer1.text : totalAtkPlayer2.text);
        if (totalAtk != 0)
        {

            int cardCount = SelectPlayer.MeleeZone.GetComponentsInChildren<DisplayCard>().Count() + SelectPlayer.RangeZone.GetComponentsInChildren<DisplayCard>().Count() + SelectPlayer.AssaultZone.GetComponentsInChildren<DisplayCard>().Count();
            if (cardCount > 0)
            {
                int average = (int)Math.Round((double)totalAtk / cardCount);

                average = Math.Abs(average);
                SetAtkInRow(SelectPlayer.MeleeZone, average);
                SetAtkInRow(SelectPlayer.RangeZone, average);
                SetAtkInRow(SelectPlayer.AssaultZone, average);
            }
        }
    }
    private void SetAtkInRow(GameObject zone, int atk)
    {
        foreach (var item in zone.GetComponentsInChildren<DisplayCard>())
        {
            if (!item.card.IsHero && !IsInmune(item))
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
            yield return new WaitUntil(() => Owner.SelectedCards.Count() > 0 && Owner.SelectedCards[Owner.SelectedCards.Count() - 1].GetComponent<DisplayCard>().card.Type.Contains(Types.Weather));
            StartCoroutine(Utility.Invocation(Owner, Weather, Types.Weather, 3));
        }
        else SwitchTurn();

    }
    private IEnumerator SetCardSpecialInOwnRowEffect(Player Owner)
    {
        bool ContainSpecial = Owner.Hand.GetComponentsInChildren<DisplayCard>().Any(item => item.card.Type.Contains(Types.SpecialMelee) || item.card.Type.Contains(Types.SpecialRange) || item.card.Type.Contains(Types.SpecialAssault));

        if (ContainSpecial)
        {
            Dictionary<UltimateInvocation, (string zoneName, Types type)> cardTypeToZoneMap = new Dictionary<UltimateInvocation, (string, Types)>
        {
            { UltimateInvocation.MeleeZone, (Player1.IsMyTurn ? "Special Melee" : "Special Melee 2", Types.SpecialMelee) },
            { UltimateInvocation.RangeZone, (Player1.IsMyTurn ? "Special Range" : "Special Range 2", Types.SpecialRange) },
            { UltimateInvocation.AssaultZone, (Player1.IsMyTurn ? "Special Assault" : "Special Assault 2", Types.SpecialAssault) }
        };

            if (cardTypeToZoneMap.ContainsKey(Owner.LastInvocation))
            {
                var (zoneName, type) = cardTypeToZoneMap[Owner.LastInvocation];

                while (Owner.SelectedCards.Count() == 0 || !Owner.SelectedCards.Last().GetComponent<DisplayCard>().card.Type.Contains(type))
                {

                    Owner.SelectedCards.Clear();
                    yield return new WaitUntil(() => Owner.SelectedCards.Count() > 0);
                }


                GameObject zoneSelected = GameObject.Find(zoneName);
                UltimateInvocation Special = UltimateInvocation.Another;
                switch (Owner.LastInvocation)
                {
                    case UltimateInvocation.MeleeZone:
                        Special = UltimateInvocation.SpecialMelee;
                        break;
                    case UltimateInvocation.RangeZone:
                        Special = UltimateInvocation.SpecialRange;
                        break;
                    case UltimateInvocation.AssaultZone:
                        Special = UltimateInvocation.SpecialAssault;
                        break;
                }
                StartCoroutine(Utility.Invocation(Owner, zoneSelected, type, 1, Special));
            }
        }
        else
        {
            SwitchTurn();
        }
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
                if (!item.GetComponent<DisplayCard>().card.IsHero && !IsInmune(item.GetComponent<DisplayCard>()))
                    Destroy(item);
            }
        }
        else
        {
            if (!extremeAtkMonsters[0].GetComponent<DisplayCard>().card.IsHero && !IsInmune(extremeAtkMonsters[0].GetComponent<DisplayCard>()))
                Destroy(extremeAtkMonsters[0]);
        }
        yield return new WaitForSeconds(0.5f);
    }

    public void PowerUpSanctuaryEffect(Player Owner, int amount)
    {
        switch (Owner.LastInvocation)
        {
            case UltimateInvocation.SpecialMelee:
                foreach (var displayCard in Owner.MeleeZone.GetComponentsInChildren<DisplayCard>())
                {
                    if (!displayCard.card.IsHero && !IsInmune(displayCard))
                        displayCard.card.Atk += amount;
                }
                break;
            case UltimateInvocation.SpecialRange:
                foreach (var displayCard in Owner.RangeZone.GetComponentsInChildren<DisplayCard>())
                {
                    if (!displayCard.card.IsHero && !IsInmune(displayCard))
                        displayCard.card.Atk += amount;
                }
                break;
            case UltimateInvocation.SpecialAssault:
                foreach (var displayCard in Owner.AssaultZone.GetComponentsInChildren<DisplayCard>())
                {
                    if (!displayCard.card.IsHero && !IsInmune(displayCard))
                        displayCard.card.Atk += amount;
                }
                break;
        }
    }

    public void WeatherOffEffect()
    {
        Utility.ClearChildGameObject(Weather);
        zonesSelectedNegate.Clear();
        zonesSelectedInmune.Clear();
    }

    private IEnumerator ZoneSelectedNegateEffect(BoxInfo boxInfo)
    {
        yield return StartCoroutine(SelectZoneBox(boxInfo));

        zonesSelectedNegate.Add(gwent.GetComponent<Gwent>().zonesSelect[0]);
        zonesSelectedNegate.Add(gwent.GetComponent<Gwent>().zonesSelect[1]);
    }
    private IEnumerator ZoneSelectedInmune(BoxInfo boxInfo)
    {
        yield return StartCoroutine(SelectZoneBox(boxInfo));

        zonesSelectedInmune.Add(gwent.GetComponent<Gwent>().zonesSelect[0]);
        zonesSelectedInmune.Add(gwent.GetComponent<Gwent>().zonesSelect[1]);
    }

    private bool IsInmune(DisplayCard card)
    {
        List<DisplayCard> listOfCardsInmune = new();
        foreach (var zone in zonesSelectedInmune)
        {
            foreach (Transform displayCard in zone.transform)
            {
                listOfCardsInmune.Add(displayCard.gameObject.GetComponent<DisplayCard>());
            }
        }

        return listOfCardsInmune.Contains(card);
    }
    private void InvocationEffect(Player Owner)
    {
        Owner.SelectdMonster = true;
    }
    private void AtkUpByEqualsNamesEffect(Player Owner)
    {
        List<string> keys = new List<string>(Owner.AtkUpByEqualsNames.Keys);
        List<string> keysCardsNegate = new List<string>();
        foreach (var item in keys)
        {
            Owner.AtkUpByEqualsNames[item] = new int[] { 1, Owner.AtkUpByEqualsNames[item][1] };
        }

        foreach (var zone in zonesSelectedNegate)
        {
            foreach (var item in zone.GetComponentsInChildren<DisplayCard>())
            {
                keysCardsNegate.Add(item.card.Name);
            }
        }

        foreach (var zone in new GameObject[] { Owner.MeleeZone, Owner.RangeZone, Owner.AssaultZone })
        {
            foreach (DisplayCard child in zone.GetComponentsInChildren<DisplayCard>())
            {
                if (child.card.Effect == Effects.AtkUpByEqualsNames)
                {
                    if (!Owner.AtkUpByEqualsNames.ContainsKey(child.card.name))
                    {
                        Owner.AtkUpByEqualsNames.Add(child.card.name, new int[] { 2, child.card.Atk });
                    }
                    else
                    {
                        Owner.AtkUpByEqualsNames[child.card.name][0]++;
                    }

                }
            }

            foreach (DisplayCard child in zone.GetComponentsInChildren<DisplayCard>())
            {
                if (child.card.Effect == Effects.AtkUpByEqualsNames)
                {
                    bool isNegate = keysCardsNegate.Contains(child.card.Name);

                    if (!isNegate && child.card.Atk != (Owner.AtkUpByEqualsNames[child.card.name][0] * Owner.AtkUpByEqualsNames[child.card.name][1]))
                    {
                        child.card.Atk = Owner.AtkUpByEqualsNames[child.card.name][0] * Owner.AtkUpByEqualsNames[child.card.name][1];
                    }
                    else if (isNegate && child.card.Atk != Owner.AtkUpByEqualsNames[child.card.name][1])
                    {
                        child.card.Atk = Owner.AtkUpByEqualsNames[child.card.name][1];
                    }
                }
            }
        }
    }

    private void ClearRowLessUnitEffect(Player Owner)
    {
        Player rival = Owner == Player1 ? Player2 : Player1;

        int meleeCount = rival.MeleeZone.GetComponentsInChildren<DisplayCard>().Count();
        int rangeCount = rival.RangeZone.GetComponentsInChildren<DisplayCard>().Count();
        int assaultCount = rival.AssaultZone.GetComponentsInChildren<DisplayCard>().Count();

        GameObject zone = null;
        int minCount = int.MaxValue;

        if (meleeCount > 0 && meleeCount < minCount)
        {
            minCount = meleeCount;
            zone = rival.MeleeZone;
        }
        if (rangeCount > 0 && rangeCount < minCount)
        {
            minCount = rangeCount;
            zone = rival.RangeZone;
        }
        if (assaultCount > 0 && assaultCount < minCount)
        {
            zone = rival.AssaultZone;
        }

        if (zone != null)
        {
            foreach (var displayCard in zone.GetComponentsInChildren<DisplayCard>())
            {
                if (!displayCard.card.IsHero && !IsInmune(displayCard))
                    Destroy(displayCard.gameObject);
            }
        }
    }

    private IEnumerator DestroyEffect(Player Owner)
    {
        Player rival = Owner == Player1 ? Player2 : Player1;
        if (rival.MeleeZone.GetComponentsInChildren<DisplayCard>().Count() + rival.RangeZone.GetComponentsInChildren<DisplayCard>().Count() + rival.AssaultZone.GetComponentsInChildren<DisplayCard>().Count() + Weather.GetComponentsInChildren<DisplayCard>().Count() > 0)
        {
            rival.SelectedCardsBoard.Clear();
            while (rival.SelectedCardsBoard.Count == 0)
            {
                rival.SelectedCardsBoard.Clear();
                yield return new WaitUntil(() => rival.SelectedCardsBoard.Count > 0);
            }
            if (!rival.SelectedCardsBoard.Last().GetComponent<DisplayCard>().card.IsHero && !IsInmune(rival.SelectedCardsBoard.Last().GetComponent<DisplayCard>()))
                Destroy(rival.SelectedCardsBoard.Last());
            SwitchTurn();
        }
        else SwitchTurn();
    }

    private IEnumerator DecoyEffect(Player Owner, GameObject CardDecoy)
    {
        if (Owner.MeleeZone.GetComponentsInChildren<DisplayCard>().Count() + Owner.RangeZone.GetComponentsInChildren<DisplayCard>().Count() + Owner.AssaultZone.GetComponentsInChildren<DisplayCard>().Count() > 0)
        {
            Owner.SelectedCards.Clear();
            GameObject decoy = Instantiate(CardDecoy);
            while (Owner.SelectedCards.Count == 0 || Owner.Hand.GetComponentsInChildren<DisplayCard>().Contains(Owner.SelectedCards.Last().GetComponent<DisplayCard>()) || Owner.SelectedCards.Last().GetComponent<DisplayCard>().card.Type.Contains(Types.Weather))
            {
                Owner.SelectedCards.Clear();
                yield return new WaitUntil(() => Owner.SelectedCards.Count > 0);
            }
            if (!Owner.SelectedCards.Last().GetComponent<DisplayCard>().card.IsHero && !IsInmune(Owner.SelectedCards.Last().GetComponent<DisplayCard>()))
            {
                GameObject copyCard = Owner.SelectedCards.Last();
                decoy.transform.SetParent(copyCard.transform.parent, false);
                Owner.SelectedCards[Owner.SelectedCards.Count - 1] = decoy;
                copyCard.transform.SetParent(Owner.Hand.transform, false);
            }
            SwitchTurn();
        }
        else
        {
            SwitchTurn();
        }
    }
}
