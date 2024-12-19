using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // singleton declaration
    public static CameraManager instance { get; private set; }
    
    // all camera objects in the scene
    private Camera[] allCameras;
    
    // whatever camera is set on default
    [SerializeField] private Camera startingCamera;

    // the current enabled camera
    private Camera currentlyActiveCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if i do not exist, create me
        if (instance == null)
        {
            instance = this;
        }

        // set the enabled camera to the camera set in inspector
        currentlyActiveCamera = startingCamera;
        
        allCameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);

        foreach (Camera c in allCameras)
        {
            if (c != currentlyActiveCamera)
                c.enabled = false;
        }
    }

    /*
     * this method is used to update the currently active camera.
     */
    public void UpdateCamera(Camera c)
    {
        if (currentlyActiveCamera == c)
            return;
        
        currentlyActiveCamera.enabled = false;
        currentlyActiveCamera = c;
        
        currentlyActiveCamera.enabled = true;
    }
}
