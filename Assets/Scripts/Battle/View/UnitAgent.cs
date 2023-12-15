using System;
using Battle.Config;
using UnityEngine;

namespace Battle.View
{
    public class UnitAgent : MonoBehaviour
    {
        public SpriteRenderer characterSprite;
        public Animator unitAnimator;
        public Rigidbody2D unitRigidbody;
        public Collider2D unitCollider;
        public bool noBlood = false;

        private int _currentAttackStep = 0;

        private UnitData _data;
        private float _delayToIdle = 0.0f;
        private float _horizontal;
        private float _timeSinceAttack = 0.0f;
        private float _moveSpeed;
        public bool DataInvalid => _data == null;

        public Vector2 Center => (Vector2) this.transform.position + unitCollider.offset;


        private void Start()
        {
            unitAnimator.SetBool("Grounded", true);
        }

        // Update is called once per frame
        private void Update()
        {
            if (_data == null)
                return;

            // Increase timer that controls attack combo
            _timeSinceAttack += Time.deltaTime;

            // -- Handle input and movement --
            float inputX = _horizontal;
            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                characterSprite.flipX = false;
            }
            else if (inputX < 0)
            {
                characterSprite.flipX = true;
            }

            //Run
            if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                _delayToIdle = 0.05f;
                EnterRun();
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

        private void FixedUpdate()
        {
            // -- Handle input and movement --
            float inputX = _horizontal;
            _horizontal = 0;
            // Move
            unitRigidbody.velocity = new Vector2(inputX * _moveSpeed, unitRigidbody.velocity.y);
        }


        public void Hurt(int damage)
        {
            // unitAnimator.SetTrigger("Hurt");
        }

        public void SetData(UnitData value)
        {
            _data = value;
            _moveSpeed = _data.MoveSpeed;
        }

        private void EnterRun()
        {
            unitAnimator.SetInteger("AnimState", 1);
        }

        private void EnterIdle()
        {
            unitAnimator.SetInteger("AnimState", 0);
        }

        public bool CanAttack()
        {
            return _timeSinceAttack > 0.25f;
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
            this.gameObject.layer = LayerMask.NameToLayer("Corpse");
            unitAnimator.SetBool("noBlood", noBlood);
            unitAnimator.SetTrigger("Death");

            Destroy(this.gameObject, 1.0f);
        }

        public void InputHorizontal(float horizontal)
        {
            _horizontal = horizontal;
        }
    }
}