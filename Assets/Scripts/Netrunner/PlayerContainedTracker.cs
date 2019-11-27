using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner
{
    public class PlayerContainedTracker : MonoBehaviour
    {
        public int playercnt = 0;
        public virtual void PlayerIn()
        {
            playercnt++;
        }
        public void PlayerOut()
        {
            playercnt--;
        }
    }
}

