using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class scoreTest
{
    private GameObject scoreObject;
    private score scoreScript;

    [SetUp]
    public void Setup()
    {
        scoreObject = new GameObject();
        scoreScript = scoreObject.AddComponent<score>();

        GameObject fireGemText = new GameObject();
        fireGemText.AddComponent<TextMeshProUGUI>();
        scoreScript.GetType().GetField("firegems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(scoreScript, fireGemText.GetComponent<TextMeshProUGUI>());

        GameObject waterGemText = new GameObject();
        waterGemText.AddComponent<TextMeshProUGUI>();
        scoreScript.GetType().GetField("watergems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(scoreScript, waterGemText.GetComponent<TextMeshProUGUI>());

        GameObject superGemText = new GameObject();
        superGemText.AddComponent<TextMeshProUGUI>();
        scoreScript.GetType().GetField("supergems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(scoreScript, superGemText.GetComponent<TextMeshProUGUI>());
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(scoreObject);
    }

    [UnityTest]
    public IEnumerator AddFireGem_IncreasesCount()
    {
        scoreScript.AddFireGem();
        yield return new WaitForSeconds(0.0f);
        Assert.AreEqual(1, scoreScript.fireGems);
    }

    [UnityTest]
    public IEnumerator AddWaterGem_IncreasesCount()
    {
        scoreScript.AddWaterGem();
        yield return new WaitForSeconds(0.0f);
        Assert.AreEqual(1, scoreScript.waterGems);
    }

    [UnityTest]
    public IEnumerator AddSuperGem_IncreasesCount()
    {
        scoreScript.AddSuperGem();
        yield return new WaitForSeconds(0.0f);
        Assert.AreEqual(1, scoreScript.superGems);
    }

    [UnityTest]
    public IEnumerator UI_Updates_When_Gems_Are_Added()
    {
        scoreScript.AddFireGem();
        scoreScript.AddWaterGem();
        scoreScript.AddSuperGem();

        TextMeshProUGUI fireGemText = (TextMeshProUGUI)scoreScript.GetType().GetField("firegems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(scoreScript);
        TextMeshProUGUI waterGemText = (TextMeshProUGUI)scoreScript.GetType().GetField("watergems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(scoreScript);
        TextMeshProUGUI superGemText = (TextMeshProUGUI)scoreScript.GetType().GetField("supergems", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(scoreScript);

        yield return new WaitForSeconds(0.0f);

        Assert.AreEqual("Fire Gems: 1", fireGemText.text);
        Assert.AreEqual("Water Gems: 1", waterGemText.text);
        Assert.AreEqual("Super Gems: 1", superGemText.text);
    }
}
