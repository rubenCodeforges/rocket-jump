using System.Xml.Serialization;

namespace Containers
{
    public class RocketPart
    {
        [XmlAttribute("name")]
        public string name;
        [XmlAttribute("thrust")]
        public float thrust;
        [XmlAttribute("type")]
        public PartType type;
    }
}