using System;
using UnityEngine;

public class AttackVisual : MonoBehaviour
{
    public float delay;
    public float timeUntilTransparent;
    private float currentTime;
    public float transparencySpeed;
    private float currentState;
    public SpriteRenderer sprite;
    private void Start()
    {
        Destroy(gameObject, delay);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > timeUntilTransparent)
        {
            currentState += Time.deltaTime * transparencySpeed;
            sprite.color = Color.Lerp(Color.white, Color.clear, currentState);
        }
    }
}
