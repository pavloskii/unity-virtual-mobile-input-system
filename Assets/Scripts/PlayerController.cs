using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float _moveSpeed = 8.0f;
    private Vector3 playerVelocity;
    private bool _isGrounded;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float _gravity = -9.81f;

    private Animator _animator;
    private CharacterController _controller;

    // animation IDs
    private int _animIDSpeed;

    //Inputs
    private Vector2 moveInput;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        //GetComponent<PlayerInput>().neverAutoSwitchControlSchemes = true;

        AssignAnimationIDs();
    }

    private void Update()
    {
        Move();
        Gravity();
    }

    private void Gravity()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        _controller.Move(_moveSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            _animator.SetFloat(_animIDSpeed, 1);
        }
        else
        {
            _animator.SetFloat(_animIDSpeed, 0);
        }
    }

    public void MoveInput(Vector2 moveDirection)
    {
        moveInput = moveDirection;
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
    }
}
