using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Netrunner.ModuleComponents{
    [RequireComponent(typeof (Rigidbody2D))]
    public class MovementAction : ModuleAction {
        [SerializeField] private float moveAcceleration = 10f;
        [SerializeField] private float moveSpeed = 6f;
        private int player => Module.PlayerInside;
        private Rigidbody2D m_Rigidbody2D;
        
        // Start is called before the first frame update
        private void Start() {
            m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update() {
//            var vel = m_Rigidbody2D.velocity;
//            vel += new Vector2(GameInput.GetHorizontalAxis(player) * moveAcceleration * Time.deltaTime, 0);
//            if (Math.Abs(vel.x) > moveSpeed) {
//                vel.x = Math.Abs(vel.x) * (vel.x > 0 ? 1 : -1);
//            }
                
//            m_Rigidbody2D.velocity = vel;
//            m_Rigidbody2D.AddForce(new Vector2(GameInput.GetHorizontalAxis(player) * moveAcceleration, 0));
            m_Rigidbody2D.velocity = (new Vector2(GameInput.GetHorizontalIgnoreDirection(player) * moveSpeed, m_Rigidbody2D.velocity.y));
        }
        
    }
}
