using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public enum AsteroidSize { Large, Medium, Small }
    public AsteroidSize size;
    public GameObject prefabMediano;
    public GameObject prefabPequeno;
    public float fuerzaFragmentacion = 3f;
    public int puntos = 10;
    private Rigidbody2D rb;

    public Vector3 escalaGrande = Vector3.one * 2.5f;
    public Vector3 escalaMediano = Vector3.one * 1.5f;
    public Vector3 escalaPequeno = Vector3.one * 0.8f;

    private bool yaFragmentado = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AjustarEscalaPorTamanio();
        AjustarVelocidadPorTamanio();
    }

    void AjustarEscalaPorTamanio()
    {
        switch (size)
        {
            case AsteroidSize.Large:
                transform.localScale = escalaGrande;
                break;
            case AsteroidSize.Medium:
                transform.localScale = escalaMediano;
                break;
            case AsteroidSize.Small:
                transform.localScale = escalaPequeno;
                break;
        }
    }

    void AjustarVelocidadPorTamanio()
    {
        float velocidad = 3f;
        switch (size)
        {
            case AsteroidSize.Large:
                velocidad = 3f;
                break;
            case AsteroidSize.Medium:
                velocidad = 5f;
                break;
            case AsteroidSize.Small:
                velocidad = 8f;
                break;
        }
        if (rb.linearVelocity == Vector2.zero) // Si no tiene dirección, apunta al centro
        {
            Vector2 dir = (Vector2.zero - (Vector2)transform.position).normalized;
            rb.linearVelocity = dir * velocidad;
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity.normalized * velocidad;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Fragmentar();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Asteroid"))
        {
            // Rebote entre asteroides: la física lo maneja
        }
        else if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>()?.RecibirDaño();
        }
    }

    public void FragmentarDesdeBala()
    {
        if (!yaFragmentado)
        {
            yaFragmentado = true;
            Debug.Log($"Fragmentando asteroide: {gameObject.name} de tipo {size}");
            Fragmentar();
        }
        else
        {
            Debug.Log($"Intento de fragmentar asteroide ya fragmentado: {gameObject.name}");
        }
    }

    void Fragmentar()
    {
        if (size == AsteroidSize.Large)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject frag = Instantiate(prefabMediano, transform.position, Quaternion.identity);
                Asteroid ast = frag.GetComponent<Asteroid>();
                if (ast != null) ast.size = AsteroidSize.Medium;
                frag.transform.localScale = escalaMediano;
                Rigidbody2D fragRb = frag.GetComponent<Rigidbody2D>();
                fragRb.linearVelocity = rb.linearVelocity + Random.insideUnitCircle * fuerzaFragmentacion;
            }
        }
        else if (size == AsteroidSize.Medium)
        {
            GameObject frag = Instantiate(prefabPequeno, transform.position, Quaternion.identity);
            Asteroid ast = frag.GetComponent<Asteroid>();
            if (ast != null) ast.size = AsteroidSize.Small;
            frag.transform.localScale = escalaPequeno;
            Rigidbody2D fragRb = frag.GetComponent<Rigidbody2D>();
            fragRb.linearVelocity = rb.linearVelocity + Random.insideUnitCircle * fuerzaFragmentacion;
        }
        Debug.Log($"Asteroide destruido: {gameObject.name} de tipo {size}");
        Destroy(gameObject);
    }
}
