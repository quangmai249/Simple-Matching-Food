using System.Collections;
using UnityEngine;

namespace Assets.Scrips.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}