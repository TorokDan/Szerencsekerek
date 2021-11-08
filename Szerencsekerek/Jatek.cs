using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Szerencsekerek
{
    class Jatek
    {
        private Jatekos[] jatekosok = new Jatekos[3];
        private int jatekosokSzama = 4;
        private int jatekosKore = 0;
        private int korSzama = 0;
        private string[] mondasok = new string[1];
        private string mondas;
        private char[] titkosMondas = new char[1];
        private string tippek = "";
        private bool jatekVege = false;
        public bool JatekVege
        {
            get
            {
                return jatekVege;
            }
        }
        public int KorSzama
        {
            get
            {
                return korSzama;
            }
        }
        public void UjKor()
        {
            this.korSzama++;
        }
        public Jatek(Random rnd)
        {
            //Nem a legoptimálisabb megoldás, ezt később megérné még átgondolni....
            StreamReader sr = new StreamReader("Mondasok.txt", Encoding.UTF8);
            int sorokSzama = 0;
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                sorokSzama++;
            }
            sr.Close();
            mondasok = new string[sorokSzama];
            sr = new StreamReader("Mondasok.txt", Encoding.UTF8);
            int szamlalo = 0;
            while (!sr.EndOfStream && szamlalo < sorokSzama)
            {
                mondasok[szamlalo] = sr.ReadLine().ToLower();
                szamlalo++;
            }
            sr.Close();
            mondas = mondasok[rnd.Next(0, mondasok.Length)];
            titkosMondas = new char[mondas.Length];
            TitkosMondas();
        }
        public int JatekosKore
        {
            get
            {
                return jatekosKore;
            }
        }
        private void TitkosMondas()
        {
            titkosMondas = new char[mondas.Length];
            int index = 0;
            for (int i = 0; i < mondas.Length; i++)
            {
                if (mondas[i] != ' ') titkosMondas[index++] = '_';
                else if (mondas[i] == ' ') titkosMondas[index++] = ' ';
            }
        }
        private void TitkosMondas(char karakter)
        {
            // Itt valami nem jó.... valamiért nem jegyzi meg a régi tippeket maga a tömb....
            int index = 0;
            for (int i = 0; i < mondas.Length; i++)
            {
                if (mondas[i] == karakter) titkosMondas[index++] = karakter;
                else if (tippek.Contains(mondas[i])) titkosMondas[index] = mondas[index++];
                else if (mondas[i] != ' ') titkosMondas[index++] = '_';
                else if (mondas[i] == ' ') titkosMondas[index++] = ' ';
            }
        }
        public Jatekos[] Jatekosok
        {
            get
            {
                return jatekosok;
            }
        }
        public string Mondas
        {
            get
            {
                return mondas;
            }
        }
        public int JatekosokSzama
        {
            set
            {
                this.jatekosokSzama = value;
                int szamlaloGep = 1;
                for (int i = 0; i < jatekosok.Length; i++)
                {
                    jatekosok[i] = new Jatekos();
                    if (i < this.jatekosokSzama)
                    {
                        jatekosok[i].Nev = $"Játékos{i + 1}";
                        jatekosok[i].JatekosE = true;
                    }
                    else
                    {
                        jatekosok[i].Nev = $"Gép{szamlaloGep++}";
                        jatekosok[i].JatekosE = false;
                    }
                }
            }
            get
            {
                return jatekosokSzama;
            }
        }
        public void Eredmeyek()
        {
            for (int i = 0; i < jatekosok.Length; i++)
            {
                Console.WriteLine($"{jatekosok[i].Nev}: \t{jatekosok[i].Pontok}");
            }
        }
        public bool MassalhangzoE(char karakter)
        {
            return "mnjlrbdgzvptkcsfh".Contains(karakter);
        }
        public int Tipp(char karakter, Random rnd) 
        {
            if (!"mnjlrbdgzvptkcsfh".Contains(karakter)) return 2;
            else if (!tippek.Contains(karakter) && mondas.Contains(karakter))
            {
                tippek += karakter;
                TitkosMondas(karakter);
                this.jatekosok[jatekosKore].PontAdas(rnd.Next(1000, 20001));
                return 0;
            }
            else
            {
                if (jatekosKore == 2) jatekosKore = 0;
                else if (jatekosKore < 2) jatekosKore++;
                return 1;
            }
        }
        public bool Rakerdez(string proba)
        {
            if (proba == mondas.ToLower())
            {
                this.jatekVege = true;
                return true; 
            }
            return false;
        }
        public string TitkosMondasString()
        {
            string valasz = "";
            for (int i = 0; i < titkosMondas.Length; i++)
            {
                if (titkosMondas[i] != '_' && titkosMondas[i] != ' ') valasz = $"{valasz}{titkosMondas[i]} ";
                if (titkosMondas[i] == '_') valasz += "_ ";
                else if (titkosMondas[i] == ' ') valasz += "  ";
            }
            return valasz;
        }
        //public void Nyertes(int jatekos)
        //{
        //    Console.WriteLine($"Gratulálunk, {this.jatekosok[jatekos]} nyert");
        //}
        public void Kilepes(int jatekos)
        {
            Console.Clear();
            Console.WriteLine($"Gratulálunk, {this.jatekosok[jatekos].Nev} nyert");
            Console.WriteLine("Köszi, hogy játszottál");
            System.Threading.Thread.Sleep(1500);
            Environment.Exit(0);
        }
        public void JatekVegePontokkal()
        {
            Console.Clear();
            int nyertes = 0;
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                if (jatekosok[i].Pontok > jatekosok[nyertes].Pontok) nyertes = i;
            }
            int nyertesekSzama = 0;
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                if (jatekosok[i].Pontok == jatekosok[nyertes].Pontok) nyertesekSzama++;
            }
            if (nyertesekSzama == 1) Console.WriteLine($"A játék véget ért. A nyertes: {jatekosok[nyertes]}");
            else
            {
                Console.Write("A játék nyertesei: ");
                for (int i = 0; i < this.jatekosok.Length; i++)
                {
                    if ( jatekosok[i].Pontok == jatekosok[nyertes].Pontok) Console.Write($"\n{jatekosok[i].Nev}\t{jatekosok[i].Pontok}");
                }
            }
            Console.WriteLine("Köszi, hogy játszottatok");
            System.Threading.Thread.Sleep(1500);
            Environment.Exit(0);
        }
        public void BotLepes(Random rnd)
        {
            string massalhangzok = "mnjlrbdgzvptkcsfh";
            char valasztott = massalhangzok[rnd.Next(0, massalhangzok.Length)];
            if (massalhangzok.Contains(valasztott) && !tippek.Contains(valasztott))
            {
                Console.WriteLine($"Talán: {valasztott}");
            }
        }
    }
}
