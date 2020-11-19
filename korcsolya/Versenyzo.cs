using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace korcsolya
{
    class Versenyzo
    {
        private string nev;

        public string Nev
        {
            get { return nev; }
        }

        private string orszag;

        public string Orszag
        {
            get { return orszag; }
        }

        private double tech;

        public double Tech
        {
            get { return tech; }
        }

        private double comp;

        public double Comp
        {
            get { return comp; }
        }

        private double hiba;

        public double Hiba
        {
            get { return hiba; }
        }

        private double pont;

        public double Pont
        {
            get { return pont; }
        }

        public Versenyzo(string nev, string orszag, double tech, double comp, double hiba)
        {
            this.nev = nev;
            this.orszag = orszag;
            this.tech = tech;
            this.comp = comp;
            this.hiba = hiba;

            pont = tech + comp - hiba;
        }
    }
}
