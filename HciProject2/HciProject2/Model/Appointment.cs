using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace HciProject2.Model
{
    public class Appointment : INotifyPropertyChanged
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
        private int kolona;
        [XmlIgnore]
        private int red;
        [XmlIgnore]
        private int velicina;
        [XmlIgnore]
        private String predmet;
        [XmlIgnore]
        private String status;
        [XmlIgnore]
        private String id;

        public Appointment()
        {

        }

        public Appointment(String id, int getColumn,int getRow, String s, String st,int vel)
        {
            this.id = id;
            this.kolona = getColumn;
            this.red = getRow;
            this.predmet = s;
            this.status = st;
            this.velicina = vel;
        }
        public String Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Kolona");
            }
        }
        public int Kolona
        {
            get { return kolona; }
            set
            {
                kolona = value;
                OnPropertyChanged("Kolona");
            }
        }

        public int Red
        {
            get { return red; }
            set
            {
                red = value;
                OnPropertyChanged("Red");
            }
        }

        public int Velicina
        {
            get { return velicina; }
            set
            {
                velicina = value;
                OnPropertyChanged("Velicina");
            }
        }
        public String Predmet
        {
            get { return predmet; }
            set
            {
                predmet = value;
                OnPropertyChanged("Predmet");
            }
        }

        public String Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }

        public override string ToString()
        {
            return String.Format("Subject: {0} ", Predmet); ;
        }

        public void Copy(Appointment other)
        {
            this.Id = other.Id;
            this.Red = other.Red;
            this.Kolona = other.Kolona;
            this.Predmet = other.Predmet;
            this.Status = other.Status;
            this.Velicina = other.Velicina;
        }
    }
}
