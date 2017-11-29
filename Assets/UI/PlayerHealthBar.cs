using Assets.Character;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class PlayerHealthBar : MonoBehaviour
{
    RawImage healthBarRawImage;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        healthBarRawImage = GetComponent<RawImage>();

        player.OnPlayerChangesHealth += UpdateHealthBar;
    }

    void UpdateHealthBar(float healthAsPercentage)
    {
        float xValue = -(healthAsPercentage / 2f) - 0.5f;
        healthBarRawImage.uvRect = new Rect(xValue, 0f, 0.5f, 1f);
    }
}
