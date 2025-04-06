using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public TextMeshProUGUI countdownText; // 3, 2, 1, GO!
    public GameObject coutdownBackground; // Fondo del countdown
    private bool juegoActivo = false;
    public static bool gameOverState = false;
    public GameObject bulletPrefab;

    //---------------------------- COUNTDOWN -----------------------------------------
    public float tiempoRestante = 60f;
    public TextMeshProUGUI timeText; // Texto para mostrar el tiempo restante

    //---------------------------- RESTART -----------------------------------------
    public Transform placasFaciles;
    public Transform placasDificiles;
    public Transform player;
    private Vector3 inicioPF;
    private Vector3 inicioPD;
    private Vector3 inicioP;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        inicioPF = placasFaciles.transform.position;
        inicioPD = placasDificiles.transform.position;
        inicioP = player.transform.position;
    }

    void Start()
    {
        StartCoroutine(ContarRegresivaInicio());
    }

    void Update()
    {
        // COUNTDOWN
        if (!juegoActivo) return; // Si el juego no está activo, no actualizamos el tiempo
        
        tiempoRestante -= Time.deltaTime; // Resta el tiempo transcurrido desde el último frame

        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0; // Aseguramos que no sea negativo
            GameOver();

            // Restart
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Restart();
            }
        }

        MostrarTiempo(tiempoRestante);
    }

    public void Restart()   // Reinicia el juego
    {
        Time.timeScale = 1; // Reanuda el juego
        tiempoRestante = 60f; // Reinicia el tiempo
        score = 0; // Reinicia el puntaje
        UpdateScoreText(); // Actualiza el texto del puntaje

        placasFaciles.transform.position = inicioPF; // Reinicia la posición de las placas fáciles
        placasDificiles.transform.position = inicioPD; // Reinicia la posición de las placas difíciles
        player.transform.position = inicioP; // Reinicia la posición del jugador

        juegoActivo = false; // Evita que Update avance
        StartCoroutine(ContarRegresivaInicio());
    }

    
    IEnumerator ContarRegresivaInicio()
    {
        juegoActivo = false;
        Time.timeScale = 0f;

        countdownText.gameObject.SetActive(true);   // Muestra el texto del countdown
        coutdownBackground.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);  // Oculta el texto del countdown
        coutdownBackground.SetActive(false);

        Time.timeScale = 1f;
        juegoActivo = true;
    }

    //---------------------------- PUNTUACION -----------------------------------------
    public void AddPoints(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
        UpdateScoreText();
    }
    
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
    
    //---------------------------- COUNTDOWN -----------------------------------------
    void MostrarTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void GameOver()
    {
        Debug.Log("¡Game Over!");
        Time.timeScale = 0; // Pausa el juego
        gameOverState = false; // Cambia el estado de Game Over

        GameObject[] Placas = GameObject.FindGameObjectsWithTag("Placa");   // Buscar todas las bolas mágicas y destruirlas
        foreach (GameObject Placa in Placas)
        {
            Placa.gameObject.SetActive(true); // Activa las placas
        }

        GameObject[] bullets = GameObject.FindGameObjectsWithTag("bullet");   // Buscar todas las bolas mágicas y destruirlas
        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }
    }
    public bool EstaJugando()
    {
        return juegoActivo;
    }
}
