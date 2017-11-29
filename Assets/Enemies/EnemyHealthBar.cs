﻿using Assets.Enemies;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    RawImage healthBarRawImage = null;
    Enemy enemy = null;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponentInParent<Enemy>(); // Different to way player's health bar finds player
        healthBarRawImage = GetComponent<RawImage>();

        enemy.OnPlayerChangesHealth += UpdateHealthBar;
    }

    // Update is called once per frame
    void UpdateHealthBar(float healthAsPercentage)
    {
        float xValue = -(healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
