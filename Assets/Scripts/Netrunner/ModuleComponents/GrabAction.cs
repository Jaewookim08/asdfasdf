
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
        [SerializeField] private Vector2 throwVector = new Vector2(3f, 1f);
        public bool IsGrabbing => _grabbing != null;

        private ChangeFacingDirectionAction _faceAction;
        private CombineAction _combineAction;
        private Grabbable _grabbing;
        private float _massRecord;


        private void Start()
        {
            _faceAction = GetComponent<ChangeFacingDirectionAction>();
            _combineAction = GetComponent<CombineAction>();
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
                var found = FindGrabbableObject();
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
            _grabbing = obj;
            grabJoint.enabled = true;
            grabJoint.connectedBody = _grabbing.Rigidbody;
            _grabbing.isGrabbed = true;
            _massRecord = _grabbing.Rigidbody.mass;
            _grabbing.Rigidbody.mass = 0.0001f;
        }

        private void UnGrab()
        {
            if (_grabbing == null) return;
            _grabbing.isGrabbed = false;
            grabJoint.enabled = false;
            _grabbing.Rigidbody.mass = _massRecord;
            Throw(_grabbing);
            _grabbing = null;
        }

        private void Throw(Grabbable obj)
        {
            Rigidbody2D body = obj.Rigidbody;
            var vec = throwVector;
            vec.x *= GetFacingDir();
            body.AddForce(vec, ForceMode2D.Impulse);
        }

        private Grabbable FindGrabbableObject()
        {
            var pos = (Vector2) transform.position;
            var checkingRect = new Rect(
                pos.x + (grabDistance / 2 - 0.1f) * GetFacingDir() - grabDistance / 2, pos.y - grabHeight, grabDistance,
                grabHeight * 2);
            // ?????? Grabbable ??? ????????? ?????? ?????? ?????? ????????? ?????????, ??? ??? ?????? ????????? ?????? ??????.
            var found = Grabbable.All
                // ?????? ?????? ?????????
                .Where(grabbable => checkingRect.Contains(grabbable.Position) && grabbable.gameObject != gameObject)
                // ????????? ???????????? ?????? ?????????
                .Where(grabbable =>
                    _combineAction == null ||
                    ((_combineAction.Lower == null || _combineAction.Lower.gameObject != grabbable.gameObject) &&
                    (_combineAction.Upper == null || _combineAction.Upper.gameObject != grabbable.gameObject)))
                // ??????????????? ?????? ?????? ?????? ?????????
                .Where(grabbable => grabbable.isGrabbed == false)
                // ???????????? ?????? ????????? ?????????????????? ?????????(????????? ????????? ??? ??? ????????? ????????? ????????? ?????? ???????????????)
                .Where(grabbable => grabbable.CombineAction==null || grabbable.CombineAction.Lower == null)
                .ToList();
            if (found.Count == 0)
                return null;

            // ????????? ?????? ?????? ??? ?????? ????????? ??? ??????.
            var closest = found.Aggregate((g1, g2) =>
                (Vector2.Distance(g1.Position, pos) < Vector2.Distance(g2.Position, pos)) ? g1 : g2);
            return closest;
        }


        private int GetFacingDir() => (_faceAction != null) ? _faceAction.FacingDir : 0;
    }
}