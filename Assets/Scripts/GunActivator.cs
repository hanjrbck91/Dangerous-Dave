using UnityEngine;

public class GunActivator : MonoBehaviour
{
    public GunController gunController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun"))
        {
            gunController.enabled = true;
        }
    }
}
