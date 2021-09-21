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
    public float moveSpeed = 100.0f;
    public GameObject face;
    public GameObject mouth;

    public Sprite[] faces;
    public Sprite[] mouths;

    private Animator _animator;
    private SpriteRenderer _faceRenderer;
    private SpriteRenderer _mouthRenderer;

    private float _xAxis;
    private float _yAxis;
    private float _xPrev;
    private int _movingFlag = 1;

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
        _faceRenderer = face.GetComponent<SpriteRenderer>();
        _mouthRenderer = mouth.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Test usage: to check the animation
        TestAnimation();
    }

    private void TestAnimation()
    {
        if (_animator.GetBool("IsIdle"))
        {
            _faceRenderer.sprite = faces[0];
            _mouthRenderer.sprite = mouths[0];
        }

        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        // Test Walking
        if (_xAxis != 0 || _yAxis != 0)
        {
            if (_xPrev * _xAxis < 0)
            {
                gameObject.transform.Rotate(Vector3.up, 180);
                _movingFlag = -_movingFlag;
            }

            _animator.SetTrigger("Move");
            transform.Translate(new Vector3(_xAxis * _movingFlag, _yAxis, 0) * Time.deltaTime * moveSpeed);

            _xPrev = _xAxis == 0 ? _xPrev : _xAxis;
        }

            // _animator.Play(ATTACK);
        // Test Attacking
        if (Input.GetMouseButtonUp(0))
        {
            _animator.SetTrigger("LeftMouseClick");
        }

        // Test Hurt
        if (Input.GetKeyUp(KeyCode.V))
        {
            _animator.Play(HURT);
            _faceRenderer.sprite = faces[1];
            _mouthRenderer.sprite = mouths[1];
        }

        // Test Smoke
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _animator.Play(SMOKE);
            _faceRenderer.sprite = faces[2];
            _mouthRenderer.sprite = mouths[1];
        }
    }
}