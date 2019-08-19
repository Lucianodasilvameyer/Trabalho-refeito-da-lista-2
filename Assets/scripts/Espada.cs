using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    private float speed;
    // Start is called before the first frame update

    Inimigo ini_ref;
    void Start()
    {
        if (!body || body == null)
        body = GetComponent<Rigidbody2D>();

        if (!ini_ref || ini_ref == null)
        ini_ref = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<Inimigo>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverEspada();
    }
    public void MoverEspada()
    {
        Vector2 Input = new Vector2(1, 0);
        Vector2 Direction = Input.normalized;
        Vector2 Velocity = speed * Direction;
        Velocity.y = body.velocity.y;
        body.velocity = Velocity;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            ini_ref.somDeMorteDoInimigo();
        }
    }
}
