using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; }}
    [SerializeField] private float moveSpeed = 1f; //Adjust move speed of player

    private PlayerControls playerControls;
    private Vector2 movement; //Store movement input
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer spriteRenderer;
    private bool facingLeft = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    // Update is called once per frame, used to get input, animation, UI
    private void Update()
    {
        PlayerInput();
    }

    //FixedUpdate is called according to realtime, used for rigidbody movement and physics
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    //Get input from playerControls
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("MoveX", movement.x);
        myAnimator.SetFloat("MoveY", movement.y);
    }

    //Move player
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    //Flip player according to mouse direction
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
}
