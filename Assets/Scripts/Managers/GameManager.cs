using Containers.RocketParts;
using Events;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public InputEventSubject inputEventSubject;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                inputEventSubject = new InputEventSubject();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}