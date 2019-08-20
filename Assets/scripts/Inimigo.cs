using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inimigo : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip somMorteInimigo;

    [SerializeField]
    protected float speed;


    [SerializeField]
     public float distanciaPlayer;

    [SerializeField]
    public Vector3 direction;

    [SerializeField]
    public Transform target;

    [SerializeField]
    protected float strength;

    [SerializeField]
    Player[] players = new Player[2];
    Game game_ref;
    bool isActive = true;


    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void init()
    {
        
        audioSource = GetComponent<AudioSource>();
        game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        GameObject[] playerGO = GameObject.FindGameObjectsWithTag("Player");
       
        players = new Player[playerGO.Length];
        for (int i = 0; i < playerGO.Length; i++)
        {
            players[i] = playerGO[i].GetComponent<Player>();
        }

        definirAlvo();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public virtual void Move()
    {
        if (!isActive) return;

        Vector2 velocity = speed * direction * Time.deltaTime;
        transform.Translate(velocity);
    }
    public virtual void definirAlvo()
    {
        
        if (!players.Any())
            return;

       
        if (!target || target == null)
        {
           
            int idx = Random.Range(0, players.Length);

            if (idx <= 0 && idx >= players.Length)
            {
                idx = 0;
            }
                if (idx >= 0 && idx < players.Length)
            {
                target = players[idx].transform; //aqui sorteia qual vai ser o alvo, player1 ou player2? sim

                direction = target.position - transform.position;
                direction = direction.normalized;
            }
            
        }
        
    }
    public virtual void CausarDano(Player alvo)
    {
        if(!alvo.isInvencivel())
        alvo.TomarDano((int)strength);
    }
    public void somPlay(AudioClip som)
    {
        audioSource.clip = som;
        audioSource.Play();
    }
    public void addPool()
    {
        game_ref.addList(this);


    } 


    public void SetActive(bool active)
    {
        isActive = active;
        GetComponent<SpriteRenderer>().enabled = active;
        GetComponent<BoxCollider2D>().enabled = active;
        if(active)
        definirAlvo();

    }
}
