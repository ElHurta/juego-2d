using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    private Rigidbody2D characterBody;
    private Animator characterAnimator;
    private bool isGrounded = false;

    private void Awake()
    {
        characterBody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        characterBody.velocity = new Vector2(horizontalInput * speed, characterBody.velocity.y);

        // Flip Character according Input
        if (horizontalInput > 0.01f) transform.localScale = new Vector3(6, 6, 1);
        else if (horizontalInput < -0.01f) transform.localScale = new Vector3(-6, 6, 1);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            Jump();

        // Set Animation Params
        characterAnimator.SetBool("Running", horizontalInput != 0);
        characterAnimator.SetBool("grounded", isGrounded);
    }

    private void Jump(){
        characterBody.velocity = new Vector2(characterBody.velocity.x, speed);
        characterAnimator.SetTrigger("jump");
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
