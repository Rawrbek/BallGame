using UnityEngine;

public class Collectible : MonoBehaviour {

    //What speed it should rotate at
    float rotateSpeed = 100;

    void Update()
    {
        transform.Rotate(0,0, rotateSpeed * Time.deltaTime);
    }


    //Function to add points
    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Player")
        {
            GameControl.currentScore += 1;

            //The Collectible count is how many collectibles there are left on the map
            GameControl.collectibleCount -= 1;
            Destroy(gameObject);
           
        }
    }
}
