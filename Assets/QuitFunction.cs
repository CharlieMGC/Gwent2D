using UnityEngine;
using UnityEngine.UI;

public class SalirDelJuego : MonoBehaviour
{
    // Asegúrate de que tu botón esté conectado en el Inspector de Unity
    public Button botonSalir;

    void Start()
    {
        // Agrega un listener al botón para que cuando se haga clic, se llame a la función Salir
        /* botonSalir.onClick.AddListener(Salir); */
    }

    void Salir()
    {
        // Cierra la aplicación
       /*  Application.Quit(); */
    }
}
