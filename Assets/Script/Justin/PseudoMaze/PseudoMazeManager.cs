using UnityEngine;

public class PseudoMazeManager : MonoBehaviour
{
    public static PseudoMazeManager pseudoMazeManager;
    public bool chestOne;
    public bool chestTwo;
    public bool chestThree;
    public bool chestFour;
    public bool chestFive;
    public bool chestSix;
    public bool chestSeven;
    public bool chestEight;
    public bool chestNine;

    public void Awake()
    {
        if (pseudoMazeManager != null && pseudoMazeManager != this)
        {
            Destroy(gameObject);
            return;
        }
        pseudoMazeManager = this;

        DontDestroyOnLoad(gameObject);
    }
}
