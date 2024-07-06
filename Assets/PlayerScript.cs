using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    /// <summary>
    /// The gravity applied to the player
    /// </summary>
    public float gravity;
    /// <summary>
    /// The velocity of the player
    /// </summary>
    public Vector2 velocity;
    /// <summary>
    /// The maximum velocity of the player
    /// </summary>
    public float maxXVelocity = 100;
    /// <summary>
    /// The maximum acceleration of the player
    /// </summary>
    public float maxAcceleration = 10;
    /// <summary>
    /// The acceleration of the player
    /// </summary>
    public float acceleration = 10;
    /// <summary>
    /// The distance the player has traveled
    /// </summary>
    public float distance = 0;
    /// <summary>
    /// The force applied to the player when jumping
    /// </summary>
    public float jumpForce = 20;
    /// <summary>
    /// The height of the ground
    /// </summary>
    public float groundHeight = 10;
    /// <summary>
    /// The state of the player on the ground
    /// </summary>
    public bool isGrounded = false;
    /// <summary>
    /// The state of the player holding the jump button
    /// </summary>
    public bool isHoldingJump = false;
    /// <summary>
    /// The maximum time the player can hold the jump button
    /// </summary>
    public float maxJumpTime = 0.4f;
    /// <summary>
    /// The time the player has been holding the jump button
    /// </summary>
    public float jumpTime = 0.0f;
    /// <summary>
    /// The threshold to consider the player on the ground
    /// </summary>
    public float jumpGroundThreshold = 1;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        HandleJumpStart();
        HandleJumpRelease();
    }

    private void FixedUpdate() {
        Vector2 pos = transform.position;

        if (!isGrounded) {
            HandleHoldJump();
            UpdatePositionWhenJumping(ref pos);
            CheckAndHandleReleaseSpaceWhenJumping();
            CheckAndResetGroundState(ref pos);
        }

        UpdateDistance();

        if (isGrounded) {
            UpdateAcceleration();
            UpdateVelocity();
        }

        transform.position = pos;
    }

    /// <summary>
    /// Handle the start of the jump
    /// </summary>
    private void HandleJumpStart() {
        Vector2 pos = transform.position;
        float distanceToGround = Math.Abs(pos.y - groundHeight);

        if (isGrounded || distanceToGround <= jumpGroundThreshold) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                isGrounded = false;
                velocity.y = jumpForce;
                isHoldingJump = true;
                jumpTime = 0.0f;
            }
        }
    }

    /// <summary>
    /// Handle the release of the jump button
    /// </summary>
    private void HandleJumpRelease() {
        if (Input.GetKeyUp(KeyCode.Space)) {
            isHoldingJump = false;
        }
    }

    /// <summary>
    /// Limit the time the player can hold the jump button
    /// </summary>
    private void HandleHoldJump() {
        if (isHoldingJump) {
            jumpTime += Time.fixedDeltaTime;
            if (jumpTime >= maxJumpTime) {
                isHoldingJump = false;
            }
        }
    }

    /// <summary>
    /// Update the position of the player when jumping
    /// </summary>
    /// <param name="pos">The current position of the player</param>
    private void UpdatePositionWhenJumping(ref Vector2 pos) {
        pos.y += velocity.y * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Check if the player is still holding the jump button and apply gravity if not
    /// </summary>
    private void CheckAndHandleReleaseSpaceWhenJumping() {
        if (!isHoldingJump) {
            velocity.y += gravity * Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// Check if the player is on the ground and reset the position to the ground level
    /// </summary>
    /// <param name="pos">The current position of the player</param>
    private void CheckAndResetGroundState(ref Vector2 pos) {
        if (pos.y <= groundHeight) {
            pos.y = groundHeight;
            isGrounded = true;
        }
    }

    /// <summary>
    /// Update the distance the player has traveled
    /// </summary>
    private void UpdateDistance() {
        distance += velocity.x * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Update the acceleration of the player
    /// </summary>
    private void UpdateAcceleration() {
        float velocityRatio = velocity.x / maxXVelocity;
        acceleration = maxAcceleration * (1 - velocityRatio);
    }

    /// <summary>
    /// Update the velocity of the player
    /// </summary>
    private void UpdateVelocity() {
        velocity.x += acceleration * Time.fixedDeltaTime;
        if (velocity.x >= maxXVelocity) {
            velocity.x = maxXVelocity;
        }
    }
}
