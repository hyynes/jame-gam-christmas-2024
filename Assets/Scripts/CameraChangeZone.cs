using UnityEngine;

public class CameraChangeZone : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;

    void OnTriggerEnter2D(Collider2D collider)
    {
        CameraManager cameraManager = CameraManager.instance;
        if (targetCamera)
        {
            cameraManager.UpdateCamera(targetCamera);
        }
        else
        {
            Debug.Log("Camera does not exist");
        }
    }
}
