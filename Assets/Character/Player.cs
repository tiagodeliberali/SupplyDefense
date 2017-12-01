using Assets.Utils;
using UnityEngine;

namespace Assets.Character
{
    public class Player : BasePlayer
    {
        private UIInfo uiInfo;

        private void Start()
        {
            uiInfo = GameObject.FindGameObjectWithTag("UIInfo").GetComponent<UIInfo>();
        }

        public override void Die()
        {
            uiInfo.PlayerDied();
        }
    }
}
