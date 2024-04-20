using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHover : MonoBehaviour
{
    private Vector3 originalPosition;

    private void Start()
    {
    }

    private void OnMouseEnter()
    {
        // Mueve el objeto ligeramente hacia arriba
        originalPosition = transform.position;
        transform.position = new Vector3(originalPosition.x, originalPosition.y + 2f, originalPosition.z);
    }

    private void OnMouseExit()
    {
        // Restablece la posición cuando el mouse sale
        transform.position = originalPosition;
    }

}
