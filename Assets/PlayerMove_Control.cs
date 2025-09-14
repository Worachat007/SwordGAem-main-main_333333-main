using Mono.Cecil;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private int direction = 1;
    public float jumpForce = 5;
    private bool grounded = false;

    private GatherInput gatherInput;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
        SetAnimatorValues();
        Flip();
        Rigidbody2D.linearVelocity = new Vector2(speed * gatherInput.valueX,
            Rigidbody2D.linearVelocity.y);
        JumpPlayer();
    }
    public Transform leftPoint;
    public float rayLength;
    public LayerMask groundLayer;
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position,
            Vector2.down, rayLength, groundLayer);
       
        grounded = leftCheckHit;
        // print(grounded);
    }
    private void JumpPlayer() {
        if (gatherInput.JumpInput) {
            Rigidbody2D.linearVelocity = new Vector2(gatherInput.valueX * speed, jumpForce);
        }
        gatherInput.JumpInput = false;
    }
    private void SetAnimatorValues()
    {
        animator.SetFloat("speed", Mathf.Abs(Rigidbody2D.linearVelocityX));
        animator.SetFloat("Vspeed", Rigidbody2D.linearVelocityY);
        animator.SetBool("Ground", grounded);
    }
    /// <summary>
    /// for flip character
    /// </summary>
    private void Flip() {
        if (gatherInput.valueX * direction < 0) {
            //to flip character
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            //update direction
            direction *= -1;
        }
    }
}