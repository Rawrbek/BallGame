using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Height the player can jump
    public int jumpHeight;

    //Mask for the layer?!
    public LayerMask mask;

    //Vector for velocity
    Vector3 velocity;

    //For controlling gravity
    public float gravity;

    //For controlling whether or not player is jumping
    bool isJumping = false;

    //Controls if the player has double jumped
    bool DoubleJumped = false;

    private float addedSpeed;

    //Initilization of rigidbody
    Rigidbody myRigidbody;

    public float speed = 6;

    float timer;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        //If not already jumping, jump
        if (Input.GetKeyDown("space") && isJumping == false)
        {
            addedSpeed = 0;
            isJumping = true;
            myRigidbody.AddForce(Vector3.up * jumpHeight);

            //sets a short timer so you can't double jump imediately
            timer = 0.5f;
        }

        //Counts down on the timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;

        }

        //Sends a ray down into the ground. This ray is to check whether or not the ball is placed on the ground or not.

        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;

        //Debug line for checking rotation.
        Debug.DrawLine(ray.origin, ray.origin * 100, Color.blue);

        if (Physics.Raycast(ray, out hitInfo, 3, mask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.red);
        }

        //figures out if you're on the ground or not
        if (hitInfo.collider != null)
        {
            isJumping = false;
            DoubleJumped = false;
        }
        else
        {
            isJumping = true;
        }

        //Figures out if the player in on a moving platform


        if (hitInfo.collider != null) { 
            if (hitInfo.collider.tag == "MovingPlatform")
            {

                //myRigidbody.useGravity = false;
                myRigidbody.velocity = hitInfo.rigidbody.velocity;
                //transform.position = hitInfo.transform.position;

            }
    }

    //Extra gravity for this ball. Useful for making gravity zones me thinks
    myRigidbody.AddForce(Vector3.down * gravity * myRigidbody.mass);
    }


    void FixedUpdate()
    {
        //The Input
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");

        //assuming we only using the single camera:
        var camera = Camera.main;

        var forward = camera.transform.forward;
        var right = camera.transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //The direction
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //finding the velocity
        velocity = desiredMoveDirection * speed;

        if (isJumping == false)
        {
           myRigidbody.AddForce(velocity);
        }

        else
        {
            //Limits how much you can move to the side in the air. Can still move a little, otherwise it feels shit.
            velocity = velocity / 3f;

            myRigidbody.AddForce(velocity);


        }


        // Code for double jumping

        if (isJumping == true && timer <= 0)
        {

            if (Input.GetKey("space") && DoubleJumped == false)
            {

                //Adds up force to a limit
                if (addedSpeed < 20000)
                {
                    addedSpeed += 300;
                }

                //Sets velocity to zero
                velocity = Vector3.zero;

                //Freezes the position
                myRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            }

            if (Input.GetKey("space") == false && addedSpeed > 0)
            {
                myRigidbody.constraints = RigidbodyConstraints.None;
                myRigidbody.AddForce(camera.transform.forward * addedSpeed);
                addedSpeed = 0;
                DoubleJumped = true;

            }
        }


    
    }


   




}
