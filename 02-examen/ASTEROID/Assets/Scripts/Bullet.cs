using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float tiempoVida = 1.5f;
    public float velocidadBala = 80f; // Muy rápida
    public Vector2 escalaBala = new Vector2(2.5f, 0.7f); // Ancho y alto ideales

    void Start()
    {
        Destroy(gameObject, tiempoVida);
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = transform.up * velocidadBala;
        }
        // Ajusta el tamaño de la bala (X = ancho, Y = alto)
        transform.localScale = new Vector3(escalaBala.x, escalaBala.y, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Si la bala choca con otra bala, ambas desaparecen
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        }
        Asteroid ast = collision.gameObject.GetComponent<Asteroid>();
        if (ast != null)
        {
            ast.FragmentarDesdeBala();
            Destroy(gameObject);
        }
    }
}
