using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Vector2 lookDirection = new(0, -1);
    Vector2 moveDirection;
    public PlayerInputActions playerControls;
    private InputAction move;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        Minigame.OnMinigameStarted += DisableMove;
        Minigame.OnMinigameEnded += EnableMove;
        Origami.OnOrigamiStarted += DisableMove;
        Origami.OnOrigamiEnded += EnableMove;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void DisableMove()
    {
        move.Disable();
    }

    private void EnableMove()
    {
        move.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();

        if (!Mathf.Approximately(moveDirection.x, 0.0f) || !Mathf.Approximately(moveDirection.y, 0.0f))
        {
            lookDirection.Set(moveDirection.x, moveDirection.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", moveDirection.magnitude);
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += speed * moveDirection.x * Time.deltaTime;
        position.y += speed * moveDirection.y * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

}
