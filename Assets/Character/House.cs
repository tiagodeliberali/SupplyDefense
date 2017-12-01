using Assets.Utils;
using UnityEngine;

public class House : BasePlayer
{
    private UIInfo uiInfo;

    private void Start()
    {
        uiInfo = GameObject.FindGameObjectWithTag("UIInfo").GetComponent<UIInfo>();
        uiInfo.AddHouses(1);
    }

    public override void Die()
    {
        uiInfo.RemoveHouses(1);
    }
}
