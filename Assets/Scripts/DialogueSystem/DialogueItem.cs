using System.Collections.Generic;
using Ink.Runtime;

namespace DialogueSystem
{
    public class DialogueItem
    {
        public string character;
        public List<Choice> choices;
        public string line;
        public Dictionary<string, string> tags;
    }
}