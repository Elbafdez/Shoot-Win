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

    //----------------------------- SONIDO -----------------------------------------
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip[] clipsDeImpacto; // Tus clips de sonido de disparo

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void Impactado()
    {
        GameManager.Instance.AddPoints(puntos);
        // Reproducir sonido
        if (clipsDeImpacto.Length > 0 && audioSource != null)
        {
            int index = Random.Range(0, clipsDeImpacto.Length);
            audioSource.PlayOneShot(clipsDeImpacto[index]);
        }

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

    public void Reactivar()
    {
        sr.enabled = true;
        col.enabled = true;
    }
}
