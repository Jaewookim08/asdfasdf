using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Netrunner.ModuleComponents {
    [RequireComponent(typeof (Rigidbody2D))]
    public class JumpAction: ModuleAction {
        [SerializeField] private float jumpSpeed = 8f;
        [SerializeField] private LayerMask groundLayer;
        public void Jump() {
            Jump(jumpSpeed);
        }
        public void Jump(float speed) {
            // determine if on ground
            var grounded = Physics2D.BoxCast(
                 transform.TransformPoint(
                     m_BoxCollider2D.offset-new Vector2(0,m_BoxCollider2D.size.y/2 - 0.01f)),
                 new Vector2(transform.TransformVector(m_BoxCollider2D.size).x-0.08f, 0.01f),
                0, Vector2.down, 0.1f, groundLayer);
            
            if (!grounded) return;
            
            var vec = m_Rigidbody2D.velocity;
            vec.y = speed;
            m_Rigidbody2D.velocity = vec;
        }


        private Rigidbody2D m_Rigidbody2D;
        private BoxCollider2D m_BoxCollider2D;
        
        // Start is called before the first frame update
        private void Start() {
            m_Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            m_BoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        }
        
        private void Update() {
            if (GameInput.GetKey(player, ActionKey))// && Math.Abs(m_Rigidbody2D.velocity.y) < 0.01f)
                Jump();
        }
        
    }
}