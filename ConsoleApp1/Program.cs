using DI_Container;
using System;

namespace ConsoleApp1
{
    class Abstraction
    {
        Interface1 _interface;
        Interface1 _interface1;
        public Abstraction(Interface1 interface1, Interface1 interface11)
        {
            _interface = interface1;
            _interface1 = interface11;
        }
        public void ShowMSG(string msg)
        {
            _interface.Show(msg);
            _interface1.Show(msg);
        }
    }
    class Resolving : Interface1
    {
        public Resolving(qwert qwert)
        {
            Console.WriteLine("qwert");
        }

        public void Show(string msg)
        {
            Console.WriteLine(msg);
        }
    }

    class qwert
    {
        public qwert() { Console.WriteLine("!!!!!!!!!!!!!!!!!!!"); }
    };
    class Program
    {
        static void Main(string[] args)
        {
            Kernel kernel = new Kernel(new DependencyTable());

            kernel.AddDependency<qwert>();
            kernel.AddDependency<Interface1, Resolving>();
            kernel.AddDependency(typeof(Abstraction));
            Abstraction ab = kernel.Get<Abstraction>();

            ab.ShowMSG("^^^^^^^^^^^");

            Abstraction bv = (Abstraction)kernel.Get(typeof(Abstraction));
            bv.ShowMSG("-------------");
            Console.ReadLine();
        }
    }
}
