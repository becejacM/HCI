using HciProject2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace HciProject2.Dialogs
{
    /// <summary>
    /// Interaction logic for CourseTable.xaml
    /// </summary>
    public partial class CourseTable : Window
    {
        private Course course;

        Boolean addNew;
        String currId;
        int izmena;
        public static ObservableCollection<Course> courseShow { get; set; }

        private Course Course
        {
            get
            {
                return course;
            }
            set
            {
                course = value;
            }
        }
        public CourseTable()
        {
            InitializeComponent();
            
            course = new Course();
            Id.DataContext = course;
            naziv.DataContext = course;
            opis.DataContext = course;

            courseShow = new ObservableCollection<Course>();

            addNew = false;
            izmena = -1;
            dgrMain.ItemsSource = courseShow;
            foreach (Course s in MainWindow.courses)
            {
                courseShow.Add(s);
            }
            enableFields(false);
            dgrMain.UnselectAllCells();
            setSelected();
            this.DataContext = this;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Console.WriteLine("***** indeks selektovanog: " + dgrMain.SelectedIndex);
                setSelected();
                enableFields(true);
                addNew = false;
                currId = course.Id;
            }
        }

        private void setSelected()
        {
            Console.WriteLine("select: " + dgrMain.SelectedIndex);
            if (dgrMain.SelectedIndex != -1)
            {
                course.Copy(MainWindow.courses[dgrMain.SelectedIndex]);
            }

            else
            {
                //deep copy
                course.Id = "";
                course.Opis = "";
                course.Naziv = "";

            }

        }
        private void enableFields(bool e)
        {
            Id.IsEnabled = e;
            naziv.IsEnabled = e;
            opis.IsEnabled = e;
            //Softver.IsEnabled = e;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                bool b = false;
                foreach(Subject c in MainWindow.subjects)
                {
                    if (c.Smer.Id.Equals(courseShow[dgrMain.SelectedIndex].Id)){
                        b = true;
                    }
                }
                if (b)
                {
                    if (MessageBox.Show("This will delete subjects with this course. Delete anyway?", "Delete course", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                        return;
                    }
                    else
                    {
                        //do yes stuff
                        for(int i = MainWindow.subjects.Count - 1;i>-1; i--)
                        {
                            if (MainWindow.subjects[i].Smer.Id.Equals(courseShow[dgrMain.SelectedIndex].Id))
                            {
                                MainWindow.subjects.RemoveAt(i);
                            }
                        }
                        courseShow.RemoveAt(dgrMain.SelectedIndex);

                        saveSub();
                    }
                }
            }
            save();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Boolean hasError = false;
            if (addNew || !currId.Equals(course.Id))
            {
                foreach (Course s in MainWindow.courses)
                {
                    if (course.Equals(s))
                    {
                        MessageBox.Show("ID allready exists. ID must be unique.");
                        hasError = true;
                    }
                }
            }
            
            if (course.Id.Equals(""))
            {
                MessageBox.Show("ID must be set.");
                hasError = true;
            }
            if (course.Opis.Equals("") || course.Naziv.Equals("") )
            {
                MessageBox.Show("One or more values doesn't set. All values must be set.");
                hasError = true;
            }

            if (!hasError)
            {
                if (addNew)
                {
                    Console.WriteLine("tuuu");
                    Course c = new Course();

                    c.Copy(course);
                    c.Datum = DateTime.Now.ToString();
                    courseShow.Add(c);
                    addNew = false;

                }
                else if (dgrMain.SelectedIndex != -1 )
                {
                    Console.WriteLine("mmm");

                    int sIndex = dgrMain.SelectedIndex;
                    courseShow[dgrMain.SelectedIndex].Copy(course);
                    dgrMain.SelectedIndex = sIndex;
                    izmena = -1;
                }
                else if (izmena != -1)
                {
                    Console.WriteLine("mmm");

                    int sIndex = izmena;
                    courseShow[izmena].Copy(course);
                    dgrMain.SelectedIndex = sIndex;
                    izmena = -1;
                }

                save();

                dgrMain.UnselectAllCells();
                setSelected();
                enableFields(false);
            }
        }

        private void save()
        {

            /*File.Delete("courses.xml");
            XmlDocument xdoc = new XmlDocument();
            foreach (Course c in MainWindow.courses)
            {
                xdoc.LoadXml(XamlWriter.Save(c));
            }
            
            xdoc.Save("courses.xml");*/
            MainWindow.courses.Clear();
            foreach (Course s in courseShow)
            {
                MainWindow.courses.Add(s);
            }
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/courses.xml", "");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Course>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/courses.xml"))
            {
                List<Course> list = new List<Course>();
                foreach (Course c in courseShow)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }

            /*using (var writer = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/courses.txt"))
            {
                foreach (Course c in MainWindow.courses)
                {
                    writer.WriteLine(c.Id + ";" + c.Naziv + ";" + c.Datum + ";" + c.Opis);
                }
            }*/
            saveFile();
        }
        private void saveFile()
        {
            if (!MainWindow.fileName.Name.Equals(""))
            {
                File.WriteAllText(MainWindow.fileName.Name, "");

                XmlSerializer serializer = new XmlSerializer(typeof(List<Shema>));

                using (FileStream stream = File.OpenWrite(MainWindow.fileName.Name))
                {
                    List<Shema> list = new List<Shema>();
                    List<Classroom> c = new List<Classroom>();
                    foreach (Classroom c1 in MainWindow.classrooms)
                    {
                        c.Add(c1);
                    }
                    List<Subject> s = new List<Subject>();
                    foreach (Subject s1 in MainWindow.subjects)
                    {
                        s.Add(s1);

                    }
                    List<Software> ss = new List<Software>();
                    foreach (Software ss1 in MainWindow.softwares)
                    {
                        ss.Add(ss1);
                    }
                    List<Course> cc = new List<Course>();
                    foreach (Course cc1 in MainWindow.courses)
                    {
                        cc.Add(cc1);
                    }
                    Shema shema = new Shema(c, s, ss, cc);
                    list.Add(shema);
                    serializer.Serialize(stream, list);
                }
            }


        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addNew = true;
            enableFields(true);
            dgrMain.UnselectAllCells();
            setSelected();
        }

        private void Label_KeyDown(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1 && izmena==-1)
            {
                izmena = dgrMain.SelectedIndex;
            }
            Console.WriteLine("count: " + izmena);
            if (!naz.Text.Equals("") && !desc.Text.Equals(""))
            {
                courseShow.Clear();
                foreach (Course s in MainWindow.courses)
                {
                    if (s.Naziv.Contains(naz.Text) && s.Opis.Contains(desc.Text))
                    {
                        courseShow.Add(s);
                    }
                }
            }
            
            else if (!naz.Text.Equals(""))
            {
                courseShow.Clear();
                foreach (Course s in MainWindow.courses)
                {
                    if (s.Naziv.Contains(naz.Text))
                    {
                        courseShow.Add(s);
                    }
                }
            }
           
            else if (!desc.Text.Equals(""))
            {
                Console.WriteLine(desc.Text);
                courseShow.Clear();
                foreach (Course s in MainWindow.courses)
                {
                    if (s.Opis.Contains(desc.Text))
                    {
                        courseShow.Add(s);
                    }
                }
            }
            else
            {
                courseShow.Clear();
                foreach (Course s in MainWindow.courses)
                {
                    courseShow.Add(s);

                }
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        private void saveSub()
        {
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/subjects.xml", "");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Subject>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/subjects.xml"))
            {
                List<Subject> list = new List<Subject>();
                foreach (Subject c in MainWindow.subjects)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (search.Text.Equals(":All"))
            {
                courseShow.Clear();
                foreach (Course ss in MainWindow.courses)
                {
                    courseShow.Add(ss);
                }
                return;
            }

            if (search.Text.Equals("") || !search.Text.StartsWith(":"))
            {
                MessageBox.Show("Invalid query!");
                return;
            }
            //:Id c1 || :Description l
            string[] lines = search.Text.Split(' ');
            if (lines.Length != 2)
            {
                MessageBox.Show("Invalid query!");
                return;
            }
            string prvi = lines[0].Substring(1);
            string drugi = lines[1];
            courseShow.Clear();
            if (prvi.Equals("Id"))
            {
                foreach (Course ss in MainWindow.courses)
                {
                    if (ss.Id.Contains(drugi))
                    {
                        courseShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Name"))
            {
                foreach (Course ss in MainWindow.courses)
                {
                    if (ss.Naziv.Contains(drugi))
                    {
                        courseShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Date"))
            {
                foreach (Course ss in MainWindow.courses)
                {
                    if (ss.Datum.ToString().Equals(drugi))
                    {
                        courseShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Description"))
            {
                foreach (Course ss in MainWindow.courses)
                {
                    if (ss.Opis.Contains(drugi))
                    {
                        courseShow.Add(ss);
                    }
                }
            }
            
        }
    }
}
