using UnityEngine;
using UnityEngine.SceneManagement;
using static JustinLevelManager;

public class ChestNine : MonoBehaviour
{
    private bool isOpened;
    public GameObject toMiniBossWalls;
    public string cutsceneName;

    public void OpenChestNine()
    {
        if (!isOpened)
        {
            toMiniBossWalls.SetActive(false);
            justinLevelManager.toMiniBossDoor = true;
            isOpened = true;
            SceneManager.LoadScene(cutsceneName);
        }
    } 
}
