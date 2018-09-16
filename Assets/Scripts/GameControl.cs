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
    public static bool canWin = false;

    // For keeping count of collectibles
    public static int collectibleCount;

    private GameObject[] getCount;

    private GameObject[] EnvironmentObjects;

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

        //Can Win reset 
        canWin = false;

        //Continually checks where the environment objects are for scene reload
        InvokeRepeating("FindEnvironmentObjects", 0f, 1f);

    }

    void Update()
    {
        //Reloads the active scene if the player has fallen below a certain threshold.

        
                

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
        scoreText.text = "Coins Collected:" + currentScore + "/" + CollectibleCountFixed;
    }

    private void FindEnvironmentObjects()

    {

        EnvironmentObjects = GameObject.FindGameObjectsWithTag("Environment");

        float[] yArray = new float[EnvironmentObjects.Length];

        for (int i = 1; i < EnvironmentObjects.Length + 1; i++)
        {

            yArray[i] = EnvironmentObjects[i].transform.position.y;

        }
        float minY = Mathf.Min(yArray);

        Debug.Log(minY);
                
        if (player.transform.position.y < minY)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

        
 }


        
  






