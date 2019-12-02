using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;

namespace Netrunner.ModuleComponents
{
    public class GrabAction : ModuleAction
    {
        [SerializeField] private float grabDistance = 2f;
        [SerializeField] private float grabHeight = 2f;
        [SerializeField] private FixedJoint2D grabJoint;
        public bool IsGrabbing => _mGrabbing != null;

        private ChangeFacingDirectionAction _mFaceAction;
        private Grabbable _mGrabbing;
        private float _massRecord;

        private void Start()
        {
            _mFaceAction = GetComponent<ChangeFacingDirectionAction>();
            grabJoint.enabled = false;
        }

        private void Update()
        {
            if (IsGrabbing) {
                if (!GameInput.GetKey(player, ActionKey)) {
                    UnGrab();
                }
            }
            else if (GameInput.GetKey(player, ActionKey)) {
                Debug.Log("asdf2");
                var found = FindGrabbableObject();
                Debug.Log(found);
                if (found == null) return;
                Grab(found);
            }
        }

        private void OnDisable()
        {
            UnGrab();
        }

        private void Grab(Grabbable obj)
        {
            _mGrabbing = obj;
            grabJoint.enabled = true;
            grabJoint.connectedBody = _mGrabbing.Rigidbody;
            _mGrabbing.isGrabbed = true;
            _massRecord = _mGrabbing.Rigidbody.mass;
            _mGrabbing.Rigidbody.mass = 0.0001f;
        }

        private void UnGrab()
        {
            if (_mGrabbing == null) return;
            _mGrabbing.isGrabbed = false;
            grabJoint.enabled = false;
            _mGrabbing.Rigidbody.mass = _massRecord;
            _mGrabbing = null;
        }

        private Grabbable FindGrabbableObject()
        {
            var pos = (Vector2) transform.position;
            var checkingRect = new Rect(
                pos.x + (grabDistance/2-0.1f) * GetFacingDir() -grabDistance/2, pos.y - grabHeight, grabDistance, grabHeight * 2);
            // 모든 Grabbable 중 지정한 범위 안에 있는 것들을 고르고, 그 중 제일 가까운 것을 고름.
            var found = Grabbable.All
                .Where(grabbable => checkingRect.Contains(grabbable.Position) && grabbable.gameObject != gameObject)
                .ToList();
            if (found.Count == 0)
                return null;
            var closest = found.Aggregate((g1, g2) =>
                (Vector2.Distance(g1.Position, pos) < Vector2.Distance(g2.Position, pos)) ? g1 : g2);
            return closest;
        }


        private int GetFacingDir() => (_mFaceAction != null) ? _mFaceAction.FacingDir : 0;
    }
}