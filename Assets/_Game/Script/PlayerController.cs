using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float rayLength = 0.2f;
    [SerializeField] private float activeGravityScale = 3f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool canMove = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isGrounded;

    [SerializeField] Player player;


    private float jumpBufferTime = 0.1f;
    private float jumpTimer;
    private float speedIncreesTime;


    private void Update()
    {
        if (!GameManager.Instance.IsGameStrated)
            return;

        jumpTimer -= Time.deltaTime;
        CheckGround();

        if (Input.GetMouseButtonDown(0))
        {
            if (IsClickedOnJumpSprite())
            {
                TryJump();
            }
        }

        UpdatePlayerPosition();
    }

    private bool IsClickedOnJumpSprite()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("JumpButton"))
        {
            return true;
        }

        return false;
    }


    private void UpdatePlayerPosition()
    {
        if (canMove && !GameManager.Instance.PauseGame)
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;

            if (player.IsBoosterActive)
                return;
            speedIncreesTime += Time.deltaTime;
            if (speedIncreesTime >= 1)
            {
                moveSpeed += .1f;
                GameManager.Instance.SetMoveSpeed(moveSpeed);
                speedIncreesTime = 0;
            }

        }
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer);

        bool groundedNow = hit.collider != null && jumpTimer <= 0f;
        isGrounded = groundedNow;

        if (isGrounded)
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            if (!GameManager.Instance.PauseGame)
                player.Run();
        }
        else
        {
            rb.gravityScale = activeGravityScale;
        }
    }

    private void TryJump()
    {
        if (!isGrounded || GameManager.Instance.IsGameFailed || GameManager.Instance.PauseGame) return;


        rb.gravityScale = activeGravityScale;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        player.jump();
        AudioManager.Instance.PlaySoundOfType(SoundEffectType.Jump);
        jumpTimer = jumpBufferTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLength);
        Gizmos.DrawSphere(transform.position + Vector3.down * rayLength, 0.02f);
    }
    public void SetSpeed(float speed) => moveSpeed = speed;
    public void SetCanMove(bool move) => canMove = move;

}
