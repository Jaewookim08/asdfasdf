using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netrunner.Network
{
    public class PlayerPacket : MonoBehaviour
    {
        public static PlayerPacket[] Instances = new PlayerPacket[3];

        public ParticleSystem particle;
        public ParticleSystem particleSwirl;
        public ParticleSystem particleSwirl2;

        public int player;
        public float Velocity;

        HackableObject HackTarget;
        NetworkNode MoveTarget;
        Transform TargetObj;

        bool Moving = false;
        bool Hacking = false;

        private void Start()
        {
            particleSwirl.transform.SetParent(null);
        }

        public void MoveTo(Vector3 startPos, NetworkNode target, Transform transform)
        {
            Moving = true;
            Hacking = false;
            MoveTarget = target;
            TargetObj = transform;
            this.transform.position = startPos;
            particle.Play(false);
            particleSwirl2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        public void MoveTo(Vector3 startPos, NetworkNode target, Transform transform, float velocity)
        {
            MoveTo(startPos, target, transform);
            Velocity = velocity;
        }
        public void HackTo(Vector3 startPos, HackableObject target, Transform transform)
        {
            Moving = true;
            Hacking = true;
            HackTarget = target;
            TargetObj = transform;
            this.transform.position = startPos;
            particle.Play(false);
            particleSwirl2.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        public void HackTo(Vector3 startPos, HackableObject target, Transform transform, float velocity)
        {
            HackTo(startPos, target, transform);
            Velocity = velocity;
        }
        // Update is called once per frame
        void Update()
        {
            if (!Moving) return;

            transform.position = Vector3.MoveTowards(transform.position, TargetObj.position, Velocity * Time.deltaTime);
            if(transform.position == TargetObj.position)
            {
                if (Hacking)
                {
                    HackTarget.HackIn(player);
                    particleSwirl.transform.position = transform.position;
                    particleSwirl.Play();
                }
                else
                {
                    MoveTarget.MoveIn(player);
                    particleSwirl2.Play();
                }
                Moving = false;
                particle.Stop(false);
            }
        }
    }
}

