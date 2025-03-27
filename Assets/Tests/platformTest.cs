using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class platformTest
{
    private GameObject player;
    private GameObject player2;

    private GameObject platformObject;
    private platform platformScript;
    private GameObject buttonObject;
    private button buttonScript;

    [SetUp]
    public void Setup()
    {
        player = Object.Instantiate(Resources.Load<GameObject>("prefab/player"));
        player.tag = "player";

        player2 = Object.Instantiate(Resources.Load<GameObject>("prefab/player"));
        player2.tag = "player";

        platformObject = Object.Instantiate(Resources.Load<GameObject>("prefab/platform"));
        platformScript = platformObject.GetComponent<platform>();

        platformScript.GetType().GetField("isNotActiveOnStart", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(platformScript, false);

        buttonObject = Object.Instantiate(Resources.Load<GameObject>("prefab/button"));
        buttonScript = buttonObject.GetComponent<button>();

        buttonScript.GetType().GetField("platform", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(buttonScript, platformScript);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player);
        Object.Destroy(player2);
        Object.Destroy(platformObject);
        Object.Destroy(buttonObject);
    }

    [UnityTest]
    public IEnumerator Platform_Activates_When_Player_Steps_On_Button()
    {
        Assert.IsFalse(platformObject.activeSelf, "Платформа должна быть неактивной в начале");

        buttonScript.SendMessage("OnTriggerEnter2D", player.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.0f);

        Assert.IsTrue(platformObject.activeSelf, "Платформа должна активироваться");
    }

    [UnityTest]
    public IEnumerator Platform_Deactivates_When_Player_Steps_Off_Button()
    {
        buttonScript.SendMessage("OnTriggerEnter2D", player.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.0f);
        Assert.IsTrue(platformObject.activeSelf, "Платформа должна быть активной");

        buttonScript.SendMessage("OnTriggerExit2D", player.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.0f);

        Assert.IsFalse(platformObject.activeSelf, "Платформа должна быть неактивной после ухода игрока");
    }

    [UnityTest]
    public IEnumerator Platform_Stays_Active_With_Multiple_Players()
    {
        buttonScript.SendMessage("OnTriggerEnter2D", player.GetComponent<Collider2D>());
        buttonScript.SendMessage("OnTriggerEnter2D", player2.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.0f);

        Assert.IsTrue(platformObject.activeSelf, "Платформа должна быть активной");

        buttonScript.SendMessage("OnTriggerExit2D", player.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.0f);

        Assert.IsTrue(platformObject.activeSelf, "Платформа должна оставаться активной, пока один игрок ещё на кнопке");

        buttonScript.SendMessage("OnTriggerExit2D", player2.GetComponent<Collider2D>());
        yield return new WaitForSeconds(0.0f);

        Assert.IsFalse(platformObject.activeSelf, "Платформа должна деактивироваться после ухода всех игроков");
    }
}
