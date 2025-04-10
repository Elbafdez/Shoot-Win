using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float tiempoEntreDisparos = 1f;
    private bool puedeDisparar = true;
    public float limiteIzquierda = -8f;
    public float limiteDerecha = 8f;
    public int balasDisparadas = 0; // Contador de balas disparadas

    //----------------------------- SONIDO -----------------------------------------
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip[] clipsDeDisparo; // Tus clips de sonido de disparo

    void Update()
    {
        if (!GameManager.Instance.EstaJugando()) return;    // Verifica si el juego está activo

        float h = Input.GetAxisRaw("Horizontal");
        Vector3 movimiento = Vector2.right * h * moveSpeed * Time.deltaTime;
        transform.Translate(movimiento);

        // Limitar la posición en X
        float xClamped = Mathf.Clamp(transform.position.x, limiteIzquierda, limiteDerecha);
        transform.position = new Vector3(xClamped, transform.position.y, transform.position.z);

        // Disparo con cooldown usando bool y corutina
        if (Input.GetKeyDown(KeyCode.Space) && puedeDisparar)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        balasDisparadas++;
        // Reproducir sonido de disparo
        if (clipsDeDisparo.Length > 0 && audioSource != null)
        {
            int index = Random.Range(0, clipsDeDisparo.Length);
            audioSource.PlayOneShot(clipsDeDisparo[index]);
        }

        puedeDisparar = false;
        StartCoroutine(ReiniciarDisparo());
    }

    IEnumerator ReiniciarDisparo()
    {
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }
}
