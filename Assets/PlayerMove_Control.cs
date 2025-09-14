using Mono.Cecil;
using UnityEngine;

public class PlayerMoveControls : MonoBehaviour
{
    public float speed = 5f;
    private int direction = 1;
    public float jumpForce = 5f;
    private bool grounded = false;
    
    public Transform leftPoint;
    public float rayLength = 1f;
    public LayerMask groundLayer;

    private GatherInput gatherInput;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    
    void Start()
    {
        gatherInput = GetComponent<GatherInput>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    void Update()
    {
        CheckStatus();
        SetAnimatorValues();
        MovePlayer();
        JumpPlayer();
        Flip();
    }
    
    private void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftPoint.position,
            Vector2.down, rayLength, groundLayer);
       
        grounded = leftCheckHit.collider != null;
    }
    
    private void MovePlayer()
    {
        Rigidbody2D.linearVelocity = new Vector2(speed * gatherInput.valueX,
            Rigidbody2D.linearVelocity.y);
    }
    
    private void JumpPlayer() 
    {
        if (gatherInput.JumpInput && grounded) 
        {
            Rigidbody2D.linearVelocity = new Vector2(Rigidbody2D.linearVelocity.x, jumpForce);
        }
        gatherInput.JumpInput = false;
    }
    
    private void SetAnimatorValues()
    {
        animator.SetFloat("speed", Mathf.Abs(Rigidbody2D.linearVelocity.x));
        animator.SetFloat("Vspeed", Rigidbody2D.linearVelocity.y);
        animator.SetBool("Ground", grounded);
    }
    
    private void Flip() 
    {
        if (gatherInput.valueX * direction < 0) 
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            direction *= -1;
        }
    }
}