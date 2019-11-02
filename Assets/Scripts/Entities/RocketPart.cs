using Containers;
using UnityEngine;
using UnityEngine.UI;

namespace Entities
{
    [CreateAssetMenu(fileName = "rocket-part", menuName = "RocketJump/items/rocket part")]
    public class RocketPart: ScriptableObject
    {
        public new string name;
        public float thrust;
        public PartType type;
        public float fuel;
        public GameObject model;
        public float weight = 0.01f;
        public Sprite icon;
    }
}