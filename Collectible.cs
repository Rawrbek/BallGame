using UnityEngine;

public class Collectible : MonoBehaviour {



    private void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime);
    }


    //Function to add points
    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Player")
        {
            GameControl.currentScore += 1;
            GameControl.collectibleCount -= 1;
            Destroy(gameObject);
           
        }
    }
}
