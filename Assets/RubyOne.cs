using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyOne : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite Win;
    public Sprite Lose;

    // Update is called once per frame
    void Update()
    {
        if (Gwent.Player1.RoundWin > 0)
        {
            gameObject.GetComponent<Image>().sprite = Win;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Lose;
        }
    }
}