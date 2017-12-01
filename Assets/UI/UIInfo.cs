using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInfo : MonoBehaviour
{
    private Text uiText;

    private Text UIText {
        get {
            if (uiText == null)
                uiText = GetComponentInChildren<Text>();

            return uiText;
        }
    }

    private int totalEnemies;
    private int currentEnemies;

    private int totalHouses;
    private int currentHouses;

    private bool isGameOver = false;

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void UpdateUIText()
    {
        UIText.text = string.Format("Total houses: {0}/{1}\nTotal enemies: {2}/{3}", currentHouses, totalHouses, currentEnemies, totalEnemies);

        if (isGameOver)
            UIText.text += "\nGame Over! Press G to start";
    }

    public void PlayerDied()
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
        currentHouses = Mathf.Clamp(currentHouses - houses, 0, totalHouses);

        if (currentHouses == 0)
            isGameOver = true;

        UpdateUIText();
    }

    public void RemoveEnemies(int enemies)
    {
        currentEnemies = Mathf.Clamp(currentEnemies - enemies, 0, totalEnemies);

        if (currentEnemies == 0)
            isGameOver = true;

        UpdateUIText();
    }
}
