﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class NetworkNode : MonoBehaviour
    {
        const float PacketVelocity = 10f;

        public bool[] Player = new bool[2];
        /// <summary>
        /// 0 1 2 3 -> R U L D
        /// </summary>
        public NetworkNode[] Connections = new NetworkNode[4];

        // Update is called once per frame
        void Update()
        {
            if (Player[1])
            {
                if (Connections[0] != null && GameInput.GetKeyDown(1, GameInput.Key.Right)) MovePlayer(1, 0);
                else if (Connections[1] != null && GameInput.GetKeyDown(1, GameInput.Key.Up)) MovePlayer(1, 1);
                else if (Connections[2] != null && GameInput.GetKeyDown(1, GameInput.Key.Left)) MovePlayer(1, 2);
                else if (Connections[3] != null && GameInput.GetKeyDown(1, GameInput.Key.Down)) MovePlayer(1, 3);
            }
            if (Player[2])
            {
                if (Connections[0] != null && GameInput.GetKeyDown(2, GameInput.Key.Right)) MovePlayer(2, 0);
                else if (Connections[1] != null && GameInput.GetKeyDown(2, GameInput.Key.Up)) MovePlayer(2, 1);
                else if (Connections[2] != null && GameInput.GetKeyDown(2, GameInput.Key.Left)) MovePlayer(2, 2);
                else if (Connections[3] != null && GameInput.GetKeyDown(2, GameInput.Key.Down)) MovePlayer(2, 3);
            }
        }

        private void MovePlayer(int player, int direction)
        {
            PlayerPacket.Instances[player].MoveTo(Connections[direction], Connections[direction].transform, PacketVelocity);
            Player[player] = false;
        }

        public void MoveIn(int player)
        {
            Player[player] = true;
        }
    }

}