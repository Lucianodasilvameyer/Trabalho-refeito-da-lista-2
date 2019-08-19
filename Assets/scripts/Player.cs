using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    Slider2 sliderHp2;

    [SerializeField]
    private float InvencibilidadeInicial;

    [SerializeField]
    private float InvenciblilidadeMax;

    [SerializeField]
    private float tempoInicialCura;

    [SerializeField]
    private float tempoFinalCura;

    [SerializeField]
    private float taxaDeCura=0.5f;


    [SerializeField]
    private float distanciaShurikenPlayer;

    [SerializeField]
    private float distanciaEspadaPlayer;

    [SerializeField]
    private float spawnarShurikenInicial;

    [SerializeField]
    private float spawnarShurikenMax;

    [SerializeField]
    private float spawnarEspadaInicial;

    [SerializeField]
    private float spawnarEspadaMax;

    [SerializeField]
    Animator ani;

    [SerializeField]
    Rigidbody2D body;

    Game game_ref;

    [SerializeField]
    private float invencibilidadeInicio;

    [SerializeField]
    private float invencibilidadeMax;

    [SerializeField]
    bool recarregar = false;

    [SerializeField]
    bool recarregar2 = false;

    
    bool isPlayer1;

    [SerializeField]
    private float speed;

    public GameObject shurikenPrefab;
    public GameObject espadaPrefab;

    private bool invencivel = false;

    [SerializeField]
    private int hpInicial = 1;

    [SerializeField]
    private int HPMax;
    
    [SerializeField]
    Slider sliderHp;
    
    private int _hp;
    public int Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value <= 0)
            {
                _hp = 0;
                sliderHp.value = 0;
                Destroy(gameObject);
                Morrer();

            }
            else if (value >= HPMax)
            {
                _hp = HPMax;
                sliderHp.value = 1;
            }
            else
            {
                _hp = value;
                sliderHp.value = (float)_hp / (float)HPMax;
            }
        }
    }
    [SerializeField]
    private int HPMax2;

    

    private int _hp2;
    public int Hp2
    {
        get
        {
            return _hp2;
        }
        set
        {
            if (value <= 0)
            {
                _hp2 = 0;
                sliderHp2.value = 0;
                Destroy(gameObject);
                Morrer();

            }
            else if (value >= HPMax2)
            {
                _hp2 = HPMax2;
                sliderHp2.value = 1;
            }
            else
            {
                _hp2 = value;
                sliderHp2.value = _hp2 /HPMax2;
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
           

        Hp = HPMax;
        Hp2 = HPMax2;
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();

        if(recarregar==false && Input.GetKeyDown(KeyCode.T) && game_ref.isGameOver() == false)
        {
            SpawnarShuriken(distanciaShurikenPlayer);
            recarregar = true;
            spawnarShurikenInicial = Time.time;
        }


        if (Time.time >= spawnarShurikenInicial + spawnarShurikenMax && recarregar==true)
        {
            recarregar = false;
        }

        if(recarregar2==false && Input.GetKeyDown(KeyCode.P) && game_ref.isGameOver() == false)
        {
            recarregar2 = true;
            SpawnarEspada(distanciaEspadaPlayer);
            spawnarEspadaInicial = Time.time;
        }
        if(Time.time>=spawnarEspadaInicial+spawnarEspadaMax && recarregar2 == true)
        {
            recarregar2 = false;
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

        if(isPlayer1)
        {
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            input=new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
        }

        Vector2 direction = input.normalized;
        Vector2 velocity = speed * direction;
        Vector2 moveAmont = velocity * Time.deltaTime;
        transform.Translate(moveAmont);

    }
    public void Morrer()
    {
        game_ref.GameOver();
        print("morreu");
    }
    




    public void SpawnarShuriken(float distanciaShurikenPlayer)
    {
        Vector3 posicaoInicial = transform.position;
        Vector3 position = posicaoInicial;
        position.x += distanciaShurikenPlayer;
        position.z = -1f;

        GameObject fit = Instantiate(shurikenPrefab, transform.position, Quaternion.identity);
    }
    

    public void SpawnarEspada(float distanciaEspadaPlayer)
    {
        Vector3 inipos = transform.position;
        Vector3 position = inipos;
        position.x = distanciaEspadaPlayer;
        position.z = -1f;

        GameObject rit = Instantiate(espadaPrefab, position, Quaternion.identity);
    }
    public void TomarDano(int dano)
    {
        Hp -= dano;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("inimigo"))
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

    internal class Slider2
{
    internal int value;
}