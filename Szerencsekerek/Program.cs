using System;
using System.Collections.Generic;
using System.IO;
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
            Jatek jatek = Menu();
            string bekertSzam = " ";
            while (bekertSzam != 1.ToString() && bekertSzam != 2.ToString() && bekertSzam != 3.ToString())
            {
                Console.WriteLine("Kérlek add meg, hogy hányan fogtok játszani (maximum 3): ");
                if (bekertSzam == 1.ToString() && bekertSzam == 2.ToString() && bekertSzam == 3.ToString())
                    jatek.JatekosokSzama = int.Parse(Console.ReadLine()); 
                else Console.WriteLine("Kérlek 1 és 3 közötti számot adj meg");
            }
            while (jatek.JatekVege == false && jatek.KorSzama < 3)
            {
                Console.WriteLine();
                Kor(jatek);
            }
            jatek.JatekVegePontokkal();
            ;
        }
        static Jatek Menu()
        {
            string valasztott = " ";
            while (valasztott != "1" && valasztott != "2")
            {
                Console.Clear();
                Console.WriteLine("Kérlek válassz az alábbi menüpontok közül: ");
                Console.WriteLine("1. Játék kezdése");
                Console.WriteLine("2. Mentes betöltése");
                Console.WriteLine("3. Kilépés");
                valasztott = Console.ReadLine();
                if (valasztott == "3") 
                    Kilepes();
                if (valasztott != "1" && valasztott != "2" && valasztott != "3") 
                    HibasValasztas();
            }
            Jatek jatek =  new Jatek();
            if (valasztott == "2") 
            {
                string[] fileName = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt", SearchOption.AllDirectories);
                bool valtozas = false;
                while (!valtozas)
                {
                    Console.WriteLine("Kérlek válassz az alábbi mentések közül!");
                    for (int i = 0; i < fileName.Length; i++)
                    {
                        int start = fileName[i].LastIndexOf('\\') + 1;
                        int end = fileName[i].LastIndexOf('.');
                        Console.WriteLine($"{i + 1} " + fileName[i].Substring(0, end).Substring(start));
                    }
                    string valasztas = Console.ReadLine();
                    for (int i = 0; i < fileName.Length; i++)
                    {
                        if (valasztas == i.ToString() && i < fileName.Length && 0 < i)
                        {
                            jatek = new Jatek(fileName[i-1]);
                            valtozas = true;
                        }
                    }
                    if (!valtozas) Console.WriteLine("\nKérlek a mefadott opciók közül válassz!\n");
                }
            }
            return jatek;
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
        static void Kor(Jatek jatek)
        {
            Random rnd = new Random();
            Console.WriteLine();
            if (jatek.Jatekosok[jatek.JatekosKore].JatekosE == true)
            {
                int dontes = 0;
                while (dontes != 1 && dontes != 2)
                {
                    Console.Clear();
                    Console.WriteLine($"{jatek.KorSzama + 1}. kör: {jatek.Jatekosok[jatek.JatekosKore].Nev} jön");
                    Console.WriteLine(jatek.ToString());
                    Console.WriteLine("\n" + jatek.TitkosMondasString());
                    Console.WriteLine("\nMit szeretnél csinálni? \nTippel(1)\nRákérdez(2)\nMentés(3)\nKilépés(4)");
                    try
                    {
                        dontes = int.Parse(Console.ReadLine());
                    }
                    catch (System.FormatException e)
                    {
                        Console.WriteLine("Kérlek számot adj meg");
                    }
                    if (4 < dontes || dontes < 1) Console.WriteLine("Kérlek a megadott opciók közül válassz!");
                    if (dontes == 4) Kilepes();
                    if (dontes == 3)
                    {
                        Console.WriteLine("Kérlek add meg a mentés nevét!");
                        jatek.MentesLetrehozasa(Console.ReadLine());
                        System.Threading.Thread.Sleep(1500);
                    }
                }
                    if (dontes == 1)
                    {
                        int valasz = 3;
                        do
                        {
                            char tipp = MassalhangzoBeker(jatek);
                            valasz = jatek.Tipp(tipp, rnd);
                            if (valasz == 2) Console.WriteLine("Kérlek mássalhangzót adj meg!");
                            else if (valasz == 1) Console.WriteLine("Sajnos rossz választás!");
                            else if (valasz == 0) Console.WriteLine("Gratulálok!");
                            System.Threading.Thread.Sleep(1500);
                        } while (valasz != 0 && valasz != 1);
                    }
                    if (dontes == 2)
                    {
                        string proba = Console.ReadLine();
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
                    System.Threading.Thread.Sleep(1500);
            }
            if (jatek.Jatekosok[jatek.JatekosKore].JatekosE == false)
            {
                string jelenlegiBot = jatek.Jatekosok[jatek.JatekosKore].Nev;
                Console.Clear();
                Console.WriteLine($"{jatek.KorSzama + 1}. kör: {jatek.Jatekosok[jatek.JatekosKore].Nev} jön");
                Console.WriteLine(jatek.ToString());
                Console.WriteLine("\n" + jatek.TitkosMondasString());
                string massalhangzok = "mnjlrbdgzvptkcsfh";
                char valasztott = massalhangzok[rnd.Next(0, massalhangzok.Length)];
                int valasz = jatek.Tipp(valasztott, rnd);
                if (valasz == 1) Console.WriteLine($"{jelenlegiBot} rosszat tippelt: {valasztott}");
                if (valasz == 0) Console.WriteLine($"{jelenlegiBot} tippje helyes volt: {valasztott}");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
