using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Motion : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public float moveInput;
    public Image bar;
    private double fill;

    int healthPoints = 100;   
    private bool isDead, isHurting;


    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    Animator anim;
    float vertical;

    private int damage;


    public void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fill = 1f;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        //moveInput = Input.GetAxis("Horizontal");
        if (!isDead)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        if (facingRight == false && moveInput > 0 && !isDead)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0 && !isDead)
        {
            Flip();
        }
    }
    void Update()
    {
        bar.fillAmount = Convert.ToSingle(fill); 
        if (moveInput != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else if (!isDead)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (isGrounded == true || isDead || isHurting)
        {
            anim.SetBool("isJumping", false); 
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
            if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jump();
        }
    }

    public void jump()
    {
        if (extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MovingPlatform"))
        {
            transform.parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("DieZone"))
        {
            healthPoints = 0;
            fill -= 1;
        }

        if (col.gameObject.tag.Equals("PassZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            anim.SetTrigger("isRecovering");
        }
        if (col.gameObject.tag.Equals("PreviousZone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            anim.SetTrigger("isRecovering");
        }

        switch (col.gameObject.tag)
        {
            case "Monsterlvl1":
                damage = 10;
                break;
            case "Monsterlvl2":
                damage = 30;
                break;
            case "Monsterlvl3":
                damage = 50;
                break;
            default:
                damage = 0;
                break;
        }

        if (col.gameObject.name.Equals("monster"))
        {
            healthPoints -= damage;
            fill = healthPoints / 100f;
            if (healthPoints > 0)
            {
                anim.SetTrigger("isHurting");
                StartCoroutine("Hurt");
                Hurt();
            }
        }
        if (healthPoints <= 0)
        {
            isDead = true;
        }
        if (isDead)
        {
            anim.SetTrigger("isDead");
        }
        if (col.gameObject.tag.Equals("DieZone2"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (healthPoints < 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    IEnumerator Hurt()
    {
        isHurting = true;
        rb.velocity = Vector2.zero;

        if (facingRight)
            rb.AddForce(new Vector2(-200f, 200f));
        else
            rb.AddForce(new Vector2(200f, 200f));

        yield return new WaitForSeconds(0.5f);

        isHurting = false;
    }
    
     
}
