using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public int puntaje = 0;
    public int combo = 1;
    public int comboMax = 5;
    public int vidas = 3;
    public int grandesSeguidos = 0;
    public TMP_Text textoPuntaje;
    public TMP_Text textoCombo;
    public Image[] iconosVida;
    public AsteroidSpawner spawner;
    public bool bossActivo = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    public void SumarPuntos(int cantidad)
    {
        puntaje += cantidad * combo;
        textoPuntaje.text = "Puntaje: " + puntaje;
        if (puntaje >= 1000 && !bossActivo)
        {
            bossActivo = true;
            if (spawner != null)
            {
                spawner.DetenerSpawneo();
                spawner.SpawnearBoss();
            }
        }
    }

    public void ActualizarCombo(bool acierto)
    {
        if (acierto)
        {
            combo = Mathf.Min(combo + 1, comboMax);
        }
        else
        {
            combo = 1;
        }
        textoCombo.text = "Combo: x" + combo;
    }

    public void ActualizarVidas(int cantidad)
    {
        vidas += cantidad;
        for (int i = 0; i < iconosVida.Length; i++)
        {
            iconosVida[i].enabled = i < vidas;
        }
    }

    public void BonusGrandes()
    {
        puntaje += 100;
        textoPuntaje.text = "Puntaje: " + puntaje + " (+Bonus!)";
    }
}
