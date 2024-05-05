using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyTwoOpponent : MonoBehaviour
{
    public Sprite Win;
    public Sprite Lose;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Gwent.Player2.RoundWin > 1)
        {
            gameObject.GetComponent<Image>().sprite = Win;

        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Lose;
        }
    }
}
