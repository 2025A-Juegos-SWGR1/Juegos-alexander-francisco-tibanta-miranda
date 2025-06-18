using UnityEngine;
using System.Collections;

public class HackBeam : MonoBehaviour
{
    public float duracion = 1.5f;
    public LayerMask capaAsteroides;
    public float largo = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DestruirDespues());
        DispararRayo();
    }

    void DispararRayo()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, largo, capaAsteroides);
        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider.CompareTag("Asteroid"))
            {
                Destroy(hit.collider.gameObject);
                // Puedes sumar puntos aquí llamando a GameManager
            }
        }
    }

    IEnumerator DestruirDespues()
    {
        yield return new WaitForSeconds(duracion);
        Destroy(gameObject);
    }
}
