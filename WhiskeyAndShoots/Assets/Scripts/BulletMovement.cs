using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed = 10f;
    public int points = 0;

    void Update()
    {
        if (GameManager.gameOverState) // Si el juego ha terminado, destruimos la bala
        {
            Destroy(gameObject); // Destruye la bala
            return; // Terminamos la ejecución de Update para evitar más operaciones
        }

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if(transform.position.y > 8f) // Limite superior de la pantalla
        {
            Destroy(gameObject); // Destruye la bala si sale de la pantalla
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Placa"))
        {
            other.GetComponent<Placa>().Impactado();
            Destroy(gameObject); // Destruye la bala al impactar con la placa
        }
    }
}
