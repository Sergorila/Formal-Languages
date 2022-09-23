using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace dfa
{
    public class DFA
    {
        private List<string> _alphabet;
        private List<State> _states;

        public DFA()
        {
            using (StreamReader streamReader = new StreamReader("TextFile1.txt"))
            {
                string[] input = streamReader.ReadLine().Split(' ');
                _alphabet = new List<string>(input);
                input = streamReader.ReadLine().Split(' ');
                _states = new List<State>();
                foreach (var st in input)
                {
                    State temp = new State();
                    if (st.Contains("*"))
                    {
                        temp.IsStartState = true;
                        var st1 = st.Split("*");
                        temp.Name = st1[0];
                        _states.Add(temp);
                        continue;
                    }
                    if (st.Contains("&"))
                    {
                        temp.IsAcceptState = true;
                        var st1 = st.Split("&");
                        temp.Name = st1[0];
                        _states.Add(temp);
                        continue;
                    }
                    temp.Name = st;
                    _states.Add(temp);
                }
                string s;
                while ((s = streamReader.ReadLine()) != null)
                {
                    var s1 = s.Split();
                    foreach (var st in _states)
                    {
                        if (st.Name == s1[0][0].ToString())
                        {
                            for (int i = 1; i <= _alphabet.Count; i++)
                            {
                                st.Transitions.Add(s1[i][0], _states[Int32.Parse(s1[i][2].ToString())]);
                            }
                        }
                    }
                }
            }
        }

        public void ShowDFA()
        {
            foreach (var symbol in _alphabet)
            {
                Console.Write("\t{0}", symbol);
            }
            Console.WriteLine();
            Console.WriteLine();
            foreach (var state in _states)
            {
                if (state.IsAcceptState)
                {
                    Console.Write("->");
                }
                if (state.IsStartState)
                {
                    Console.Write("*");
                }
                Console.Write(state.Name);
                foreach (var trans in state.Transitions)
                {
                    Console.Write("\t({0},{1})", trans.Key, trans.Value.Name);
                }
                Console.WriteLine();
                Console.WriteLine();
            }
            
        }

        public bool? Run(IEnumerable<char> s)
        {
            Console.WriteLine("Chain: {0}", s.ToString());
            State current = _states[0];
            foreach (var c in s) // цикл по всем символам 
            {
                Console.WriteLine("Symbol - {0}, current state - {1}, transition to state {2}", c, current.Name, current.Transitions[c].Name);
                current = current.Transitions[c]; // меняем состояние на то, в которое у нас переход
                if (current == null)              // если его нет, возвращаем признак ошибки
                    return null;
                // иначе переходим к следующему
            }
            if (current.IsAcceptState)
            {
                Console.WriteLine("True");
            }
            return current.IsAcceptState;         // результат true если в конце финальное состояние 
        }
    }
}
