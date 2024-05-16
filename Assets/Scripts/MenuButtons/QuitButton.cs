using UnityEngine;

public class QuitButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
