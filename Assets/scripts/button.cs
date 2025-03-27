using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour
{
    [SerializeField] private platform platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("player"))
        {
            platform.ChangePlayerCount(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            platform.ChangePlayerCount(-1);
        }
    }
}
