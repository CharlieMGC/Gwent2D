using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{

    public GameObject Hand;
    public List<GameObject> CardsInDeck;

    private void Start()
    {
        Scope.CardsInHand = new List<GameObject>();
       // Scope.BoxInfo.SetActive(false);
       // Scope.BoxInfoShow = false;
        //Debug.Log//(Scope.BoxInfo.transform.position);
        InitializeGame();

    }

    private void DrawFunction()
    {
        GameObject drawCard = Instantiate(CardsInDeck[Random.Range(0, CardsInDeck.Count)], Vector3.zero, Quaternion.identity);
        drawCard.transform.SetParent(Hand.transform, false);
        Scope.CardsInHand.Add(drawCard);
    }

    public void OnMouseDown()
    {
        if (Scope.CardsInHand.Count < 10)
        {
            DrawFunction();
        }
    }

    private void InitializeGame()
    {
        StartCoroutine(DrawCardsOverTime());
    }

    private IEnumerator DrawCardsOverTime()
    {
        while (Scope.CardsInHand.Count < 10)
        {
            DrawFunction();
            yield return new WaitForSeconds(0.25f); // Adjust the delay as needed
        }
       // Scope.BoxInfo.SetActive(true);
       // Scope.BoxInfoShow = true;
    }

}