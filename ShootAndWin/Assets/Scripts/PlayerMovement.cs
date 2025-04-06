using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float tiempoEntreDisparos = 1f;
    private bool puedeDisparar = true;

    public float limiteIzquierda = -8f;
    public float limiteDerecha = 8f;

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
        puedeDisparar = false;
        StartCoroutine(ReiniciarDisparo());
    }

    IEnumerator ReiniciarDisparo()
    {
        yield return new WaitForSeconds(tiempoEntreDisparos);
        puedeDisparar = true;
    }
}
