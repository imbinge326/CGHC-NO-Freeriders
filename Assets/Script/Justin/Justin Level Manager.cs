using UnityEngine;

public class JustinLevelManager : MonoBehaviour
{
    public static JustinLevelManager justinLevelManager;
    public bool leftPortalDoor;
    public bool mazeDoor;
    public bool miniBossDoor;
    public bool toBossLevelDoor;
    public bool toMiniBossDoor;
    public bool pseudoMazeBridge;

    public void Awake()
    {
        if (justinLevelManager != null && justinLevelManager != this)
        {
            Destroy(gameObject);
            return;
        }
        justinLevelManager = this;

        DontDestroyOnLoad(gameObject);
    }
}