#region

using UnityEngine;

#endregion

namespace Battle.Unit
{
    public abstract class UnitAgent : MonoBehaviour
    {
        private static readonly int AnimStateHash = Animator.StringToHash("AnimState");
        private static readonly int NoBloodHash = Animator.StringToHash("noBlood");
        private static readonly int DeathHash = Animator.StringToHash("Death");

        public SpriteRenderer characterSprite;
        public Animator unitAnimator;
        public Rigidbody2D unitRigidbody;
        public Collider2D unitCollider;
        public int attackMaxStep = 3;
        public bool noBlood;

        private int _currentAttackStep;
        private float _delayToIdle;
        private float _horizontalVelocity;

        public Vector2 Center => (Vector2)transform.position + unitCollider.offset;

        // Update is called once per frame
        private void Update()
        {
            // -- Handle input and movement --
            float velocityX = _horizontalVelocity;
            // Swap direction of sprite depending on walk direction
            SwapSpriteToward(velocityX);
            //Run
            if (Mathf.Abs(velocityX) > Mathf.Epsilon)
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

        private void FixedUpdate()
        {
            // Move
            unitRigidbody.velocity = new Vector2(_horizontalVelocity, unitRigidbody.velocity.y);
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

        public void SwapSpriteToward(float targetDirection)
        {
            if (targetDirection > 0)
                characterSprite.flipX = false;
            else if (targetDirection < 0) characterSprite.flipX = true;
        }

        public void WaitingInPlace()
        {
            InputHorizontalVelocity(0);
        }

        public void InputHorizontalVelocity(float horizontalVelocity)
        {
            _horizontalVelocity = horizontalVelocity;
        }

        public void Attack()
        {
            _currentAttackStep++;

            // Loop back to one after max step attack
            if (_currentAttackStep > attackMaxStep)
                _currentAttackStep = 1;

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            unitAnimator.SetTrigger($"Attack{_currentAttackStep}");
        }

        public void ResetAttackCombo()
        {
            _currentAttackStep = 1;
        }

        protected virtual void EnterMove()
        {
            unitAnimator.SetInteger(AnimStateHash, 1);
        }

        protected virtual void EnterIdle()
        {
            unitAnimator.SetInteger(AnimStateHash, 0);
        }

        public virtual void Hurt(int damage)
        {
            // unitAnimator.SetTrigger("Hurt");
        }

        public virtual void Death()
        {
            gameObject.layer = LayerMask.NameToLayer("Corpse");
            unitAnimator.SetBool(NoBloodHash, noBlood);
            unitAnimator.SetTrigger(DeathHash);

            Destroy(gameObject, 1.0f);
        }
    }
}