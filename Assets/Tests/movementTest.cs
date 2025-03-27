using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class movementTest
{
    private GameObject player;
    private Movement movement;
    private Rigidbody2D rb;

    [SetUp]
    public void Setup()
    {
        GameObject playerPrefab = Resources.Load<GameObject>("prefab/player"); 
        player = Object.Instantiate(playerPrefab); 
        rb = player.GetComponent<Rigidbody2D>();
        movement = player.GetComponent<Movement>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        float initialX = player.transform.position.x;
        rb.AddForce(Vector2.right * 100);
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(player.transform.position.x, initialX);
    }

    [UnityTest]
    public IEnumerator PlayerJumps()
    {
        float initialY = player.transform.position.y;
        movement.GetType().GetField("inAir", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(movement, false); 
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * 350);
        yield return new WaitForSeconds(0.1f);
        Assert.AreNotEqual(rb.transform.position.y, initialY);
    }
}
