using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMode : MonoBehaviour
{
    public enum PlayerState { Attacking, Idle, HighHp, MediumHp, LowHp }

    [SerializeField] private Light ld;
    [SerializeField] private float intensity = 5f;

    private float _alpha = 1f;
    private ParticleSystem _particleSystem;
    private ParticleSystem.MainModule _main;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        if (_particleSystem == null)
        {
            Debug.LogError("Brak komponentu ParticleSystem!");
            return;
        }

        _main = _particleSystem.main;

        SetParticles();
        SetParticleColor(PickColor(PlayerState.HighHp));
    }

    private void SetParticles()
    {
        _main.loop = true;
        _main.simulationSpace = ParticleSystemSimulationSpace.Local; 
        _main.startLifetime = 2f;
        _main.playOnAwake = true;

        _main.startSize = 1f;
        _main.startSpeed = 0.5f;
        _main.maxParticles = 200;

        var emission = _particleSystem.emission;
        emission.rateOverTime = 50f;

        var shape = _particleSystem.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Circle;
    }



    private Color PickColor(PlayerState ps)
    {
        return ps switch
        {
            PlayerState.HighHp => new Color(200f / 255f, 0f, 255f / 255f, _alpha),
            PlayerState.MediumHp => new Color(255f / 255f, 100f / 255f, 0f, _alpha),
            PlayerState.LowHp => new Color(200f / 255f, 0f, 0f, _alpha),
            _ => new Color(1f, 1f, 1f, _alpha)
        };
    }

    private void SetParticleColor(Color color)
    {
        var gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[2];
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];

        colorKeys[0] = new GradientColorKey(color, 0.0f);
        colorKeys[1] = new GradientColorKey(new Color(color.r / 10f, color.g / 10f, color.b / 10f), 1.0f);

        alphaKeys[0] = new GradientAlphaKey(color.a, 0.0f);
        alphaKeys[1] = new GradientAlphaKey(color.a, 1.0f);

        gradient.colorKeys = colorKeys;
        gradient.alphaKeys = alphaKeys;

        _main.startColor = new ParticleSystem.MinMaxGradient(gradient);
    }
}
