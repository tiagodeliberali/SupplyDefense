using System;
using UnityEngine;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour
{
    private Text uiText;

    private int totalEnemies;
    private int currentEnemies;

    private int totalHouses;
    private int currentHouses;

    private bool isGameOver = false;

    void Start ()
    {
        uiText = GetComponentInChildren<Text>();

        UpdateUIText();
    }

    private void UpdateUIText()
    {
        uiText.text = string.Format("Total houses: {0}/{1}\nTotal enemies: {2}/{3}", currentHouses, totalHouses, currentEnemies, totalEnemies);

        if (isGameOver)
            uiText.text += "\nGame Over!";
    }

    internal void PlayerDied()
    {
        isGameOver = true;
        UpdateUIText();
    }

    public void AddHouses(int houses)
    {
        totalHouses += houses;
        currentHouses += houses;

        UpdateUIText();
    }

    public void AddEnemies(int enemies)
    {
        totalEnemies += enemies;
        currentEnemies += enemies;

        UpdateUIText();
    }

    public void RemoveHouses(int houses)
    {
        currentHouses -= houses; ;
        UpdateUIText();
    }

    public void RemoveEnemies(int enemies)
    {
        currentEnemies -= enemies; ;
        UpdateUIText();
    }
}
