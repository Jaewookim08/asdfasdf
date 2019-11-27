using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Netrunner.Network
{
    public class NetworkNode : MonoBehaviour
    {
        public PlayerContainedTracker tracker;
        const float PacketVelocity = 20f;
        public int MaxUse = -1;
        
        [HideInInspector]
        public bool[] Player = new bool[3];
        [HideInInspector]
        public bool[] Movable = { true, true, true };

        public List<NodeAction> Actions = new List<NodeAction>();


        /// <summary>
        /// 0 1 2 3 -> R U L D
        /// </summary>
        public NetworkNode[] Connections = new NetworkNode[4];

        // Update is called once per frame
        void Update()
        {
            if (Player[1] && Movable[1])
            {
                if (Connections[0] != null && Connections[0].MoveAvailable() && GameInput.GetKeyDown(1, GameInput.Key.Right)) MovePlayer(1, 0);
                else if (Connections[1] != null && Connections[1].MoveAvailable() && GameInput.GetKeyDown(1, GameInput.Key.Up)) MovePlayer(1, 1);
                else if (Connections[2] != null && Connections[2].MoveAvailable() && GameInput.GetKeyDown(1, GameInput.Key.Left)) MovePlayer(1, 2);
                else if (Connections[3] != null && Connections[3].MoveAvailable() && GameInput.GetKeyDown(1, GameInput.Key.Down)) MovePlayer(1, 3);
            }
            if (Player[2] && Movable[2])
            {
                if (Connections[0] != null && Connections[0].MoveAvailable() && GameInput.GetKeyDown(2, GameInput.Key.Right)) MovePlayer(2, 0);
                else if (Connections[1] != null && Connections[1].MoveAvailable() && GameInput.GetKeyDown(2, GameInput.Key.Up)) MovePlayer(2, 1);
                else if (Connections[2] != null && Connections[2].MoveAvailable() && GameInput.GetKeyDown(2, GameInput.Key.Left)) MovePlayer(2, 2);
                else if (Connections[3] != null && Connections[3].MoveAvailable() && GameInput.GetKeyDown(2, GameInput.Key.Down)) MovePlayer(2, 3);
            }
        }

        protected virtual void MovePlayer(int player, int direction)
        {
            PlayerPacket.Instances[player].MoveTo(transform.position, Connections[direction], Connections[direction].transform, PacketVelocity);
            Connections[direction].MoveDeclare(player);
            MoveOut(player);
        }

        public void MoveOut(int player)
        {
            if (tracker != null) tracker.PlayerOut();
            Player[player] = false;
            if (!(Player[1] || Player[2]))
                foreach (NodeAction action in Actions)
                    action.enabled = false;
        }

        public bool MoveAvailable()
        {
            if (MaxUse == -1 || MaxUse > 0) return true;
            else return false;
        }

        public void MoveDeclare(int player)
        {
            Debug.Log("movdec");
            if (MaxUse > 0) MaxUse--;
        }

        public virtual void MoveIn(int player)
        {
            if (tracker != null) tracker.PlayerIn();
            Player[player] = true;
            UI.UIManager.current.ChangeAbilities(player);
            foreach(NodeAction action in Actions)
            {
                action.enabled = true;
                action.Init(player);
            }
        }


        [ContextMenu("Initialize")]
        void EditorInitialize()
        {
            

            List<NodeAction> actions = new List<NodeAction>(GetComponents<NodeAction>());
            Actions.Clear();
            foreach (NodeAction a in actions)
                Actions.Add(a);
            List<Selection> s = new List<Selection>(GetComponentsInChildren<Selection>());
            if (Actions.Any(a=>a is NetworkHackAction))
            {
                while (s.Count < 2)
                {
                    GameObject g = Instantiate(Resources.Load<GameObject>("Builder/Selection"), transform);
                    s.Add(g.GetComponent<Selection>());
                }
            }
            foreach (NodeAction a in Actions)
            {
                if (a is NetworkHackAction) ((NetworkHackAction)a).selections = s.ToArray();
                a.enabled = false;
            }
        }
        [ContextMenu("Initialize", true)]
        bool EditorValidate()
        {
            return !(this is NetworkConsole);
        }
    }

}