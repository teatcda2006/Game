using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class triggersTest
{
    private GameObject player;
    private Triggers triggers;
    private Rigidbody2D rb;

    private GameObject player2;
    private Triggers triggers2;
    private Rigidbody2D rb2;

    private score score;

    [SetUp]
    public void Setup()
    {
        player = Object.Instantiate(Resources.Load<GameObject>("prefab/player")); 
        rb = player.GetComponent<Rigidbody2D>();
        triggers = player.GetComponent<Triggers>();

        player2 = Object.Instantiate(Resources.Load<GameObject>("prefab/player")); 
        rb2 = player2.GetComponent<Rigidbody2D>();
        triggers2 = player2.GetComponent<Triggers>();

        GameObject scoreObject = new GameObject();
        score = scoreObject.AddComponent<score>();

        triggers.GetType().GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers, rb);
        triggers.GetType().GetField("score", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers, score);
        triggers.GetType().GetField("rb_friend", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers, rb2);
        triggers.GetType().GetField("dangerousTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers, "water");
        triggers.GetType().GetField("gemTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers, "firegem");
        triggers.GetType().GetField("doorTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers, "ldoor");

        triggers2.GetType().GetField("rb", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers2, rb2);
        triggers2.GetType().GetField("score", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers2, score);
        triggers2.GetType().GetField("rb_friend", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers2, rb);
        triggers2.GetType().GetField("dangerousTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers2, "lava");
        triggers2.GetType().GetField("gemTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers2, "watergem");
        triggers2.GetType().GetField("doorTag", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(triggers2, "wdoor");

    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player);
        Object.Destroy(player2);
        Object.Destroy(score);
    }

    [UnityTest]
    public IEnumerator PlayerCollectsSuperGem()
    {
        int initialSuperGems = score.superGems;

        GameObject gem = new GameObject();
        gem.tag = "supergem";
        Collider2D collider = gem.AddComponent<BoxCollider2D>();
        gem.layer = LayerMask.NameToLayer("gem");
        collider.isTrigger = true;

        player.transform.position = gem.transform.position;

        yield return new WaitForSeconds(0.1f);
        Debug.Log(score.superGems);
        Assert.AreEqual(initialSuperGems + 1, score.superGems);
    }

    [UnityTest]
    public IEnumerator PlayerRespawnsOnDangerAcid()
    {
        GameObject danger = new GameObject();
        danger.tag = "acid";
        var collider = danger.AddComponent<BoxCollider2D>();

        triggers.SendMessage("OnTriggerEnter2D", collider);
        yield return new WaitForSeconds(0.0f);
        Assert.AreEqual((new Vector3(-14, -7, -0.5f), new Vector3(-13, -7, -0.5f)), (rb.transform.position, rb2.transform.position), "Игрок должен вернуться на начальную позицию");
    }

    [UnityTest]
    public IEnumerator PlayerRespawnsOnDangerLava()
    {
        GameObject danger = new GameObject();
        danger.tag = "lava";
        var collider = danger.AddComponent<BoxCollider2D>();

        triggers2.SendMessage("OnTriggerEnter2D", collider);
        yield return new WaitForSeconds(0.0f);
        Assert.AreEqual((new Vector3(-14, -7, -0.5f), new Vector3(-13, -7, -0.5f)), (rb2.transform.position, rb.transform.position), "Игрок должен вернуться на начальную позицию");
    }

    [UnityTest]
    public IEnumerator PlayerRespawnsOnDangerWater()
    {
        GameObject danger = new GameObject();
        danger.tag = "water";
        var collider = danger.AddComponent<BoxCollider2D>();

        triggers.SendMessage("OnTriggerEnter2D", collider);
        yield return new WaitForSeconds(0.0f);
        Assert.AreEqual((new Vector3(-14, -7, -0.5f), new Vector3(-13, -7, -0.5f)), (rb.transform.position, rb2.transform.position), "Игрок должен вернуться на начальную позицию");
    }

    [UnityTest]
    public IEnumerator FireGemCollected()
    {
        int initialSuperGems = score.fireGems;

        GameObject gem = new GameObject();
        gem.tag = "firegem";
        Collider2D collider = gem.AddComponent<BoxCollider2D>();
        gem.layer = LayerMask.NameToLayer("gem");
        collider.isTrigger = true;

        player.transform.position = gem.transform.position;

        yield return new WaitForSeconds(0.1f);
        Debug.Log(score.fireGems);
        Assert.AreEqual(initialSuperGems + 1, score.fireGems);
    }

    [UnityTest]
    public IEnumerator WaterGemCollected()
    {
        int initialSuperGems = score.waterGems;

        GameObject gem = new GameObject();
        gem.tag = "watergem";
        Collider2D collider = gem.AddComponent<BoxCollider2D>();
        gem.layer = LayerMask.NameToLayer("gem");
        collider.isTrigger = true;
        player2.transform.position = gem.transform.position;

        yield return new WaitForSeconds(0.1f);
        Debug.Log(score.waterGems);
        Assert.AreEqual(initialSuperGems + 1, score.waterGems);
    }

    [UnityTest]
    public IEnumerator EnteringDoorSetsInDoorTrue()
    {
        GameObject door = new GameObject();
        door.tag = "ldoor";
        var collider = door.AddComponent<BoxCollider2D>();

        triggers.SendMessage("OnTriggerStay2D", collider);

        yield return new WaitForSeconds(0.1f);

        Assert.IsTrue(triggers.inDoor, "Игрок должен находиться в двери");
    }

    [UnityTest]
    public IEnumerator ExitingDoorSetsInDoorFalse()
    {
        GameObject door = new GameObject();
        door.tag = "ldoor";
        var collider = door.AddComponent<BoxCollider2D>();

        triggers.SendMessage("OnTriggerExit2D", collider);

        yield return new WaitForSeconds(0.1f);

        Assert.IsFalse(triggers.inDoor, "Игрок должен выйти из двери");
    }
}
