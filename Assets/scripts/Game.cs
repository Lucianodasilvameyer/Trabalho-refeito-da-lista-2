using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Game : MonoBehaviour
{

    private float spawnarDinossauroInicial;
    [SerializeField]
    private float spawnarDinossauroMax;

    private float spawnarCapaDeInvencibilidadeInicial;

    [SerializeField]
    private float spawnarCapaDeInvencibilidadeMax;


    private float spawnarAboboraInicial;
    [SerializeField]
    private float spawnarAboboraMax;


    private float spawnarZumbiInicial;
    [SerializeField]
    private float spawnarZumbiMax;


    private float spawnarRoboInicial;
    [SerializeField]
    private float spawnarRoboMax;


    [SerializeField]
    private float distanciaInimigoPlayer;



    public GameObject CapaDeInvencibilidadePrefab;
    public GameObject InimigoDinossauroPrefab;
    public GameObject InimigoAboboraPrefab;
    public GameObject InimigoZumbiPrefab;
    public GameObject InimigoRoboPrefab;

    public TextMeshProUGUI gameOvertext;

    [SerializeField]
    private float taxaDePontos;

    [SerializeField]
    private float pontuacaoInicio;

    [SerializeField]
    private float pontuacaoMax;

    bool isplayer1;

    public TextMeshProUGUI textoPontuacao;

    public TextMeshProUGUI textoPontuacao2;

    private float _score;

    bool gameOver = false;

    [SerializeField]
    Player[] players;//se define o numero de player no inspector



   
    public float Score
    {
        get
        {
            return _score;
        }
        set
        {
            if (value <= 0)
            {
                _score = 0;
            }
            else
            {
                _score=value;
                textoPontuacao.text = "Pontuacao" + _score;

            }
        }
    }
    private float _score2;

    public float Score2
    {
        get
        {
            return _score2;
        }
        set
        {
            if (value <= 0)
            {
                _score2 = 0;
            }
            else
            {
                _score2 = value;
                textoPontuacao2.text = "Pontuacao" + _score2;

            }
        }
    }
    Queue<PowerUps> poolPowerUps = new Queue<PowerUps>();
    List<Inimigo> listaInimigos = new List<Inimigo>();

    // Start is called before the first frame update
    void Start()
    {
       

        GameObject[] playerGO = GameObject.FindGameObjectsWithTag("Player");
        players = new Player[playerGO.Length];
        for (int i = 0; i < playerGO.Length; i++)
        {
            players[i] = playerGO[i].GetComponent<Player>();
        }
        
        Score = 0;
        Score2 = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=pontuacaoInicio+pontuacaoMax)
        {
            pontuacaoInicio = Time.time;
            Score += taxaDePontos;
            Score2 += taxaDePontos;
        }
        

        if (Time.time >= spawnarDinossauroInicial + spawnarDinossauroMax)
        {
            spawnarDinossauroInicial = Time.time;
          
            SpawnarInimigo<Dinossauro>(distanciaInimigoPlayer, Random.Range(0, players.Length ));
        }
        if (Time.time>= spawnarAboboraInicial + spawnarAboboraMax)
        {
            spawnarAboboraInicial = Time.time;
            SpawnarInimigo<Abobora>(distanciaInimigoPlayer, Random.Range(0, players.Length));//o Length é o tamanho maximo do array
        }
        if (Time.time >= spawnarZumbiInicial + spawnarZumbiMax)
        {
            spawnarZumbiInicial = Time.time;
            SpawnarInimigo<Zumbi>(distanciaInimigoPlayer, Random.Range(0, players.Length));//quando usa random range com float os valores são inclusivos, ou seja contando o primeiro e o ultimo numero
        }                                                                                   //quando usa random range com in os valor maximo é exclusivo, ou seja não ira contar o ultimo numero
        if (Time.time >= spawnarRoboInicial+ spawnarRoboMax)
        {
            spawnarRoboInicial = Time.time;
            SpawnarInimigo<Robo>(distanciaInimigoPlayer,Random.Range(0, players.Length));//(0, players.Length) servem para diser q deve ir de zero ao tamanho do array, sempre usar o zero
        }

        if (Time.time >= spawnarCapaDeInvencibilidadeInicial + spawnarCapaDeInvencibilidadeMax)
        {
            spawnarCapaDeInvencibilidadeInicial = Time.time;
            spawnarCapaDeInvencibilidade(5f);
        }

    }
    public void GameOver()
    {
        gameOvertext.gameObject.SetActive(true);
    }
    public bool isGameOver()
    {
        return gameOver;
    }
    public void spawnarCapaDeInvencibilidade(float distanciaDoPlayer)
    {
        Vector2 position = players[Random.Range(0, players.Length)].transform.position;
        position.x += distanciaDoPlayer;
        position.y += distanciaDoPlayer;

        if (poolPowerUps.Count > 0)
        {
            PowerUps p = poolPowerUps.Dequeue();
            p.transform.position = position;
            p.gameObject.SetActive(true);
        }
        else
        {
            GameObject go = Instantiate(CapaDeInvencibilidadePrefab, position, Quaternion.identity);
        }
        

       

        


    }
    public void addPool(PowerUps p)
    {

        if (poolPowerUps.Count > 0)
        {
            poolPowerUps.Enqueue(p);
            p.gameObject.SetActive(false);
        }
        else
        {
            Destroy(p.gameObject);
        }
    }


    public void SpawnarInimigo<Y>(float distanciaInimigoPlayer, int indexPlayer)//aqui o int indexplayer é o valor q sera usado no array
    {
        if (!typeof(Y).IsSubclassOf(typeof(Inimigo)) || (indexPlayer < 0 || indexPlayer >= players.Length))
            return;

   
        Vector3 iniPos = players[indexPlayer].transform.position;
        Vector3 position = iniPos;
        position.x += distanciaInimigoPlayer;
        position.y += distanciaInimigoPlayer;
        position.z = -1f;

        if (listaInimigos.Count > 0)
        {
            if (typeof(Y) == typeof(Dinossauro))
            {
                if (listaInimigos.OfType<Dinossauro>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Dinossauro));
                   
                    Dinossauro D = (Dinossauro)listaInimigos[index];
                    listaInimigos.RemoveAt(index);


                    D.transform.position = position;//aqui o segundo position é o vector 3 de cima
                    D.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Robo))
            {
                if (listaInimigos.OfType<Robo>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Robo));
                    Robo D = (Robo)listaInimigos[index]; //aqui tem q fazer a referencia para poder tirar da lista
                    listaInimigos.RemoveAt(index);//aqui se colocar o listInimigos antes do Robo D, não podera achalo na lista pois ele não estará la 
                                                  //o listaInimigos.RemoveAt(index) ja sabe q tem q tirar o robo da lista pois esta no if do robo
                    position.x += D.distanciaPlayer;
                    position.y += D.distanciaPlayer;
                    D.transform.position = position;
                    D.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Zumbi))
            {
                if (listaInimigos.OfType<Zumbi>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Zumbi));
                    
                    Zumbi D = (Zumbi)listaInimigos[index];
                    listaInimigos.RemoveAt(index);
                    position.x += D.distanciaPlayer;
                    position.y += D.distanciaPlayer;
                    D.transform.position = position;
                    D.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Abobora))
            {
                if (listaInimigos.OfType<Abobora>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Abobora));
                    Abobora D = (Abobora)listaInimigos[index];
                    listaInimigos.RemoveAt(index);
                    
                    position.x += D.distanciaPlayer;
                    position.y += D.distanciaPlayer;
                    D.transform.position = position;
                    D.SetActive(true);
                }
            }
        }
        else
        {
           
            if (typeof(Y) == typeof(Dinossauro))
            {
                

                Dinossauro inimigo = Instantiate(InimigoDinossauroPrefab, position, Quaternion.identity).GetComponent<Dinossauro>();
                //inimigo.init();
            }
            if (typeof(Y) == typeof(Robo))
            {
              

                Robo inimigo = Instantiate(InimigoRoboPrefab, position, Quaternion.identity).GetComponent<Robo>();

               // inimigo.init();
            }
            if (typeof(Y) == typeof(Zumbi))
            {
               
                Zumbi inimigo = Instantiate(InimigoZumbiPrefab, position, Quaternion.identity).GetComponent<Zumbi>();
               // inimigo.init();
            }
            if (typeof(Y) == typeof(Abobora))
            {
               

                Abobora inimigo = Instantiate(InimigoAboboraPrefab, position, Quaternion.identity).GetComponent<Abobora>();
               // inimigo.init();
            }
        }
            
        

    }
    public void addList(Inimigo inimigo)
    {
        if (listaInimigos.Count > 0)
        {
            inimigo.SetActive(false);
            listaInimigos.Add(inimigo);
        }
        else
        {
            inimigo.somPlay(inimigo.somMorteInimigo);
            Destroy(inimigo.gameObject, inimigo.somMorteInimigo.length);
            
        }
    }


}

    