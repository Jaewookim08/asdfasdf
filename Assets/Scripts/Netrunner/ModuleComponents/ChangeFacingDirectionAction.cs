using System;
using UnityEngine;

namespace Netrunner.ModuleComponents
{
    /*
     *  selectingAction 중이면 cursor 방향, 아니면 키보드 방향에 맞춰 spriteRenderer의 flipX값을 수정합니다.
     */
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChangeFacingDirectionAction: ModuleAction
    {
        [SerializeField] private Selection selection;

        public int GetFacingDir => IsFacingRight ? 1 : -1;
        
        public bool IsFacingRight { get; private set; } = true;

        private SpriteRenderer _mSpriteRenderer;

        private void Start()
        {
            _mSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Face(bool isRight)
        {
            IsFacingRight = isRight;
            _mSpriteRenderer.flipX = !isRight;
        }
        
        private void Update()
        {
            if (_mSpriteRenderer == null) return;

            
            if (selection!=null && selection.Cursor.activeSelf) {
                Face(selection.Cursor.transform.position.x > transform.position.x);
            }
            else {
                var xInput = GameInput.GetHorizontal(player);
                if (xInput > 0.01) {
                    Face(true);
                }
                else if (xInput < -0.01) {
                    Face(false);
                }
            }
        }
    }
}