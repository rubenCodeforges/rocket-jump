using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Containers.RocketParts
{
    public class RocketPartsDatabase : MonoBehaviour
    {
        public List<RocketPart> rocketParts = new List<RocketPart>();
        public List<RocketPartController> inventory = new List<RocketPartController>();
        public static RocketPartsDatabase Instance { get; private set; }
        
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}