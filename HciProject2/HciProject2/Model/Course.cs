using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HciProject2.Model
{
    public class Course : INotifyPropertyChanged
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
        public String datum;
        public String opis;

        public Course()
        {
            id = "";
        }

        public Course(String i, String n, String d, String o)
        {
            id = i;
            naziv = n;
            datum = d;
            opis = o;
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

        public String Datum
        {
            get { return datum; }
            set
            {
                datum = value;
                OnPropertyChanged("Datum");
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

        public void Copy(Course other)
        {
            this.Id = other.Id;
            this.Opis = other.Opis;
            this.Naziv = other.Naziv;
            this.Datum = other.Datum;
        }

        public override bool Equals(object obj)
        {
            Course other = obj as Course;
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }
    }
}
