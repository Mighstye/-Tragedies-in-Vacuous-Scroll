using System;
using System.Collections.Generic;
using Logic_System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace CardSystem.DataContainers
{
    [Serializable]
    public class CardDropDeckJson
    {
        [SerializeField] [JsonConverter(typeof(StringEnumConverter))]
        public DropDeckType type;
        [SerializeField] public List<string> keys;
    }
    
}