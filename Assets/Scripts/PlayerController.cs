using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    [SerializeField] float runSpeed = 10f;
    

    bool isAlive = true;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Sprint();
        FlipSprite();
        EndGame();

    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }
    void Sprint()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.linearVelocity.y);
        myRigidbody.linearVelocity = playerVelocity;
        myAnimator.SetBool("IsRunning", true);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(myRigidbody.linearVelocity.x), 1f);
        }
        else
        {
            myAnimator.SetBool("IsRunning", false);
        }
    }
    void EndGame()
    {
        if (myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Spike")))
        {
            Collider2D[] colliders = Physics2D.OverlapBoxAll(myBoxCollider.bounds.center, myBoxCollider.bounds.size, 0);
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Spike"))
                {
                    isAlive = false;
                    break;
                }
            }
        }
    }

}
