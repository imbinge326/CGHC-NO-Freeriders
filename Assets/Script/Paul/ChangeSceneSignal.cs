using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneSignal : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("Paul");
    }
}
