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
        }

        private void OnUserInput(object source, UserInputEventArgs args)
        {
        }
    }
}