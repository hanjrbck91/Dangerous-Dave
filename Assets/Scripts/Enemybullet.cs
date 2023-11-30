using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("SkyBlueGem"))
        {

        }
        else
        {
            Destroy(gameObject);
        }
    }

}
