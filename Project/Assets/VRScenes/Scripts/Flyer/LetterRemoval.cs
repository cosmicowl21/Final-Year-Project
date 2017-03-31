using System;
using System.Collections;
using UnityEngine;
using VRStandardAssets.Flyer;

namespace VRStanderdAssets.Flyer
{ 
        // This script looks after the explosion after a letter has been hit.
    public class LetterRemoval : MonoBehaviour
    {
            public event Action<LetterRemoval> onRemovalEnded;                    // This event is triggerd after the last particle system has finnished.


            private ParticleSystem[] m_ParticleSystems;                         // The particle systems for all the explosion and their children.
            private float m_Duration;                                           // The longest duration of all the particle systems.                
            internal Action<AsteroidExplosion> OnRemovalEnded;

            // Use this for initialization
            void Awake ()
        {
                // try and find all the particle systems 
                m_ParticleSystems = GetComponentsInChildren<ParticleSystem>(true);

                // by default the duration is zero.
                m_Duration = 0f;

                // go through all the particle systems and if their duration is longer use that instead
                for (int i = 0; i<m_ParticleSystems.Length; i++)
                {
                    if (m_ParticleSystems[i].duration > m_Duration)
                        m_Duration = m_ParticleSystems[i].duration;
                }
	    }
	
	    // Update is called once per frame
	    private void OnDestory ()
        {
                onRemovalEnded = null;
	    }
    
        public void Restart()
        {
           // go through all the particle system and clear their current particles then play them
           for (int i=0; i<m_ParticleSystems.Length;i++)
           {
                    m_ParticleSystems[i].Clear();
                    m_ParticleSystems[i].Play();
           }

                // Start the timeout
                StartCoroutine(Timeout());
        }

        private IEnumerator Timeout()
        {
                // Wait for the longest particle system to finish
                yield return new WaitForSeconds(m_Duration);

                // If anything is subscribed to OnRemovalEnded call it
                if (onRemovalEnded != null)
                    onRemovalEnded(this);
        }
    }
}