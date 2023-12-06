using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public float amplitude = 1f;
    public float length = 2f;
    public float speed = 1f;
    public float xOffset = 0f;
    public float zOffset = 0f;
    //public float randomness = 0.1f; // Adjust the amount of randomness

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object.");
            Destroy(this);
        }
    }

    private void Update()
    {
        xOffset += Time.deltaTime * speed;
        zOffset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float _x, float _z)
    {
        //float randomOffset = Random.Range(-randomness, randomness);
        return amplitude * Mathf.Sin((_x / length) + xOffset + (_z / length) + zOffset);
    }
}
