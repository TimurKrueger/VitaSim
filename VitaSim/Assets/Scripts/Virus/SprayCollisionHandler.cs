using System.Collections.Generic;
using UnityEngine;

public class SprayCollisionHandler : MonoBehaviour {
    public float damagePerParticle = 0.01f;

    void OnParticleCollision(GameObject other) {
        VirusCloud virusCloud = other.GetComponent<VirusCloud>();

        if (virusCloud != null) {
            int numParticles = GetComponent<ParticleSystem>().GetCollisionEvents(other, new List<ParticleCollisionEvent>());
            int totalDamage = (int) (damagePerParticle * numParticles);

            virusCloud.TakeDamage(totalDamage);
        }
    }
}
