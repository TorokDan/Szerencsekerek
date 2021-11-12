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
            Jatek jatek = new Jatek();
            Menu(jatek);
            JatekosokSzamaBeker(jatek);
            while (jatek.JatekVege == false && jatek.KorSzama < 3)
            {
                Console.WriteLine();
                Kor(jatek);
            }
            jatek.JatekVegePontokkal();
            ;
        }
        static void Menu(Jatek jatek)
        {
            string valasztott = " ";
            while (valasztott != "1" && valasztott != "2" && valasztott != 3.ToString())
            {
                Console.Clear();
                Console.WriteLine("Kérlek válassz az alábbi menüpontok közül: ");
                Console.WriteLine("1. Játék kezdése");
                Console.WriteLine("2. Mentes betöltése");
                Console.WriteLine("3. Beállítások");
                Console.WriteLine("4. Kilépés");
                valasztott = Console.ReadLine();
                if (valasztott == "4")
                    Kilepes();
                if (valasztott != 1.ToString() && valasztott != 2.ToString() && valasztott != 3.ToString() && valasztott != 4.ToString())
                    HibasValasztas();
            }
            if (valasztott == 2.ToString())  // mentes
            {
                string[] fileName = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.txt", SearchOption.AllDirectories);
                bool valtozas = false;
                while (!valtozas)
                {
                    Console.WriteLine("Kérlek válassz az alábbi mentések közül! (Kilépéshez nyomd meg a q-t)");
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
                            jatek = new Jatek(fileName[i - 1]);
                            valtozas = true;
                        }
                    }
                    if (valasztas == "q") Menu(jatek);
                    if (!valtozas) Console.WriteLine("\nKérlek a mefadott opciók közül válassz!\n");
                }
            }
            if (valasztott == 3.ToString()) // beállítások
            {
                string be = " ";
                while ( be != 5.ToString())
                {
                    Console.Clear();
                    Console.WriteLine("Beállítások");
                    Console.WriteLine("1. Játékosok száma \t {0}", jatek.JatekosokSzama == 4 ? "nincs állítva" : jatek.JatekosokSzama.ToString());
                    Console.WriteLine("2. Nevek beállítása");
                    Console.WriteLine("3. Mássalhangzok mutatása: \t {0}", jatek.SegitsegMassalhangzo == true ? "Be" : "Ki");
                    Console.WriteLine("4. Magánhangzók mutatása: \t {0}", jatek.SegitsegMaganhangzo == true ? "Be" : "Ki");
                    Console.WriteLine("5. Visza");
                    be = Console.ReadLine();
                    if (be == 1.ToString()) // Játékosok száma
                        JatekosokSzamaBeker(jatek);
                    else if (be == 2.ToString())
                    {
                        if (jatek.JatekosokSzama == 4)
                        {
                            Console.WriteLine("Kérlek először állítsd be, hogy hány játékos legyen!");
                            System.Threading.Thread.Sleep(1500);
                        }
                        else
                        {
                            string valtoztatniValoJatekos = " ";
                            while (valtoztatniValoJatekos != (jatek.Jatekosok.Length+1).ToString())
                            {
                                Console.Clear();
                                Console.WriteLine("Név Beállítása: ");
                                for (int i = 0; i <= jatek.Jatekosok.Length; i++)
                                {
                                    if (i < jatek.Jatekosok.Length)
                                        Console.WriteLine("{0}. {1} : {2}", i + 1, jatek.Jatekosok[i].JatekosE == true ? "Jatékos" : "Gép", jatek.Jatekosok[i].Nev);
                                    if (i == jatek.Jatekosok.Length)
                                        Console.WriteLine($"{i + 1}. Vissza");
                                }
                                valtoztatniValoJatekos = Console.ReadLine();
                                for (int i = 0; i < jatek.Jatekosok.Length; i++)
                                {
                                    if (valtoztatniValoJatekos == (i+1).ToString())
                                    {
                                        Console.WriteLine($"Kérlek add meg a(z) {valtoztatniValoJatekos}. új nevét");
                                        jatek.Jatekosok[int.Parse(valtoztatniValoJatekos) - 1].Nev = Console.ReadLine();
                                    }
                                }
                            }
                        }
                    }
                    else if (be == 3.ToString()) jatek.SegitsegMassalhangzo = jatek.SegitsegMassalhangzo == true ? false : true;
                    else if (be == 4.ToString()) jatek.SegitsegMaganhangzo = jatek.SegitsegMaganhangzo == true ? false : true;
                }
                Menu(jatek);
            }
        }
        static void JatekosokSzamaBeker(Jatek jatek)
        {
            while (jatek.JatekosokSzama == 4)
            {
                Console.WriteLine("Kérlek add meg, hogy hányan fogtok játszani (maximum 3): ");
                string bekertSzam = Console.ReadLine();
                Console.WriteLine(bekertSzam);
                if (bekertSzam == 1.ToString() || bekertSzam == 2.ToString() || bekertSzam == 3.ToString())
                    jatek.JatekosokSzama = int.Parse(bekertSzam);
                else Console.WriteLine("Kérlek 1 és 3 közötti számot adj meg");
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
            string tippStr = Console.ReadLine();
            char tipp = ' ';
            if (tippStr.Length == 1)
                tipp = char.Parse(tippStr);
            if (!Jatek.MassalhangzoE(tipp))
            {
                Console.WriteLine("Ez nem egy mássalhangzó.");
                System.Threading.Thread.Sleep(1500);
                MassalhangzoBeker(jatek);
            }
            return tipp;
        }
        static char MaganhangzoBeker(Jatek jatek)
        {
            Console.WriteLine($"\n{jatek.Jatekosok[jatek.JatekosKore].Nev} tippeljen egy magánhangzót");
            string tippStr = Console.ReadLine();
            char tipp = ' ';
            while (Jatek.MaganhangzoE(tipp))
            {
                if (tippStr.Length == 1)
                    tipp = char.Parse(tippStr);
                if (!Jatek.MaganhangzoE(tipp))
                {
                    Console.WriteLine("Ez nem egy magánhangzó.");
                    System.Threading.Thread.Sleep(1500);
                    //MaganhangzoBeker(jatek);
                }
            }
            return tipp;
        }
        static void Kor(Jatek jatek)
        {
            Random rnd = new Random();
            Console.WriteLine();
            if (jatek.Jatekosok[jatek.JatekosKore].JatekosE == true)
            {
                string dontes = " ";
                while (dontes != 1.ToString() && dontes != 2.ToString() && dontes != 3.ToString())
                {
                    Console.Clear();
                    Console.WriteLine($"{jatek.KorSzama + 1}. kör: {jatek.Jatekosok[jatek.JatekosKore].Nev} jön");
                    Console.WriteLine(jatek.ToString());
                    Console.WriteLine($"\n{jatek.TitkosMondasString()}\n");
                    Console.WriteLine("1. Mássalhangzó\t {0}", jatek.SegitsegMassalhangzo == false ? "" : jatek.SegitsegMassalhangzoString);
                    Console.WriteLine("2. Magánhangzó\t {0}", jatek.SegitsegMaganhangzo == false ? "" : jatek.SegitsegMaganhangzoString);
                    Console.WriteLine("3. Rákérdez");
                    Console.WriteLine("4. Mentés");
                    Console.WriteLine("5. Kilépés");
                    // itt sokminden át lett írva, ezt alaposan ki kell tesztelni
                    dontes = Console.ReadLine();
                    if (dontes == 1.ToString()) //tipp
                    {
                        int valasz = 3;
                        while (valasz != 0 && valasz != 1)
                        {
                            char tipp = MassalhangzoBeker(jatek);
                            Console.WriteLine("asdf");
                            valasz = jatek.TippMassalhangzo(tipp);
                            //if (valasz == 2) Console.WriteLine("Kérlek mássalhangzót adj meg!");
                            if (valasz == 1) Console.WriteLine("Sajnos rossz választás!");
                            else if (valasz == 0) Console.WriteLine("Gratulálok!");
                            System.Threading.Thread.Sleep(1500);
                        }
                    }
                    else if (dontes == 2.ToString())
                    {
                        int valasz = 3;
                        do
                        {
                            char tipp = MaganhangzoBeker(jatek);
                            valasz = jatek.MaganhangzoVetel(tipp);
                            if (valasz == 2) Console.WriteLine();
                        } while (valasz != 0 && valasz != 1);
                    }
                    else if (dontes == 3.ToString()) // rákérdez
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
                    else if (dontes == 4.ToString()) // ment
                    {
                        Console.WriteLine("Kérlek add meg a mentés nevét!");
                        jatek.MentesLetrehozasa(Console.ReadLine());
                        System.Threading.Thread.Sleep(1500);
                    }
                    else if (dontes == 5.ToString()) // kilep
                        Kilepes();
                    else
                        Console.WriteLine("Kérlek a lehetőségek közül válassz");
                    System.Threading.Thread.Sleep(1500);
                }
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
                int valasz = jatek.TippMassalhangzo(valasztott);
                if (valasz == 1) Console.WriteLine($"{jelenlegiBot} rosszat tippelt: {valasztott}");
                if (valasz == 0) Console.WriteLine($"{jelenlegiBot} tippje helyes volt: {valasztott}");
                System.Threading.Thread.Sleep(2000);
            }
        }
    }
}
