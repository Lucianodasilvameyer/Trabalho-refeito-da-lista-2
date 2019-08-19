using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class Game : MonoBehaviour
{
    [SerializeField]
    private float spawnarDinossauroInicial;
    [SerializeField]
    private float spawnarDinossauroMax;

    [SerializeField]
    private float spawnarAboboraInicial;
    [SerializeField]
    private float spawnarAboboraMax;

    [SerializeField]
    private float spawnarZumbiInicial;
    [SerializeField]
    private float spawnarZumbiMax;

    [SerializeField]
    private float spawnarRoboInicial;
    [SerializeField]
    private float spawnarRoboMax;


    [SerializeField]
    private int distanciaInimigoPlayer;

    [SerializeField]
    private int distanciaInimigoPlayer2;

    [SerializeField]
    private int distanciaInimigoPlayer3;

    [SerializeField]
    private int distanciaInimigoPlayer4;

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

    Player player_ref;
    Inimigo ini;
   
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
        if (!ini || ini == null)
        ini = GameObject.FindGameObjectWithTag("Inimigo").GetComponent<Inimigo>();    

        Score = 0;
        Score2 = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=pontuacaoInicio+pontuacaoMax && isplayer1==true)
        {
            pontuacaoInicio = Time.time;
            Score += taxaDePontos;
        }
        else
        {
            pontuacaoInicio = Time.time;
            Score2 += taxaDePontos;
        }

        if (Time.time >= spawnarDinossauroInicial + spawnarDinossauroMax)
        {
            spawnarDinossauroInicial = Time.time;
            SpawnarInimigo<Dinossauro>(distanciaInimigoPlayer);
        }
        if (Time.time>= spawnarAboboraInicial + spawnarAboboraMax)
        {
            spawnarAboboraInicial = Time.time;
            SpawnarInimigo<Abobora>(distanciaInimigoPlayer2);
        }
        if (Time.time >= spawnarZumbiInicial + spawnarZumbiMax)
        {
            spawnarZumbiInicial = Time.time;
            SpawnarInimigo<Zumbi>(distanciaInimigoPlayer3);
        }
        if(Time.time >= spawnarRoboInicial+ spawnarRoboMax)
        {
            spawnarRoboInicial = Time.time;
            SpawnarInimigo<Robo>(distanciaInimigoPlayer4);
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
    public void SpawnarInimigo<Y>(int distanciaInimigoPlayer)
    {
        if (!typeof(Y).IsSubclassOf(typeof(Inimigo)))
            return;
        Vector3 iniPos = player_ref.transform.position;
        Vector3 position = iniPos;
        position.x = distanciaInimigoPlayer;
        position.y = distanciaInimigoPlayer;
        position.z = -1f;

        if (listaInimigos.Count > 0)
        {
            if (typeof(Y) == typeof(Dinossauro))
            {
                if(listaInimigos.OfType<Dinossauro>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Dinossauro));
                    listaInimigos.RemoveAt(index);
                    Dinossauro D = (Dinossauro)listaInimigos[index];
                    D.transform.position = position;
                    D.gameObject.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Robo))
            {
                if (listaInimigos.OfType<Robo>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Robo));
                    listaInimigos.RemoveAt(index);
                    Robo D = (Robo)listaInimigos[index];
                    D.transform.position = position;
                    D.gameObject.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Zumbi))
            {
                if (listaInimigos.OfType<Zumbi>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Zumbi));
                    listaInimigos.RemoveAt(index);
                    Zumbi D = (Zumbi)listaInimigos[index];
                    D.transform.position = position;
                    D.gameObject.SetActive(true);
                }
            }
            else if (typeof(Y) == typeof(Abobora))
            {
                if (listaInimigos.OfType<Abobora>().Any())
                {
                    int index = listaInimigos.FindLastIndex(x => x.GetType() == typeof(Abobora));
                    listaInimigos.RemoveAt(index);
                    Abobora D = (Abobora)listaInimigos[index];
                    D.transform.position = position;
                    D.gameObject.SetActive(true);
                }
            }
            else
            {
                if (typeof(Y) == typeof(Dinossauro))
                {
                    Instantiate(InimigoDinossauroPrefab, position, Quaternion.identity);
                }
                if (typeof(Y) == typeof(Robo))
                {
                    Instantiate(InimigoRoboPrefab, position, Quaternion.identity);
                }
                if (typeof(Y) == typeof(Zumbi))
                {
                    Instantiate(InimigoZumbiPrefab, position, Quaternion.identity);
                }
                if (typeof(Y) == typeof(Abobora))
                {
                    Instantiate(InimigoAboboraPrefab, position, Quaternion.identity);
                }
            }
            
        }

    }
    public void addList(Inimigo inimigo)
    {
        if (listaInimigos.Count > 0)
        {
            listaInimigos.Add(inimigo);
        }
        else
        {
            inimigo.somDeMorteDoInimigo();
            Destroy(inimigo);
            
        }
    }

}

    