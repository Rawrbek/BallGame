using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverSimple : MonoBehaviour {

    //What speed the platform should move at
	public int speed;

    //How far from the center it should move at maximun
    public int moveDistance;

    private int moveDistanceWithDir;

    //Which axis it should move on
    public enum MoveAxis {X,Y,Z}

    public MoveAxis Axis;

    //Which way from origin it should go

    public enum DirectionToTake {Plus, Minus}

    public DirectionToTake direction;

    //Original time and position
    private float startTime;
    //Vector3 originalPosition = transform.position;

    //New start and end position
    Vector3 extremeOne;
    Vector3 extremeTwo;

    Vector3 tmpE1;
    Vector3 tmpE2;

    void Start()
    {
        //Finds the original position of the platform
        startTime = Time.time;
        Vector3 originalPosition = transform.position;

        //Figures out whether the distance is to be substracted or added
        if (direction == DirectionToTake.Minus)
        {
            moveDistanceWithDir = -1 * moveDistance;
        }

        //Start and end position
        if (Axis == MoveAxis.X)
        {

            extremeOne.x = originalPosition.x;
            extremeTwo.x = originalPosition.x + moveDistanceWithDir;

            extremeOne.y = originalPosition.y;
            extremeTwo.y = originalPosition.y;

            extremeOne.z = originalPosition.z;
            extremeTwo.z = originalPosition.z;



             }

        if (Axis == MoveAxis.Y)
        {
            extremeOne.x = originalPosition.x;
            extremeTwo.x = originalPosition.x;

            extremeOne.y = originalPosition.y;
            extremeTwo.y = originalPosition.y + moveDistanceWithDir;

            extremeOne.z = originalPosition.z;
            extremeTwo.z = originalPosition.z;
        }

        if (Axis == MoveAxis.Z)
        {
            extremeOne.x = originalPosition.x;
            extremeTwo.x = originalPosition.x;

            extremeOne.y = originalPosition.y;
            extremeTwo.y = originalPosition.y;

            extremeOne.z = originalPosition.z;
            extremeTwo.z = originalPosition.z + moveDistanceWithDir;
        }
    }

   

    // Update is called once per frame
    void Update()
    {

        //How far it has moved
        float distCovered = (Time.time - startTime) * speed;

        //How far it has gone on the distance
        float fracJourney = distCovered / (moveDistance * 2f);

        transform.position = Vector3.Lerp(extremeOne, extremeTwo, fracJourney);

        if (fracJourney >= 1)
        {


            //Switches the two end point around so lerp back around
            tmpE1 = extremeOne;
            tmpE2 = extremeTwo;

            extremeOne = tmpE2;
            extremeTwo = tmpE1;

            //Resets the time
            startTime = Time.time;
            

        }




    }


}

