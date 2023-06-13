using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude >= 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController e = collision.gameObject.GetComponent<EnemyController>();

        if (e != null)
        {
            e.Fix();
        }
        Destroy(gameObject);
    }
}
