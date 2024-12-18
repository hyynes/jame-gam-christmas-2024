using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

// to be fixed - doesn't work atm
public class MovingPlatform : MonoBehaviour
{
    // initialised in editor
    [SerializeField] private float PlatformSpeed = 3;
    [SerializeField] private Transform[] Waypoints;
    [SerializeField] private float DelayAfterReachingWaypoint = 2;
    [SerializeField] private float SnapThreshold = 0.05f;
    
    // flexibility to be controlled
    private bool isMoving { get; set; }
    
    // current waypoint
    private int index = 0;
    
    // Update is called once per frame
    void Update()
    {
        // if cannot move, do nothing
        if (!isMoving)
            return;
        
        // otherwise, check if the current waypoint is below the snap threshold (helps check for incredible speeds)
        if (Vector2.Distance(transform.position, Waypoints[index].position) < SnapThreshold)
        {
            index++;
            StartCoroutine(RefreshAfterDelay());
            if (index == Waypoints.Length)
                index = 0;
        }
        
        // otherwise, move if able to
        transform.position =
            Vector2.MoveTowards(transform.position, Waypoints[index].position, PlatformSpeed * Time.deltaTime);
        
    }

    IEnumerator RefreshAfterDelay()
    {
        isMoving = false;
        yield return new WaitForSeconds(DelayAfterReachingWaypoint);

        isMoving = true;
    }
}
