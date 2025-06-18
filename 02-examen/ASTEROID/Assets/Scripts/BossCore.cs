using UnityEngine;
using System.Collections;

public class BossCore : MonoBehaviour
{
    public GameObject prefabBala;
    public float tiempoEntreDisparos = 2f;
    public int vidas = 10;
    public float radioDisparo = 1f;
    private bool activo = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        activo = true;
        StartCoroutine(PatronDisparo());
    }

    IEnumerator PatronDisparo()
    {
        while (activo)
        {
            for (int i = -1; i <= 1; i++)
            {
                float angulo = i * 15f;
                Quaternion rot = Quaternion.Euler(0, 0, angulo);
                Instantiate(prefabBala, transform.position + rot * Vector3.up * radioDisparo, rot);
            }
            yield return new WaitForSeconds(tiempoEntreDisparos);
        }
    }

    public void RecibirDaño(int cantidad)
    {
        vidas -= cantidad;
        if (vidas <= 0)
        {
            activo = false;
            Destroy(gameObject);
        }
    }
}
