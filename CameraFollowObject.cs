using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    //Finds the player
    public Transform target;


    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 25f;

    public float MouseSensitivity = 4f;
    //public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;


    // Update is called once per frame
    void Update()
    {

        transform.position = target.transform.position;

        if (Input.GetKey(KeyCode.Mouse1))
        {

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y += Input.GetAxis("Mouse Y") * MouseSensitivity/2;


                //Clamping the camera
                if (_LocalRotation.y < 0f)
                    _LocalRotation.y = 0f;
                else if (_LocalRotation.y > 100f)
                    _LocalRotation.y = 100f;


            }

            Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * OrbitDampening);


        }
    }

}
