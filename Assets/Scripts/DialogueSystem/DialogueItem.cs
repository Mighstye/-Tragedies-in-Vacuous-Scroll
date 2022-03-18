using System.Collections.Generic;
using Ink.Runtime;

namespace DialogueSystem
{
    public class DialogueItem
    {
        public string character;
        public string line;
        public List<Choice> choices;
        public string emotion;
    }
}