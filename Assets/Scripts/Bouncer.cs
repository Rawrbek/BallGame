using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour {

    float forceApplied = 13000;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {

            // Calculate Angle Between the collision point and the player
            Vector3 dir = other.contacts[0].point - transform.position;

            //Sets the y component to zero
            dir.y = 0;


            //Set the players velocity to zero
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //add force in the direction of dir and multiply it by forceApllied. 
            other.gameObject.GetComponent<Rigidbody>().AddForce(dir * forceApplied);



        }
    }
}
