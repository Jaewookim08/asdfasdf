using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.UI;

namespace Netrunner.Network
{
    public class NodeAction : MonoBehaviour
    {
        public Sprite Icon;

        public NetworkNode Node { get; private set; }

        protected bool[] player => Node.Player;

        [SerializeField] protected GameInput.Key ActionKey;


        /// <summary>
        /// Called when player gets in the module
        /// </summary>
        /// <param name="player"></param>
        public void Init(int player)
        {
            Node = GetComponent<NetworkNode>();
            
            var keys = GameInput.GetRealKeys(player, ActionKey);
            if (keys.Count>0) 
                UIManager.current.AddAbility(Icon, keys[0]);
        }
    }

}
