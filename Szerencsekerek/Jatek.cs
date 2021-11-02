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
                mondasok[szamlalo] = sr.ReadLine();
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
            titkosMondas = new char[mondas.Length];
            int index = 0;
            for (int i = 0; i < mondas.Length; i++)
            {
                if (mondas[i] == karakter) titkosMondas[index++] = karakter;
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
                    if (i < this.jatekosokSzama) jatekosok[i].Nev = $"Játékos{i + 1}";
                    else jatekosok[i].Nev = $"Gép{szamlaloGep++}";
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
        /// <summary>
        /// Leellenőrőzi, hogy az adott karakter helyes mássalhangzó-e.
        /// </summary>
        /// <param name="karakter"></param>
        /// <returns>
        /// 0, ha a karakter mássalhangzó, és még nem volt tippelve. Ebben az esetben elmenti a tippelt mássalhagnzók közé.
        /// 1, ha a karakter mássalhangzó, és már volt tippelve.
        /// 2, ha a karakter nem mássalhangzó.
        /// </returns>
        public int Tipp(char karakter, Random rnd) 
        {

            // Valahol itt van egy hiba, de nem tudom pontosan, hogy mi...
            // Valahol itt van egy hiba, de nem tudom pontosan, hogy mi...
            // Valahol itt van egy hiba, de nem tudom pontosan, hogy mi...
            // Valahol itt van egy hiba, de nem tudom pontosan, hogy mi...
            // Valahol itt van egy hiba, de nem tudom pontosan, hogy mi...
            if (!"mnjlrbdgzvptkcsfh".Contains(karakter)) return 2;
            else if (!tippek.Contains(karakter))
            {
                tippek += karakter;
                TitkosMondas(karakter);
                this.jatekosok[jatekosKore].PontAdas(rnd.Next(1000, 20001));
                //jatekosKore = jatekosKore == 2 ? 0 : + 1; 
                return 0;
            }
            else
            {
                return 1;
            }
        }
        public string TitkosMondasString()
        {
            string valasz = "";
            for (int i = 0; i < titkosMondas.Length; i++)
            {
                if (titkosMondas[i] != '_' && titkosMondas[i] != ' ') valasz += titkosMondas[i];
                if (titkosMondas[i] == '_') valasz += "_ ";
                else if (titkosMondas[i] == ' ') valasz += "  ";
            }
            return valasz;
        }
    }
}
