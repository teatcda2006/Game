using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finish : MonoBehaviour
{

    [SerializeField] private Triggers plLava;
    [SerializeField] private Triggers plWater;

    void Update()
    {
        if (plWater.inDoor && plLava.inDoor)
        {
            SceneManager.LoadScene(0);
        }
    }
}
