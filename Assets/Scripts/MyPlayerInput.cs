using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//this script it use to move and anim the player character
public class MyPlayerInput : MonoBehaviour
{

    private GameManager manager;
    private Vector2 pos; 
    private bool isJumping = false;
    private Animator anim;
    private Rigidbody2D rigid;
    private PlayerAnimate player;
    private PanelController panel;

    public bool IsJuxmping { get => isJumping; set => isJumping = value; }

    void Start()
    {
        player = GetComponent<PlayerAnimate>();
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        manager = GameManager.instance;
        panel = PanelController.instance;
    }


    public void Move(InputAction.CallbackContext context)
    {
        pos = context.ReadValue<Vector2>().normalized;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.performed;
    }

    public void GamePause(InputAction.CallbackContext context)
    {
        if (context.performed && !manager.IsOver)
          panel.PauseMenu();
    }

    private void Animate()
    {
        //local variable
        var veloY = rigid.velocity.y;
        var veloMag = rigid.velocity.magnitude;
        var isWallJump = player.WallJumpAnim;
       
        anim.SetFloat("v", veloY);
        anim.SetFloat("m", veloMag);

        if (veloY == 0 && veloMag == 0)
            anim.SetFloat("h", 0);
        
        else 
            anim.SetFloat("h", Mathf.Abs(pos.x));
        
        if (isWallJump)
        {
            anim.SetBool("WallJump", isWallJump);
            if (veloY <= 0)
            {
                player.WallJumpAnim = !isWallJump;
                anim.SetBool("WallJump", !isWallJump);
            }
                
        }

    }
    private void WaitTimerWallJump()
    {
        anim.SetBool("WallJump", player.WallJumpAnim);
    }

    private void FixedUpdate()
    {
        player.PlayerOnMove(pos.x, isJumping);
    }
    private void Update()
    {
        Animate();
    }
}
