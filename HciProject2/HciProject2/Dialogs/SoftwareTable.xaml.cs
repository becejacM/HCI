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

            os.Items.Add("Linux");
            os.Items.Add("Windows");
            os.Items.Add("Cross-platform");

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
            osistem.Items.Add("Both");

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
            Id.IsEnabled = e;
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
                softwareShow.RemoveAt(dgrMain.SelectedIndex);

            }
            save();
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Boolean hasError = false;
            if (addNew || !currId.Equals(software.Id))
            {
                foreach (Software s in MainWindow.softwares)
                {
                    if (software.Equals(s))
                    {
                        MessageBox.Show("ID allready exists. ID must be unique.");
                        hasError = true;
                    }
                }
            }
            if (software.Id.Equals(""))
            {
                MessageBox.Show("ID must be set.");
                hasError = true;
            }
            if (software.Naziv.Equals("") || software.Opis.Equals("") || software.Cena.Equals("") || software.Proizvodjac.Equals("") ||
                software.Sajt.Equals("") || software.Os.Equals("") || software.GodinaIzdavanja.Equals("") )
            {
                MessageBox.Show("One or more values doesn't set. All values must be set.");
                hasError = true;
            }
            if (software.Cena.GetType() != typeof(Int32)){
                MessageBox.Show("Price must be real number.");
                hasError = true;
            }
            if (!hasError)
            {
                if (addNew)
                {
                    Software c = new Software();

                    c.Copy(software);
                    softwareShow.Add(c);
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

            MainWindow.softwares.Clear();
            foreach (Software s in softwareShow)
            {
                MainWindow.softwares.Add(s);
            }
            File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/softwares.xml", "");

            XmlSerializer serializer = new XmlSerializer(typeof(List<Software>));

            using (FileStream stream = File.OpenWrite(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/softwares.xml"))
            {
                List<Software> list = new List<Software>();
                foreach (Software c in softwareShow)
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
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
        }

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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                                if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                                {
                                    softwareShow.Add(s);
                                }
                                else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
                            if (osistem.SelectedItem.Equals("Windows") && s.Os.Equals("Windows"))
                            {
                                softwareShow.Add(s);
                            }
                            else if (osistem.SelectedItem.Equals("Linux") && s.Os.Equals("Linux"))
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
    }
}
