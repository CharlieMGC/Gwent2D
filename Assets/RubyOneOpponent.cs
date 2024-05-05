using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyOneOpponent : MonoBehaviour
{
    public Sprite Win;
    public Sprite Lose;

    private Sprite sprite;

    void Start()
    {
        sprite = gameObject.GetComponent<Sprite>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gwent.Player2.RoundWin > 0)
        {
            gameObject.GetComponent<Image>().sprite = Win;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Lose;
        }
    }
}
