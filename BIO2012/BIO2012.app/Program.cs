using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BIO2012.app
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(model("GHIJKL", "AE", 6) == "FA");
            Debug.Assert(model("GHIJKL", "AE", 100) == "VP");
            Console.WriteLine("Should be fine.");
            Console.ReadKey();
        }
        /* Some Info
         * train entering from curved must exit from straight
         * lazy point, train from curved sets exit point of straight to that curved trail
         * entering from curved does not affect. each time a point passed from straight to curve, the point flips
         */
        //what is required: three lines of input
        //1. six different uppercase letters, indicating these are flip flops
        //2. two letters, x then y, indicating starting point
        //3. integer n, (1 <= n < 10,000), number of points passed in simulation
        //output: two letters, last point passed and the next point the train will pass
        //letters not separated by spaces
        static string model(string letters, string startposition, int n)
        {
            char[] sp = startposition.ToCharArray();
            char[] fp = letters.ToCharArray();
            (char pre, char current) position = (sp[0], sp[1]);
            Dictionary<char, (char pre, char left, char right, bool flipflop, char next)> stations = new Dictionary<char, (char pre, char left, char right, bool flipflop, char next)>()
            {
                {'A',('D','E','F',false,'E')},
                {'B',('C','G','H',false,'G')},
                {'C',('B','I','J',false,'I')},
                {'D',('A','K','L',false,'K')},
                {'E',('A','M','N',false,'M')},
                {'F',('A','N','O',false,'N')},
                {'G',('B','O','P',false,'O')},
                {'H',('B','P','Q',false,'P')},
                {'I',('C','Q','R',false,'Q')},
                {'J',('C','R','S',false,'R')},
                {'K',('D','S','T',false,'S')},
                {'L',('D','T','M',false,'T')},
                {'M',('U','L','E',false,'L')},
                {'N',('U','E','F',false,'E')},
                {'O',('V','F','G',false,'F')},
                {'P',('V','G','H',false,'G')},
                {'Q',('W','H','I',false,'H')},
                {'R',('W','I','J',false,'I')},
                {'S',('X','J','K',false,'J')},
                {'T',('X','K','L',false,'K')},
                {'U',('V','M','N',false,'M')},
                {'V',('U','O','P',false,'O')},
                {'W',('X','Q','R',false,'Q')},
                {'X',('W','S','T',false,'S')}
            };
            Dictionary<char, (char pre, char left, char right, bool flipflop, char next)> s = new Dictionary<char, (char pre, char left, char right, bool flipflop, char next)>();
            char next;
            foreach (KeyValuePair<char, (char pre, char left, char right, bool flipflop, char next)> station in stations)
            {
                if (fp.Contains(station.Key))
                {
                    s.Add(station.Key, (station.Value.pre, station.Value.left, station.Value.right, true, station.Value.next));
                }
                else
                    s.Add(station.Key, (station.Value.pre, station.Value.left, station.Value.right, false, station.Value.next));
            }
            for (int i = 0; i < n; i++)
            {
                if (!(position.pre == s[position.current].pre))
                {
                    if (!s[position.current].flipflop)
                    {
                        s[position.current] = (s[position.current].pre, s[position.current].left, s[position.current].right, false, position.pre);
                        position = (position.current, s[position.current].pre);
                    }
                    else
                    {
                        position = (position.current, s[position.current].pre);
                    }
                }
                else 
                {
                    if (!s[position.current].flipflop)
                    {
                        position = (position.current, s[position.current].next);
                    }
                    else
                    {
                        next = s[position.current].next;
                        s[position.current] = (s[position.current].pre, s[position.current].left, s[position.current].right, false, (s[position.current].next == s[position.current].left) ? s[position.current].right : s[position.current].left);
                        position = (position.current, next);
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(position.pre);
            sb.Append(position.current);
            return sb.ToString();

            
        }
    }
}

