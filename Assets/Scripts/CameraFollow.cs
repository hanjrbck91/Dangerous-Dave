using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The player's transform
    public float slideDistance = 15.3f; // Distance to slide the camera

    private Vector3 initialCameraPosition;
    private float lastSlidePositionX;

    void Start()
    {
        initialCameraPosition = transform.position;
        lastSlidePositionX = initialCameraPosition.x;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            float playerDistance = target.position.x - initialCameraPosition.x;

            // Check if the player has traveled a multiple of the slide distance
            if (Mathf.Abs(playerDistance - lastSlidePositionX) >= slideDistance)
            {
                SlideCamera();
            }
        }
    }

    void SlideCamera()
    {
        float slideAmount = Mathf.Floor(Mathf.Abs(target.position.x - initialCameraPosition.x) / slideDistance) * slideDistance;
        Vector3 targetPosition = initialCameraPosition + new Vector3(slideAmount, 0f, 0f);
        transform.position = targetPosition;
        lastSlidePositionX = target.position.x;
    }
}
