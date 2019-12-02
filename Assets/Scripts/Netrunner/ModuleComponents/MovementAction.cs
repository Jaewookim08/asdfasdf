using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Netrunner.ModuleComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MovementAction : ModuleAction
    {
        [SerializeField] private float moveForce = 40f;
        [SerializeField] private float moveSpeed = 6f;

        private Rigidbody2D _mRigidbody2D;
        // Grab 상태면 움직이지 않아야.
        private Grabbable _grabbable;
        // Start is called before the first frame update
        private void Start()
        {
            _mRigidbody2D = GetComponent<Rigidbody2D>();
            _grabbable = GetComponent<Grabbable>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (_grabbable!=null && _grabbable.isGrabbed) return;
            var vel = _mRigidbody2D.velocity;
            if (Math.Abs(vel.x) > moveSpeed) {
                vel.x = moveSpeed * (vel.x > 0 ? 1 : -1);
            }

            _mRigidbody2D.velocity = vel;
            _mRigidbody2D.AddForce(new Vector2(GameInput.GetHorizontal(player) * moveForce, 0));
        }
        

        public override float GetSuspicion()
        {
            return _mRigidbody2D.velocity.magnitude;
        }
    }
}