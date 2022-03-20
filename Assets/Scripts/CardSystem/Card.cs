using UnityEngine;

namespace CardSystem
{
    public class Card: MonoBehaviour
    {
        public string description { get; set; }

        public Card()
        {
            description = "No description yet"; //Default value
        }
    }
}