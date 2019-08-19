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
    Player[] players;

   
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
            SpawnarInimigo<Abobora>(distanciaInimigoPlayer, Random.Range(0, players.Length));
        }
        if (Time.time >= spawnarZumbiInicial + spawnarZumbiMax)
        {
            spawnarZumbiInicial = Time.time;
            SpawnarInimigo<Zumbi>(distanciaInimigoPlayer, Random.Range(0, players.Length));
        }
        if(Time.time >= spawnarRoboInicial+ spawnarRoboMax)
        {
            spawnarRoboInicial = Time.time;
            SpawnarInimigo<Robo>(distanciaInimigoPlayer,Random.Range(0, players.Length));
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
    public void SpawnarInimigo<Y>(float distanciaInimigoPlayer, int indexPlayer)
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
                    listaInimigos.RemoveAt(index);
                    Dinossauro D = (Dinossauro)listaInimigos[index];
                   
                   
                    D.transform.position = position;
                    D.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Robo))
            {
                if (listaInimigos.OfType<Robo>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Robo));
                    listaInimigos.RemoveAt(index);
                    Robo D = (Robo)listaInimigos[index];
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
                    listaInimigos.RemoveAt(index);
                    Zumbi D = (Zumbi)listaInimigos[index];
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
                    listaInimigos.RemoveAt(index);
                    Abobora D = (Abobora)listaInimigos[index];
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
            Destroy(inimigo, inimigo.somMorteInimigo.length);
            
        }
    }

}

    