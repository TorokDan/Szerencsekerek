using System;
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
        private char[] titkosMondas = new char[1];
        private string tippekMassalhangzo = "";
        private string tippekMaganhangzo = "";
        private bool jatekVege = false;
        private bool segitsegMassalhangzo = false;
        private bool segitsegMaganhangzo = false;
        private string segitsegMassalhangzoString = "Eddigi tippek :";
        private string segitsegMaganhangzoString = "Eddigi tippek :";
        private int pontHatar = 3000;
        public string Mondas { get; private set; }

        public Jatek()
        {
            string file = "Mondasok.txt";
            string[] sorok = new string[0];
            if (File.Exists(file))
                sorok = File.ReadAllLines(file);
            else
            {
                Console.WriteLine("Mondasok.txt nem található, kérlek tetdd a megfelelő helyre!");
                System.Threading.Thread.Sleep(1500);
                Environment.Exit(0);
            }
            mondasok = new string[sorok.Length];
            Mondas = sorok[rnd.Next(0, mondasok.Length)];
            TitkosMondas();
        }
        public Jatek(string fileNev)
        {
            string[] adatok = File.ReadAllLines(fileNev);
            this.jatekosKore = int.Parse(adatok[0]);
            this.jatekosok[0] = new Jatekos(adatok[1].Split(' ')[0], int.Parse(adatok[1].Split(' ')[1]), int.Parse(adatok[1].Split(' ')[2]), (JatekosFajta)int.Parse(adatok[1].Split(' ')[3]));
            this.jatekosok[1] = new Jatekos(adatok[2].Split(' ')[0], int.Parse(adatok[2].Split(' ')[1]), int.Parse(adatok[2].Split(' ')[2]), (JatekosFajta)int.Parse(adatok[2].Split(' ')[3]));
            this.jatekosok[2] = new Jatekos(adatok[3].Split(' ')[0], int.Parse(adatok[3].Split(' ')[1]), int.Parse(adatok[3].Split(' ')[2]), (JatekosFajta)int.Parse(adatok[3].Split(' ')[3]));
            this.jatekosokSzama = int.Parse(adatok[4]);
            this.korSzama = int.Parse(adatok[5]);
            this.segitsegMassalhangzoString = adatok[6];
            this.segitsegMaganhangzoString = adatok[7];
            this.tippekMaganhangzo = adatok[8];
            this.tippekMassalhangzo = adatok[9];
            this.titkosMondas = adatok[10].ToCharArray();
            this.Mondas = adatok[11];
            this.segitsegMassalhangzo = bool.Parse(adatok[12]);
            this.segitsegMaganhangzo = bool.Parse(adatok[13]);
        }

        public int PontHatar
        {
            get
            {
                return pontHatar;
            }
        }
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
        public int JatekosKore
        {
            get
            {
                return jatekosKore;
            }
        }
        public Jatekos[] Jatekosok
        {
            get
            {
                return jatekosok;
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
                    if (i < this.jatekosokSzama)
                        jatekosok[i] = new Jatekos($"Játékos{i + 1}", JatekosFajta.jatekos);
                    else
                        jatekosok[i] = new Jatekos($"Gép{szamlaloGep++}", JatekosFajta.gep);
                }
            }
            get
            {
                return jatekosokSzama;
            }
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
                $"{this.jatekosok[0].Nev} {this.jatekosok[0].Pontok} {this.jatekosok[0].Talalat} {(int)this.jatekosok[0].Tipus}",
                $"{this.jatekosok[1].Nev} {this.jatekosok[1].Pontok} {this.jatekosok[1].Talalat} {(int)this.jatekosok[1].Tipus}",
                $"{this.jatekosok[2].Nev} {this.jatekosok[2].Pontok} {this.jatekosok[2].Talalat} {(int)this.jatekosok[2].Tipus}",
                this.jatekosokSzama.ToString(),
                this.korSzama.ToString(),
                this.segitsegMassalhangzoString,
                this.segitsegMaganhangzoString,
                this.tippekMaganhangzo,
                this.tippekMassalhangzo,
                new string(this.titkosMondas),
                this.Mondas,
                this.segitsegMassalhangzo.ToString(),
                this.segitsegMaganhangzo.ToString()
        };
            File.WriteAllLines($"{fileNev}.mentes", adatok);
        }
        public void UjKor()
        {
            this.korSzama++;
        }
        private void TitkosMondas()
        {
            titkosMondas = new char[Mondas.Length];
            int index = 0;
            for (int i = 0; i < Mondas.Length; i++)
            {
                if (Mondas[i] != ' ') titkosMondas[index++] = '_';
                else if (Mondas[i] == ' ') titkosMondas[index++] = ' ';
            }
        }
        private void TitkosMondas(char karakter)
        {
            int index = 0;
            for (int i = 0; i < Mondas.Length; i++)
            {
                if (Mondas[i] == karakter) titkosMondas[index++] = karakter;
                else if (tippekMassalhangzo.Contains(Mondas[i].ToString())) titkosMondas[index] = Mondas[index++];
                else if (Mondas[i] != ' ') titkosMondas[index++] = '_';
                else if (Mondas[i] == ' ') titkosMondas[index++] = ' ';
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
            if (proba == Mondas.ToLower())
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
            if (nyertesekSzama == 1) Console.WriteLine($"A játék véget ért, ezt a mondást nem tudta senki kitalálni: {this.Mondas}.\nA nyertes: {this.jatekosok[nyertes].Nev}");
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
            if (!MassalhangzoE(karakter)) 
                return 2;
            else if (!tippekMassalhangzo.Contains(karakter.ToString()) && Mondas.Contains(karakter.ToString()))
            {
                tippekMassalhangzo += karakter;
                segitsegMassalhangzoString += karakter + " ";
                TitkosMondas(karakter);
                this.jatekosok[jatekosKore].Eltalált();
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
                else if (jatekosKore < 2) 
                    jatekosKore++;
                return 1;
            }
        }
        public int MaganhangzoVetel(char karakter)
        {
            if (!MaganhangzoE(karakter))
                return 2;
            if (!tippekMaganhangzo.Contains(karakter.ToString()) && this.Mondas.Contains(karakter.ToString()))
            {
                this.tippekMaganhangzo += karakter;
                this.segitsegMaganhangzoString += karakter + " ";
                TitkosMondas(karakter);
                this.jatekosok[jatekosKore].Eltalált();
                this.jatekosok[jatekosKore].PontLevonas(this.pontHatar);
                return 0;
            }
            else
            {
                segitsegMaganhangzoString += karakter + " ";
                this.jatekosok[jatekosKore].PontLevonas(this.pontHatar);
                if (jatekosKore == 2)
                {
                    jatekosKore = 0;
                    korSzama++;
                }
                else if (jatekosKore < 2)
                    jatekosKore++;
                return 1;
            }
        }
    }
}
