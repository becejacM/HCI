﻿using HciProject2.Model;
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
    /// Interaction logic for ClassroomTable.xaml
    /// </summary>
    public partial class ClassroomTable : Window
    {
        Point startPoint = new Point();
        private Classroom classroom;
        private ObservableCollection<Software> softwares;
        public static ObservableCollection<Classroom> classroomShow { get; set; }
        public static ObservableCollection<Software> softwareShow { get; set; }

        Boolean addNew;
        String currId;
        private Classroom Classroom
        {
            get
            {
                return classroom;
            }
            set
            {
                classroom = value;
            }
        }
        public ClassroomTable()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

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

            classroom = new Classroom();
            softwares = new ObservableCollection<Software>();
            classroomShow = new ObservableCollection<Classroom>();
            softwareShow = new ObservableCollection<Software>();

            Id.DataContext = classroom;
            brRadnihMesta.DataContext = classroom;
            opis.DataContext = classroom;
            prisustvoProjektora.DataContext = classroom;
            prisustvoTable.DataContext = classroom;
            smartTable.DataContext = classroom;
            os.DataContext = classroom;
            
            foreach (Classroom s in MainWindow.classrooms)
            {
                classroomShow.Add(s);
            }
            foreach (Software s in MainWindow.softwares)
            {
                softwareShow.Add(s);
            }
            addNew = false;
            dgrMain.ItemsSource = classroomShow;
            allSofts.ItemsSource = softwareShow;
            DropList.ItemsSource = softwares;
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
                currId = classroom.Id;
            }
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                classroom.Copy(classroomShow[dgrMain.SelectedIndex]);
                softwares.Clear();
                foreach(Software s in classroomShow[dgrMain.SelectedIndex].Softver)
                {
                    softwares.Add(s);
                }
                softwareShow.Clear();
                foreach(Software ss in MainWindow.softwares)
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
                classroom.Id = "";
                classroom.Opis = "";
                classroom.BrRadnihMesta = 0;
                classroom.PrisustvoProjektora = false;
                classroom.PrisustvoTable = false;
                classroom.PrisustvoPametneTable = false;
                classroom.Os = "Windows";
                classroom.Softver = new List<Software>();
                classroom.Termini = new List<Appointment>();
                softwares.Clear();
                os.SelectedValue = "Windows";
                classroom.ImenaSoftvera = "";

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
            brRadnihMesta.IsEnabled = e;
            opis.IsEnabled = e;
            prisustvoProjektora.IsEnabled = e;
            prisustvoTable.IsEnabled = e;
            smartTable.IsEnabled = e;
            os.IsEnabled = e;
            allSofts.IsEnabled = e;
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

                if (listView.Name.Equals("DropList")){
                    return;
                }
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

        private void DropList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Software s = e.Data.GetData("myFormat") as Software;
                softwares.Add(s);

                classroom.Softver.Add(s);
                softwareShow.Remove(s);

            }
        }
        
        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Classroom c = classroomShow.ElementAt(dgrMain.SelectedIndex);
                MainWindow.classrooms.Remove(c);
                foreach (Subject cc in MainWindow.subjects)
                {
                    for (int i = c.Termini.Count - 1; i > -1; i--)
                    {
                        if (cc.Naziv.Equals(c.Termini[i].Predmet))
                        {
                            cc.BrTermina += 1;
                        }
                    }
                }
                save();
            }
            
        }
        

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if(!addNew && dgrMain.SelectedIndex == -1)
            {
                return;
            }
            Boolean hasError = false;
            if (addNew || !currId.Equals(classroom.Id))
            {
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (classroom.Equals(s))
                    {
                        MessageBox.Show("ID allready exists. ID must be unique.");
                        hasError = true;
                        return;
                    }
                }
            }
            if (classroom.Id.Equals(""))
            {
                MessageBox.Show("ID must be set.");
                hasError = true;
                return;
            }
            if (classroom.Opis.Equals("") || classroom.BrRadnihMesta.Equals("") || classroom.PrisustvoProjektora.Equals("") || classroom.PrisustvoTable.Equals("") ||
                classroom.PrisustvoPametneTable.Equals("") || classroom.Os.Equals("") || classroom.Softver.Count==0)
            {
                MessageBox.Show("One or more values doesn't set. All values must be set.");
                hasError = true;
                return;
            }
            foreach(Software s in classroom.Softver)
            {
                Console.WriteLine(s.Naziv);
                Console.WriteLine(classroom.Os);
                if (!s.Os.Equals(classroom.Os))
                {
                    if (!classroom.Os.Equals("Both") && !s.Os.Equals("Cross platform"))
                    {
                        softwares.Clear();
                        classroom.Softver.Clear();
                        softwareShow.Clear();
                        o.SelectedIndex = -1;
                        foreach (Software ss in MainWindow.softwares)
                        {
                            if (!softwares.Contains(ss))
                            {
                                softwareShow.Add(ss);
                            }
                        }
                        MessageBox.Show("Classroom has " + classroom.Os + " and Softver that you chose has " + s.Os + ". This two values must be the same");
                        hasError = true;
                        return;
                    }
                    
                }
            }
            int d;
            if (int.TryParse(brRadnihMesta.Text, out d))
            {
                //valid 
            }
            else
            {
                //invalid
                MessageBox.Show("Please enter a valid number for number of workers!");
                return;
            }

            if (!hasError)
            {
                
                if (addNew)
                {
                    Classroom c = new Classroom();
                    c.Copy(classroom);
                    String s = "";
                    foreach (Software sp in classroom.Softver)
                    {
                        s += sp.Naziv + " ";
                        Console.WriteLine("ddd " + s);
                    }
                    c.ImenaSoftvera = s;
                    Console.WriteLine(c.Termini.Count);
                    MainWindow.classrooms.Add(c);
                    addNew = false;

                }
                else if (dgrMain.SelectedIndex != -1)
                {
                    int sIndex = dgrMain.SelectedIndex;
                    String s = "";
                    foreach(Software sp in classroom.Softver)
                    {
                        s += sp.Naziv + " ";
                    }
                    classroom.ImenaSoftvera = s;
                    classroomShow[dgrMain.SelectedIndex].Copy(classroom);
                    Classroom currClassroom = classroomShow[dgrMain.SelectedIndex];

                    bool b = false;
                    for (int i = currClassroom.Termini.Count - 1; i > -1; i--)
                    {
                        Console.WriteLine(i);
                        foreach (Subject sub in MainWindow.subjects)
                        {
                            if (sub.Naziv.Equals(currClassroom.Termini[i].Predmet))
                            {
                                if (!currClassroom.PrisustvoProjektora && sub.PrisustvoProjektora)
                                {
                                    b = true;
                                    break;

                                }
                                if (!currClassroom.PrisustvoTable && sub.PrisustvoTable)
                                {
                                    b = true;
                                    break;
                                }
                                if (!currClassroom.PrisustvoPametneTable && sub.PrisustvoPametneTable)
                                {
                                    b = true;
                                    break;

                                }
                                if (!currClassroom.Os.Equals("Both") && !sub.Os.Equals("Both"))
                                {
                                    if (!currClassroom.Os.Equals(sub.Os))
                                    {
                                        b = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    if (b)
                    {
                        if (MessageBox.Show("This action will remove appointments on schedule, delete anyway?", "Delete classroom", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        {
                            //do no stuff
                            return;
                        }
                        else
                        {
                            //do yes stuff
                            for (int i = currClassroom.Termini.Count - 1; i > -1; i--)
                            {
                                Console.WriteLine(i);
                                foreach (Subject sub in MainWindow.subjects)
                                {
                                    if (sub.Naziv.Equals(currClassroom.Termini[i].Predmet))
                                    {
                                        if (!currClassroom.PrisustvoProjektora && sub.PrisustvoProjektora)
                                        {
                                            Console.WriteLine("Subject has projector, but classroom don't!");
                                            currClassroom.Termini.RemoveAt(i);
                                            sub.BrTermina += 1;
                                            break;

                                        }
                                        if (!currClassroom.PrisustvoTable && sub.PrisustvoTable)
                                        {
                                            Console.WriteLine("Subject has table, but classroom don't!");
                                            currClassroom.Termini.RemoveAt(i);
                                            sub.BrTermina += 1;
                                            break;


                                        }
                                        if (!currClassroom.PrisustvoPametneTable && sub.PrisustvoPametneTable)
                                        {
                                            Console.WriteLine("Subject has smart table, but classroom don't!");
                                            currClassroom.Termini.RemoveAt(i);
                                            sub.BrTermina += 1;
                                            break;

                                        }
                                        if (!currClassroom.Os.Equals("Both") && !sub.Os.Equals("Both"))
                                        {
                                            if (!currClassroom.Os.Equals(sub.Os))
                                            {
                                                Console.WriteLine("Subject has " + sub.Os + " but classroom has " + currClassroom.Os);

                                                currClassroom.Termini.RemoveAt(i);
                                                sub.BrTermina += 1;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    dgrMain.SelectedIndex = sIndex;
                }
                
                save();
                
                
            }
        }

        private void save()
        {
            //MainWindow.classrooms.Clear();
            MainWindow.classroomsShow.Clear();
            classroomShow.Clear();
            foreach (Classroom s in MainWindow.classrooms)
            {
                classroomShow.Add(s);
                MainWindow.classroomsShow.Add(s);
            }
            dgrMain.UnselectAllCells();
            setSelected();
            enableFields(false);
            object o = new object();
            RoutedEventArgs r = new RoutedEventArgs();
            os_SelectionChanged(o, r);
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
        
        private void os_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("count: " + MainWindow.subjects.Count);
            if (proj.IsChecked == true && smart.IsChecked == true && table.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoTable == true && s.PrisustvoPametneTable)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }

                    }
                }
            }
            else if (proj.IsChecked == true && smart.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoPametneTable)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                }
            }
            else if (proj.IsChecked == true && table.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                }
            }
            else if (smart.IsChecked == true && table.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoTable == true && s.PrisustvoPametneTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                }
            }
            else if (smart.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoPametneTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                }
            }
            else if (proj.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoProjektora == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                }
            }
            else if (table.IsChecked == true)
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (s.PrisustvoTable == true)
                    {
                        if (osistem.SelectedIndex > -1)
                        {
                            if (!osistem.SelectedItem.Equals(""))
                            {
                                if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                                {
                                    classroomShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                }
            }
            else
            {
                classroomShow.Clear();
                foreach (Classroom s in MainWindow.classrooms)
                {
                    if (osistem.SelectedIndex > -1)
                    {
                        if (!osistem.SelectedItem.Equals(""))
                        {
                            if (osistem.SelectedItem.Equals("Windows") && (s.Os.Equals("Windows") || s.Os.Equals("Both")))
                            {
                                classroomShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Linux") && (s.Os.Equals("Linux") || s.Os.Equals("Both")))
                            {
                                classroomShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                            {
                                classroomShow.Add(s);
                            }

                        }
                        else
                        {
                            classroomShow.Add(s);
                        }
                    }
                    else
                    {
                        classroomShow.Add(s);
                    }

                }
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            foreach(Software s in softwares)
            {
                softwareShow.Add(s);
            }
            o.SelectedIndex = -1;
            softwares.Clear();
            classroom.Softver.Clear();
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
                        if (!softwares.Contains(ss))
                        {
                            softwareShow.Add(ss);
                        }
                    }
                    /*if(o.SelectedItem.Equals("Cross platform") || ss.Os.Equals("Cross platform"))
                    {
                        if (!softwares.Contains(ss) && !softwareShow.Contains(ss))
                        {
                            softwareShow.Add(ss);
                        }
                    }*/
                }
            }
            
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (search.Text.Equals(":All"))
            {
                osistem.SelectedIndex = -1;
                table.IsChecked = false;
                smart.IsChecked = false;
                proj.IsChecked = false;
                classroomShow.Clear();
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    classroomShow.Add(ss);
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

            osistem.SelectedIndex = -1;
            table.IsChecked = false;
            smart.IsChecked = false;
            proj.IsChecked = false;
            object o = new object();
            RoutedEventArgs r = new RoutedEventArgs();
            os_SelectionChanged(o, r);
            string prvi = lines[0].Substring(1);
            string drugi = lines[1];
            classroomShow.Clear();
            if (prvi.Equals("Id"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    if (ss.Id.Contains(drugi))
                    {
                        classroomShow.Add(ss);
                    }
                }
            }
            else if(prvi.Equals("NumberOfWorkers"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    if (ss.BrRadnihMesta==Int32.Parse(drugi))
                    {
                        classroomShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Projector"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    if (ss.PrisustvoProjektora.ToString().Equals(drugi))
                    {
                        classroomShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Table"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    if (ss.PrisustvoTable.ToString().Equals(drugi))
                    {
                        classroomShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("SmartTable"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    if (ss.PrisustvoPametneTable.ToString().Equals(drugi))
                    {
                        classroomShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Os"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    if (ss.Os.Equals(drugi))
                    {
                        classroomShow.Add(ss);
                    }
                }
            }
            else if (prvi.Equals("Software"))
            {
                foreach (Classroom ss in MainWindow.classrooms)
                {
                    foreach(Software s in ss.Softver)
                    {
                        if (s.Naziv.Equals(drugi))
                        {
                            classroomShow.Add(ss);
                            break;
                        }
                    }
                    
                }
            }

        }
    }
}
