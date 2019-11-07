using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class PlayerPacket : MonoBehaviour
    {
        public static PlayerPacket[] Instances = new PlayerPacket[3];

        public int player;
        public float Velocity;

        HackableObject HackTarget;
        NetworkNode MoveTarget;
        Transform TargetObj;

        bool Moving = false;
        bool Hacking = false;


        public void MoveTo(NetworkNode target, Transform transform)
        {
            Moving = true;
            Hacking = false;
            MoveTarget = target;
            TargetObj = transform;

            target.MoveIn(player);
        }
        public void MoveTo(NetworkNode target, Transform transform, float velocity)
        {
            MoveTo(target, transform);
            Velocity = velocity;
        }
        public void HackTo(HackableObject target, Transform transform)
        {
            Moving = true;
            Hacking = true;
            HackTarget = target;
            TargetObj = transform;

            target.HackIn(player);
        }
        public void HackTo(HackableObject target, Transform transform, float velocity)
        {
            HackTo(target, transform);
            Velocity = velocity;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

