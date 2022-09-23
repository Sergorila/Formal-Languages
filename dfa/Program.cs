using System;

namespace dfa
{
    class Program
    {
        static void Main(string[] args)
        {
            DFA d = new DFA();
            d.ShowDFA();
            d.Run("aaabc");
        }
    }
}
