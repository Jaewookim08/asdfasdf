using System.Collections.Generic;
using UnityEngine;

namespace _4d
{
    public class RewindableObject : MonoBehaviour
    {
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private struct RigidbodyCapture
        {
            public Vector2 Position;
            public float Rotation;
            public Vector2 Velocity;
            public float AngularVelocity;
        }

        private void FixedUpdate()
        {
            if (TimeRunningState.IsRewindOn)
            {
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                var prevState = _memory.Count == 0 ? _lastLoaded : _lastLoaded = _memory.Pop();
                _rigidbody.velocity = prevState.Velocity;
                _rigidbody.angularVelocity = prevState.AngularVelocity;
                _rigidbody.MoveRotation(prevState.Rotation);
                _rigidbody.MovePosition(prevState.Position);
            }
            else
            {
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _memory.Push(new RigidbodyCapture
                {
                    Position = _rigidbody.position, Rotation = _rigidbody.rotation, Velocity = _rigidbody.velocity,
                    AngularVelocity = _rigidbody.angularVelocity
                });
            }
        }

        private RigidbodyCapture _lastLoaded;
        private readonly Stack<RigidbodyCapture> _memory = new Stack<RigidbodyCapture>();
        private Rigidbody2D _rigidbody;
    }
}