using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    Transform leadsTo;

    private void OnTriggerStay(Collider other)
    {
        if (leadsTo != null && other.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                other.transform.position = leadsTo.position;
            }
        }
    }
}
