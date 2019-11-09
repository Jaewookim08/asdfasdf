using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.Network;

namespace Netrunner
{
    public class PlayerPacketInitializer : MonoBehaviour
    {
        public PlayerPacket p1, p2;
        /// <summary>
        /// initial player position
        /// </summary>
        public Transform t1, t2;

        void Start()
        {
            PlayerPacket.Instances[1] = p1;
            PlayerPacket.Instances[2] = p2;
            p1.transform.SetParent(null);
            p2.transform.SetParent(null);

            HackableObject h;
            NetworkNode n;
            h = t1.GetComponent<HackableObject>(); if (h != null) p1.HackTo(t1.position, h, t1);
            h = t2.GetComponent<HackableObject>(); if (h != null) p2.HackTo(t2.position, h, t2);
            n = t1.GetComponent<NetworkNode>(); if (n != null) p1.MoveTo(t1.position, n, t1);
            n = t2.GetComponent<NetworkNode>(); if (n != null) p2.MoveTo(t2.position, n, t2);

            Destroy(gameObject);
        }
    }
}

