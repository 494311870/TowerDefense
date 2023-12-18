#region

using System;
using Battle.Shared;
using UnityEngine;

#endregion

namespace Battle.Unit
{
    public abstract class UnitAgent : MonoBehaviour
    {
        public SpriteRenderer characterSprite;
        public Animator unitAnimator;
        public Rigidbody2D unitRigidbody;
        public Collider2D unitCollider;
        public bool noBlood;

        private float _attackInterval;
        private int _currentAttackStep;
        private float _delayToIdle;
        private float _inputHorizontal;
        private float _moveSpeed;
        private float _nextInputHorizontal;
        private float _timeSinceAttack;

        protected UnitData Data { get; private set; }
        public bool DataInvalid => Data == null;

        public Vector2 Center => (Vector2) transform.position + unitCollider.offset;

        // Update is called once per frame
        private void Update()
        {
            if (Data == null)
                return;

            // -- Handle input and movement --
            float inputX = _inputHorizontal;
            // Swap direction of sprite depending on walk direction
            SwapSpriteToward(inputX);
            //Run
            if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                _delayToIdle = 0.05f;
                EnterMove();
            }
            //Idle
            else
            {
                // Prevents flickering transitions to idle
                _delayToIdle -= Time.deltaTime;
                if (_delayToIdle < 0)
                    EnterIdle();
            }
        }

        public void SwapSpriteToward(float targetDirection)
        {
            if (targetDirection > 0)
            {
                characterSprite.flipX = false;
            }
            else if (targetDirection < 0)
            {
                characterSprite.flipX = true;
            }
        }

        private void FixedUpdate()
        {
            if (Data == null)
                return;

            // Increase timer that controls attack combo
            _timeSinceAttack += Time.fixedDeltaTime;
            // -- Handle input and movement --
            _inputHorizontal = _nextInputHorizontal;
            // _nextInputHorizontal = 0;
            // Move
            unitRigidbody.velocity = new Vector2(_inputHorizontal * _moveSpeed, unitRigidbody.velocity.y);
        }

        private void OnValidate()
        {
            if (characterSprite == null)
                characterSprite = GetComponentInChildren<SpriteRenderer>();

            if (unitAnimator == null)
                unitAnimator = GetComponentInChildren<Animator>();

            if (unitRigidbody == null)
                unitRigidbody = GetComponentInChildren<Rigidbody2D>();

            if (unitCollider == null)
                unitCollider = GetComponentInChildren<Collider2D>();
        }


        public virtual void Hurt(int damage)
        {
            // unitAnimator.SetTrigger("Hurt");
        }

        public void SetData(UnitData value)
        {
            Data = value;
            _moveSpeed = CalculateUtil.ConvertSpeed(Data.MoveSpeed);
            _attackInterval = CalculateUtil.ConvertAttackInterval(Data.AttackSpeed);
        }

        public virtual bool CanAttack()
        {
            return _timeSinceAttack > _attackInterval;
        }

        public void Attack()
        {
            if (!CanAttack())
                return;

            _currentAttackStep++;

            // Loop back to one after third attack
            if (_currentAttackStep > 3)
                _currentAttackStep = 1;

            // Reset Attack combo if time since last attack is too large
            if (_timeSinceAttack > 1.0f)
                _currentAttackStep = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            unitAnimator.SetTrigger("Attack" + _currentAttackStep);

            // Reset timer
            _timeSinceAttack = 0.0f;
        }

        public void Death()
        {
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            unitAnimator.SetBool("noBlood", noBlood);
            unitAnimator.SetTrigger("Death");

            Destroy(gameObject, 1.0f);
        }

        public void WaitingInPlace() => InputHorizontal(0);

        public void InputHorizontal(float horizontal)
        {
            // _inputHorizontal = horizontal;
            _nextInputHorizontal = horizontal;
        }

        protected virtual void EnterMove()
        {
            unitAnimator.SetInteger("AnimState", 1);
        }

        protected virtual void EnterIdle()
        {
            unitAnimator.SetInteger("AnimState", 0);
        }
    }
}