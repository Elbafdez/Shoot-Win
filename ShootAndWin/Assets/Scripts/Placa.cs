using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Placa : MonoBehaviour
{
    public int puntos;
    public float tiempoReaparicion;
    private SpriteRenderer sr;
    private Collider2D col;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Impactado()
    {
        GameManager.Instance.AddPoints(puntos);
        StartCoroutine(Reaparecer());
    }

    IEnumerator Reaparecer()
    {
        sr.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(tiempoReaparicion);

        sr.enabled = true;
        col.enabled = true;
    }
}
