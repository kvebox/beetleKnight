using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 50f;
    public float jump = 800f;
    public float dashSpeed = 250f;
    public float dashCountThreshold = 225;
    public float dashCooldown = 25;
    public float dashTimer = 0;
    private float currentDashSpeed;

    public CharacterController2D controller;
    float horizontalMove = 0f;
    float isJumping;
    float direction = 0;
    bool isDashing = false;

    public enum State {
        NORMAL,
        DASH,
    }

    private State state;

    void Start() {
        state = State.NORMAL;
    }

    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        if (horizontalMove != 0) direction = horizontalMove;

        if (Input.GetButtonDown("Jump")){
            isJumping = jump;
        }

        switch (state) {
            case State.NORMAL:
                if (Input.GetButtonDown("Dash")){
                    if (dashTimer <= 0) {
                        currentDashSpeed = dashSpeed;
                        dashTimer = dashCooldown;
                        state = State.DASH;
                        isDashing = true;
                    }
                }
                break;
            
            case State.DASH:
                currentDashSpeed -= currentDashSpeed * Time.deltaTime;
                horizontalMove += direction < 0 ? -currentDashSpeed : currentDashSpeed;

                if (currentDashSpeed < dashCountThreshold) {
                    state = State.NORMAL;
                    isDashing = false;
                    dashTimer = dashCooldown;
                }
                break;
        }
    }

    void FixedUpdate() {
        dashTimer--;
        controller.Move(horizontalMove * Time.fixedDeltaTime, isJumping, isDashing);
        isJumping = 0;
    }
}
