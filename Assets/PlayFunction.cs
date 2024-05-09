using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject gameCanvas; // Arrastra aquí el Canvas del juego en el Inspector de Unity
    public GameObject menuCanvas; // Arrastra aquí el Canvas del menú en el Inspector de Unity

    private void Start() {
        gameCanvas.SetActive(false);
        Instantiate(gameCanvas);
    }

    public void OnPlayButtonClicked()
    {
        // Activa el Canvas del juego y desactiva el Canvas del menú cuando se haga clic en el botón "Play"
        gameCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }
}