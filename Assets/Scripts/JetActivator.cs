using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetActivator : MonoBehaviour
{
    private JetController jetController;

    private void Start()
    {
        jetController = GetComponent<JetController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jet"))
        {
            jetController.enabled = true;
        }
    }
}
