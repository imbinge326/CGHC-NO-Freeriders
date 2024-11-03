using UnityEngine;
using UnityEngine.SceneManagement;

public class LeviathanSceneController : MonoBehaviour
{
    public float changeTime;
    public string sceneName; 

    public void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime < 0)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (!player)
            {
                Debug.LogError("Player not found in scene");
                return;
            }

            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                Debug.LogWarning("Player does not have a Rigidbody2D component.");
            }
            
            SceneManager.LoadScene(sceneName);
        }
    }    
}
