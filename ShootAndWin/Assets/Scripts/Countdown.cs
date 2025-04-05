using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float tiempoRestante = 60f;
    public TextMeshProUGUI textoCuentaAtras;
    private bool tiempoActivo = true;

    void Update()
    {
        if (tiempoActivo)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0; // Aseguramos que no sea negativo
                tiempoActivo = false;
                TerminarJuego();
            }

            MostrarTiempo(tiempoRestante);
        }
    }

    void MostrarTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        textoCuentaAtras.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    void TerminarJuego()
    {
        Debug.Log("¡Tiempo agotado!");
        Time.timeScale = 0; // Pausa el juego
    }
}
