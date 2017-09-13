using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzalesMovement : MonoBehaviour
{
    private Animator anim;

    public float moveSpeed = 5;
    public float jumpForce = 25;

    public int maxJumps = 2;
    int currentJumpCount;

    bool onGround;
    bool moving;

    bool lookingRight = true;

    bool canJump
    {
        get
        {
            return currentJumpCount < maxJumps;
        }
    }

    private Rigidbody2D rb2d;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        DetectGround();

        anim.SetFloat("speed", 0);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetMouseButton(0))
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            anim.SetFloat("speed", 1);

            if (lookingRight)
            {
                lookingRight = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButton(1))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            anim.SetFloat("speed", 1);

            if (!lookingRight)
            {
                lookingRight = true;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void DetectGround()
    {
        if (!onGround && rb2d.velocity.y < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2);
            Debug.DrawRay(transform.position, Vector2.down * 2, Color.red);
            if (hit)
            {
                onGround = true;
                currentJumpCount = 0;
            }
        }
    }

    public void Death(Vector3 newSpawnPos)
    {

    }

    void Jump()
    {
        if (canJump)
        {
            if (onGround)
            {
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                currentJumpCount++;
                onGround = false;
            }
            else
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
                rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                currentJumpCount++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FireWave")
        {
            GameManager.Instance.ResetLevel();
        }
    }
}
