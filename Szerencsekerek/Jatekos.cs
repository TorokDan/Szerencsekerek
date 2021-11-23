namespace Szerencsekerek
{
    class Jatekos
    {
        private string nev = "";
        private int pontok = 0;
        private int talalat = 0;
        private bool jatekosE;

        public Jatekos()
        {

        }
        public Jatekos(string nev, int pontok, int talalat, bool jatekosE)
        {
            this.nev = nev;
            this.pontok = pontok;
            this.talalat = talalat;
            this.jatekosE = jatekosE;
        }

        public string Nev
        {
            get
            {
                return nev;
            }
            set 
            {
                nev = value;
            }
        }
        public bool JatekosE
        {
            get
            {
                return this.jatekosE;
            }
            set
            {
                this.jatekosE = value;
            }
        }
        public int Pontok
        {
            get
            {
                return pontok;
            }
            set
            {
                this.pontok = value;
            }
        }
        public void PontAdas(int pont)
        {
            pontok += this.talalat * pont;
        }
        public int Talalat
        {
            get
            {
                return this.talalat;
            }
            set
            {
                this.talalat = value;
            }
        }
    }
}
