using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    public int fireGems;
    public int waterGems;
    public int superGems;

    [Header("������ �� UI ��������")]
    [SerializeField] private TextMeshProUGUI firegems;
    [SerializeField] private TextMeshProUGUI watergems;
    [SerializeField] private TextMeshProUGUI supergems;

    void Start()
    {
        UpdateUI(); // ��������� ����� ��� ������
    }

    public void AddFireGem()
    {
        fireGems++;
        UpdateUI();
    }

    public void AddWaterGem()
    {
        waterGems++;
        UpdateUI();
    }

    public void AddSuperGem()
    {
        superGems++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (firegems) firegems.text = "Fire Gems: " + fireGems;
        if (watergems) watergems.text = "Water Gems: " + waterGems;
        if (supergems) supergems.text = "Super Gems: " + superGems;
    }
}
