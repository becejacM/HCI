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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace HciProject2.Dialogs
{
    /// <summary>
    /// Interaction logic for SoftwareTable.xaml
    /// </summary>
    public partial class SoftwareTable : Window
    {
        private Software software;

        Boolean addNew;
        String currId;
        int izmena;
        public static ObservableCollection<Software> softwareShow { get; set; }

        private Software Software
        {
            get
            {
                return software;
            }
            set
            {
                software = value;
            }
        }
        public SoftwareTable()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            os.Items.Add("Linux");
            os.Items.Add("Windows");
            os.Items.Add("Cross platform");

            software = new Software();
            Id.DataContext = software;
            naziv.DataContext = software;
            opis.DataContext = software;
            godinaIzdavanja.DataContext = software;
            proizvodjac.DataContext = software;
            sajt.DataContext = software;
            cena.DataContext = software;
            os.DataContext = software;

            osistem.Items.Add("");
            osistem.Items.Add("Windows");
            osistem.Items.Add("Linux");
            osistem.Items.Add("Cross platform");

            softwareShow = new ObservableCollection<Software>();

            addNew = false;
            izmena = -1;
            dgrMain.ItemsSource = softwareShow;
            foreach (Software s in MainWindow.softwares)
            {
                softwareShow.Add(s);
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
                currId = software.Id;
            }
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                software.Copy(MainWindow.softwares[dgrMain.SelectedIndex]);
            }

            else
            {
                //deep copy
                software.Id = "";
                software.Opis = "";
                software.Naziv = "";
                software.GodinaIzdavanja = "";
                software.Proizvodjac = "";
                software.Sajt = "";
                software.Cena = 0;
                software.Os = "Windows";
                os.SelectedValue = "Windows";
            }

        }
        private void enableFields(bool e)
        {
            if (e)
            {
                if (addNew)
                {
                    Id.IsEnabled = e;
                }
            }
            else
            {
                Id.IsEnabled = e;
            }
            naziv.IsEnabled = e;
            opis.IsEnabled = e;
            godinaIzdavanja.IsEnabled = e;
            proizvodjac.IsEnabled = e;
            os.IsEnabled = e;
            cena.IsEnabled = e;
            sajt.IsEnabled = e;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                bool b = false;
                foreach (Subject c in MainWindow.subjects)
                {
                    Console.WriteLine("piseeeeeeeeeem: " + c.Softver.Id + "       " + softwareShow[dgrMain.SelectedIndex].Id);
                    if (c.Softver.Id.Equals(softwareShow[dgrMain.SelectedIndex].Id))
                    {
                        b = true;
                    }
                }
                if (b)
                {
                    if (MessageBox.Show("This will delete subjects and appointment with this software. Delete anyway?", "Delete software", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        //do no stuff
                        return;
                    }
                    else
                    {
                        //do yes stuff
                       

                        bool bb2 = false;
                        bool o = false;
                        foreach (Classroom c in MainWindow.classrooms)
                        {
                            foreach (Software s in c.Softver)
                            {
                                Console.WriteLine("mikiiiiiiiiii: "+c.Id+"    " + s.Id + "       " + softwareShow[dgrMain.SelectedIndex].Id);
                                if (s.Id.Equals(softwareShow[dgrMain.SelectedIndex].Id))
                                {
                                    bb2 = true;
                                    o = true;
                                }
                            }

                            if (o)
                            {
                                if (c.Softver.Count == 1)
                                {
                                    MessageBox.Show("You can't delete this software. He is the only one in classroom " + c.Id);
                                    return;
                                }
                            }
                            o = false;
                        }

                        if (bb2)
                        {
                            if (MessageBox.Show("This will delete software from list in classrooms. Delete anyway?", "Delete course", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            {
                                //do no stuff
                                return;
                            }
                            else
                            {
                                foreach (Classroom c in MainWindow.classrooms)
                                {
                                    for (int i = c.Softver.Count - 1; i > -1; i--)
                                    {
                                        if (c.Softver[i].Id.Equals(softwareShow[dgrMain.SelectedIndex].Id))
                                        {
                                            
                                            c.Softver.RemoveAt(i);
                                            String s = "";
                                            foreach (Software sp in c.Softver)
                                            {
                                                s += sp.Naziv + " ";
                                                Console.WriteLine("ddd " + s);
                                            }
                                            c.ImenaSoftvera = s;
                                        }
                                    }
                                }
                                for (int i = MainWindow.subjects.Count - 1; i > -1; i--)
                                {
                                    if (MainWindow.subjects[i].Softver.Id.Equals(softwareShow[dgrMain.SelectedIndex].Id))
                                    {
                                        foreach (Classroom c in MainWindow.classrooms)
                                        {
                                            for (int ii = c.Termini.Count - 1; ii > -1; ii--)
                                            {
                                                if (MainWindow.subjects[i].Naziv.Equals(c.Termini[ii].Predmet))
                                                {
                                                    c.Termini.RemoveAt(ii);
                                                }
                                            }
                                        }
                                        MainWindow.subjects.RemoveAt(i);
                                    }
                                }
                                Software sss = softwareShow.ElementAt(dgrMain.SelectedIndex);
                                MainWindow.softwares.Remove(sss);
                                
                            }
                        }

                        //saveSub();
                        //saveClass();
                    }
                }
                else
                {
                    bool bb = false;
                    bool o = false;
                    foreach (Classroom c in MainWindow.classrooms)
                    {
                        foreach (Software s in c.Softver)
                        {
                            if (s.Id.Equals(softwareShow[dgrMain.SelectedIndex].Id))
                            {
                                bb = true;
                                o = true;
                            }
                        }

                        if (o)
                        {
                            if (c.Softver.Count == 1)
                            {
                                MessageBox.Show("You can't delete this software. He is the only one in classroom " + c.Id);
                                return;
                            }
                        }
                        o = false;
                    }

                    if (bb)
                    {
                        if (MessageBox.Show("This will delete software from list in classrooms. Delete anyway?", "Delete course", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            //do no stuff
                            return;
                        }
                        else
                        {
                            foreach (Classroom c in MainWindow.classrooms)
                            {
                                for (int i = c.Softver.Count - 1; i > -1; i--)
                                {
                                    if (c.Softver[i].Id.Equals(softwareShow[dgrMain.SelectedIndex].Id))
                                    {
                                        c.Softver.RemoveAt(i);
                                        String s = "";
                                        foreach (Software sp in c.Softver)
                                        {
                                            s += sp.Naziv + " ";
                                            Console.WriteLine("ddd " + s);
                                        }
                                        c.ImenaSoftvera = s;
                                    }
                                }
                            }
                            Software sss = softwareShow.ElementAt(dgrMain.SelectedIndex);
                            MainWindow.softwares.Remove(sss);

                        }
                    }
                }
                //saveSub();
                //saveClass();

            }
            save();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!addNew && dgrMain.SelectedIndex == -1 && izmena==-1)
            {
                return;
            }
            Boolean hasError = false;
            if (addNew || !currId.Equals(software.Id))
            {
                foreach (Software s in MainWindow.softwares)
                {
                    if (software.Equals(s))
                    {
                        MessageBox.Show("ID allready exists. ID must be unique.");
                        hasError = true;
                        return;
                    }
                }
            }
            if (software.Id.Equals(""))
            {
                MessageBox.Show("ID must be set.");
                hasError = true;
                return;
            }
            if (software.Naziv.Equals("") || software.Opis.Equals("") || software.Cena.Equals("") || software.Proizvodjac.Equals("") ||
                software.Sajt.Equals("") || software.Os.Equals("") || software.GodinaIzdavanja.Equals("") )
            {
                MessageBox.Show("One or more values doesn't set. All values must be set.");
                hasError = true;
                return;
            }
            int d;
            if (int.TryParse(cena.Text, out d))
            {
                //valid 
            }
            else
            {
                //invalid
                MessageBox.Show("Please enter a valid number for price!");
                return;
            }
            if (!hasError)
            {
                if (addNew)
                {
                    Software c = new Software();

                    c.Copy(software);
                    MainWindow.softwares.Add(c);
                    addNew = false;

                }
                else if (dgrMain.SelectedIndex != -1)
                {
                    int sIndex = dgrMain.SelectedIndex;
                    softwareShow[dgrMain.SelectedIndex].Copy(software);
                    dgrMain.SelectedIndex = sIndex;
                }
                else if (izmena != -1)
                {
                    Console.WriteLine("mmm");

                    int sIndex = izmena;
                    softwareShow[izmena].Copy(software);
                    dgrMain.SelectedIndex = sIndex;
                    izmena = -1;
                }

                save();
            }
        }

        private void save()
        {

            softwareShow.Clear();
            foreach (Software s in MainWindow.softwares)
            {
                softwareShow.Add(s);
            }
            /*File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/softwares.xml", "");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Software>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/softwares.xml"))
            {
                List<Software> list = new List<Software>();
                foreach (Software c in softwareShow)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }*/
            dgrMain.UnselectAllCells();
            setSelected();
            enableFields(false);
            object o = new object();
            RoutedEventArgs r = new RoutedEventArgs();
            osistem_SelectionChanged(o, r);
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
        }

        /*private void saveSub()
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
        private void saveClass()
        {
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml", "");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Classroom>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml"))
            {
                List<Classroom> list = new List<Classroom>();
                foreach (Classroom c in MainWindow.classrooms)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }
        }*/
        private void osistem_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1 && izmena == -1)
            {
                izmena = dgrMain.SelectedIndex;
            }
            Console.WriteLine("count: " + MainWindow.subjects.Count);
            if (!naz.Text.Equals("") && !man.Text.Equals("") && !desc.Text.Equals(""))
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Naziv.Contains(naz.Text) && s.Proizvodjac.Contains(man.Text) && s.Opis.Contains(desc.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }

                    }
                }
            }
            else if (!naz.Text.Equals("") && !man.Text.Equals(""))
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Naziv.Contains(naz.Text) && s.Proizvodjac.Contains(man.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                }
            }
            else if (!naz.Text.Equals("") && !desc.Text.Equals(""))
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Naziv.Contains(naz.Text)  && s.Opis.Contains(desc.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                }
            }
            else if (!man.Text.Equals("") && !desc.Text.Equals(""))
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Proizvodjac.Contains(man.Text) && s.Opis.Contains(desc.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                }
            }
            else if (!naz.Text.Equals(""))
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Naziv.Contains(naz.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                }
            }
            else if (!man.Text.Equals(""))
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Proizvodjac.Contains(man.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                }
            }
            else if (!desc.Text.Equals(""))
            {
                Console.WriteLine(desc.Text);
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (s.Opis.Contains(desc.Text))
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    softwareShow.Add(s);
                                }

                            }
                            else
                            {
                                softwareShow.Add(s);
                            }
                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                }
            }
            else
            {
                softwareShow.Clear();
                foreach (Software s in MainWindow.softwares)
                {
                    if (osistem.SelectedIndex > -1)
                    {
                        if (!osistem.SelectedItem.Equals(""))
                        {
                            if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                            {
                                softwareShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                            {
                                softwareShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                            {
                                softwareShow.Add(s);
                            }

                        }
                        else
                        {
                            softwareShow.Add(s);
                        }
                    }
                    else
                    {
                        softwareShow.Add(s);
                    }

                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (search.Text.Equals(":All"))
            {
                osistem.SelectedIndex = -1;
                naz.Text = "";
                man.Text = "";
                desc.Text = "";
                softwareShow.Clear();
                foreach (Software ss in MainWindow.softwares)
                {
                    softwareShow.Add(ss);
                }
                return;
            }

            if (search.Text.Equals("") || !search.Text.StartsWith(":"))
            {
                MessageBox.Show("Invalid query!");
                return;
            }

            osistem.SelectedIndex = -1;
            naz.Text = "";
            man.Text = "";
            desc.Text = "";
            softwareShow.Clear();
            object o = new object();
            RoutedEventArgs r = new RoutedEventArgs();
            osistem_SelectionChanged(o, r);
            //:Id c1 || :Description l
            string[] lines = search.Text.Split(' ');
            if (lines.Length != 2)
            {
                MessageBox.Show("Invalid query!");
                return;
            }
            string prvi = lines[0].Substring(1);
            string drugi = lines[1];
            softwareShow.Clear();
            if (prvi.Equals("Id"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Id.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Name"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Naziv.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Os"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Os.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Manufacturer"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Proizvodjac.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Site"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Sajt.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("YearOfPublication"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.GodinaIzdavanja.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Price"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Cena==Int32.Parse(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Description"))
            {
                foreach (Software ss in MainWindow.softwares)
                {
                    if (ss.Opis.Contains(drugi))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }
        }
    }
}
