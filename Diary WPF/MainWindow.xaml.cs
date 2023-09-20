using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pr2Sofa
{
    public partial class MainWindow : Window
    {
        public static string way = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static Note example = new Note("name", "description", selectedDate);

        public static List<Note> notes = De_Serialize_.Deserialize<Note>(way, example);
        public static DateTime selectedDate = DateTime.Today;

        public MainWindow()
        {
            InitializeComponent();
            DatePick.SelectedDate = DateTime.Today;
        }
        private void Notes_Change(object sender, SelectionChangedEventArgs e)
        {
            foreach (var _ in notes)
            {
                if (NoteName.Text == _.Name)
                {
                    NoteDesc.Text = _.Description;
                }
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Note note = new Note(NoteName.Text, NoteDesc.Text, (DateTime)DatePick.SelectedDate);
            notes.Add(note);

            foreach (var _ in notes)
            {
                if (DatePick.SelectedDate == _.Date)
                {
                    if (NotesLBX.Items.Contains(_.Name)) { }
                    else NotesLBX.Items.Add(_.Name);
                }
            }
            NotesLBX.SelectedIndex = notes.Count - 1;
            De_Serialize_.Serialize(notes, way + "\\allNotes.json");
            NoteName.Text = null;
            NoteDesc.Text = null;
        }

        private void DatePick_Changd(object sender, SelectionChangedEventArgs e)
        {
            selectedDate = Convert.ToDateTime(DatePick.Text);
            
            ItemsCheck();
        }

        private void ItemsCheck()
        {
            NotesLBX.Items.Clear();

            foreach (var _ in notes)
            {
                if (_.Date == selectedDate)
                {
                    NotesLBX.Items.Add(_.Name);
                }
            }
        }
        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            foreach (var _ in notes)
            {
                if (_.Name == NotesLBX.SelectedItem.ToString())
                {
                    index = notes.IndexOf(_);
                }
            }
            NotesLBX.SelectedIndex = notes.Count - 1;
            notes.RemoveAt(index);
            De_Serialize_.Serialize(notes, way + "\\allNotes.json");

            NoteName.Text = null;
            NoteDesc.Text = null;

            ItemsCheck();
        }
    }
}
