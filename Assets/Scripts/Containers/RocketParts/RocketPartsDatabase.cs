using System;
using System.Collections.Generic;
using UnityEngine;

namespace Containers.RocketParts
{
    public class RocketPartsDatabase : MonoBehaviour
    {
        public List<RocketPart> rocketParts = new List<RocketPart>();
        public static RocketPartsDatabase Instance { get; private set; }
        private const string _xmlPath = "configs/items";

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                rocketParts = RocketPartsContainer.load(_xmlPath).RocketParts;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}