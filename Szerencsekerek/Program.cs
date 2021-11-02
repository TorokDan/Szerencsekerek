using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szerencsekerek
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            Menu();
            Jatek jatek = new Jatek(rnd);
            while (jatek.JatekosokSzama > 3 || jatek.JatekosokSzama < 0)
            {
                Console.WriteLine("Kérlek add meg, hogy hányan fogtok játszani (maximum 3): ");
                try { jatek.JatekosokSzama = int.Parse(Console.ReadLine()); }
                catch (System.FormatException e) { Console.WriteLine("Kérlek számot adj meg"); } //Ha a megadott érték nem szám, akkor azt itt kezelem.
                if (jatek.JatekosokSzama > 3 || jatek.JatekosokSzama < 0) Console.WriteLine("Kérlek 0 és 3 közötti számot adj meg");
            }
            while (jatek.JatekVege == false)
            {
                Console.WriteLine("\n");
                Kor(jatek);
            }
            ;
        }

        static void Menu()
        {
            int valasztott = 0;
            while (valasztott != 1)
            {
                Console.Clear();
                Console.WriteLine("Kérlek válassz az alábbi menüpontok közül: ");
                Console.WriteLine("1. Játék kezdése");
                Console.WriteLine("2. Kilépés");
                try
                {
                    valasztott = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException e)
                {
                }//Ha a megadott érték nem szám, akkor azt itt kezelem
                if (valasztott == 2) Kilepes();
                if (valasztott > 2 || valasztott < 1) HibasValasztas();
            }
        }
        static void Kilepes()
        {
            Console.Clear();
            Console.WriteLine("Köszi, hogy játszottál");
            System.Threading.Thread.Sleep(1500);
            Environment.Exit(0);
        }
        static void HibasValasztas()
        {
            Console.Clear();
            Console.WriteLine("Kérlek a megadott opciók közül válassz!!");
            System.Threading.Thread.Sleep(1500);
        }
        static char MassalhangzoBeker(Jatek jatek) 
        {
            Console.WriteLine("Kérlek tippelj egy mássalhangzót");
            char tipp = ' ';
            try { tipp = char.ToLower(char.Parse(Console.ReadLine())); }
            catch (System.FormatException e) { }
            if (!jatek.MassalhangzoE(tipp))
            {
                Console.WriteLine("Ez nem egy mássalhangzó.");
                System.Threading.Thread.Sleep(1500);
                MassalhangzoBeker(jatek);
            }
            return tipp;
        }
        static void Kor(Jatek jatek)
        {
            jatek.Eredmeyek();
            int valasz = 3;
            Random rnd = new Random();
            while (valasz > 2)
            {
                char tipp = MassalhangzoBeker(jatek);
                valasz = jatek.Tipp(tipp, rnd);
            }
            Console.WriteLine(jatek.Mondas);
            Console.WriteLine(jatek.TitkosMondasString());
        }
    }
}
