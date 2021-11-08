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
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
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
            int eddigiKorok = 0;
            int eddigiJatekos = 0;
            while (jatek.JatekVege == false)
            {
                Console.WriteLine("\n");
                Kor(jatek, eddigiKorok);
                if (eddigiJatekos != jatek.JatekosKore)
                if (eddigiJatekos != jatek.JatekosKore)
                {
                    eddigiKorok++;
                    eddigiJatekos = jatek.JatekosKore;
                }
            }
            jatek.JatekVegePontokkal();
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
            Console.WriteLine($"\n{jatek.Jatekosok[jatek.JatekosKore].Nev} tippeljen egy mássalhangzót");
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
        static void Kor(Jatek jatek, int eddigiKorok)
        {
            Console.Clear();
            jatek.Eredmeyek();
            Console.WriteLine(jatek.TitkosMondasString());
            Random rnd = new Random();
            Console.WriteLine();
            if (jatek.Jatekosok[jatek.JatekosokSzama].JatekosE == true)
            {
                Console.WriteLine(jatek.Mondas);
                Console.WriteLine("\nRá szeretnél kérdezni?(I/N)");
                char rakerdezE = ' ';
                try
                {
                    rakerdezE = char.Parse(Console.ReadLine().ToLower());
                }
                catch (System.FormatException e)
                {
                }
                if ( rakerdezE == 'n')
                {
                    int valasz = 2;
                    do
                    {
                        char tipp = MassalhangzoBeker(jatek);
                        valasz = jatek.Tipp(tipp, rnd);
                        if (valasz == 2) Console.WriteLine("Kérlek mássalhangzót adj meg!");
                        else if (valasz == 1) Console.WriteLine("Sajnos rossz választás!");
                        else if (valasz == 0) Console.WriteLine("Gratulálok!");
                        System.Threading.Thread.Sleep(1000);
                    } while (valasz != 0 && valasz != 1) ;
                }
                else if ( rakerdezE == 'i')
                {
                    string proba = Console.ReadLine();
                    //Console.WriteLine($"\n{jatek.Mondas}\n{proba}");
                    if (!jatek.Rakerdez(proba))
                    {
                       Console.WriteLine("Sajnos nem talált! :(");
                       System.Threading.Thread.Sleep(1500);
                    }
                    else
                    {
                        Console.WriteLine("Gratulálunk, kitaláltad!");
                        jatek.Kilepes(jatek.JatekosKore);
                    }
                }
            }
            if (jatek.Jatekosok[jatek.JatekosokSzama].JatekosE == true)
            {
                jatek.BotLepes(rnd);
            }
        }
    }
}
