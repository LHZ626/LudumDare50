using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    private float speed = 300;
    private float jumpForce = 400f;
    public LayerMask ground;

    private Rigidbody2D rb;
    private Animator anim;
    private Collider2D coll;
    public Text txt;
    public GameObject death;
    public GameObject win;

    private float horizontalMove;
    private bool isTouchingEnemy;
    private bool isShoe;
    private float time = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        coll = gameObject.GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        if (!isTouchingEnemy)
        {
            Movement();
        }
        Jump();
        Shoe();
        Death();
    }

    //角色移动
    void Movement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove < 0) transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        if (horizontalMove > 0) transform.localScale = new Vector3(-0.05f, 0.05f, 0.05f);
        if(!anim.GetBool("isJumping")&&!anim.GetBool("isFalling"))
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
        else
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime * 0.5f, rb.velocity.y);
        if(horizontalMove != 0)
            anim.SetBool("isRunning", true);
        else
            anim.SetBool("isRunning", false);
    }

    void Jump()
    {
        if (coll.IsTouchingLayers(ground) && Input.GetButton("Jump") && !anim.GetBool("isJumping") && !anim.GetBool("isFalling"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            anim.SetBool("isJumping", true);
            anim.SetBool("isFalling", false);
        }
        if (rb.velocity.y < -1)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("isFalling", false);
        }
    }
    void Shoe()
    {
        if (isShoe)
        {
            speed = 500;
            jumpForce = 500f;
            Invoke("ShoeOff", 5.0f);
        }
        else
        {
            speed = 300;
            jumpForce = 400f;
        }
    }
    void ShoeOff()
    {
        isShoe = false;
    }
    void Death()
    {
        time -= Time.fixedDeltaTime;
        txt.text = time.ToString();
        if(time<=0)
        {
            txt.text = "0";
            Time.timeScale = 0;
            death.SetActive(true);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shoe")
        {
            isShoe = true;
            time -= 1f;
        }
        else if (other.tag == "Trap")
            time -= 3f;
        else if (other.tag == "Capsule")
            time += 2f;
        else if (other.tag == "Syringe")
            time += 5f;
        else if(other.tag == "Door")
        {
            Time.timeScale = 0;
            win.SetActive(true);
        }
    }
}
