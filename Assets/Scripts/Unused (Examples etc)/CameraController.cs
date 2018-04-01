using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject target;

    //public float xOffset, yOffset, zOffset;

    public float distance;

    public float hAngle = 00;

    public float vAngle = 00;

    float currentRotationAngle;

    

    private void Update()
    {


        transform.position = distance * (new Vector3(Mathf.Cos(hAngle), Mathf.Sin(vAngle), Mathf.Sin(hAngle)));

        // make the camara look directly at the player
        transform.forward = target.transform.position - transform.position;


       // currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        //float xAngle = target.transform.rotation.x;
        //float yAngle = target.transform.rotation.y;
        //float zAngle = target.transform.rotation.z;


        //transform.rotation = Quaternion.Euler(xAngle, yAngle, zAngle);  // 35 degree angle forced
        //}

    }


        //    // The target we are following
        //    public Transform target;


        //    float wantedRotationAngle;
        //    float wantedHeight;
        //    float currentRotationAngle;
        //    float currentHeight;
        //    float currentRotation;


        //void LateUpdate()
        //    {

        //        // The distance in the x-z plane to the target
        //        var distance = 10.0f;
        //        // the height we want the camera to be above the target
        //        var height = 5.0f;
        //        // How much we 
        //        var heightDamping = 2.0f;
        //        var rotationDamping = 3.0f;


        //        // Calculate the current rotation angles
        //        wantedRotationAngle = target.eulerAngles.y;
        //        wantedHeight = target.position.y + height;
        //        currentRotationAngle = transform.eulerAngles.y;
        //        currentHeight = transform.position.y;
        //        // Damp the rotation around the y-axis
        //        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        //        // Damp the height
        //        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        //        // Convert the angle into a rotation
        //        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        //        // Set the position of the camera on the x-z plane to:
        //        // distance meters behind the target
        //        transform.position = target.position;
        //        transform.position -= currentRotation * Vector3.forward * distance;
        //        // Set the height of the camera
        //        transform.position.y = currentHeight;
        //        // Always look at the target
        //        transform.LookAt(target);

        //    }




    }


//}