using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciProject2.Model
{
    public class Software : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String id;
        public String naziv;
        public String os;
        public String proizvodjac;
        public String sajt;
        public String godinaIzdavanja;
        public int cena;
        public String opis;

        public Software(String i, String n, String o, String p, String s, String gi, int c, String op)
        {
            id = i;
            naziv = n;
            os = o;
            proizvodjac = p;
            sajt = s;
            godinaIzdavanja = gi;
            cena = c;
            opis = op;
        }

        public Software()
        {
            Id = "";
        }

        public String Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public String Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }
        public String Os
        {
            get { return os; }
            set
            {
                os = value;
                OnPropertyChanged("Os");
            }
        }

        public String Proizvodjac
        {
            get { return proizvodjac; }
            set
            {
                proizvodjac = value;
                OnPropertyChanged("Proizvodjac");
            }
        }

        public String Sajt
        {
            get { return sajt; }
            set
            {
                sajt = value;
                OnPropertyChanged("Sajt");
            }
        }

        public String GodinaIzdavanja
        {
            get { return godinaIzdavanja; }
            set
            {
                godinaIzdavanja = value;
                OnPropertyChanged("GodinaIzdavanja");
            }
        }

        public int Cena
        {
            get { return cena; }
            set
            {
                cena = value;
                OnPropertyChanged("Cena");
            }
        }

        public String Opis
        {
            get { return opis; }
            set
            {
                opis = value;
                OnPropertyChanged("Opis");
            }
        }

        public void Copy(Software other)
        {
            this.Id = other.Id;
            this.Opis = other.Opis;
            this.Naziv = other.Naziv;
            this.Cena = other.Cena;
            this.Proizvodjac = other.Proizvodjac;
            this.Sajt = other.Sajt;
            this.Os = other.Os;
            this.GodinaIzdavanja = other.GodinaIzdavanja;
        }

        public override bool Equals(object obj)
        {
            Software other = obj as Software;
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }
    }
}

