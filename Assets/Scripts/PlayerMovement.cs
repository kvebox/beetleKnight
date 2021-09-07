using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 25f;
    public CharacterController2D controller;
    float horizontalMove = 0f;
    bool isJumping = false;
    bool isDashing = false;

    void Update() {
        if (Input.GetButtonDown("Jump")){
            isJumping = true;
        }

        if (Input.GetButtonDown("Dash")){
            isDashing = true;
        }
        
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
    }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isJumping, isDashing);
        isJumping = false;
        isDashing = false;
    }
}
