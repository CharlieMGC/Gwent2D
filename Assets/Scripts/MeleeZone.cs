
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MeleeZone : MonoBehaviour
{

    // Start is called before the first frame update
    private Player Owner { get; set; }
    /*     private Image image; */
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Owner = Gwent.Player1;
        Gwent.Player1.MeleeZone = gameObject;

        /*         image = gameObject.GetComponent<Image>(); */
    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        Utility.Invocation(Owner, gameObject, Types.Melee);
    }

    /*  private void Update()
     {
         if (Owner.SelectedCards != null && Owner.SelectedCards.Count > 0)
         {
             image.color = Owner.SelectedCards[Owner.SelectedCards.Count - 1].GetComponent<DisplayCard>().card.Type.Contains(Types.Melee) ? Color.red : Color.RGBToHSV(219, 155, 155);
         }
     } */
}
