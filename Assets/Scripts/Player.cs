using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    Animator anim;
    public Slider slider;
    public LoadData user;
    public PlayManager play;

    [Header("jump")]
    public float jumpPower;
    public float downGravity;
    public LayerMask ground;
    public float jumpTime;
    public float defaultJumpTime;
    public bool onGround;
    public bool jumping;
    public bool canjump => (onGround || jumpTime > 0);

    public float Hp;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        jumpTime = defaultJumpTime;
        anim = GetComponent<Animator>();
        slider = FindObjectOfType<Slider>();
        user = FindObjectOfType<LoadData>();
        play = FindObjectOfType<PlayManager>();
        slider.maxValue = 30 + (user.GetHealthLV() - 1) * 5;
        slider.value = slider.maxValue;
        Hp = slider.value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!play.pause)
        {
            slider.value -= Time.deltaTime * 0.5f;
            Hp = slider.value;
            if (canjump)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    rigid.velocity = new Vector2(0, 0);
                    rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    jumpTime--;
                    jumping = true;
                }
            }

            if (rigid.velocity.y > 0)
            {
                boxCollider.size = new Vector2(boxCollider.size.x, 1f);
                anim.SetBool("Up", true);
                anim.SetBool("Down", false);
            }
            else if (rigid.velocity.y < 0)
            {
                boxCollider.size = new Vector2(boxCollider.size.x, 1f);
                anim.SetBool("Up", false);
                anim.SetBool("Down", true);
            }

            if (Input.GetKey(KeyCode.Z))
            {
                rigid.gravityScale = downGravity;
                boxCollider.size = new Vector2(boxCollider.size.x, 0.7f);
                anim.SetBool("Slide", true);
            }
            if (Input.GetKeyUp(KeyCode.Z))
            {
                
                anim.SetBool("Slide", false);
                boxCollider.size = new Vector2(boxCollider.size.x, 2.1f);
                rigid.gravityScale = 1;
            }
            if (Hp <= 0 )
            {
                Debug.Log("Ã¼·Â");
                play.GameOver();

            }
        }
    }

    void FixedUpdate()
    {
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(!anim.GetBool("Slide"))
                boxCollider.size = new Vector2(boxCollider.size.x, 2.1f);
            onGround = true;
            anim.SetBool("Ground", true);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
            jumping = false;
            jumpTime = defaultJumpTime;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = false;
            anim.SetBool("Ground", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Spike")
        {
            slider.value -= 5;

        }
        else if(collision.tag == "Death")
        {
            Debug.Log("³«»ç");
            play.GameOver();
        }
        else if(collision.tag == "BCoin")
        {
            play.ObtainCoin((int)(1+user.GetCoinLV() * 0.1));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "SCoin")
        {
            play.ObtainCoin((int)(5 + user.GetCoinLV() * 0.5));
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "GCoin")
        {
            play.ObtainCoin((int)(10 + user.GetCoinLV()));
            Destroy(collision.gameObject);
        }
    }

    public void JumpClick()
    {
        if (canjump)
        {
                rigid.velocity = new Vector2(0, 0);
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                jumpTime--;
                jumping = true;
        }
    }

    public void SlideDown()
    {
        rigid.gravityScale = downGravity;
        boxCollider.size = new Vector2(boxCollider.size.x, 0.7f);
        anim.SetBool("Slide", true);
    }
    public void SlideUp()
    {
        rigid.gravityScale = 1;
        boxCollider.size = new Vector2(boxCollider.size.x, 2.2f);
        anim.SetBool("Slide", false);
    }
}
