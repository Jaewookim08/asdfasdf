using Netrunner;
using Netrunner.ModuleComponents;
using UnityEngine;

namespace _4d
{
    public class RewindAction: ModuleAction
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.J))
            {
                TimeRunningState.IsRewindOn = true;
            }
            if (Input.GetKey(KeyCode.K))
            {
                TimeRunningState.IsRewindOn = false;
            }
        }
    }
}