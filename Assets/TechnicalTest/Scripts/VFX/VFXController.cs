using System.Collections;
using UnityEngine;

namespace TechnicalTest.Effects
{
    public class VFXController : MonoBehaviour
    {
        private ParticleSystem[] particles;
        private float longestDuration = 0f;

        private void Start()
        {
            if (particles == null)
            {
                particles = GetComponentsInChildren<ParticleSystem>();
            }

        }
        public void ActiveParticle()
        {
            gameObject.SetActive(true);

            foreach (ParticleSystem particle in particles)
                particle.Play();

            StartCoroutine(DesactiveParticle(ParticleDuration()));
        }
        private float ParticleDuration()
        {
            foreach (ParticleSystem particle in particles)
            {
                float duration = particle.main.duration;

                if (duration > longestDuration)
                    longestDuration = duration;
            }

            return longestDuration;
        }
        private IEnumerator DesactiveParticle(float duration)
        {
            yield return new WaitForSeconds(duration);
            gameObject.SetActive(false);
        }
    }
}
