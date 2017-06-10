using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciProject2.Model
{
    public class Subject : INotifyPropertyChanged
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
        public Course smer;
        public String opis;
        public int velicinaGrupe;
        public int minDuzinaTermina;
        public int brTermina;
        public Boolean prisustvoProjektora;
        public Boolean prisustvoTable;
        public Boolean prisustvoPametneTable;
        public String os;
        public Software softver;

        public Subject(String i,String n, Course c, String o, int v,int mdt,int bt, Boolean pp, Boolean pt, Boolean ppt, String osistem, Software soft)
        {
            id = i;
            naziv = n;
            smer = c;
            opis = o;
            velicinaGrupe = v;
            minDuzinaTermina = mdt;
            brTermina = bt;
            prisustvoProjektora = pp;
            prisustvoTable = pt;
            prisustvoPametneTable = ppt;
            os = osistem;
            softver = soft;
        }

        public Subject()
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
        public String Naziv
        {
            get { return naziv; }
            set
            {
                naziv = value;
                OnPropertyChanged("Naziv");
            }
        }

        public Course Smer
        {
            get { return smer; }
            set
            {
                smer = value;
                OnPropertyChanged("Smer");
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

        public int VelicinaGrupe
        {
            get { return velicinaGrupe; }
            set
            {
                velicinaGrupe = value;
                OnPropertyChanged("VelicinaGrupe");
            }
        }

        public int MinDuzinaTermina
        {
            get { return minDuzinaTermina; }
            set
            {
                minDuzinaTermina = value;
                OnPropertyChanged("MinDuzinaTermina");
            }
        }

        public int BrTermina
        {
            get { return brTermina; }
            set
            {
                brTermina = value;
                OnPropertyChanged("BrTermina");
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

        public Software Softver
        {
            get { return softver; }
            set
            {
                softver = value;
                OnPropertyChanged("Softver");
            }
        }

        public void Copy(Subject other)
        {
            this.Id = other.Id;
            this.Opis = other.Opis;
            this.Naziv = other.Naziv;
            this.PrisustvoProjektora = other.PrisustvoProjektora;
            this.PrisustvoTable = other.PrisustvoTable;
            this.PrisustvoPametneTable = other.PrisustvoPametneTable;
            this.Os = other.Os;
            this.Softver = other.Softver;
            this.BrTermina = other.BrTermina;
            this.MinDuzinaTermina = other.MinDuzinaTermina;
            this.VelicinaGrupe = other.VelicinaGrupe;
            this.Smer = other.Smer;
        }

        public override bool Equals(object obj)
        {
            Subject other = obj as Subject;
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }
    }

}

