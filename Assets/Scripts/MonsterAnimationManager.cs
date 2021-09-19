using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Serialization;

// Only if the GameObject has an animator as its component can use this script
[RequireComponent(typeof(Animator))]
public class MonsterAnimationManager : MonoBehaviour
{
    private Animator _animator;
    public float moveSpeed = 100.0f;

    private float _xAxis;
    private float _yAxis;
    private static readonly int Move = Animator.StringToHash("Move");
    // ReSharper disable once InconsistentNaming
    private static readonly int LMClick = Animator.StringToHash("Left Mouse Click");

    private const string IDLE = "Idle";
    private const string HURT = "Hurt";
    private const string ATTACK = "Attacking";
    private const string WALK = "Walking";
    private const string IDLE_BLINK = "Idle Blinking";
    private const string SMOKE = "Smoke";
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Test usage: to check the animation
        TestAnimation();
    }
    private void TestAnimation()
    {
        
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");
        // Test Walking
        if (_xAxis != 0 || _yAxis!=0)
        {
            // _animator.Play(WALK);
            _animator.SetTrigger(Move);
            transform.Translate(new Vector3(_xAxis,_yAxis,0) * Time.deltaTime * moveSpeed);
        }
        // Test Attacking
        if (Input.GetMouseButtonUp(0))
        {
            // _animator.Play(ATTACK);
            _animator.SetTrigger(LMClick);
        }
        
        // Test Hurt
        if (Input.GetKeyUp(KeyCode.V))
        {
            _animator.Play(HURT);
        }
        
        // Test Smoke
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.Play(SMOKE);
        }
    }
}
