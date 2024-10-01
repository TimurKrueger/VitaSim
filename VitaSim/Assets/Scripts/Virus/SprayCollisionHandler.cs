using System.Collections.Generic;
using UnityEngine;

public class SprayCollisionHandler : MonoBehaviour {
    public float damagePerParticle = 1;

    void OnParticleCollision(GameObject other) {
        VirusCloud virusCloud = other.GetComponent<VirusCloud>();

        if (virusCloud != null) {
            int numParticles = GetComponent<ParticleSystem>().GetCollisionEvents(other, new List<ParticleCollisionEvent>());
            int totalDamage = (int) (damagePerParticle * numParticles);

            virusCloud.TakeDamage(totalDamage);
        }
    }
}
