using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxInfo : MonoBehaviour
{
    public Text Title;
    public bool Active;
    public Button Button1;
    public Button Button2;
    public Button Button3;

    public int result { get; set; }

    public void Show(string title, string button1Text = null, string button2Text = null, string button3Text = null)
    {
        result = -1;
        Title.text = title;
        Button1.GetComponentInChildren<Text>().text = button1Text;
        Button1.gameObject.SetActive(button1Text != null);
        Button2.GetComponentInChildren<Text>().text = button2Text;
        Button2.gameObject.SetActive(button2Text != null);
        Button3.GetComponentInChildren<Text>().text = button3Text;
        Button3.gameObject.SetActive(button3Text != null);
        Active = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Active = false;
        gameObject.SetActive(false);
    }

    public int GetResult()
    {
        return result;
    }


    private void Start()
    {

        Gwent.BoxInfo = gameObject;
        Hide();
    }

}
