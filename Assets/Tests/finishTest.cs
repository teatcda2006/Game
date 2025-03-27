using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class finishTest
{
    private GameObject finishObject;
    private finish finishScript;

    private GameObject player;
    private Triggers triggers;

    private GameObject player2;
    private Triggers triggers2;

    [SetUp]
    public void Setup()
    {
        finishObject = new GameObject();
        finishScript = finishObject.AddComponent<finish>();

        player = Object.Instantiate(Resources.Load<GameObject>("prefab/player"));
        triggers = player.GetComponent<Triggers>();

        player2 = Object.Instantiate(Resources.Load<GameObject>("prefab/player"));
        triggers2 = player2.GetComponent<Triggers>();

        finishScript.GetType().GetField("plLava", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(finishScript, triggers);

        finishScript.GetType().GetField("plWater", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(finishScript, triggers2);
    }

    [UnityTest]
    public IEnumerator Scene_Reloads_When_Both_Players_Are_In_Door()
    {
        int initialSceneIndex = SceneManager.GetActiveScene().buildIndex;

        triggers.inDoor = true;
        triggers2.inDoor = true;

        yield return new WaitForSeconds(1.0f);

        Assert.AreNotEqual(initialSceneIndex, SceneManager.GetActiveScene().buildIndex, "—цена должна перезагрузитьс€");
    }
}
