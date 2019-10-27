using System;
using Enums;
using Events;
using Managers;
using UnityEngine;

namespace Entities
{
    public class RocketPartController : MonoBehaviour
    {
        public RocketPart part;
        private InputEventSubject eventSubject;
        private ParticleSystem particleSystem;
        private bool isThrusting = false;

        private void Start()
        {
            particleSystem = transform.GetComponentInChildren<ParticleSystem>();
            eventSubject = GameManager.Instance.inputEventSubject;
            eventSubject = GameManager.Instance.inputEventSubject;
            eventSubject.UserInput += OnUserInput;
            eventSubject.UserInputStop += OnUserInputStop;
            StartExhaust();
        }

        private void Update()
        {
            if (!Input.anyKey)
            {
                StopExhaust();
            }
        }

        private void OnUserInput(object source, UserInputEventArgs args)
        {
            StartExhaust();
        }

        private void OnUserInputStop(object source, EventArgs args)
        {
            StopExhaust();
        }

        private void StopExhaust()
        {
            if (particleSystem != null)
            {
                particleSystem.Stop();
            }
        }

        private void StartExhaust()
        {
            if (particleSystem != null)
            {
                particleSystem.Play();                
            }
        }
    }
}