using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite Image;
    public GameObject Atk;
    public GameObject Effect;
    void Start()
    {
        gameObject.GetComponent<Image>().sprite = Image;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
