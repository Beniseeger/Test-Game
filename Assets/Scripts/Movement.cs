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

    private bool isJumping = false;

    private Animator anim;

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
            isJumping = true;
        }

        if (groundedPlayer && isJumping)
        {
            print("Play Audio");
            isJumping = false;
            landAudio.Play();
        }

        if (groundedPlayer && playerVelocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical") * velocitySmoothing);
        controller.Move(move * Time.deltaTime * playerSpeed);

        //Animation
        if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.3 && Mathf.Abs(Input.GetAxis("Vertical")) <= 0.3)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }


        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            jumpAudio.Play();
            anim.SetTrigger("TakeOff");
            Invoke("Jump", 0.2f);

            
        }


        playerVelocity.y += gravityValue * Time.deltaTime;

        if (playerVelocity.y < 0.0)
        {
            playerVelocity += Vector3.up * gravityValue * (fallMultipier - 1) * Time.deltaTime;
        }
        else if (playerVelocity.y > 0.0 && !Input.GetButton("Jump"))
        {
            playerVelocity += Vector3.up * gravityValue * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (transform.position.y < -10.5)
        {
            FindObjectOfType<GameController>().EndGame();
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
}