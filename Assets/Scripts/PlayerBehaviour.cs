using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private GameManager manager;
    private Rigidbody2D rigid;
    private PlayerAnimate player_Anim_Script;
    private SpriteRenderer defaultColor;

    private int playerLives = 0;
    private bool isHit = false;
    private float actualSmooth;
    public int PlayerLives { get => playerLives; set => playerLives = value; }

    
    private void Awake()
    {
        PlayerPrefs.SetFloat("CKPositionX", transform.position.x);
        PlayerPrefs.SetFloat("CKPositionY", transform.position.y);
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        player_Anim_Script = GetComponent<PlayerAnimate>();
        actualSmooth = player_Anim_Script.Smooting;
        manager = GameManager.instance;
        defaultColor = gameObject.GetComponent<SpriteRenderer>();
    }
    #region collionEffect
    //add force fonction depend on axis
    private void AxisXForce(float forceSide)
    {
        rigid.AddForce(Vector2.right * forceSide, ForceMode2D.Impulse);
    }
    private void AxisYForce(float forceSide)
    {
         rigid.AddForce(Vector2.up * forceSide, ForceMode2D.Impulse);
    }

    private void CollisionPoint(Collision2D collision)
    {
        var obj = collision.GetContact(0).point; //objectPosition
        Vector2 collSide =  new Vector2( transform.position.x - obj.x, transform.position.y -obj.y);
        print(collSide);
        player_Anim_Script.Smooting = 0.25f;
        rigid.velocity = Vector2.zero;
        if (collSide.x >= 0)
        {
            AxisXForce(6f);
        }
        else if(collSide.x <= 0)
        {
            AxisXForce(-6f);
        }
        else if(collSide.y >= 0 || collSide.y <= 0)
        {
            AxisYForce(6f);
        }
        Invoke("WaitForResetSmooth", 0.5f);
      
        #region garbage comment

        //if (collSide.x <= -0.1f && collSide.y <= 0f)
        //{
        //    rigid.AddForce(Vector2.right * -15, ForceMode2D.Impulse);
        //}
        //else if (collSide.x <= -0.1f && collSide.y >= 0f)
        //{
        //    rigid.AddForce(Vector2.right * -15, ForceMode2D.Impulse);
        //}
        //else if (collSide.x >= 0.1f && collSide.y <= -0.1f)
        //{
        //    rigid.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
        //}
        //else if (collSide.x <= -0.1f && collSide.y >= 0.1f)
        //{
        //    rigid.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
        //}
        //else if (collSide.x <= -0.4f && collSide.y >= 0.4f)
        //{
        //    rigid.AddForce(new Vector2(-8, 4), ForceMode2D.Impulse);
        //}
        //else
        //{
        //    rigid.AddForce(Vector2.right * 15, ForceMode2D.Impulse);
        //}


        //  }
        // else if (collision.gameObject.CompareTag("Pic"))
        //{
        //    //player_Smooting.Smooting = 0.10f;
        //    //rigid.velocity = Vector2.right;
        //    if (collSide.x <= -0.1f && collSide.y >= 0.1f)
        //    {
        //        rigid.AddForce(Vector2.right * -10, ForceMode2D.Impulse);
        //    }
        //    else if (collSide.x >= 0.1f && collSide.y >= 0.1f)
        //    {
        //        rigid.AddForce(Vector2.right * 10, ForceMode2D.Impulse);
        //    }

        //    else
        //    {
        //        int randomRebond = Random.Range(0, 2);
        //        print(randomRebond);
        //        if (randomRebond == 0)
        //            rigid.AddForce(new Vector2(4, 2) * 10, ForceMode2D.Impulse);
        //        else
        //            rigid.AddForce(new Vector2(-4, 2) * 10, ForceMode2D.Impulse);
        //    }
        //}
        // }
        #endregion

    }
    private void PlayerHit()
    {
        if (!isHit)
        {
            bool isDead = manager.PlayerTouch(this.playerLives, transform.position);
            StartCoroutine(DisableHitablePlayer());
            if (isDead )
            {
                StartCoroutine(ResetPostion());
            }
        }
    }

    #endregion
    //if player die
    private  IEnumerator ResetPostion()
    {
        yield return new WaitForSeconds(0.5f);
            RespawnMe();
    }
    private  void RespawnMe()
    {
        float xRespawn = PlayerPrefs.GetFloat("CKPositionX");
        float yRespawn = PlayerPrefs.GetFloat("CKPositionY");
        transform.position = new Vector3(xRespawn, yRespawn, 0);
    }
    //if player lose life points
    private IEnumerator DisableHitablePlayer()
    {
        isHit = true;
        Color transparentColor = defaultColor.color;
        transparentColor.a = 0.5f;
        
        for (float i = 0; i <= 0.2f; i += 0.10f)
        {
            defaultColor.color = transparentColor;
            yield return new WaitForSeconds(0.25f);
            defaultColor.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
        isHit = false;
    }

    #region Detect collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LevelPass") && player_Anim_Script.Grounded())
        {
            manager.LevelWin();
        }
    }
    private void CollisionBehaviour(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Enemie"))
        {
            CollisionPoint(collision);
            PlayerHit();
        }
        
        else if (collision.gameObject.CompareTag("BottomBlock"))
        {
            if (manager.PlayerTouch(this.playerLives, transform.position, true))
                StartCoroutine(DisableHitablePlayer());
            if (this.playerLives > 0)
                RespawnMe();
       }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionBehaviour(collision);
        if (collision.gameObject.CompareTag("Jumper"))
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(Vector2.up * 15.00f, ForceMode2D.Impulse);
            Invoke("WaitEndOfJumpZone", 0.35f);
        }
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CollisionBehaviour(collision);
    }
    #endregion
    // use by an Invoke
    private void WaitForResetSmooth()
    {
        player_Anim_Script.Smooting = actualSmooth;

    }
    //use by an Invoke
    private void WaitEndOfJumpZone()
    {
        rigid.AddForce(new Vector2(0, -5.5f),ForceMode2D.Impulse);

    }
}
