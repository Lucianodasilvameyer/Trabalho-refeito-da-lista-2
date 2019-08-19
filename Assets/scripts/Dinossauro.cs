using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinossauro : Inimigo
{
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        base.Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projetil"))
        {
            Destroy(collision.gameObject);
            addPool();

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            CausarDano(collision.GetComponent<Player>());
            addPool();
        }
    }
}
