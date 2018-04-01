using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    //How many coins have been collected
    public static int currentScore;

    int CollectibleCountFixed;

    //Transform of the player
    public Transform player;

    //Keeps track of if the win portal should open
    bool  canWin = false;

    // For keeping count of collectibles
    public static int collectibleCount;

    private GameObject[] getCount;

    //For writing on screen
    public Text scoreText;
    

    private void Start()
    {
        //Finds amount of collectibles
        getCount = GameObject.FindGameObjectsWithTag("Collectible");
        collectibleCount = getCount.Length;

        //The max, used to draw UI
        CollectibleCountFixed = collectibleCount;

        //Sets scoretext alignment  
        scoreText.alignment = TextAnchor.UpperLeft;
      
        //resets current score
        currentScore = 0;

    }

    void Update()
    {

        //Reloads the active scene if the player has fallen below a certain threshold.

        ///////////// CAN BE CHANGED TO LOWEST THING ON THE MAP MINUS SOMETHING?
        if (player.transform.position.y < -100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        //Reloads the active scene if R is pressed
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //Check if all collectibles have been collected

        if (collectibleCount == 0)
        {

            canWin = true;

        }

        //Writes the score text
        scoreText.text = "Coins Collected:"+currentScore + "/" + CollectibleCountFixed;
          }

}



