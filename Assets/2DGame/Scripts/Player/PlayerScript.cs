using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private float horizontal;
    private float speed = 4f;
    private float jumpingPower = 7f;
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private Animator playerAnimator;

    [SerializeField] private Rigidbody2D rbPlayer;
    [SerializeField] Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rbPlayer.velocity = new Vector2(horizontal * speed, rbPlayer.velocity.y);
        IsGrounded();

        playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));

        if(!isFacingRight && horizontal > 0f)
        {
            FlipCharacter();
        }
        else if(isFacingRight && horizontal < 0f)
        {
            FlipCharacter();
        }

        if (rbPlayer.velocity.y == 0f && isGrounded)
            OnLanding();

        if (rbPlayer.velocity.y != 0f)
            playerAnimator.SetFloat("yVelocity", rbPlayer.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpingPower);
            playerAnimator.SetBool("isJumping", true);
        }

        if(context.canceled && rbPlayer.velocity.y > 0f)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f);
        }
    }

    public void OnLanding()
    {
        playerAnimator.SetBool("isJumping", false);
    }

    void IsGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);

        playerAnimator.SetBool("isJumping", !isGrounded);
    }

    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
