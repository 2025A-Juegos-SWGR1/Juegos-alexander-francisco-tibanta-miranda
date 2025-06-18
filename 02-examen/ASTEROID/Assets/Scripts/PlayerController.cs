using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Componentes
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    // Movimiento
    public float velocidadAngular = 700f; // Giro mucho más rápido
    public float fuerzaAceleracion = 40f; // Aceleración mucho más rápida
    public float maxVelocidad = 40f; // Velocidad máxima mucho mayor
    private bool puedeMover = true;

    // Frenado
    public float factorFrenado = 0.5f;

    // Teletransporte
    public float cooldownBlink = 3f;
    private float tiempoUltimoBlink = -10f;
    public float radioTeletransporte = 8f;

    // Disparo principal
    public GameObject prefabBala;
    public Transform puntoDisparo;
    public float velocidadBala = 15f;
    public float escalaBala = 2f; // Ajusta el tamaño de la bala

    // Disparo especial (Hack Beam)
    public GameObject prefabHackBeam;
    public float energiaMaxima = 100f;
    private float energiaActual = 0f;
    public float energiaPorAsteroide = 25f;
    public float duracionHackBeam = 1.5f;
    private bool hackBeamActivo = false;

    // Vidas y feedback visual
    public int vidas = 3;
    public float tiempoParpadeo = 1f;
    private bool invulnerable = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!puedeMover) return;
        ManejarRotacion();
        ManejarAceleracion();
        ManejarFrenado();
        ManejarDisparo();
        ManejarHackBeam();
        ManejarTeletransporte();
    }

    void ManejarRotacion()
    {
        float rot = -Input.GetAxisRaw("Horizontal"); // A/D o ←/→
        rb.MoveRotation(rb.rotation + rot * velocidadAngular * Time.deltaTime);
    }

    void ManejarAceleracion()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(transform.up * fuerzaAceleracion);
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxVelocidad);
        }
    }

    void ManejarFrenado()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.linearVelocity *= factorFrenado;
        }
    }

    void ManejarDisparo()
    {
        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetMouseButtonDown(0)))
        {
            if (prefabBala == null || puntoDisparo == null)
            {
                Debug.LogWarning("Prefab de bala o punto de disparo no asignado en el Inspector.");
                return;
            }
            Disparar();
        }
    }

    void Disparar()
    {
        GameObject bala = Instantiate(prefabBala, puntoDisparo.position, transform.rotation);
        // Tamaño y velocidad de la bala
        float escala = escalaBala > 0 ? escalaBala : 2f;
        bala.transform.localScale = Vector3.one * escala;
        Bullet bulletScript = bala.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.velocidadBala = velocidadBala > 0 ? velocidadBala : 40f;
        }
        SpriteRenderer sr = bala.GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite == null)
        {
            Sprite bulletSprite = Resources.Load<Sprite>("Sprites/bullet");
            if (bulletSprite != null) sr.sprite = bulletSprite;
        }
    }

    void ManejarHackBeam()
    {
        if (Input.GetKeyDown(KeyCode.E) && energiaActual >= energiaMaxima && !hackBeamActivo)
        {
            StartCoroutine(ActivarHackBeam());
        }
    }

    IEnumerator ActivarHackBeam()
    {
        hackBeamActivo = true;
        GameObject beam = Instantiate(prefabHackBeam, transform.position, transform.rotation);
        beam.transform.parent = transform;
        energiaActual = 0f;
        yield return new WaitForSeconds(duracionHackBeam);
        Destroy(beam);
        hackBeamActivo = false;
    }

    void ManejarTeletransporte()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - tiempoUltimoBlink >= cooldownBlink)
        {
            StartCoroutine(Teletransporte());
        }
    }

    IEnumerator Teletransporte()
    {
        puedeMover = false;
        spriteRenderer.enabled = false;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        Vector2 nuevaPos = new Vector2(Random.Range(-radioTeletransporte, radioTeletransporte), Random.Range(-radioTeletransporte, radioTeletransporte));
        transform.position = nuevaPos;
        spriteRenderer.enabled = true;
        puedeMover = true;
        tiempoUltimoBlink = Time.time;
    }

    public void GanarEnergia(float cantidad)
    {
        energiaActual = Mathf.Min(energiaMaxima, energiaActual + cantidad);
    }

    public void RecibirDaño()
    {
        if (invulnerable) return;
        vidas--;
        StartCoroutine(FeedbackParpadeo());
        // Aquí puedes llamar a GameManager para actualizar UI y vidas
    }

    IEnumerator FeedbackParpadeo()
    {
        invulnerable = true;
        float t = 0f;
        while (t < tiempoParpadeo)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }
        spriteRenderer.enabled = true;
        invulnerable = false;
    }
}
