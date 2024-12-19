using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneHooking : MonoBehaviour

    //This works by catching a hookable (set it to everything atm) while holding M1. 
{
    [SerializeField] private float candy_length; //desired length
    [SerializeField] private LayerMask hookable; //For now set it to everything, 

    private Vector3 hook_point;
    private DistanceJoint2D joint;

    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false; // Disable the joint initially
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, hookable);

            if (hit != null && Vector2.Distance(transform.position, mouseWorldPosition) <= candy_length) 
            {
                hook_point = hit.transform.position;
                hook_point.z = 0;
                joint.connectedAnchor = hook_point;
                joint.enabled = true;
                joint.distance = candy_length;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
        }
    }

}
