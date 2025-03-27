using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void loadLevel0()
    {
        SceneManager.LoadScene(0);
    }

    public void loadLevel1()
    {
        SceneManager.LoadScene(1);
    }
}
