namespace Szerencsekerek
{
    enum JatekosFajta { jatekos, gep}
    class Jatekos
    {
        public string Nev { get; set; }
        public int Pontok { get; private set; }
        public int Talalat { get; private set; }
        public JatekosFajta Tipus { get; private set; }

        public Jatekos() :this("", 0, 0, JatekosFajta.gep) { }
        public Jatekos(string nev, JatekosFajta jatekosTipus) :this(nev, 0, 0, jatekosTipus) { }
        public Jatekos(string nev, int pontok, int talalat, JatekosFajta jatekosTipus)
        {
            this.Nev = nev;
            this.Pontok = pontok;
            this.Talalat = talalat;
            this.Tipus = jatekosTipus;
        }
        public void PontAdas(int pont)
        {
            Pontok += this.Talalat * pont;
        }
        public void PontLevonas(int pont)
        {
            Pontok -= pont;
        }
        public void Eltalált()
        {
            this.Talalat++;
        }
    }
}
