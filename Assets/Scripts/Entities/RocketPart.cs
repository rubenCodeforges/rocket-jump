using Containers;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "rocket-part", menuName = "RocketJump/items/rocket part")]
    public class RocketPart: ScriptableObject
    {
        public new string name;
        public float thrust;
        public PartType type;
        public GameObject model;
    }
}