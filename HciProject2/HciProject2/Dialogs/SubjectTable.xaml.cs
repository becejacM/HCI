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
    /// Interaction logic for SubjectTable.xaml
    /// </summary>
    public partial class SubjectTable : Window
    {
        Point startPoint = new Point();
        private Subject subject;
        private ObservableCollection<Software> softwares;
        private ObservableCollection<Course> courses;
        public static ObservableCollection<Subject> subjectsShow { get; set; }
        public static ObservableCollection<Software> softwareShow { get; set; }

        Boolean addNew;
        String currId;

        private Subject Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }
        public SubjectTable()
        {
            InitializeComponent();
            os.Items.Add("Linux");
            os.Items.Add("Windows");
            os.Items.Add("Both");

            osistem.Items.Add("");
            osistem.Items.Add("Windows");
            osistem.Items.Add("Linux");
            osistem.Items.Add("Both");

            o.Items.Add("Windows");
            o.Items.Add("Linux");
            o.Items.Add("Cross platform");

            subject = new Subject();
            softwares = new ObservableCollection<Software>();
            courses = new ObservableCollection<Course>();
            subjectsShow = new ObservableCollection<Subject>();
            softwareShow = new ObservableCollection<Software>();

            id.DataContext = subject;
            naziv.DataContext = subject;
            opis.DataContext = subject;
            prisustvoProjektora.DataContext = subject;
            prisustvoTable.DataContext = subject;
            smartTable.DataContext = subject;
            os.DataContext = subject;
            brTermina.DataContext = subject;
            minDuzinaTermina.DataContext = subject;
            velicinaGrupe.DataContext = subject;

            foreach(Subject s in MainWindow.subjects)
            {
                subjectsShow.Add(s);
            }
            foreach (Software s in MainWindow.softwares)
            {
                softwareShow.Add(s);
            }
            dgrMain.ItemsSource = subjectsShow;
            allSofts.ItemsSource = softwareShow;
            allCourses.ItemsSource = MainWindow.courses;

            DropList.ItemsSource = softwares;
            DropListC.ItemsSource = courses;

            addNew = false;
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
                currId = subject.Id;
            }
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                subject.Copy(subjectsShow[dgrMain.SelectedIndex]);
                softwares.Clear();
                softwares.Add(subjectsShow[dgrMain.SelectedIndex].Softver);
                courses.Clear();
                courses.Add(subjectsShow[dgrMain.SelectedIndex].Smer);

                softwareShow.Clear();
                foreach (Software ss in MainWindow.softwares)
                {
                    if (!softwares.Contains(ss))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }

            else
            {
                //deep copy
                subject.Id = "";
                subject.Opis = "";
                subject.Naziv = "";
                subject.PrisustvoProjektora = false;
                subject.PrisustvoTable = false;
                subject.PrisustvoPametneTable = false;
                subject.Os = "Windows";
                subject.MinDuzinaTermina = 0;
                subject.VelicinaGrupe = 0;
                subject.BrTermina = 0;
                softwares.Clear();
                courses.Clear();
                os.SelectedValue = "Windows";
                subject.Smer = new Course();
                subject.Softver = new Software();

                softwareShow.Clear();
                foreach (Software ss in MainWindow.softwares)
                {
                    if (!softwares.Contains(ss))
                    {
                        softwareShow.Add(ss);
                    }
                }
            }

        }

        private void enableFields(bool e)
        {
            id.IsEnabled = e;
            naziv.IsEnabled = e;
            opis.IsEnabled = e;
            prisustvoProjektora.IsEnabled = e;
            prisustvoTable.IsEnabled = e;
            smartTable.IsEnabled = e;
            os.IsEnabled = e;
            velicinaGrupe.IsEnabled = e;
            minDuzinaTermina.IsEnabled = e;
            brTermina.IsEnabled = e;
            allSofts.IsEnabled = e;
            allCourses.IsEnabled = e;
            o.IsEnabled = e;
            //Softver.IsEnabled = e;
        }

        private void lvAllS_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Software t = listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem) as Software;
                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", t);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private void lvAllC_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Course t = listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem) as Course;
                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormatC", t);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void lvAllS_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void DropList_DragEnter(object sender, DragEventArgs e)
        {

            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }


        private void DropListC_DragEnter(object sender, DragEventArgs e)
        {

            if (!e.Data.GetDataPresent("myFormatC") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DropList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Software s = e.Data.GetData("myFormat") as Software;
                softwares.Clear();
                softwares.Add(s);
                subject.Softver.Copy(s);
                softwareShow.Remove(s);

            }
        }

        private void DropListC_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormatC"))
            {
                Course s = e.Data.GetData("myFormatC") as Course;
                courses.Clear();
                courses.Add(s);
                subject.Smer.Copy(s);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                subjectsShow.RemoveAt(dgrMain.SelectedIndex);
                
            }
            save();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Boolean hasError = false;
            if (addNew || !currId.Equals(subject.Id))
            {
                foreach (Subject s in MainWindow.subjects)
                {
                    if (subject.Equals(s))
                    {
                        MessageBox.Show("ID allready exists. ID must be unique.");
                        hasError = true;
                    }
                }
            }
            if (subject.Id.Equals(""))
            {
                MessageBox.Show("ID must be set.");
                hasError = true;
            }
            if (subject.Opis.Equals("") || subject.Naziv.Equals("") || subject.PrisustvoProjektora.Equals("") || subject.PrisustvoTable.Equals("") ||
                subject.PrisustvoPametneTable.Equals("") || subject.Os.Equals("") || subject.Softver.Id.Equals("") || subject.VelicinaGrupe.Equals("")
                || subject.MinDuzinaTermina.Equals("") || subject.BrTermina.Equals("") || subject.Smer.Id.Equals(""))
            {
                MessageBox.Show("One or more values doesn't set. All values must be set.");
                hasError = true;
            }
            if (!subject.Os.Equals(subject.Softver.Os))
            {
                MessageBox.Show("Subject has " + subject.Os + " and Softver that you chose has " + subject.Softver.Os + " This two values must be the same");
                hasError = true;
            }
            if (!hasError)
            {
                if (addNew)
                {
                    Subject c = new Subject();
                    c.Copy(subject);
                    subjectsShow.Add(c);
                    //MainWindow.subjects.Add(c);
                    addNew = false;

                }
                else if (dgrMain.SelectedIndex != -1)
                {
                    Console.WriteLine("index: " + dgrMain.SelectedIndex+"  "+subjectsShow[dgrMain.SelectedIndex].Id);
                    int sIndex = dgrMain.SelectedIndex;
                    subjectsShow[dgrMain.SelectedIndex].Copy(subject);
                    Console.WriteLine("index: " + dgrMain.SelectedIndex + "  " + subjectsShow[dgrMain.SelectedIndex].Id);

                    Subject currSubject = subjectsShow[dgrMain.SelectedIndex];

                    bool b = false;
                    foreach (Classroom c in MainWindow.classrooms)
                    {
                        for (int i = c.Termini.Count - 1; i > -1; i--)
                    {
                        Console.WriteLine(i);
                        
                            if (currSubject.Naziv.Equals(c.Termini[i].Predmet))
                            {
                                if (!c.PrisustvoProjektora && currSubject.PrisustvoProjektora)
                                {
                                    b = true;
                                    break;

                                }
                                if (!c.PrisustvoTable && currSubject.PrisustvoTable)
                                {
                                    b = true;
                                    break;
                                }
                                if (!c.PrisustvoPametneTable && currSubject.PrisustvoPametneTable)
                                {
                                    b = true;
                                    break;

                                }
                                if (!c.Os.Equals(currSubject.Os))
                                {
                                    b = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (b)
                    {
                        if (MessageBox.Show("This will remove appointments on schedule, delete anyway?", "Delete classroom", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            //do no stuff
                            return;
                        }
                        else
                        {
                            //do yes stuff
                            foreach (Classroom c in MainWindow.classrooms)
                            {
                                for (int i = c.Termini.Count - 1; i > -1; i--)
                            {
                                Console.WriteLine(i);
                                
                                    if (currSubject.Naziv.Equals(c.Termini[i].Predmet))
                                    {
                                        if (!c.PrisustvoProjektora && currSubject.PrisustvoProjektora)
                                        {
                                            Console.WriteLine("Subject has projector, but classroom don't!");
                                            c.Termini.RemoveAt(i);
                                            break;

                                        }
                                        if (!c.PrisustvoTable && currSubject.PrisustvoTable)
                                        {
                                            Console.WriteLine("Subject has table, but classroom don't!");
                                            c.Termini.RemoveAt(i);
                                            break;


                                        }
                                        if (!c.PrisustvoPametneTable && currSubject.PrisustvoPametneTable)
                                        {
                                            Console.WriteLine("Subject has smart table, but classroom don't!");
                                            c.Termini.RemoveAt(i);
                                            break;

                                        }
                                        if (!c.Os.Equals(currSubject.Os))
                                        {
                                            Console.WriteLine("Subject has " + currSubject.Os + " but classroom has " + c.Os);

                                            c.Termini.RemoveAt(i);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    saveClass();
                    dgrMain.SelectedIndex = sIndex;
                }

                save();
            }
        }

        private void save()
        {
            MainWindow.subjects.Clear();
            MainWindow.subjectsShow.Clear();
            foreach(Subject s in subjectsShow)
            {
                MainWindow.subjectsShow.Add(s);
                MainWindow.subjects.Add(s);
            }
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/subjects.xml", "");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Subject>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/subjects.xml"))
            {
                List<Subject> list = new List<Subject>();
                foreach (Subject c in subjectsShow)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }
            dgrMain.UnselectAllCells();
            setSelected();
            enableFields(false);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            addNew = true;
            enableFields(true);
            dgrMain.UnselectAllCells();
            setSelected();
        }

        private void os_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("count: "+MainWindow.subjects.Count);
            if (proj.IsChecked == true && smart.IsChecked == true && table.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoTable == true && s.PrisustvoPametneTable)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }

                    }
                }
            }
            else if (proj.IsChecked == true && smart.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoPametneTable)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                }
            }
            else if (proj.IsChecked == true && table.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                }
            }
            else if (smart.IsChecked == true && table.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoTable == true && s.PrisustvoPametneTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                }
            }
            else if (smart.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoPametneTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                }
            }
            else if (proj.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoProjektora == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                }
            }
            else if (table.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (s.PrisustvoTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    subjectsShow.Add(s);
                                }

                            }
                            else
                            {
                                subjectsShow.Add(s);
                            }
                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                }
            }
            else
            {
                subjectsShow.Clear();
                foreach (Subject s in MainWindow.subjects)
                {
                    if (osistem.SelectedIndex > -1)
                    {
                        if (!osistem.SelectedItem.Equals(""))
                        {
                            if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                            {
                                subjectsShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                            {
                                subjectsShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                            {
                                subjectsShow.Add(s);
                            }

                        }
                        else
                        {
                            subjectsShow.Add(s);
                        }
                    }
                    else
                    {
                        subjectsShow.Add(s);
                    }

                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
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
        }

        private void o_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (o.SelectedIndex > -1)
            {
                softwareShow.Clear();
                foreach (Software ss in MainWindow.softwares)
                {
                    if (o.SelectedItem.Equals(ss.Os))
                    {
                        if(!softwares.Contains(ss)){
                            softwareShow.Add(ss);
                        }
                    }
                    if (o.SelectedItem.Equals("Cross platform") && ss.Os.Equals("Both"))
                    {
                        if (!softwares.Contains(ss))
                        {
                            softwareShow.Add(ss);
                        }
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
