using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform otherPortal;  // Reference to the linked portal

    public Transform GetOtherPortal()
    {
        return otherPortal;
    }
}
