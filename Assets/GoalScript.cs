using UnityEngine;

public class GoalScript : MonoBehaviour
{


    //Particle system for the goal
    public GameObject GoalParticle;

    //Particle check
    private int GoalParticleActive = 0;

    void Start()
    {

        GameObject GameControl = GameObject.Find("GameController");
    }


    // Update is called once per frame
    void Update()
    {

        if (GameControl.canWin && GoalParticleActive == 0)
        {
            GameObject GoalParticleObject = Instantiate(GoalParticle);

            GoalParticleObject.transform.parent = transform;
            GoalParticleObject.transform.localPosition = new Vector3(0, 0, 0);

            GoalParticleActive = 1;
        }



    }
}
