using System;
using FEViewUtil;

namespace FEViewConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Model m = new Model();
            m.read(args[0]);
        }
    }
}
