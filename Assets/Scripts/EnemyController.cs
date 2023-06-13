using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rb;
    float timer;
    int direction = 1;

    bool broken = true;
    Animator anim;

    public ParticleSystem smokeParticles;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (!broken)
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position;

        if (vertical)
        {
            anim.SetFloat("Move X", 0);
            anim.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            anim.SetFloat("Move X", direction);
            anim.SetFloat("Move Y", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        if (!broken)
        {
            return;
        }
        

        rb.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController player = collision.gameObject.GetComponent<RubyController>();

        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        smokeParticles.Stop();
        broken = false;
        rb.simulated = false;
        anim.SetTrigger("Fixed");
    }
}
