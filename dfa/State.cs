using System;
using System.Collections.Generic;
using System.Text;

namespace dfa
{
    public class State
    {
        public string Name { get; set; }
        public Dictionary<char, State> Transitions { get; set; }
        public bool IsAcceptState { get; set; }
        public bool IsStartState { get; set; }

        public State()
        {
            Name = "";
            Transitions = new Dictionary<char, State>();
            IsAcceptState = false;
        }
    }
}
