using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HciProject2.Model
{
    public class Shema : INotifyPropertyChanged
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
        private List<Classroom> classrooms;
        [XmlIgnore]
        private List<Subject> subjects;
        [XmlIgnore]
        private List<Software> softwares;
        [XmlIgnore]
        private List<Course> courses;

        public Shema()
        {

        }

        public Shema(List<Classroom> c, List<Subject> s, List<Software> ss, List<Course> cc)
        {
            this.classrooms = c;
            this.subjects = s;
            this.softwares = ss;
            this.courses = cc;
        }

        public List<Classroom> Classrooms
        {
            get { return classrooms; }
            set
            {
                classrooms = value;
                OnPropertyChanged("Classrooms");
            }
        }

        public List<Subject> Subjects
        {
            get { return subjects; }
            set
            {
                subjects = value;
                OnPropertyChanged("Subjects");
            }
        }

        public List<Software> Softwares
        {
            get { return softwares; }
            set
            {
                softwares = value;
                OnPropertyChanged("Softwares");
            }
        }

        public List<Course> Courses
        {
            get { return courses; }
            set
            {
                courses = value;
                OnPropertyChanged("Courses");
            }
        }

    }
}
