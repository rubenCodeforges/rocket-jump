using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Containers.RocketParts
{
    [XmlRoot("RocketPartsContainer")]
    public class RocketPartsContainer
    {
        [XmlArray("Parts")]
        [XmlArrayItem("Part")]
        public List<RocketPart> RocketParts = new List<RocketPart>();

        public static RocketPartsContainer load(string path)
        {
            var xml = Resources.Load<TextAsset>(path);
            var serializer = new XmlSerializer(typeof(RocketPartsContainer));
            var reader = new StringReader(xml.text);
            var items = serializer.Deserialize(reader) as RocketPartsContainer;
            reader.Close();
            
            return items;
        }
    }
}