using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenNav : MonoBehaviour
{
    public void ChangeScene (string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
