using UnityEngine;

public class SprayController : MonoBehaviour {
    public ParticleSystem sprayParticleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    void Start() {
        emissionModule = sprayParticleSystem.emission;
        emissionModule.rateOverTime = 0f;
    }

    void Update() {
        if (Input.GetMouseButton(0))
        {
            emissionModule.rateOverTime = 100f;
        }
        else {
            emissionModule.rateOverTime = 0f;
        }
    }
}
