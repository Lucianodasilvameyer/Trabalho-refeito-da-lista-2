using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    
    private float InvencibilidadeInicial;

    [SerializeField]
    private float InvenciblilidadeMax;

    [SerializeField]
    private float tempoInicialCura;

    [SerializeField]
    private float tempoFinalCura;

    [SerializeField]
    private int taxaDeCura = 1;


    [SerializeField]
    private float distanciaProjetilPlayer;


   
    private float spawnarProjetilInicial;

    [SerializeField]
    private float spawnarProjetilnMax;



    [SerializeField]
    Animator ani;

    [SerializeField]
    Rigidbody2D body;

    Game game_ref;


    private float invencibilidadeInicio;

    [SerializeField]
    private float invencibilidadeMax;

    [SerializeField]
    bool recarregar = false;


    [SerializeField]
    bool isPlayer1;

    [SerializeField]
    private float speed;

    [SerializeField]
    private int hpInicial;

    public GameObject projetilPrefab;

    private bool invencivel = false;


    [SerializeField]
    private int HPMax = 50;

    [SerializeField]
    Slider sliderHp;

    [SerializeField]
    private int _hp = 50;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
            if (_hp <= 0)
            {

                print("morreu");
                if (sliderHp && sliderHp !=null)//aqui garante q não tem referencias ainda?
                sliderHp.value = 0;

                _hp = 0;
                Destroy(gameObject);
                Morrer();

            }
            else if (_hp >= HPMax)
            {
                _hp = HPMax;
                if(sliderHp && sliderHp != null)
                    sliderHp.value = 1;
            }
            else
            {
               
                if(sliderHp && sliderHp != null)
                    sliderHp.value = (float)_hp / (float)HPMax;
            }
        }
    }













    // Start is called before the first frame update
    void Start()
    {
        if (!game_ref || game_ref == null)
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        if (!body || body == null)
            body = GetComponent<Rigidbody2D>();

        if (!ani || ani == null)
            ani = GetComponent<Animator>();


        if (sliderHp && sliderHp != null)
            sliderHp.value = 1;

            Hp = 50;
       
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();

        if(isPlayer1)
        {
            if (recarregar == false && Input.GetKeyDown(KeyCode.T) && game_ref.isGameOver() == false)
            {
                SpawnarProjetil(distanciaProjetilPlayer);
                recarregar = true;
                spawnarProjetilInicial = Time.time;
            }
        }
        else
        {
            if (recarregar == false && Input.GetKeyDown(KeyCode.Keypad7) && game_ref.isGameOver() == false)
            {
                SpawnarProjetil(distanciaProjetilPlayer);
                recarregar = true;
                spawnarProjetilInicial = Time.time;
            }
        }
       


        if (Time.time >= spawnarProjetilInicial + spawnarProjetilnMax && recarregar == true)
        {
            recarregar = false;
        }



        if (Time.time >= tempoInicialCura + tempoFinalCura)
        {
            tempoInicialCura = Time.time;
            Hp += (int)taxaDeCura;

        }
        if (invencivel == true)
        {
            if (Time.time >= InvencibilidadeInicial + InvenciblilidadeMax)
            {

                toggleInvencibilidade();
            }
        }

    }
    private void Movimento()
    {
        Vector2 input;

        if (isPlayer1)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
        }

        Vector2 direction = input.normalized;
        Vector2 velocity = speed * direction;
        Vector2 moveAmont = velocity * Time.deltaTime;
        transform.Translate(moveAmont);

    }
    public void Morrer()
    {
        if(Hp <= 0)
        game_ref.GameOver();
     
    }





    public void SpawnarProjetil(float distanciaShurikenPlayer)
    {
        Vector3 posicaoInicial = transform.position;
        Vector3 position = posicaoInicial;
        position.x += distanciaShurikenPlayer;
        position.z = -1f;

        GameObject fit = Instantiate(projetilPrefab, transform.position, Quaternion.identity);
    }



    public void TomarDano(int dano)
    {
        Hp -= dano;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Inimigo"))
        {
            if (!invencivel)
                collision.GetComponent<Inimigo>().CausarDano(this);

            Destroy(collision.gameObject);


        }
        if (collision.CompareTag("CapaDeInvencibilidade"))
        {
            if (invencivel == false)
            {

                InvencibilidadeInicial = Time.time;//colocar aqui pq tem q começar a contar no momento q pegou
                toggleInvencibilidade();
            }
        }

    }
    private void toggleInvencibilidade()
    {
        invencivel = !invencivel;

    }

}