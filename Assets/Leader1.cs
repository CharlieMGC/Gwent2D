using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leader1 : MonoBehaviour
{
    public GameObject displayCard;
    void Start()
    {
        GameObject leader = Instantiate(displayCard, gameObject.transform);
        leader.transform.SetParent(transform, false);
    }
}
