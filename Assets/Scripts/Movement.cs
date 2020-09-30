using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    private CharacterController controller;

    public AudioSource jumpAudio;
    public AudioSource landAudio;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public float fallMultipier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float velocitySmoothing = 1.5f;

    private bool isInAir = false;

    private Animator anim;
    private float jumpTime;
    public float jumpMaxTime = 0.5f;
    private bool isJumping = false;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        
    }

    void Update()
    {
        
        groundedPlayer = controller.isGrounded;
        
        if (!groundedPlayer)
        {
            
            jumpTime += Time.deltaTime;
            if (jumpTime > jumpMaxTime)
            {
                isInAir = true;
            }
            
        }

        anim.SetBool("isLanding", false);
        if (groundedPlayer && playerVelocity.y < 0)
        {
            jumpTime = 0;
            

            if (isInAir)
            {
                anim.SetBool("isLanding", true);
                landAudio.Play();
                isInAir = false;
                isJumping = false;
            }
                
            anim.SetBool("isJumping", false);

            playerVelocity.y = 0f;
        }

        //Walk the player according to the input
        Walk();
        
          

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer && !isJumping)
        {
            isJumping = true;
            jumpAudio.Play();
            anim.SetTrigger("TakeOff");
            Invoke("Jump", 0.2f);

            
        }

        //Gravity
        playerVelocity.y += gravityValue * Time.deltaTime;

        //Low and High jump values
        if (playerVelocity.y < 0.0)
        {
            playerVelocity += Vector3.up * gravityValue * (fallMultipier - 1) * Time.deltaTime;
        }
        else if (playerVelocity.y > 0.0 && !Input.GetButton("Jump"))
        {
            playerVelocity += Vector3.up * gravityValue * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        

        controller.Move(playerVelocity * Time.deltaTime);
        
    }


    private void Jump()
    {
    
        anim.SetBool("isJumping", true);
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        if (playerVelocity.y > 0.0 && !Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt((jumpHeight / 2) * -3.0f * gravityValue);
        }
    }

    private void Walk()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        controller.Move(move * Time.deltaTime * playerSpeed);
        
        //Animation
        if (Mathf.Abs(move.x) <= 0.3 && Mathf.Abs(move.z) <= 0.3)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }


        if (move != Vector3.zero)
        {
            //gameObject.transform.forward = move;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "DeathTerrain")
        {
            //Make cool death animation
            FindObjectOfType<GameController>().EndGame();

        }
    }
}