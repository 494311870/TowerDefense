using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UnitAgent : MonoBehaviour, IAttackTarget
{
    public SpriteRenderer characterSprite;
    public Animator unitAnimator;
    public Rigidbody2D unitRigidbody;
    public float moveSpeed = 4.0f;
    public bool noBlood = false;

    public int initialHp = 50;
    public int attackPower = 15;
    public int defensivePower = 5;
        
    private int _currentAttack = 0;
    private float _timeSinceAttack = 0.0f;
    private float _delayToIdle = 0.0f;
    private float _horizontal;
    
    private int _currentHp;

    private void Start()
    {
        unitAnimator.SetBool("Grounded", true);
        _currentHp = initialHp;
    }

    // Update is called once per frame
    void Update()
    {
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

        // Move
        unitRigidbody.velocity = new Vector2(inputX * moveSpeed, unitRigidbody.velocity.y);

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

        _currentAttack++;

        // Loop back to one after third attack
        if (_currentAttack > 3)
            _currentAttack = 1;

        // Reset Attack combo if time since last attack is too large
        if (_timeSinceAttack > 1.0f)
            _currentAttack = 1;

        // Call one of three attack animations "Attack1", "Attack2", "Attack3"
        unitAnimator.SetTrigger("Attack" + _currentAttack);

        // Reset timer
        _timeSinceAttack = 0.0f;
    }

 
    public void Hurt(int damage)
    {
        _currentHp -= Mathf.Max(0, damage - defensivePower);
        if (_currentHp <= 0)
        {
            Death();
            return;
        }
        unitAnimator.SetTrigger("Hurt");
    }

    public void Death()
    {
        unitAnimator.SetBool("noBlood", noBlood);
        unitAnimator.SetTrigger("Death");
        
        Destroy(this.gameObject,0.5f);
    }


    public void InputHorizontal(float horizontal)
    {
        _horizontal = horizontal;
    }
}