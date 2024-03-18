using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoninLabs
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f; // Base jump force
        [SerializeField] private float springStiffness = 100f; // Adjust for desired jump strength
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private bool isGrounded;

        private float initialJumpPadHeight; // Stores height of collided jump pad

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            PlayerMovements();
        }

        private void PlayerMovements()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Debug.Log("Jump");
                PlayerJump();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }

            if (collision.gameObject.CompareTag("JumpPad"))
            {
                initialJumpPadHeight = collision.gameObject.transform.position.y;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }

        private void PlayerJump()
        {
            //Hooke's Law
            float compressAmount = initialJumpPadHeight - transform.position.y;
            float jumpForceWithSpring = jumpForce + (compressAmount * springStiffness);
            rb.AddForce(Vector2.up * jumpForceWithSpring, ForceMode2D.Impulse);
        }
    }
}