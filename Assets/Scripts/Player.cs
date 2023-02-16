using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public GameObject deathPrefab;
    public int health = 1;
    //public int diamonds;
    private Rigidbody2D rb;
    private PlayerAnimator playerAnim;
    private SpriteRenderer sprite;
    public float speed;
    public float jumpForce = 5.0f;
    private bool resetJump = false;
    void Start()
    {
        //diamonds = GameManager.score;
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<PlayerAnimator>();
        sprite = playerAnim.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed,rb.velocity.y);
        playerAnim.Run(Mathf.Abs(rb.velocity.x));
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }else if (rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }
        if (IsGrounded())
        {
            playerAnim.Jump(false);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            SoundManager.instance.Play("Jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
            playerAnim.Jump(true);
        }
        
    }
    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, .6f, 1 << 8);
        
        if (hitInfo.collider != null)
        {
            if (!resetJump)
            {
                return true;
            }

        }
        return false;
    }
    IEnumerator ResetJumpRoutine()
    {
        resetJump = true;
        yield return new WaitForSeconds(.1f);
        resetJump = false;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            if (collision.contacts[0].normal.y > 0)
            {
                collision.gameObject.SetActive(false);
                SoundManager.instance.Play("Damage");
            }
            else if(collision.contacts[0].normal.x > 0)
            {
                SoundManager.instance.Play("Death");
                Hurt(1);
            }
        }
    }
    public void Hurt(int damage)
    {
        health -= damage;
        StartCoroutine(Red());

    }
    IEnumerator Red()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.white;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y - .6f));
    }
    //public void AddDiamonds()
    //{
    //    diamonds += 1;
    //    GameManager.score = diamonds;
    //}
}
