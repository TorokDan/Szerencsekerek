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
        public string Tippek 
        {
            get
            {
                return tippek;
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
            StreamReader sr;
            try
            {
                sr = new StreamReader("Mondasok.txt", Encoding.UTF8);
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
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Mondasok.txt file nem található. Kérlek rakj az szerencsekerek.exe mellé egy Mondasok.txt-t, ami tartalmazza a mondásokat, majd próbálkozz újra.");
                Console.WriteLine("Nyomj entert a kilépéshez");
                Console.ReadLine();
                Environment.Exit(0);
            }
            mondas = mondasok[rnd.Next(0, mondasok.Length)];
            //titkosMondas = new char[mondas.Length];
            TitkosMondas();
        }
        public Jatek(string fileNev)
        {
            try
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
                this.tippek = adatok[6];
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Nem található a megadott file, kérlek csinálj egyet, és próbálkozz újra.");
                Environment.Exit(0);
            }
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
            Console.WriteLine("\n" + String.Format("{0,-20}{1,5}", "Név", "Pontok") + "\n");
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                Console.WriteLine(String.Format("{0,-20}{1,5}", this.jatekosok[i].Nev, jatekosok[i].Pontok));
                //Console.WriteLine($"{jatekosok[i].Nev}: \t{jatekosok[i].Pontok}");
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
                if (jatekosok[i].Pontok > jatekosok[nyertes].Pontok) nyertes = i;
            }
            int nyertesekSzama = 0;
            for (int i = 0; i < this.jatekosok.Length; i++)
            {
                if (jatekosok[i].Pontok == jatekosok[nyertes].Pontok) nyertesekSzama++;
            }
            if (nyertesekSzama == 1) Console.WriteLine($"A játék véget ért. A nyertes: {jatekosok[nyertes].Nev}");
            else
            {
                Console.Write("A játék nyertesei: ");
                for (int i = 0; i < this.jatekosok.Length; i++)
                {
                    if ( jatekosok[i].Pontok == jatekosok[nyertes].Pontok) Console.Write($"\n{jatekosok[i].Nev}\t{jatekosok[i].Pontok}");
                }
            }
            Console.WriteLine("Köszi, hogy játszottatok\nKilépéshez nyomj meg egy gombot.");
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
                this.tippek
            };
            File.WriteAllLines($"{fileNev}.txt", adatok);
        }
    }
}
