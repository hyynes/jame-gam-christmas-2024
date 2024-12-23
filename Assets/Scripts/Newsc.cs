using System;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Newsc : MonoBehaviour
{
    /*
     * INSTRUCTIONS
     * Create a small circle and add this to it
     * Add fixed and distance joint components to circle and set player as connections
     * Set hookables (Just a collider and set collider to trigger)
     * Add tags ("Spinner" for spinning hookable "StaticHook" for default)
     * Set rigidbody and collider
     * 
     * 
     * The circle should move freely around player
     * you can engage hooking behaviour with holding m1, releasing it will release hook
     * 
     * Notes:
     * There should be no ground or walls around spinning hookable in the radius of hook length. Or it'll just pass through.
     * DistanceJoint needs just a little adjustment.
     * 
     */


    private Rigidbody2D rb;
    DistanceJoint2D distanceJoint;
    FixedJoint2D fixedJoint;
    private Boolean followMouse = true;
    public Rigidbody2D playerRigidbody;
    public float maxDistance = 4f;
    private Boolean releasedSpinner = false;
    private Boolean rotateEnabled = true;

    void Start()
    {
        distanceJoint = GetComponent<DistanceJoint2D>();
        fixedJoint = GetComponent<FixedJoint2D>();
        rb = GetComponent<Rigidbody2D>();

        fixedJoint.enabled = false;
        distanceJoint.enabled = false;


        rb.bodyType = RigidbodyType2D.Kinematic;
    }


    void Update()
    {
        moveObj();

        if (rotateEnabled)
        {
            Vector2 mousepos = Input.mousePosition;
            mousepos = Camera.main.ScreenToWorldPoint(mousepos);
            Vector2 direction = new Vector2(transform.position.x - playerRigidbody.position.x, transform.position.y - playerRigidbody.position.y);
            transform.up = direction;
        }



        if (Input.GetMouseButtonUp(0))
        {
            
            // Disable joints
            
           
            distanceJoint.enabled = false;

            // Allow the object to follow the mouse again
            followMouse = true;

            if (releasedSpinner)
            {
                Vector2 perpendicular = new Vector2(playerRigidbody.position.y - transform.position.y, transform.position.x - playerRigidbody.position.x);
                playerRigidbody.AddForce(-perpendicular * 300);
                
                releasedSpinner = false;
            }
            // Reset the Rigidbody2D to Kinematic and allow for interactions later
            rb.bodyType = RigidbodyType2D.Kinematic;
            
            if (fixedJoint.enabled)
            {
                fixedJoint.enabled = false;
                playerRigidbody.freezeRotation = true;
                releasedSpinner = true;
                playerRigidbody.transform.rotation = Quaternion.identity;
                rotateEnabled = true;
                
            }
            

        }

        if (fixedJoint.enabled == true)
        {
            playerRigidbody.freezeRotation = false;
            transform.Rotate(0, 0, 2);
        }
    }


    void moveObj()
    {
        if (followMouse)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - playerRigidbody.position;
            if (direction.magnitude > maxDistance)
            {
                direction = direction.normalized * maxDistance;
            }

            transform.position = playerRigidbody.position + direction;
        }

    }


    

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("AAAA");
        if (Input.GetMouseButton(0)) {
            if (collision.gameObject.tag == "StaticHook")
            {
                rb.bodyType = RigidbodyType2D.Static;
                Debug.Log("hook");
                distanceJoint.enabled = true;
                followMouse = false;
            }

            else if (collision.gameObject.tag == "Spinner")
            {
                Debug.Log("spin");
                rotateEnabled = false;
                rb.bodyType = RigidbodyType2D.Static;
                playerRigidbody.transform.up = transform.position;
                fixedJoint.enabled = true;
                followMouse = false;
            }

        }
    }

}
