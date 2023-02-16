using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    [SerializeField]
    protected Transform pointA, pointB;
    protected Vector3 target;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Player player;
    public bool go, back;
    void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    public virtual void Movement()
    {

        if (target == pointA.position)
        {
            sprite.flipX = go;
        }
        else
        {
            sprite.flipX = back;
        }
        if (transform.position == pointA.position)
        {
            target = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            target = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        
    }
    public void Hurting()
    {
        Collider2D  coll = GetComponent<Collider2D>();
        StartCoroutine(enableColl(coll));
    }
    public void Hurt()
    {
        
        Destroy(gameObject);
    }
    IEnumerator enableColl(Collider2D coll)
    {
        coll.enabled = false;
        yield return new WaitForSeconds(1);
        coll.enabled = true;
    }
}
