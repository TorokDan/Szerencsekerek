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
        Random rnd = new Random();
        private Jatekos[] jatekosok = new Jatekos[3];
        private int jatekosokSzama = 4;
        private int jatekosKore = 0;
        private int korSzama = 0;
        private string[] mondasok = new string[1];
        private string mondas;
        private char[] titkosMondas = new char[1];
        private string tippekMassalhangzo = "";
        private string tippekMaganhangzo = "";
        private bool jatekVege = false;
        private bool segitsegMassalhangzo = false;
        private bool segitsegMaganhangzo = false;
        private string segitsegMassalhangzoString = "Eddigi tippek :";
        private string segitsegMaganhangzoString = "Eddigi tippek :";
        public string SegitsegMassalhangzoString
        {
            get { return segitsegMassalhangzoString; }
        }
        public bool SegitsegMassalhangzo
        {
            get { return segitsegMassalhangzo; }
            set { segitsegMassalhangzo = value; }
        }
        public string SegitsegMaganhangzoString
        {
            get { return segitsegMaganhangzoString; }
        }
        public bool SegitsegMaganhangzo
        {
            get { return segitsegMaganhangzo; }
            set { segitsegMaganhangzo = value; }
        }
        public bool JatekVege
        {
            get
            {
                return jatekVege;
            }
        }
        public string TippekMassalhangzo 
        {
            get
            {
                return tippekMassalhangzo;
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
        public Jatek()
        {
            string file = "Mondasok.txt";
            string[] sorok = new string[0];
            if (File.Exists(file))
                sorok = File.ReadAllLines(file);
            else
            {
                Console.WriteLine("Mondasok.txt nem található, kérlek tetdd a megfelelő helyre!");
                System.Threading.Thread.Sleep(2000);
                Environment.Exit(0);
            }
            mondasok = new string[sorok.Length];
            mondas = sorok[rnd.Next(0, mondasok.Length)];
            TitkosMondas();
        }
        public Jatek(string fileNev)
        {
            string[] adatok = File.ReadAllLines(fileNev);
            this.mondas = adatok[0];
            this.titkosMondas = adatok[1].ToCharArray();
            JatekosokSzama = int.Parse(adatok[2]);
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                this.jatekosok[i].Pontok = int.Parse(adatok[3].Split(' ')[i]);
            }
            this.jatekosKore = int.Parse(adatok[4]);
            this.korSzama = int.Parse(adatok[5]);
            this.tippekMassalhangzo = adatok[6];
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
            int index = 0;
            for (int i = 0; i < mondas.Length; i++)
            {
                if (mondas[i] == karakter) titkosMondas[index++] = karakter;
                else if (tippekMassalhangzo.Contains(mondas[i])) titkosMondas[index] = mondas[index++];
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
        public override string ToString()
        {
            string s = "\n" + String.Format("{0,-20}{1,5}", "Név", "Pontok") + "\n";
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                s += String.Format("{0,-20}{1,5}", this.jatekosok[i].Nev, jatekosok[i].Pontok) + "\n";
            }
            return s;
        }
        public static bool MassalhangzoE(char karakter)
        {
            return "mnjlrbdgzvptkcsfh".Contains(karakter);
        }
        public static bool MaganhangzoE(char karakter)
        {
            return "öüóeuoőúűaéáí".Contains(karakter);
        }
        public int TippMassalhangzo(char karakter) 
        {
            if (!"mnjlrbdgzvptkcsfh".Contains(karakter)) return 2;
            else if (!tippekMassalhangzo.Contains(karakter) && mondas.Contains(karakter))
            {
                tippekMassalhangzo += karakter;
                segitsegMassalhangzoString += karakter + " ";
                TitkosMondas(karakter);
                this.jatekosok[jatekosKore].Talalat += 1;
                this.jatekosok[jatekosKore].PontAdas(this.rnd.Next(1000, 20001));
                return 0;
            }
            else
            {
                segitsegMassalhangzoString += karakter + " ";
                if (jatekosKore == 2)
                {
                    jatekosKore = 0;
                    korSzama++;
                }
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
        public void Kilepes(int jatekos)
        {
            Console.Clear();
            Console.WriteLine($"Gratulálunk, {this.jatekosok[jatekos].Nev} nyert");
            Console.WriteLine("Köszi, hogy játszottál");
            Console.WriteLine("Nyomj entert a kilépéshez");
            Console.ReadLine();
            Environment.Exit(0);
        }
        public void JatekVegePontokkal()
        {
            Console.Clear();
            int nyertes = 0;
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                if (this.jatekosok[i].Pontok > this.jatekosok[nyertes].Pontok) nyertes = i;
            }
            int nyertesekSzama = 0;
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                if (this.jatekosok[i].Pontok == this.jatekosok[nyertes].Pontok) nyertesekSzama++;
            }
            if (nyertesekSzama == 1) Console.WriteLine($"A játék véget ért, ezt a mondást nem tudta senki kitalálni: {this.mondas}.\nA nyertes: {this.jatekosok[nyertes].Nev}");
            else
            {
                Console.Write("A játék nyertesei: ");
                for (int i = 0; i < this.jatekosok.Length; i++)
                {
                    if ( this.jatekosok[i].Pontok == this.jatekosok[nyertes].Pontok) Console.Write($"\n{this.jatekosok[i].Nev}\t{this.jatekosok[i].Pontok}");
                }
            }
            Console.WriteLine("Köszi, hogy játszottatok\nKilépéshez nyomj entert.");
            Console.ReadLine();
            Environment.Exit(0);
        }
        public void MentesLetrehozasa(string fileNev) 
        {
            string titkosMondasString = "";
            for (int i = 0; i < this.titkosMondas.Length; i++)
            {
                titkosMondasString += titkosMondas[i];
            }
            string pontok = "";
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                pontok += this.jatekosok[i].Pontok.ToString() + " ";
            }
            string[] adatok = new string[]
            {
                this.mondas,
                titkosMondasString,
                this.jatekosokSzama.ToString(),
                pontok,
                this.jatekosKore.ToString(),
                this.korSzama.ToString(),
                this.tippekMassalhangzo
            };
            File.WriteAllLines($"{fileNev}.txt", adatok);
        }
        public int MaganhangzoVetel(char karakter)
        {
            string pool = "öüóeuoőúűaéáí";
            int ar = 3000;
            if (this.jatekosok[jatekosKore].Pontok < ar)
                return 1;
            if (!pool.Contains(karakter))
                return 2;
            if (pool.Contains(karakter) && this.mondas.Contains(karakter))
            {
                this.tippekMaganhangzo += karakter;
                this.segitsegMaganhangzoString += karakter + " ";
                TitkosMondas(karakter);
                return 0;
            }
            else
            {
                segitsegMaganhangzoString += karakter + " ";
                if (jatekosKore == 0)
                {
                    jatekosKore = 0;
                    korSzama++;
                }
                else if (jatekosKore < 2)
                    jatekosKore++;
                return 2;
            }
        }
    }
}
