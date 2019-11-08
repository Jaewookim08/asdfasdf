using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netrunner.Network;

namespace Netrunner{
    public class PlayerInitializer : MonoBehaviour {
        public int PlayerNum;

        // Start is called before the first frame update
        void Start() {
            HackableObject obj = GetComponent<HackableObject>();
            if(obj!=null) obj.HackIn(PlayerNum);
            NetworkNode node = GetComponent<NetworkNode>();
            if (node != null) node.MoveIn(PlayerNum);
        }
    }
}
