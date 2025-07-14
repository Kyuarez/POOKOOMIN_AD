using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EffectActivator : MonoBehaviour
{
    private ParticleSystem[] _particleSystems;

    private void Awake()
    {
        _particleSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
    }

    public void OnParticle()
    {
        foreach (var particle in _particleSystems)
        {
            particle.Play();
        }
    }
    public void OffParticle()
    {
        foreach (var particle in _particleSystems)
        {
            particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}
