using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject prefabAsteroideGrande;
    public Transform player;
    public GameObject bossPrefab;
    public Vector2[] spawnPoints = new Vector2[] {
        new Vector2(403, -177), // Inferior derecha
        new Vector2(-390, -176), // Inferior izquierda
        new Vector2(-400, 176), // Superior izquierda
        new Vector2(415, 182) // Superior derecha
    };
    private int nextSpawnIndex = 0;
    public float tiempoEntreAsteroides = 10f; // Ahora 10 segundos
    private float tiempoSiguienteAsteroide = 0f;
    private bool spawnear = true;
    public Vector2 centro = Vector2.zero; // Centro del mapa

    // Update is called once per frame
    void Update()
    {
        if (!spawnear) return;
        if (Time.time >= tiempoSiguienteAsteroide)
        {
            SpawnAsteroide();
            tiempoSiguienteAsteroide = Time.time + tiempoEntreAsteroides;
        }
    }

    void SpawnAsteroide()
    {
        // Solo spawnea desde fuera de los límites
        int intentos = 0;
        Vector2 spawnPos = spawnPoints[nextSpawnIndex];
        while (player != null && Vector2.Distance(player.position, spawnPos) < 10f && intentos < spawnPoints.Length)
        {
            nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
            spawnPos = spawnPoints[nextSpawnIndex];
            intentos++;
        }
        nextSpawnIndex = (nextSpawnIndex + 1) % spawnPoints.Length;
        GameObject asteroide = Instantiate(prefabAsteroideGrande, spawnPos, Quaternion.identity);
        // Siempre hacia el centro
        Vector2 dir = (centro - spawnPos).normalized;
        asteroide.GetComponent<Rigidbody2D>().linearVelocity = dir * 3f;
    }

    public void DetenerSpawneo()
    {
        spawnear = false;
    }

    public void SpawnearBoss()
    {
        Instantiate(bossPrefab, Vector3.zero, Quaternion.identity);
    }
}
