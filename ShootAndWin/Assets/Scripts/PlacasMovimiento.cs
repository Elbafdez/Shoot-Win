using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacasFacilesMovimiento : MonoBehaviour
{
    public float speed;
    public float limiteIzquierda;
    public float limiteDerecha;

    private int direccion = 1; // 1 = derecha, -1 = izquierda

    void Update()
    {
        transform.Translate(Vector2.right * speed * direccion * Time.deltaTime);

        if (transform.position.x > limiteDerecha)
            direccion = -1;
        else if (transform.position.x < limiteIzquierda)
            direccion = 1;
    }
}
