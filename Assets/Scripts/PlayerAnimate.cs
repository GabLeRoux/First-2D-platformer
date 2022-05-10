using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    MyPlayerInput player;
   
    //force to apply
     private const float jumpForce = 9.50f;
     private const float speed = 10.00f;
    [Range(0, 0.3f)] [SerializeField] private float smooting = 0.05f;
    private Vector2 refVelo = Vector2.zero;
   
    //check player axis side
    private SpriteRenderer sprite;
    private Rigidbody2D rigid;
    private  bool facingLeft = false;
    
    //Wall jump
    private bool wallJumpAnim = false;
    [SerializeField] private Transform WallCheck;
    private float colValue; //use to set the walljump collison value side
    bool collside = false; // use for walljump side

    //check if grounded variable
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundDef;

    //jump sound
    private new AudioSource audio;


    public float Smooting { get => smooting; set => smooting = value; }
    public bool WallJumpAnim { get => wallJumpAnim; set => wallJumpAnim = value; }

    //Animation
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        player = GetComponent<MyPlayerInput>();
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        colValue = WallCheck.position.x;

    }
    public void PlayerOnMove(float move , bool isjump)
    {
        Mouvement(move);
        Jumping(isjump);
        CheckOrientation(move);
    }
   
    private void CheckOrientation(float move)
    {
        if (move > 0 && facingLeft || move < 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            sprite.flipX = facingLeft;
            collside = facingLeft;
        }
    }
    //always updating the collision side if player jump
    private void WallJump()
    {
        var side = collside ? WallCheck.position.x - 0.5f : WallCheck.position.x + 0.5f;
        colValue = side;
    }
   private void AddJumpForce()
    {
        audio.Play();
        rigid.velocity = Vector2.zero;
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        player.IsJuxmping = false;
    }

    private void Jumping(bool isJump)
    {
        var isGround = Grounded();
        if ((isJump && isGround))
        {
            AddJumpForce();
        }
        else if (CanWallJump() && isJump && !isGround)
        {
            wallJumpAnim = true;
            AddJumpForce();
            collside = !collside; //changer player side if jump on wall
            player.IsJuxmping = false;
        }
    }
    private bool CanWallJump()
    {
        Collider2D col = Physics2D.OverlapBox(new Vector2(colValue, WallCheck.position.y), new Vector2(0.6f, 0.2f), 0f, groundDef);
        return col != null;
    }
    public bool Grounded()
    {
        Collider2D col = Physics2D.OverlapBox(groundCheck.position, new Vector2(0.6f, 0.2f), 0f, groundDef);
        return col != null;
    }
    private void OnDrawGizmosSelected()
    {
        var isGround = Grounded();
        var isWall = CanWallJump();
        Gizmos.color = isWall ? Color.yellow : Color.red;
        Gizmos.color = isGround ? Color.blue : Color.red;

        Gizmos.DrawWireCube(new Vector2(colValue, WallCheck.position.y), new Vector3(0.6f, 0.2f, 0f));
        Gizmos.DrawWireCube(groundCheck.position, new Vector3(0.6f, 0.2f, 0f));
    }

    private void Mouvement(float m)
    {
        // float  m  -- represent player input mouvement

        rigid.velocity = Vector2.SmoothDamp(rigid.velocity, new Vector2(m * speed, rigid.velocity.y),
                         ref refVelo, smooting);
        rigid.AddForce (((Physics2D.gravity * 2.5f) - Physics2D.gravity));
        
    }

    private void FixedUpdate()
    {
        Grounded();
        WallJump();
        CanWallJump();
    }
}
