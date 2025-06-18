using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo; // Arrastra aquí la nave (Player)
    public float suavizado = 0.1f;
    public Vector3 offset = new Vector3(0, 0, -10);
    private Vector3 velocidad = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (objetivo != null)
        {
            Vector3 posicionDeseada = objetivo.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, posicionDeseada, ref velocidad, suavizado);
        }
    }
}
