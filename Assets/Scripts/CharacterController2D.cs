using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour {
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private bool m_isGrounded = true;
    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private bool m_DirectionRight = true;

    const float m_GroundedRadius = .2f; 
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;

    [Header("Events")]
	[Space]
    public UnityEvent OnLandEvent;

	private void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null) OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate() {
        bool wasGrounded = m_isGrounded;
        m_isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				m_isGrounded = true;
				if (!wasGrounded) OnLandEvent.Invoke();
			}
		}
    }

    public void Move(float move, float jump, bool dash) {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (move > 0 && !m_DirectionRight) Flip();
        else if (move < 0 && m_DirectionRight) Flip();

        if (jump > 0 && m_isGrounded) {
            m_Rigidbody2D.AddForce(new Vector2(0f, jump));
            m_isGrounded = false;
        }
    }

    public void Flip() {
        m_DirectionRight = !m_DirectionRight;
    }
}