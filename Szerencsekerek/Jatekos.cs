using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szerencsekerek
{
    class Jatekos
    {
        private string nev = "";
        private int pontok = 0;

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
        public int Pontok
        {
            get
            {
                return pontok;
            }
        }

        public void PontAdas(int pont)
        {
            pontok += pont;
        }
    }
}
