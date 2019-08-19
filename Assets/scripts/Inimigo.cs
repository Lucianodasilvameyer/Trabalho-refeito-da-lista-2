using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somMorteInimigo;

    [SerializeField]
    protected float speed;

    [SerializeField]
    public Vector3 direction;

    [SerializeField]
    public Transform target;

    [SerializeField]
    protected float strength;

    Player player_ref;
    Game game_ref;


    // Start is called before the first frame update
    void Start()
    {
        alvo();
        game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        player_ref = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        Vector2 velocity = speed * direction * Time.deltaTime;
        transform.Translate(velocity);
    }
    public virtual void alvo()
    {
        if (!target || target == null)
        {
            target = GameObject.FindGameObjectWithTag("player").transform;
        }
        direction = target.position - transform.position;
        direction = direction.normalized;
    }
    public virtual void CausarDano(Player alvo)
    {
        player_ref.TomarDano((int)strength);
    }
    public void somPlay(AudioClip som)
    {
        audioSource.clip = som;
        audioSource.Play();
    }
    public void somDeMorteDoInimigo()
    {
        somPlay(somMorteInimigo);
        game_ref.addList(this);
    } 
}
