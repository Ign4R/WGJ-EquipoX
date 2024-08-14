using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Vector3 minMovement;

    [Header("Ground Check")]
    public Transform groundCheck;
    public Transform orientation;
    public float groundCheckRadius = 0.2f;
    public int indexAnim = 0;
    public LayerMask groundLayer;

    public bool canJump = true;

    [Header("References")]
    public Animator[] anim;
    private Rigidbody rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        minMovement = new Vector3(.02f, .02f, .02f);
    }

    void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputZ = Input.GetAxis("Vertical");
        Vector3 moveDirection = orientation.forward * moveInputZ + orientation.right * moveInputX;

        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }

        Vector3 moveVelocity = moveDirection * moveSpeed;
        moveVelocity.y = rb.velocity.y;
        rb.velocity = moveVelocity;

        if (moveVelocity.magnitude > minMovement.magnitude)
        {
            anim[indexAnim].SetBool("IsRunning", true);
        }
        else
        {
            anim[indexAnim].SetBool("IsRunning", false);
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim[indexAnim].SetTrigger("IsJumping");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
