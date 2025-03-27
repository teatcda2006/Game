using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    private Rigidbody2D rb;
    private score score;
    [SerializeField] private Rigidbody2D rb_friend;
    public bool inDoor = false;
    [SerializeField] private string dangerousTag;
    [SerializeField] private string doorTag;
    [SerializeField] private string gemTag;
    private HashSet<GameObject> collectedGems = new HashSet<GameObject>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        score = FindObjectOfType<score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(dangerousTag) || collision.gameObject.CompareTag("acid"))
        {
            rb.transform.position = new Vector3(-14, -7, -0.5f);
            rb_friend.transform.position = new Vector3(-13, -7, -0.5f);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("gem"))
        {
            if (gemTag == "firegem" && collision.gameObject.tag == gemTag && !collectedGems.Contains(collision.gameObject))
            {
                collectedGems.Add(collision.gameObject);
                Destroy(collision.gameObject);
                score.AddFireGem();
            }

            if (gemTag == "watergem" && collision.gameObject.tag == gemTag && !collectedGems.Contains(collision.gameObject))
            {
                collectedGems.Add(collision.gameObject);
                Destroy(collision.gameObject);
                score.AddWaterGem();
            }

            if (collision.gameObject.CompareTag("supergem") && !collectedGems.Contains(collision.gameObject))
            {
                collectedGems.Add(collision.gameObject);
                Destroy(collision.gameObject);
                score.AddSuperGem();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(doorTag))
        {
            inDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(doorTag))
        {
            inDoor = false;
        }
    }
}
