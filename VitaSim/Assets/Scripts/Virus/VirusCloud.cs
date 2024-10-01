using UnityEngine;
using UnityEngine.UI;

public class VirusCloud : MonoBehaviour {
    private ParticleSystem virusParticleSystem;
    private ParticleSystem.EmissionModule emissionModule;

    public HealthBar healthBar; 
    public int maxHP = 100;
    private float currentHP;

    void Start() {
        currentHP = maxHP;
        virusParticleSystem = GetComponent<ParticleSystem>();
        emissionModule = virusParticleSystem.emission;
    }

    public void TakeDamage(int damage) {
        currentHP -= damage;
        healthBar.UpdateHealth((float) currentHP / (float) maxHP);

        float emissionRate = Mathf.Lerp(0, 100, currentHP / maxHP);
        //.rateOverTime = emissionRate;

        if (currentHP <= 0) {
            Destroy(gameObject);
        }
    }
}
