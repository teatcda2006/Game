using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platform : MonoBehaviour
{
    public int plInbtn;
    [SerializeField] private bool isNotActiveOnStart;

    void Start()
    {
        UpdatePlatformState();
    }

    public void ChangePlayerCount(int change)
    {
        plInbtn += change;
        UpdatePlatformState();
    }

    private void UpdatePlatformState()
    {
        if (!isNotActiveOnStart)
        {
            gameObject.SetActive(plInbtn > 0);
        } else
        {
            gameObject.SetActive(plInbtn == 0);
        }
        
    }
}
