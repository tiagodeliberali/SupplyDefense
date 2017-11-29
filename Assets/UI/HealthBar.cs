﻿using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class HealthBar : MonoBehaviour
{
    RawImage healthBarRawImage;
    BasePlayer player;

    void Start()
    {
        player = GetComponentInParent<BasePlayer>();
        healthBarRawImage = GetComponent<RawImage>();

        UpdateHealthBar(player.HealthAsPercentage);
        player.OnPlayerChangesHealth += UpdateHealthBar;
    }

    void UpdateHealthBar(float healthAsPercentage)
    {
        float xValue = -(healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}