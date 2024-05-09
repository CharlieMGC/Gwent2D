using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxInfo : MonoBehaviour
{
    public Text Title;
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
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public int GetResult()
    {
        return result;
    }

    public void OnButton1Click()
    {
        result = 0;
    }

    public void OnButton2Click()
    {
        result = 1;
    }

    public void OnButton3Click()
    {
        result = 2;
    }

    private void Start()
    {
        Button1.onClick.AddListener(OnButton1Click);
        Button2.onClick.AddListener(OnButton2Click);
        Button3.onClick.AddListener(OnButton3Click);
        Gwent.BoxInfo = gameObject;
        Hide();
    }
}
