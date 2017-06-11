using HciProject2.Dialogs;
using HciProject2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;

namespace HciProject2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Software> softwares { get; set; }
        public static ObservableCollection<Classroom> classrooms { get; set; }
        public static ObservableCollection<Course> courses { get; set; }
        public static ObservableCollection<Subject> subjects { get; set; }
        public static ObservableCollection<Subject> subjectsShow { get; set; }
        public static ObservableCollection<Classroom> classroomsShow { get; set; }

        Point startPoint = new Point();
        Classroom currClassroom;
        String currDay;
        String currMode;
        int currIndexOfDay;
        Dictionary<int, Classroom> currClassromForDay;
        bool dropFromHelpGrid;
        bool gridHelper;
        List<Appointment> helpLista;
        int brisiOvaj;
        bool dropFromSubjects;
        int selClass;

        public MainWindow()
        {
            InitializeComponent();

            //menu.Visibility = Visibility.Collapsed;
            classrooms = new ObservableCollection<Classroom>();
            softwares = new ObservableCollection<Software>();
            courses = new ObservableCollection<Course>();
            subjects = new ObservableCollection<Subject>();
            subjectsShow = new ObservableCollection<Subject>();
            classroomsShow = new ObservableCollection<Classroom>();

            currClassroom = new Classroom();
            currDay = "";
            currMode = "";
            currIndexOfDay = 0;
            currClassromForDay = new Dictionary<int, Classroom>();
            dropFromHelpGrid = false;
            gridHelper = false;
            helpLista = new List<Appointment>();
            brisiOvaj = -1;
            allSubjects.ItemsSource = subjectsShow;
            dropFromSubjects = false;
            selClass = -1;

            br.Items.Add("1");
            br.Items.Add("2");
            br.Items.Add("3");
            br.Items.Add("4");
            br.Items.Add("5");
            br.Items.Add("6");


            mode.Items.Add("one day");
            mode.Items.Add("whole week");

            day.Items.Add("Monday");
            day.Items.Add("Tuesday");
            day.Items.Add("Wednesday");
            day.Items.Add("Thursday");
            day.Items.Add("Friday");
            day.Items.Add("Saturday");

            classroom.ItemsSource = classroomsShow;

            readFromFile();


            day.Visibility = Visibility.Collapsed;
            daylabel.Visibility = Visibility.Collapsed;

            classroom.Visibility = Visibility.Collapsed;
            classlabel.Visibility = Visibility.Collapsed;


            os.Items.Add("");
            os.Items.Add("Windows");
            os.Items.Add("Linux");
            os.Items.Add("Both");


        }

        public void readFromFile()
        {
            //read softwares
            XmlSerializer serializer0 = new XmlSerializer(typeof(List<Software>));


            using (FileStream stream = File.OpenRead(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/softwares.xml"))
            {
                List<Software> dezerializedList = (List<Software>)serializer0.Deserialize(stream);
                foreach (var item in dezerializedList)
                    softwares.Add(item);
            }
            //read courses

            XmlSerializer serializer = new XmlSerializer(typeof(List<Course>));
            

            using (FileStream stream = File.OpenRead(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/courses.xml"))
            {
                List<Course> dezerializedList = (List<Course>)serializer.Deserialize(stream);
                foreach (var item in dezerializedList)
                    courses.Add(item);
            }
            
            //read classrooms
           XmlSerializer serializer1 = new XmlSerializer(typeof(List<Classroom>));

            using (FileStream stream = File.OpenRead(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml"))
            {
                List<Classroom> dezerializedList = (List<Classroom>)serializer1.Deserialize(stream);

                bool b = true;
                foreach (var item in dezerializedList)
                {
                    if (b)
                    {
                        currClassroom.Copy(item);
                        //currClassroom = item;
                    }
                    classrooms.Add(item);
                    classroomsShow.Add(item);
                    //classroom.Items.Add(item.Id);
                    
                }
                
            }
            //read subjects
           XmlSerializer serializer2 = new XmlSerializer(typeof(List<Subject>));


            using (FileStream stream = File.OpenRead(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/subjects.xml"))
            {
                List<Subject> dezerializedList = (List<Subject>)serializer2.Deserialize(stream);
                foreach (var item in dezerializedList)
                {
                    subjects.Add(item);
                    subjectsShow.Add(item);
                }
                    
            }
            br.SelectedValue = "1";
            /*foreach (ComboBoxItem cbi in mode.Items)
            {
                if (cbi.Content as String == "whole week")
                {
                    mode.SelectedItem = cbi;
                    break;
                }
            }
            day.SelectedItem = "Monday";
            HandleDay();
            classroom.SelectedItem = currClassroom.Id;
            Handle();
            ucitaj();*/

            /*if (classroomsShow.Count > 0)
            {
                object o = new object();
                EventArgs e = new EventArgs();
                mode.SelectedIndex = 0;
                day.SelectedIndex = 0;
                mode_DropDownClosed(o, e);
                day_DropDownClosed(o, e);
            }
            */
        }

        public void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {//view all classrooms
            softwares.Clear();
            courses.Clear();
            classrooms.Clear();
            subjects.Clear();
            subjectsShow.Clear();
            classroomsShow.Clear();
            readFromFile();
            Window w = new ClassroomTable();
            w.ShowDialog();
            this.Close();

        }
        
        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {//view all subject
            softwares.Clear();
            courses.Clear();
            classrooms.Clear();
            subjects.Clear();
            subjectsShow.Clear();
            classroomsShow.Clear();
            readFromFile();
            Window w = new SubjectTable();
            w.ShowDialog();
            this.Close();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {//view all smer
            softwares.Clear();
            courses.Clear();
            classrooms.Clear();
            subjects.Clear();
            subjectsShow.Clear();
            classroomsShow.Clear();
            readFromFile();
            Window w = new CourseTable();
            w.ShowDialog();
            this.Close();

        }
        
        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {//view all softwares
            softwares.Clear();
            courses.Clear();
            classrooms.Clear();
            subjects.Clear();
            subjectsShow.Clear();
            classroomsShow.Clear();
            readFromFile();
            Window w = new SoftwareTable();
            w.ShowDialog();
            this.Close();

        }

        private void allSubjects_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void allSubjects_MouseMove(object sender, MouseEventArgs e)
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
                    dropFromSubjects = true;
                    // Find the data behind the ListViewItem
                    Subject t = listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem) as Subject;
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

        private void Grid_Drop(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = e.Source as TextBlock;

            //ako se dropuje na liniju
            if (textBlock == null)
            {
                return;
            }
            /*MessageBox.Show("drop item into grid row:{0} row:"+
                Grid.GetRow(textBlock).ToString());
            MessageBox.Show("drop item into grid row:{0} row:" +
                Grid.GetColumn(textBlock).ToString());*/

            DataObject item = (((DragEventArgs)e).Data) as DataObject;
            int broj = -1;

            //zastita da se ne preklapaju termini
            /*for(int i = 0; i < broj * 4; i++)
            {
                var ch = grid.Children.Cast<UIElement>()
                    .Where(x => Grid.GetRow(x) == Grid.GetRow(textBlock)+i && Grid.GetColumn(x) == Grid.GetColumn(textBlock))
                    .FirstOrDefault(); ;
                TextBlock tt = (TextBlock)ch;
                Console.WriteLine("reeeed: " + Grid.GetRow(tt) + "   kolona: " + Grid.GetColumn(tt));
                if (tt.Tag!=null)
                {
                    MessageBox.Show("Your appointment can't fit.");
                    return;
                }
            }*/
            
            if (gridHelper)
            {
                Appointment listItem = item.GetData("myFormatA") as Appointment;
                gridHelper = false;
                int kolona;
                Classroom old = new Classroom();
                if (currMode.Equals("one day"))
                {
                    kolona = currIndexOfDay;
                    Console.WriteLine("1 dan prebacujem u: " + kolona);
                    currClassroom.Copy(currClassromForDay[Int32.Parse(Grid.GetColumn(textBlock).ToString())]);
                    old.Copy(currClassromForDay[brisiOvaj]);
                }
                else
                {
                    kolona = Int32.Parse(Grid.GetColumn(textBlock).ToString());
                }
                int red = Int32.Parse(Grid.GetRow(textBlock).ToString());
                broj = listItem.Velicina;

                Console.WriteLine("iispisssss: " + grid.Children.Count+"  "+Grid.GetRow(textBlock)+"   "+Grid.GetColumn(textBlock));
                //zastita da se ne preklapaju termini
                for (int i = 0; i < broj * 4; i++)
                {
                    var ch = grid.Children.Cast<UIElement>()
                        .Where(x => Grid.GetRow(x) == Grid.GetRow(textBlock) + i && Grid.GetColumn(x) == Grid.GetColumn(textBlock))
                        .FirstOrDefault(); 
                    TextBlock tt = (TextBlock)ch;
                    //if (tt != null)
                    //{
                    
                        Console.WriteLine("reeeed: " + Grid.GetRow(tt) + "   kolona: " + Grid.GetColumn(tt) + " text: " + tt.Text);
                    if (tt.Tag != null)
                    {
                        Appointment aaa = (Appointment)tt.Tag;
                        if (!aaa.Id.Equals(listItem.Id))
                        {
                            MessageBox.Show("Your appointment can't fit.");
                            return;
                        }
                        
                   }
                    //}
                }
                textBlock.Text = listItem.Predmet;
                textBlock.Background = new SolidColorBrush(Colors.Red);
                Console.WriteLine("mode grid listitem "+listItem.Red+" "+listItem.Kolona);
                if (currMode.Equals("one day"))
                {
                    Console.WriteLine("brisem sa gridaaaaaaaaaaaaaaaa: " + listItem.Red + "  " + brisiOvaj);
                    var ch = grid.Children.Cast<UIElement>()
                    .Where(x => Grid.GetRow(x) == listItem.Red && Grid.GetColumn(x) == brisiOvaj)
                    .FirstOrDefault(); ;
                    TextBlock tt = (TextBlock)ch;
                    grid.Children.Remove(tt);
                }
                else
                {
                    var ch = grid.Children.Cast<UIElement>()
                    .Where(x => Grid.GetRow(x) == listItem.Red && Grid.GetColumn(x) == listItem.Kolona)
                    .FirstOrDefault(); ;
                    TextBlock tt = (TextBlock)ch;
                    grid.Children.Remove(tt);
                }
                
                //listItem.Red = red;
                //listItem.Kolona = kolona;
                //listItem.Status = "ok";
                

                Console.WriteLine("cccccccc"+currClassroom.Id);
                if (currMode.Equals("one day"))
                {
                    foreach (Classroom c in classrooms)
                    {
                        if (c.Id.Equals(old.Id))
                        {
                            //c.termini.Add(listItem);
                            foreach (Appointment a in c.Termini)
                            {
                                if (a.Id == listItem.Id)
                                {
                                    Console.WriteLine("--------red " + red + " kolona " + kolona);
                                    a.Red = red;
                                    a.Kolona = kolona;
                                    if (!currClassroom.Id.Equals(old.Id))
                                    {
                                        Appointment n = new Appointment();
                                        n.Copy(a);
                                        //String last = c.Termini.Last().Id.Substring()
                                        Random rnd = new Random();
                                        int num = rnd.Next(1, 1000000) + currClassroom.Termini.Count + 1;
                                        String id = currClassroom.Id+ num;
                                        n.Id = id;
                                        n.Status = "ok";
                                        n.Velicina = a.Velicina;
                                        Console.WriteLine("status od n: " + n.Status);
                                        currClassroom.Termini.Add(n);
                                        a.Status = "no";
                                    }
                                    else
                                    {
                                        a.Status = "ok";
                                    }
                                    //c.Termini.Remove(a);
                                    Console.WriteLine("status od a: " + a.Status);
                                    break;
                                }
                            }
                        }
                    }
                }else
                {
                    foreach (Classroom c in classrooms)
                    {
                        if (c.Id.Equals(currClassroom.Id))
                        {
                            //c.termini.Add(listItem);
                            foreach (Appointment a in c.Termini)
                            {
                                //if (a.Red == listItem.Red && a.Kolona == listItem.Kolona)
                                if(a.Id==listItem.Id)
                                {
                                    Console.WriteLine("--------red " + red + " kolona " + kolona);
                                    a.Red = red;
                                    a.Kolona = kolona;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if (dropFromHelpGrid)
            {
                Appointment listItem = item.GetData("myFormatA") as Appointment;
                dropFromHelpGrid = false;

                broj = listItem.Velicina;
                //zastita da se ne preklapaju termini
                for (int i = 0; i < broj * 4; i++)
                {
                    var ch2 = grid.Children.Cast<UIElement>()
                        .Where(x => Grid.GetRow(x) == Grid.GetRow(textBlock) + i && Grid.GetColumn(x) == Grid.GetColumn(textBlock))
                        .FirstOrDefault(); ;
                    TextBlock tt2 = (TextBlock)ch2;
                    Console.WriteLine("reeeed: " + Grid.GetRow(tt2) + "   kolona: " + Grid.GetColumn(tt2));
                    if (tt2.Tag != null)
                    {
                        MessageBox.Show("Your appointment can't fit.");
                        return;
                    }
                }
                int kolona;
                if (currMode.Equals("one day"))
                {
                    kolona = currIndexOfDay;
                    MessageBox.Show(Grid.GetColumn(textBlock).ToString());
                    currClassroom.Copy(currClassromForDay[Int32.Parse(Grid.GetColumn(textBlock).ToString())]);
                    //old.Copy(currClassromForDay[brisiOvaj]);
                }
                else
                {
                    kolona = Int32.Parse(Grid.GetColumn(textBlock).ToString());
                }
                int red = Int32.Parse(Grid.GetRow(textBlock).ToString());
                foreach(Subject s in subjects)
                {
                    if (s.Naziv.Equals(listItem.Predmet))
                    {
                        if (!currClassroom.PrisustvoProjektora && s.PrisustvoProjektora)
                        {
                            MessageBox.Show("Subject has projector, but classroom don't!");
                            return;
                        }
                        if (!currClassroom.PrisustvoTable && s.PrisustvoTable)
                        {
                            MessageBox.Show("Subject has table, but classroom don't!");
                            return;
                        }
                        if (!currClassroom.PrisustvoPametneTable && s.PrisustvoPametneTable)
                        {
                            MessageBox.Show("Subject has smart table, but classroom don't!");
                            return;
                        }
                        if (!currClassroom.Os.Equals(s.Os))
                        {
                            MessageBox.Show("Subject has " + s.Os + " but classroom has " + s.Os);
                            return;
                        }
                    }
                }
                
                textBlock.Text = listItem.Predmet;
                textBlock.Background = new SolidColorBrush(Colors.Red);
                Console.WriteLine("mode help");
                Console.WriteLine(listItem.Velicina + " mmmmmmmmmmm");

                var ch = helpGrid.Children.Cast<UIElement>()
                    .Where(x => Grid.GetRow(x) == listItem.Red && Grid.GetColumn(x) == listItem.Kolona)
                    .FirstOrDefault(); ;
                TextBlock tt = (TextBlock)ch;
                helpGrid.Children.Remove(tt);
                helpLista.Remove(listItem);
                //Classroom old = new Classroom();
                
                listItem.Red = red;
                listItem.Kolona = kolona;
                //listItem.Status = "ok";
                foreach (Subject s in subjects)
                {
                    if (s.Naziv.Equals(listItem.Predmet))
                    {
                        s.BrTermina -= 1;
                    }
                }
                saveSub();
                if (currMode.Equals("one day"))
                {
                    bool find = false;
                    foreach(Classroom c in classrooms)
                    {
                        if (c.Id.Equals(currClassroom.Id))
                        {
                            foreach (Appointment a in c.Termini)
                            {
                                if (a.Id == listItem.Id)
                                {
                                    Console.WriteLine(currClassroom.Id);
                                    Console.WriteLine("--------red " + red + " kolona " + kolona);
                                    a.Red = red;
                                    a.Kolona = kolona;
                                    //a.Status = "ok";
                                    
                                    a.Status = "ok";
                                    find = true;
                                    //currClassroom.Termini.Remove(a);
                                    break;
                                    //a.Status = "no";
                                }
                                

                            }
                            //c.Termini.Add(listItem);
                        }
                    }
                    if (!find)
                    {
                        Random rnd = new Random();
                        int num = rnd.Next(1, 1000000)+currClassroom.Termini.Count+1;
                        String id =currClassroom.Id+ num ;
                        Appointment n = new Appointment();
                        n.Copy(listItem);
                        n.Red = red;
                        n.Kolona = kolona;
                        n.Status = "ok";
                        n.Id = id;
                        Console.WriteLine("Velicinaaaa: " + n.Velicina);
                        currClassroom.Termini.Add(n);

                    }
                }
                else
                {
                    bool find = false;
                    listItem.Status = "ok";
                    foreach (Classroom c in classrooms)
                    {
                        if (c.Id.Equals(currClassroom.Id))
                        {

                            foreach (Appointment a in c.Termini)
                            {
                                if (a.Id == listItem.Id)
                                {
                                    a.Status = "ok";
                                    a.Red = listItem.Red;
                                    a.Kolona = listItem.Kolona;
                                    find = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (!find)
                    {
                        Random rnd = new Random();
                        int num = rnd.Next(1, 1000000) + currClassroom.Termini.Count + 1;
                        String id =currClassroom.Id+ num;
                        Appointment n = new Appointment();
                        n.Copy(listItem);
                        n.Red = red;
                        n.Kolona = kolona;
                        n.Status = "ok";
                        n.Id = id;
                        currClassroom.Termini.Add(n);

                    }
                }
            }
            else
            {
                Subject listItem = item.GetData("myFormatC") as Subject;
                broj = Int32.Parse(br.SelectedValue.ToString());
                Console.WriteLine(broj);
                //zastita da se ne preklapaju termini
                for (int i = 0; i < broj * 4; i++)
                {
                    var ch = grid.Children.Cast<UIElement>()
                        .Where(x => Grid.GetRow(x) == Grid.GetRow(textBlock) + i && Grid.GetColumn(x) == Grid.GetColumn(textBlock))
                        .FirstOrDefault(); ;
                    TextBlock tt = (TextBlock)ch;
                    Console.WriteLine("reeeed: " + Grid.GetRow(tt) + "   kolona: " + Grid.GetColumn(tt));
                    if (tt.Tag != null)
                    {
                        MessageBox.Show("Your appointment can't fit.");
                        return;
                    }
                }
                Console.WriteLine("min: " + listItem.MinDuzinaTermina + " odabrano: " + broj);
                if (listItem.MinDuzinaTermina > broj)
                {
                    MessageBox.Show("Yor appointment must be at least " + listItem.MinDuzinaTermina + " size.");
                    return;
                }
                if (listItem.BrTermina == 0)
                {
                    MessageBox.Show("All appointments are at schedule.");
                    return;
                }
                
                

                int kolona;
                if (currMode.Equals("one day"))
                {
                    kolona = currIndexOfDay;
                    MessageBox.Show(Grid.GetColumn(textBlock).ToString());
                    currClassroom.Copy(currClassromForDay[Int32.Parse(Grid.GetColumn(textBlock).ToString())]);
                }
                else
                {
                    kolona = Int32.Parse(Grid.GetColumn(textBlock).ToString());
                }
                int red = Int32.Parse(Grid.GetRow(textBlock).ToString());

                if(!currClassroom.PrisustvoProjektora && listItem.PrisustvoProjektora)
                {
                    MessageBox.Show("Subject has projector, but classroom don't!");
                    return;
                }
                if (!currClassroom.PrisustvoTable && listItem.PrisustvoTable)
                {
                    MessageBox.Show("Subject has table, but classroom don't!");
                    return;
                }
                if (!currClassroom.PrisustvoPametneTable && listItem.PrisustvoPametneTable)
                {
                    MessageBox.Show("Subject has smart table, but classroom don't!");
                    return;
                }
                if (!currClassroom.Os.Equals(listItem.Os))
                {
                    MessageBox.Show("Subject has "+listItem.Os+" but classroom has "+ currClassroom.Os);
                    return;
                }
                bool t = false;
                foreach(Software s in currClassroom.Softver)
                {
                    if (s.Id.Equals(listItem.Softver.Id))
                    {
                        t = true;
                    }
                }
                if (!t)
                {
                    MessageBox.Show("Classroom don't have "+listItem.Softver.Id + "software");
                    return;
                }
                textBlock.Text = listItem.Naziv;
                textBlock.Background = new SolidColorBrush(Colors.Red);
                Console.WriteLine("mode sub");
                foreach (Subject s in subjects)
                {
                    if (s.Id.Equals(listItem.Id))
                    {
                        s.BrTermina -= 1;
                    }
                }
                saveSub();
                Random rnd = new Random();
                int num = rnd.Next(1, 1000000) + currClassroom.Termini.Count + 1;
                String id =currClassroom.Id+ num;
                Appointment a = new Appointment(id,kolona, red, listItem.Naziv, "ok",broj);
                foreach (Classroom c in classrooms)
                {
                    if (c.Id.Equals(currClassroom.Id))
                    {
                        c.termini.Add(a);
                        Console.WriteLine("cuvam a: "+a.Red+"  "+a.Kolona);
                    }
                }
            }


            //this.grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4) });

            //textBlock.Text = listItem.Naziv;
            //textBlock.Background = new SolidColorBrush(Colors.Red);
            //textBlock.TextAlignment = TextAlignment.Center;
            //textBlock.TextWrapping = TextWrapping.Wrap;
            //Grid.SetRow(txt, Grid.GetRow(textBlock));
            //Grid.SetRowSpan(textBlock, 4);

            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml", "");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Classroom>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml"))
            {
                List<Classroom> list = new List<Classroom>();
                foreach (Classroom c in classrooms)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }

            if (currMode.Equals("one day"))
            {
                ucitajZaDan();
            }
            else
            {
                ucitaj();
            }
        }
        
        private void classroom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }
        private bool handle = true;
        private void classroom_DropDownClosed(object sender, EventArgs e)
        {
            if (classroom.SelectedIndex > -1)
            {
                if (handle) Handle();
                handle = true;
            }
        }

        private void Handle()
        {
            if (classroom.SelectedIndex > -1)
            {
                Classroom sel = (Classroom)classroom.SelectedItem;
                foreach (Classroom c in classrooms)
                {
                    if (c.Id.Equals(sel.Id))
                    {
                        Console.WriteLine("select: " + sel.Id);
                        currClassroom.Copy(c);
                        selClass = classroom.SelectedIndex;
                    }
                }
                ucitaj();
            }
            
        }

        private void ucitaj()
        {
            Console.WriteLine("grid pre svegaaaaa:" + grid.Children.Count);

            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            helpGrid.Children.Clear();
            helpGrid.RowDefinitions.Clear();
            helpGrid.ColumnDefinitions.Clear();
            for (int i = 0; i <= 65; i++)
            {
                RowDefinition gridRow1 = new RowDefinition();
                grid.RowDefinitions.Add(gridRow1);
            }

            for (int j = 0; j <= 6; j++)
            {
                ColumnDefinition gridCol1 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridCol1);
                
            }
            bool f = false;
            foreach (Classroom c in classroomsShow)
            {
                if (c.Id.Equals(currClassroom.Id))
                {
                    f = true;
                }
            }
            if (classroomsShow.Count >0 && !f)
            {
                classroom.SelectedIndex = 0;
                Handle();
            }
            if (classroomsShow.Count == 0)
            {
                
                grid.Children.Clear();
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();
                RowDefinition gridRow1 = new RowDefinition();
                grid.RowDefinitions.Add(gridRow1);

                ColumnDefinition gridCol1 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridCol1);

                TextBlock textBlockb = new TextBlock();
                textBlockb.Text = "Nothing to show";
                textBlockb.Background = new SolidColorBrush(Colors.Red);

                textBlockb.TextAlignment = TextAlignment.Center;
                textBlockb.TextWrapping = TextWrapping.Wrap;
                Grid.SetRow(textBlockb, 0);
                Grid.SetColumn(textBlockb, 0);
                grid.Children.Add(textBlockb);
                return;
            }
            int b = 0;
            foreach(Classroom c in classroomsShow)
            {
                if (c.Id.Equals(currClassroom.Id))
                {
                    selClass = b;
                }
                b += 1;
            }
            classroom.SelectedIndex = selClass;
            ucitajTextblokove();
            Console.WriteLine(currClassroom.Id + " ccccccccccccccccccccccccccccccc");
            foreach (Appointment a in currClassroom.Termini)
            {
                if (a.Status.Equals("ok"))
                {
                    var childs = grid.Children.Cast<UIElement>()
                        .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == a.Kolona)
                        .FirstOrDefault(); ;
                    TextBlock t = (TextBlock)childs;
                    grid.Children.Remove(t);
                    TextBlock textBlockb = new TextBlock();
                    textBlockb.Text = a.Predmet;
                    textBlockb.Background = new SolidColorBrush(Colors.Red);


                    textBlockb.TextAlignment = TextAlignment.Center;
                    textBlockb.TextWrapping = TextWrapping.Wrap;
                    Grid.SetRow(textBlockb, a.Red);
                    Grid.SetColumn(textBlockb, a.Kolona);
                    Grid.SetRowSpan(textBlockb, 4*a.Velicina);
                    Console.WriteLine("zauzimam: " + Grid.GetRow(textBlockb) + "  " + Grid.GetColumn(textBlockb));
                    textBlockb.Tag = a;
                    textBlockb.AllowDrop = true;
                    textBlockb.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                    textBlockb.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);

                    
                    ContextMenu c = new ContextMenu();
                    MenuItem m = new MenuItem();
                    m.Header = "delete";
                    m.Click += delegate { closeTabItem(textBlockb); };
                    c.Items.Add(m);
                    textBlockb.ContextMenu = c;
                    grid.Children.Add(textBlockb);
                }
            }

            ucitajSate();
            ucitajDane();
        }

        private void closeTabItem(TextBlock textBlockb)
        {
            Appointment a = (Appointment)textBlockb.Tag;

            if (currMode.Equals("one day"))
            {
                Console.WriteLine("briseeem: " + a.Red + "  " + brisiOvaj);
                //kolona = currIndexOfDay;
                //Console.WriteLine("1 dan prebacujem u: " + kolona);
                //currClassroom.Copy(currClassromForDay[Int32.Parse(Grid.GetColumn(textBlock).ToString())]);
                var childs = grid.Children.Cast<UIElement>()
                    .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == brisiOvaj)
                    .FirstOrDefault(); ;
                TextBlock t = (TextBlock)childs;
                grid.Children.Remove(t);
            }
            else
            {
                var childs = grid.Children.Cast<UIElement>()
                            .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == a.Kolona)
                            .FirstOrDefault(); ;
                TextBlock t = (TextBlock)childs;
                grid.Children.Remove(t);
            }

            if (currMode.Equals("one day"))
            {
                currClassroom.Copy(currClassromForDay[Grid.GetColumn(textBlockb)]);
            }

            foreach (Classroom c in classrooms) {
                if (c.Id.Equals(currClassroom.Id))
                {
                    foreach (Appointment aa in c.Termini)
                    {
                        if (aa.Id.Equals(a.Id))
                        {
                            c.Termini.Remove(aa);
                            break;
                        }
                    }
                }
                
            }
            if (currMode.Equals("one day"))
            {
                ucitajZaDan();
            }
            else
            {
                ucitaj();
            }
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml", "");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Classroom>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml"))
            {
                List<Classroom> list = new List<Classroom>();
                foreach (Classroom c in classrooms)
                {
                    list.Add(c);
                }
                serializer.Serialize(stream, list);
            }
        }

        private void ucitajZaDan()
        {
            Console.WriteLine("grid pre svegaaaaa:" + grid.Children.Count);
            //MessageBox.Show(currClassroom.Id);
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();
            helpGrid.Children.Clear();
            helpGrid.RowDefinitions.Clear();
            helpGrid.ColumnDefinitions.Clear();
            for (int i = 0; i <= 65; i++)
            {
                RowDefinition gridRow1 = new RowDefinition();
                grid.RowDefinitions.Add(gridRow1);
            }
            for (int j = 0; j <= classroomsShow.Count; j++)
            {
                Console.WriteLine("iiiiiii: " + j);
                ColumnDefinition gridCol1 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridCol1);

            }
            Console.WriteLine("fsdsdcf " + currIndexOfDay);

            int num = 1;
            if (classroomsShow.Count == 0)
            {
                grid.Children.Clear();
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();
                RowDefinition gridRow1 = new RowDefinition();
                grid.RowDefinitions.Add(gridRow1);

                ColumnDefinition gridCol1 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(gridCol1);

                TextBlock textBlockb = new TextBlock();
                textBlockb.Text = "Nothing to show";
                textBlockb.Background = new SolidColorBrush(Colors.Red);

                textBlockb.TextAlignment = TextAlignment.Center;
                textBlockb.TextWrapping = TextWrapping.Wrap;
                Grid.SetRow(textBlockb, 0);
                Grid.SetColumn(textBlockb, 0);
                grid.Children.Add(textBlockb);
                return;
            }
            ucitajTextblokove();

            foreach (Classroom c in classroomsShow)
            {
                Console.WriteLine("num: " + num);

                foreach (Appointment a in c.Termini)
                {
                    if (a.Kolona == currIndexOfDay && a.Status.Equals("ok"))
                    {
                        var childs = grid.Children.Cast<UIElement>()
                        .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == num)
                        .FirstOrDefault(); ;
                        TextBlock t = (TextBlock)childs;
                        grid.Children.Remove(t);

                        TextBlock textBlockb = new TextBlock();
                        textBlockb.Text = a.Predmet;
                        textBlockb.Background = new SolidColorBrush(Colors.Red);

                        textBlockb.Tag = a;
                        textBlockb.TextAlignment = TextAlignment.Center;
                        textBlockb.TextWrapping = TextWrapping.Wrap;
                        Grid.SetRow(textBlockb, a.Red);
                        Grid.SetColumn(textBlockb, num);
                        Grid.SetRowSpan(textBlockb, 4*a.Velicina);
                        Console.WriteLine("zauzimam: " + Grid.GetRow(textBlockb) + "  " + Grid.GetColumn(textBlockb)+"  "+textBlockb.Text);
                        textBlockb.AllowDrop = true;
                        textBlockb.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                        textBlockb.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);
                        ContextMenu cc = new ContextMenu();
                        MenuItem m = new MenuItem();
                        m.Header = "delete";
                        m.Click += delegate { closeTabItem(textBlockb); };
                        cc.Items.Add(m);
                        textBlockb.ContextMenu = cc;
                        grid.Children.Add(textBlockb);
                    }
                }
                num += 1;
            }
            

            ucitajSate();
            //ucitajDane();
            ucitajUcionice();
        }
        private void ucitajSate()
        {

            int red = 0;
            TextBlock textBlock0 = new TextBlock();
            textBlock0.Text = "";
            Grid.SetRow(textBlock0, red);
            Grid.SetColumn(textBlock0, 0);

            grid.Children.Add(textBlock0);
            for (int i = 7; i <= 22; i++)
            {
                TextBlock textBlock1 = new TextBlock();
                textBlock1.Text = i + ":00";
                textBlock1.TextAlignment = TextAlignment.Center;
                textBlock1.Height = ActualHeight / 30;
                Grid.SetRow(textBlock1, ++red);
                Grid.SetColumn(textBlock1, 0);
                grid.Children.Add(textBlock1);
                TextBlock textBlock22 = new TextBlock();
                textBlock22.Text = i + ":15";
                textBlock22.TextAlignment = TextAlignment.Center;
                textBlock22.Height = ActualHeight / 30;
                Grid.SetRow(textBlock22, ++red);
                Grid.SetColumn(textBlock22, 0);
                grid.Children.Add(textBlock22);
                TextBlock textBlock33 = new TextBlock();
                textBlock33.Text = i + ":30";
                textBlock33.TextAlignment = TextAlignment.Center;
                textBlock33.Height = ActualHeight / 30;
                Grid.SetRow(textBlock33, ++red);
                Grid.SetColumn(textBlock33, 0);
                grid.Children.Add(textBlock33);
                TextBlock textBlock44 = new TextBlock();
                textBlock44.Text = i + ":45";
                textBlock44.TextAlignment = TextAlignment.Center;
                textBlock44.Height = ActualHeight / 30;
                Grid.SetRow(textBlock44, ++red);
                Grid.SetColumn(textBlock44, 0);
                grid.Children.Add(textBlock44);
            }

        }

        private void ucitajDane()
        {

            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Monday";
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.Height = ActualHeight / 30;
            Grid.SetRow(textBlock, 0);
            Grid.SetColumn(textBlock, 1);
            grid.Children.Add(textBlock);

            TextBlock textBlock2 = new TextBlock();
            textBlock2.Text = "Tuesday";
            textBlock2.TextAlignment = TextAlignment.Center;
            textBlock2.Height = ActualHeight / 30;
            Grid.SetRow(textBlock2, 0);
            Grid.SetColumn(textBlock2, 2);
            grid.Children.Add(textBlock2);

            TextBlock textBlock3 = new TextBlock();
            textBlock3.Text = "Wednesday";
            textBlock3.TextAlignment = TextAlignment.Center;
            textBlock3.Height = ActualHeight / 30;
            Grid.SetRow(textBlock3, 0);
            Grid.SetColumn(textBlock3, 3);
            grid.Children.Add(textBlock3);

            TextBlock textBlock4 = new TextBlock();
            textBlock4.Text = "Thursday";
            textBlock4.TextAlignment = TextAlignment.Center;
            textBlock4.Height = ActualHeight / 30;
            Grid.SetRow(textBlock4, 0);
            Grid.SetColumn(textBlock4, 4);
            grid.Children.Add(textBlock4);

            TextBlock textBlock5 = new TextBlock();
            textBlock5.Text = "Friday";
            textBlock5.TextAlignment = TextAlignment.Center;
            textBlock5.Height = ActualHeight / 30;
            Grid.SetRow(textBlock5, 0);
            Grid.SetColumn(textBlock5, 5);
            grid.Children.Add(textBlock5);

            TextBlock textBlock6 = new TextBlock();
            textBlock6.Text = "Saturday";
            textBlock6.TextAlignment = TextAlignment.Center;
            textBlock6.Height = ActualHeight / 30;
            Grid.SetRow(textBlock6, 0);
            Grid.SetColumn(textBlock6, 6);
            grid.Children.Add(textBlock6);
        }

        private void ucitajTextblokove()
        {
            helpGrid.Children.Clear();
            helpGrid.RowDefinitions.Clear();
            helpGrid.ColumnDefinitions.Clear();
            grid.ShowGridLines = true;
            Console.WriteLine("grid pre:" + grid.Children.Count+"    "+classroomsShow.Count);
            if (mode.SelectedItem.ToString().Equals("one day"))
            {
                for (int i = 1; i <= 65; i++)
                {
                    for (int j = 1; j <= classroomsShow.Count; j++)
                    {
                        Console.WriteLine(i+"        "+j);
                        TextBlock textBlockt = new TextBlock();
                        textBlockt.Height = ActualHeight / 30;
                        Grid.SetRow(textBlockt, i);
                        Grid.SetColumn(textBlockt, j);
                        textBlockt.AllowDrop = true;
                        textBlockt.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                        textBlockt.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);
                        grid.Children.Add(textBlockt);
                    }
                }
            }
            else
            {
                for (int i = 1; i <= 65; i++)
                {
                    for (int j = 1; j <= 6; j++)
                    {
                        TextBlock textBlockt = new TextBlock();
                        textBlockt.Height = ActualHeight / 30;
                        Grid.SetRow(textBlockt, i);
                        Grid.SetColumn(textBlockt, j);
                        textBlockt.AllowDrop = true;
                        textBlockt.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                        textBlockt.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);
                        grid.Children.Add(textBlockt);
                    }
                }
            }
            Console.WriteLine("grid posle:" + grid.Children.Count);

            helpGrid.ShowGridLines = true;
            for (int i = 0; i <= 3; i++)
            {
                RowDefinition gridRow1 = new RowDefinition();
                helpGrid.RowDefinitions.Add(gridRow1);
            }
            for (int j = 0; j <= 6; j++)
            {
                ColumnDefinition gridCol1 = new ColumnDefinition();
                helpGrid.ColumnDefinitions.Add(gridCol1);

            }
            Console.WriteLine("help pre:" + helpGrid.Children.Count);

            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    TextBlock textBlockt = new TextBlock();
                    textBlockt.Height = ActualHeight / 10;
                    Grid.SetRow(textBlockt, i);
                    Grid.SetColumn(textBlockt, j);
                    textBlockt.AllowDrop = true;
                    
                    helpGrid.Children.Add(textBlockt);
                }
            }
            foreach(Appointment a in helpLista)
            {
                var childs = helpGrid.Children.Cast<UIElement>()
                        .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == a.Kolona)
                        .FirstOrDefault(); ;
                TextBlock t = (TextBlock)childs;
                helpGrid.Children.Remove(t);

                TextBlock textBlockb = new TextBlock();
                textBlockb.Text = a.Predmet;
                textBlockb.Background = new SolidColorBrush(Colors.Red);


                textBlockb.TextAlignment = TextAlignment.Center;
                textBlockb.TextWrapping = TextWrapping.Wrap;
                Grid.SetRow(textBlockb, a.Red);
                Grid.SetColumn(textBlockb, a.Kolona);

                textBlockb.Tag = a;
                textBlockb.AllowDrop = true;
                textBlockb.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                textBlockb.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);
                helpGrid.Children.Add(textBlockb);
            }
            Console.WriteLine("help posle:" + helpGrid.Children.Count);

        }
        private void ucitajUcionice()
        {
            int i = 1;
            //classroom.Items.Clear();
            Console.WriteLine("brooooooj: " + classroomsShow.Count);
            foreach (Classroom c in classroomsShow)
                {
                currClassromForDay[i] = c;
                TextBlock textBlockt = new TextBlock();
                textBlockt.Text = c.Id;
                textBlockt.TextAlignment = TextAlignment.Center;
                textBlockt.Height = ActualHeight / 30;
                Grid.SetRow(textBlockt, 0);
                Grid.SetColumn(textBlockt, i);
                grid.Children.Add(textBlockt);
                i += 1;
                //classroom.Items.Add(c.Id);
                }
        }
        private void day_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handleDay = !cmb.IsDropDownOpen;
            HandleDay();
            
        }
        private bool handleDay = true;

        private void day_DropDownClosed(object sender, EventArgs e)
        {
            if (day.SelectedIndex > -1)
            {
                if (handleDay) HandleDay();
                handleDay = true;
            }
        }
        private void HandleDay()
        {
            currDay = day.SelectedItem.ToString();
            if (currDay.Equals("Monday"))
            {
                currIndexOfDay = 1;
            }
            else if (currDay.Equals("Tuesday"))
            {
                currIndexOfDay = 2;
            }
            else if (currDay.Equals("Wednesday"))
            {
                currIndexOfDay = 3;
            }
            else if (currDay.Equals("Thursday"))
            {
                currIndexOfDay = 4;
            }
            else if (currDay.Equals("Friday"))
            {
                currIndexOfDay = 5;
            }
            else if (currDay.Equals("Saturday"))
            {
                currIndexOfDay = 6;
            }
            ucitajZaDan();

        }
        private void mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handleMode = !cmb.IsDropDownOpen;
            HandleMode();
        }
        private bool handleMode = true;

        private void mode_DropDownClosed(object sender, EventArgs e)
        {
            if (mode.SelectedIndex > -1)
            {
                if (handleMode) HandleMode();
                handleMode = true;
            }
        }
        private void HandleMode()
        {
            currMode = mode.SelectedItem.ToString();
            Console.WriteLine("cccc " + currMode);
            if(mode.SelectedItem.ToString().Equals("one day"))
            {
                day.Visibility = Visibility.Visible;
                daylabel.Visibility = Visibility.Visible;
                classroom.Visibility = Visibility.Collapsed;
                classlabel.Visibility = Visibility.Collapsed;
                if (day.SelectedIndex > -1)
                {
                    ucitajZaDan();
                    RoutedEventArgs r = new RoutedEventArgs();
                    Object s = new object();
                    CheckBox_Checked(s, r);
                }
                else
                {
                    grid.Children.Clear();
                    grid.ShowGridLines = false;
                }
            }
            else
            {
                classroom.Visibility = Visibility.Visible;
                classlabel.Visibility = Visibility.Visible;
                day.Visibility = Visibility.Collapsed;
                daylabel.Visibility = Visibility.Collapsed;
                if (classroom.SelectedIndex > -1)
                {
                    ucitaj();
                    RoutedEventArgs r = new RoutedEventArgs();
                    Object s = new object();
                    CheckBox_Checked(s, r);
                }
                else
                {
                    grid.Children.Clear();
                    grid.ShowGridLines = false;
                }
            }
            
        }

        private void helpGrid_Drop(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = e.Source as TextBlock;
            /*MessageBox.Show("drop item into grid row:{0} row:" +
                Grid.GetRow(textBlock).ToString());
            MessageBox.Show("drop item into grid row:{0} row:" +
                Grid.GetColumn(textBlock).ToString());*/
            DataObject item = (((DragEventArgs)e).Data) as DataObject;
            Appointment a = item.GetData("myFormatA") as Appointment;



            //this.grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(4) });

            //textBlock.Text = listItem.Naziv;
            //textBlock.Background = new SolidColorBrush(Colors.Red);
            //textBlock.TextAlignment = TextAlignment.Center;
            //textBlock.TextWrapping = TextWrapping.Wrap;
            //Grid.SetRow(txt, Grid.GetRow(textBlock));
            //Grid.SetRowSpan(textBlock, 4);
            //Console.WriteLine("reeeeeeed: " + a.Red + "  kolonaaaaaa: " + a.Kolona+" velicina: "+a.Velicina);
            if (!dropFromHelpGrid)
            {
                if (!dropFromSubjects)
                {
                    //brisanje termina sa grida
                    if (currMode.Equals("one day"))
                    {
                        Console.WriteLine("briseeem: " + a.Red + "  " + brisiOvaj);
                        //kolona = currIndexOfDay;
                        //Console.WriteLine("1 dan prebacujem u: " + kolona);
                        //currClassroom.Copy(currClassromForDay[Int32.Parse(Grid.GetColumn(textBlock).ToString())]);
                        var childs = grid.Children.Cast<UIElement>()
                            .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == brisiOvaj)
                            .FirstOrDefault(); ;
                        TextBlock t = (TextBlock)childs;
                        grid.Children.Remove(t);
                    }
                    else
                    {
                        var childs = grid.Children.Cast<UIElement>()
                            .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == a.Kolona)
                            .FirstOrDefault(); ;
                        TextBlock t = (TextBlock)childs;
                        grid.Children.Remove(t);
                    }

                    if (currMode.Equals("one day"))
                    {
                        currClassroom.Copy(currClassromForDay[brisiOvaj]);
                    }
                    Console.WriteLine("curr class : " + currClassroom.Id);

                   //dodavanje praznih textblock-ova
                    int doo = (a.Red + 4) * a.Velicina;
                    for (int j = a.Red; j < doo; j++)
                    {
                        TextBlock textBlockt = new TextBlock();
                        textBlockt.Height = ActualHeight / 30;
                        textBlockt.Background = new SolidColorBrush(Colors.Yellow);
                        Grid.SetRow(textBlockt, j);
                        Grid.SetColumn(textBlockt, a.Kolona);
                        textBlockt.AllowDrop = true;
                        textBlockt.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                        textBlockt.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);
                        grid.Children.Add(textBlockt);
                    }
                    //brisanje termina
                    foreach (Classroom c in classrooms)
                    {
                        if (c.Id.Equals(currClassroom.Id))
                        {
                            foreach (Appointment aa in c.Termini)
                            {
                                if (aa.Id==a.Id)
                                {
                                    Console.WriteLine("milanaaaaaaaaaaaa");
                                    //aa.Status = "no";
                                    c.Termini.Remove(aa);
                                    break;
                                }

                            }
                        }
                    }
                    foreach (Subject s in subjects)
                    {
                        if (s.Naziv.Equals(a.Predmet))
                        {
                            s.BrTermina += 1;
                        }
                    }
                    saveSub();
                    //dropFromHelpGrid = true;
                    gridHelper = false;
                    a.Red = Int32.Parse(Grid.GetRow(textBlock).ToString());
                    a.Kolona = Int32.Parse(Grid.GetColumn(textBlock).ToString());
                    Console.WriteLine("pre:" + helpGrid.Children.Count);
                    var ch = helpGrid.Children.Cast<UIElement>()
                            .Where(x => Grid.GetRow(x) == a.Red && Grid.GetColumn(x) == a.Kolona)
                            .FirstOrDefault(); ;
                    TextBlock tt = (TextBlock)ch;
                    helpGrid.Children.Remove(tt);
                    Console.WriteLine("posleeee:" + helpGrid.Children.Count);
                    textBlock = new TextBlock();
                    Grid.SetColumn(textBlock, a.Kolona);
                    Grid.SetRow(textBlock, a.Red);
                    textBlock.Text = a.Predmet;
                    textBlock.Background = new SolidColorBrush(Colors.Red);
                    textBlock.TextAlignment = TextAlignment.Center;
                    textBlock.AllowDrop = true;
                    textBlock.Tag = a;
                    Console.WriteLine("vel: " + a.Velicina);
                    helpLista.Add(a);
                    textBlock.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(grid_PreviewMouseLeftButtonDown);
                    textBlock.PreviewMouseMove += new MouseEventHandler(grid_MouseMove);
                    helpGrid.Children.Add(textBlock);

                    if (currMode.Equals("one day"))
                    {
                        ucitajZaDan();
                    }
                    else
                    {
                        ucitaj();
                    }
                    File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml", "");

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Classroom>));

                    using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.xml"))
                    {
                        List<Classroom> list = new List<Classroom>();
                        foreach (Classroom c in classrooms)
                        {
                            list.Add(c);
                        }
                        serializer.Serialize(stream, list);
                    }
                }
            }
            dropFromHelpGrid=false;
        }

        private void grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            startPoint = e.GetPosition(null);
        }

        private void grid_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                TextBlock listView = sender as TextBlock;
                //MessageBox.Show("Dada");
                brisiOvaj = Grid.GetColumn(listView);
                dropFromHelpGrid = false;
                gridHelper = false;
                dropFromSubjects = false;
                if (listView.Tag != null)
                {
                    //gridHelper = true;

                    Appointment a = (Appointment)listView.Tag;
                    bool find = false;
                    foreach (Classroom c in classroomsShow)
                    {
                        foreach (Appointment aa in c.Termini)
                        {
                            if (aa.Status.Equals("ok") && aa.Red == a.Red && aa.Kolona == a.Kolona)
                            {
                                Console.WriteLine("sa grida");
                                gridHelper = true;
                                find = true;
                            }
                        }
                    }
                    if (!find)
                    {
                        Console.WriteLine("sa help grida");

                        dropFromHelpGrid = true;
                    }
                    DataObject dragData = new DataObject("myFormatA", a);
                    DragDrop.DoDragDrop(listView, dragData, DragDropEffects.Move);

                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (proj.IsChecked == true && smart.IsChecked == true && table.IsChecked == true)
            {
                subjectsShow.Clear();
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoTable == true && s.PrisustvoPametneTable)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoPametneTable)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoProjektora == true && s.PrisustvoTable == true)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoTable == true && s.PrisustvoPametneTable == true)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoPametneTable == true)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoProjektora == true)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (s.PrisustvoTable == true)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    subjectsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
                foreach (Subject s in subjects)
                {
                    if (os.SelectedIndex > -1)
                    {
                        if (!os.SelectedItem.Equals(""))
                        {
                            if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                            {
                                subjectsShow.Add(s);
                            }
                            else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                            {
                                subjectsShow.Add(s);
                            }
                            else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
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
            //if (currMode.Equals("one day"))
            //{


                Console.WriteLine("count: " + MainWindow.subjects.Count);
                if (proj.IsChecked == true && smart.IsChecked == true && table.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoProjektora == true && s.PrisustvoTable == true && s.PrisustvoPametneTable)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }

                        }
                    }
                }
                else if (proj.IsChecked == true && smart.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoProjektora == true && s.PrisustvoPametneTable)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                    }
                }
                else if (proj.IsChecked == true && table.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoProjektora == true && s.PrisustvoTable == true)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                    }
                }
                else if (smart.IsChecked == true && table.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoTable == true && s.PrisustvoPametneTable == true)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                    }
                }
                else if (smart.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoPametneTable == true)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                    }
                }
                else if (proj.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoProjektora == true)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                    }
                }
                else if (table.IsChecked == true)
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (s.PrisustvoTable == true)
                        {
                            if (os.SelectedIndex > -1)
                            {
                                if (!os.SelectedItem.Equals(""))
                                {
                                    if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                    {
                                        classroomsShow.Add(s);
                                    }
                                    else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                    {
                                        classroomsShow.Add(s);
                                    }

                                }
                                else
                                {
                                    classroomsShow.Add(s);
                                }
                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                    }
                }
                else
                {
                    classroomsShow.Clear();
                    foreach (Classroom s in MainWindow.classrooms)
                    {
                        if (os.SelectedIndex > -1)
                        {
                            if (!os.SelectedItem.Equals(""))
                            {
                                if (os.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    classroomsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
                                {
                                    classroomsShow.Add(s);
                                }
                                else if (os.SelectedItem.Equals("Both") && s.Os.Equals("Both"))
                                {
                                    classroomsShow.Add(s);
                                }

                            }
                            else
                            {
                                classroomsShow.Add(s);
                            }
                        }
                        else
                        {
                            classroomsShow.Add(s);
                        }

                    }
                }
            Console.WriteLine("cccccccccccc: " + classroomsShow.Count);
            if (mode.SelectedIndex>-1 && currMode.Equals("one day") && day.SelectedIndex>-1)
            {
                ucitajZaDan();
            }
            else if(mode.SelectedIndex>-1 && currMode.Equals("whole week") )
            {
                ucitaj();
            }
            //}
        }

        private void saveSub()
        {
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
        }
    }
}
