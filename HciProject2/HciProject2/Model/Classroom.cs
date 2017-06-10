using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HciProject2.Model
{
    public class Classroom : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        [XmlIgnore]
        public String id;
        [XmlIgnore]
        public String opis;
        [XmlIgnore]
        public int brRadnihMesta;
        [XmlIgnore]
        public Boolean prisustvoProjektora;
        [XmlIgnore]
        public Boolean prisustvoTable;
        [XmlIgnore]
        public Boolean prisustvoPametneTable;
        [XmlIgnore]
        public String os;
        [XmlIgnore]
        public List<Software> softver;
        [XmlIgnore]
        public List<Appointment> termini = new List<Appointment>();
        [XmlIgnore]
        public String imenaSoftvera;

        public Classroom(String i, String o, int br, Boolean pp, Boolean pt, Boolean ppt, String osistem,List<Software> soft)
        {
            id = i;
            opis = o;
            brRadnihMesta = br;
            prisustvoProjektora = pp;
            prisustvoTable = pt;
            prisustvoPametneTable = ppt;
            os = osistem;
            softver = soft;
            termini = new List<Appointment>();
            imenaSoftvera = "";
        }

        public Classroom()
        {

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

        public String Opis
        {
            get { return opis; }
            set
            {
                opis = value;
                OnPropertyChanged("Opis");
            }
        }

        public String ImenaSoftvera
        {
            get { return imenaSoftvera; }
            set
            {
                imenaSoftvera = value;
                OnPropertyChanged("ImenaSoftvera");
            }
        }
        public int BrRadnihMesta
        {
            get { return brRadnihMesta; }
            set
            {
                brRadnihMesta = value;
                OnPropertyChanged("BrRadnihMesta");
            }
        }

        public Boolean PrisustvoProjektora
        {
            get { return prisustvoProjektora; }
            set
            {
                prisustvoProjektora = value;
                OnPropertyChanged("PrisustvoProjektora");
            }
        }

        public Boolean PrisustvoTable
        {
            get { return prisustvoTable; }
            set
            {
                prisustvoTable = value;
                OnPropertyChanged("PrisustvoTable");
            }
        }

        public Boolean PrisustvoPametneTable
        {
            get { return prisustvoPametneTable; }
            set
            {
                prisustvoPametneTable = value;
                OnPropertyChanged("PrisustvoPametneTable");
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

        public List<Software> Softver
        {
            get { return softver; }
            set
            {
                softver = value;
                OnPropertyChanged("Softver");
            }
        }

        public List<Appointment> Termini
        {
            get { return termini; }
            set
            {
                termini = value;
                OnPropertyChanged("Termini");
            }
        }
        public void Copy(Classroom other)
        {
            this.Id = other.Id;
            this.Opis = other.Opis;
            this.BrRadnihMesta = other.BrRadnihMesta;
            this.PrisustvoProjektora = other.PrisustvoProjektora;
            this.PrisustvoTable = other.PrisustvoTable;
            this.PrisustvoPametneTable = other.PrisustvoPametneTable;
            this.Os = other.Os;
            this.Softver = other.Softver;
            this.Termini = other.termini;
            this.ImenaSoftvera = other.ImenaSoftvera;
        }

        public override bool Equals(object obj)
        {
            Classroom other = obj as Classroom;
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }
    }
}
