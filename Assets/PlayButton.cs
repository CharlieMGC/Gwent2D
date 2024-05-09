using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Game");
    }
}
