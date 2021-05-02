using UnityEngine;

namespace Netrunner.ModuleComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpAction : ModuleAction
    {
        [SerializeField] private float impulse = 8f;

        public void Jump() => Jump(impulse);

        public void Jump(float _impulse)
        {
            var vel = _mRigidbody2D.velocity;
            vel.y = 0;
//            vel.y = impulse;
            _mRigidbody2D.velocity = vel;
            _mRigidbody2D.AddForce(new Vector2(0, _impulse), ForceMode2D.Impulse);
        }


        private Rigidbody2D _mRigidbody2D;
        private BoxCollider2D _mBoxCollider2D;

        // Start is called before the first frame update
        private void Start()
        {
            _mRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _mBoxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            if (GameInput.GetKey(player, ActionKey) && _mRigidbody2D.velocity.y < 0.01f && CheckOnGround())
                Jump();
        }


        private bool CheckOnGround()
        {
            var temp = _mBoxCollider2D.enabled;
            _mBoxCollider2D.enabled = false;
            
            Physics2D.queriesHitTriggers = false;
            var grounded = Physics2D.BoxCast(
                transform.TransformPoint(
                    _mBoxCollider2D.offset - new Vector2(0, _mBoxCollider2D.size.y / 2 - 0.01f)),
                new Vector2(transform.TransformVector(_mBoxCollider2D.size).x - 0.08f, 0.01f),
                0, Vector2.down, 0.1f, Physics2D.GetLayerCollisionMask(gameObject.layer));
            Physics2D.queriesHitTriggers = true;
            _mBoxCollider2D.enabled = temp;
            return grounded;
        }
        
    }
}