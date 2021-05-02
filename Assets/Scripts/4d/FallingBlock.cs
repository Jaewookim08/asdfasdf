using System.Collections.Generic;
using UnityEngine;

namespace _4d
{
    public class FallingBlock: MonoBehaviour
    {
        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (!TimeRunningState.IsRewindOn && CheckAbove())
            {
                Debug.Log("asdf");
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        private bool CheckAbove()
        {
            var temp = _boxCollider.enabled;
            _boxCollider.enabled = false;
            
            Physics2D.queriesHitTriggers = false;
            var grounded = Physics2D.BoxCast(
                transform.TransformPoint(
                    _boxCollider.offset + new Vector2(0, _boxCollider.size.y / 2 - 0.01f)),
                new Vector2(transform.TransformVector(_boxCollider.size).x - 0.08f, 0.01f),
                0, Vector2.up, 0.1f, Physics2D.GetLayerCollisionMask(gameObject.layer));
            Physics2D.queriesHitTriggers = true;
            
            _boxCollider.enabled = temp;
            return grounded;
        }

        private readonly Stack<float> _memory = new Stack<float>();
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider;
    }
}