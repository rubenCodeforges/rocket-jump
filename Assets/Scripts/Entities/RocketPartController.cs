using System;
using UnityEngine;

namespace Entities
{
    public class RocketPartController: MonoBehaviour
    {
        public RocketPart part;

        private void Start()
        {
            print(part.name);
        }
    }
}