using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Sprite Image;
    public GameObject Atk;
    public GameObject Effect;
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = Image;
    }


}
