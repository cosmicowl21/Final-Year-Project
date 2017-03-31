using System;
using UnityEngine;
using VRStandardAssets.Common;
using VRStandardAssets.Flyer;
using Random = UnityEngine.Random;

namespace VRStanderdAssets.Flyer
{
    // This class controls each word in the Flyer scene
    public class Letter : MonoBehaviour
    {
            // Event triggers for the Letters
            public event Action<Letter> onLetterRemovalDistance;                // This event triggers when the word if far enoug behind the camera
            public event Action<Letter> onLetterHit;                           // This event triggers when the ship hits the Letter.   

            // Setting up all the values for the letters
            [SerializeField] private float m_LettersMinSize = 10f;              // The Miniuim ammount the leters can be scaled up. 
            [SerializeField] private float m_LettersMaxSize = 20f;             // The Maxiuim ammount the leters can be scaled up. 
            [SerializeField] private float m_MinSpeed = 0f;                   // The Miniuim speed that the letter can move towards the camera 
            [SerializeField] private float m_MaxSpeed = 10;                  // The Maxiuim speed that the letter can moves towards the camera
            [SerializeField] private float m_MinRotationSpeed = 60f;        // The miniuim speed that the letter will rotate.
            [SerializeField] private float m_MaxRotationSpeed = 140f;      //  The Maxuimin speed that the letter will rotate.

            // setting up the references to diffrent objects 
            private Rigidbody m_RigidBody;                                 // reference to the word rigid body, used to move and rotate it.
            private FlyerHealthController m_FlyerHealthController;         // Reference to the flyer's health script, used to damage it.
            private GameObject m_Flyer;                                    // Referance to the flyer its self.
            private Transform m_Camera;                                    // Referance to the camera
            private float m_Speed;                                         // How fast the letters will move towards the camera.
            private Vector3 m_RotationAxis;                                // The Axis around the letter.
            private float m_RotationSpeed;                                 // how fast the letters will rotate.

            private const float k_RemovalDistance = 50f;                    // How far behind the camera the letter can go before being destroyed.

            void Awake ()
            {
                m_RigidBody = GetComponent<Rigidbody>();

                m_FlyerHealthController = FindObjectOfType<FlyerHealthController>();
                m_Flyer = m_FlyerHealthController.gameObject;

                m_Camera = Camera.main.transform;
            }

          private void Start ()
          {
                // set a random scale for the letters
                float scaleMultipler = Random.Range(m_LettersMinSize, m_LettersMaxSize);
                transform.localScale = new Vector3(scaleMultipler, scaleMultipler, scaleMultipler);

                // set a random rotation for the letters of the word
                transform.rotation = Random.rotation;

                // set a random spin for the letters
                m_Speed = Random.Range(m_MinSpeed, m_MaxSpeed);

                // set up a random spin for the leters
               // m_RotationAxis = Random.insideUnitSphere;
               // m_RotationSpeed = Random.Range(m_MinRotationSpeed, m_MaxRotationSpeed);
          }

        private void Update()
        {
                // move and rotate the letters to the given parameters
                m_RigidBody.MoveRotation(m_RigidBody.rotation * Quaternion.AngleAxis(m_RotationSpeed * Time.deltaTime, m_RotationAxis));
                m_RigidBody.MovePosition(m_RigidBody.position - Vector3.forward * m_Speed * Time.deltaTime);

                // if the letter is far enough behind the camera and something has subscribed to onwordremoval call it 
                if(transform.position.z < m_Camera.position.z - k_RemovalDistance)
                {
                    if (onLetterRemovalDistance != null)
                        onLetterRemovalDistance(this);
                }
        }

        private void OnTriggerEnter(Collider other)
        {
                // only continue if the letter has hit the player (flyer)
                if (other.gameObject != m_Flyer)
                {
                    return;
                }
        }

        private void OnRemoval()
        {
                // ensure the events are completly removed when the letter is destroyed
                onLetterRemovalDistance = null;
                onLetterHit = null;
        }
  
        public void hit()
        {
                if (onLetterHit != null)
                    onLetterHit(this);
        }
    }      
}