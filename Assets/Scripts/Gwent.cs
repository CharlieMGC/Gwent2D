using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gwent : MonoBehaviour
{
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

    private void DrawCard(Player player, List<GameObject> cards)
    {
        if (cards.Count > 0)
        {
            int cardIndex = Random.Range(0, cards.Count);
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
        // Prepara todo para la siguiente ronda
        Player1.IsMyTurn = true;
        Player2.IsMyTurn = false;
        Start();

        // Restaura el número de rondas ganadas
        Player1.RoundWin = roundWinPlayer1;
        Player2.RoundWin = roundWinPlayer2;

        Player1.EndTurn = false;
        Player2.EndTurn = false;
    }

}