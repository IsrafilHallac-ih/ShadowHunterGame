using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 10f;
    public float gravity = -14f;
    public int playerHealth = 100;
    public Text healthText;
    public Slider slider;


    //EnemyHealth
    public int EnemyHealt = 100;
    public Text EnemyHealthText;
    public Slider EnemySlider;



    private Vector3 gravityVector;


    public Transform groundCheckPoint;
    public float groundCheckRadius = 0.35f;
    public LayerMask groundLayer;

    public bool isGrounded=false;
    public float jumpSpeed = 5f;
   


    void Start()
    {
        characterController = GetComponent<CharacterController>();
       
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        GroundCheck();
        PlayerJump();
       














}

     void MovePlayer() 
    {
        Vector3 moveVector=Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        characterController.Move(moveVector * speed * Time.deltaTime);
    }  

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    void PlayerJump()
    {
        gravityVector.y += gravity * Time.deltaTime;
        characterController.Move(gravityVector * Time.deltaTime);
        if (isGrounded && gravityVector.y < 0)
        {
            gravityVector.y = -3f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVector.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }
    }
    void PlayerTakeDamage(int damageAmount)
    {
        

        playerHealth -= 100;
        slider.value -= 100;
        

        if (playerHealth<=0)
        {

            PlayerDeath();
           
            slider.value = 0;        
        }
    }

    private void EnemyTakeDamage(int damageAmount)
    {
        EnemyHealt -= 100;
        slider.value -= 100;
       
        if (EnemyHealt <= 0)
        {
           
            EnemySlider.value= 0;
        }

    }
    void PlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    

}
