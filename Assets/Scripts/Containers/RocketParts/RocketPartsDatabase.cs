using System;
using System.Collections.Generic;
using UnityEngine;

namespace Containers.RocketParts
{
    public class RocketPartsDatabase : MonoBehaviour
    {
        public List<RocketPart> rocketParts = new List<RocketPart>();
        private const string _xmlPath = "configs/items";

        void Awake()
        {
            rocketParts = RocketPartsContainer.load(_xmlPath).RocketParts;
        }
    }
}