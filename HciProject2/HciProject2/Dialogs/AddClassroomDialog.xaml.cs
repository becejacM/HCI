using HciProject2.Model;
using System;
using System.Collections.Generic;
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

namespace HciProject2.Dialogs
{
    /// <summary>
    /// Interaction logic for AddClassroomDialog.xaml
    /// </summary>
    public partial class AddClassroomDialog : Window
    {
        private Classroom classroom;
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
        public AddClassroomDialog()
        {
            InitializeComponent();
            os.Items.Add("Linux");
            os.Items.Add("Windows");
            os.Items.Add("Both");
            classroom = new Classroom();

            id.DataContext = classroom;
            brRadnihMesta.DataContext = classroom;
            opis.DataContext = classroom;
            projector.DataContext = classroom;
            prisustvoTable.DataContext = classroom;
            smartTable.DataContext = classroom;
            os.DataContext = classroom;
            softver.DataContext = classroom;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (var writer = new StreamWriter(System.AppDomain.CurrentDomain.BaseDirectory + "/Files/classrooms.txt", true))
            {
                writer.WriteLine(classroom.id + ";" + classroom.opis + ";" + classroom.brRadnihMesta + ";" + classroom.PrisustvoProjektora + ";" + classroom.PrisustvoTable + ";" + classroom.PrisustvoPametneTable + ";" + classroom.Os + ";" + classroom.Softver);
                MainWindow.classrooms.Add(new Classroom(classroom.Id, classroom.Opis, classroom.BrRadnihMesta, classroom.PrisustvoProjektora, classroom.PrisustvoTable, classroom.PrisustvoPametneTable, classroom.Os, classroom.Softver));
                MessageBox.Show("Classroom successfuly added!");
                Close();
            }
        }

    }
}
