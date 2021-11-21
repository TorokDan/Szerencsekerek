using System;
using System.Collections.Generic;
//using System.Linq;
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
            this.jatekosKore = int.Parse(adatok[0]);
            this.jatekosok[0] = new Jatekos(adatok[1].Split(' ')[0], int.Parse(adatok[1].Split(' ')[1]), int.Parse(adatok[1].Split(' ')[2]), bool.Parse(adatok[1].Split(' ')[3]));
            this.jatekosok[1] = new Jatekos(adatok[2].Split(' ')[0], int.Parse(adatok[2].Split(' ')[1]), int.Parse(adatok[2].Split(' ')[2]), bool.Parse(adatok[2].Split(' ')[3]));
            this.jatekosok[2] = new Jatekos(adatok[3].Split(' ')[0], int.Parse(adatok[3].Split(' ')[1]), int.Parse(adatok[3].Split(' ')[2]), bool.Parse(adatok[3].Split(' ')[3]));
            this.jatekosokSzama = int.Parse(adatok[4]);
            this.korSzama = int.Parse(adatok[5]);
            this.segitsegMassalhangzoString = adatok[6];
            this.segitsegMaganhangzoString = adatok[7];
            this.tippekMaganhangzo = adatok[8];
            this.tippekMassalhangzo = adatok[9];
            this.titkosMondas = adatok[10].ToCharArray();
        }
        public void MentesLetrehozasa(string fileNev) 
        {
            string pontok = "";
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                pontok += this.jatekosok[i].Pontok.ToString() + " ";
            }
            string[] adatok = new string[]
            {
                this.jatekosKore.ToString(),
                $"{this.jatekosok[0].Nev} {this.jatekosok[0].Pontok} {this.jatekosok[0].Talalat} {this.jatekosok[0].JatekosE}",
                $"{this.jatekosok[1].Nev} {this.jatekosok[1].Pontok} {this.jatekosok[1].Talalat} {this.jatekosok[1].JatekosE}",
                $"{this.jatekosok[2].Nev} {this.jatekosok[2].Pontok} {this.jatekosok[2].Talalat} {this.jatekosok[2].JatekosE}",
                this.jatekosokSzama.ToString(),
                this.korSzama.ToString(),
                this.segitsegMassalhangzoString,
                this.segitsegMaganhangzoString,
                this.tippekMaganhangzo,
                this.tippekMassalhangzo,
                new string(this.titkosMondas)
        };
            File.WriteAllLines($"{fileNev}.mentes", adatok);
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
                else if (tippekMassalhangzo.Contains(mondas[i].ToString())) titkosMondas[index] = mondas[index++];
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
            return "mnjlrbdgzvptkcsfh".Contains(karakter.ToString());
        }
        public static bool MaganhangzoE(char karakter)
        {
            return "öüóuioőúűaéáí".Contains(karakter.ToString());
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
        public int TippMassalhangzo(char karakter) 
        {
            if (!MassalhangzoE(karakter)) return 2;
            else if (!tippekMassalhangzo.Contains(karakter.ToString()) && mondas.Contains(karakter.ToString()))
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
                segitsegMassalhangzoString += karakter+ " ";
                if (jatekosKore == 2)
                {
                    jatekosKore = 0;
                    korSzama++;
                }
                else if (jatekosKore < 2) jatekosKore++;
                return 1;
            }
        }
        public int MaganhangzoVetel(char karakter)
        {
            int ar = 3000;
            if (!MaganhangzoE(karakter))
                return 2;
            if (this.jatekosok[jatekosKore].Pontok < ar)
                return 1;
            if (!tippekMaganhangzo.Contains(karakter.ToString()) && this.mondas.Contains(karakter.ToString()))
            {
                this.tippekMaganhangzo += karakter;
                this.segitsegMaganhangzoString += karakter + " ";
                TitkosMondas(karakter);
                this.jatekosok[jatekosKore].Talalat += 1;
                this.jatekosok[jatekosKore].Pontok -= ar;
                return 0;
            }
            else
            {
                segitsegMaganhangzoString += karakter + " ";
                this.jatekosok[jatekosKore].Pontok -= ar;
                if (jatekosKore == 2)
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
