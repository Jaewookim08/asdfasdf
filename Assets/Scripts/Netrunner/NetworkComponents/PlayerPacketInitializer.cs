using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class PlayerPacketInitializer : MonoBehaviour
    {
        public PlayerPacket p1, p2;

        void Start()
        {
            PlayerPacket.Instances[1] = p1;
            PlayerPacket.Instances[2] = p2;
            p1.transform.SetParent(null);
            p2.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}

