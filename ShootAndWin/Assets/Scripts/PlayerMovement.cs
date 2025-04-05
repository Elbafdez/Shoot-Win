using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float limiteIzquierda = -8f;
    public float limiteDerecha = 8f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 movimiento = Vector2.right * h * moveSpeed * Time.deltaTime;
        transform.Translate(movimiento);

        // Limitar la posición en X
        float xClamped = Mathf.Clamp(transform.position.x, limiteIzquierda, limiteDerecha);
        transform.position = new Vector3(xClamped, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        }
    }
}
