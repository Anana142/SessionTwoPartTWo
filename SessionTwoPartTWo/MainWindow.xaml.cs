using Microsoft.EntityFrameworkCore;
using SessionTwoPartTWo.DataBaseFolder;
using SessionTwoPartTWo.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SessionTwoPartTWo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string fIO;

        public int HospitalizationCode { get; set; }
        public string BedRoomNumber { get; set; }
        public string FIO { get => fIO; set { fIO = value; Signal(); } }

        public List<Canvas> Canvases { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Canvases = new List<Canvas>() {
                        Б111,
                        В111,
                        Г111,
                        А111,
                        Б113,
                        В113,
                        Г113,
                        А113,
                        Б114,
                        В114,
                        Г114,
                        А114,
                        А115,
                        Б115,
                        В115,
                        Г115,
                        Е115,
                        Д115,
                        А116,
                        Б116,
                        А117,
                        Б117,
                        А118,
                        Б118,
                        В101,
                        Б101,
                        А101,
                        Г101,
                        Д101,
                        Д102,
                        В102,
                        Б102,
                        А102,
                        Г102,
                        Г103,
                        В103,
                        Е103,
                        Д103,
                        Б103,
                        А103,
                        Г104,
                        А104,
                        Б104,
                        В104,
                        Г105,
                        А105,
                        Б105,
                        В105,
                        Г106,
                        А106,
                        Б106,
                        В106,
                        Г107,
                        А107,
                        Б107,
                        В107,
                        В108,
                        Б108,
                        А108,
                        В109,
                        Б109,
                        А109,
                        В110,
                        Б110,
                        А110,
                        Б112,
                        В112,
                        Г112,
                        А112,
            };
            DataContext = this;
            GenerateClient();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void Signal([CallerMemberName] string prop = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        private void GenerateClient()
        {
            List<Hospitalization> hospitalizations = DataBase.instance.Hospitalizations.ToList();
            ContextMenu ctxMenu = new ContextMenu();
            var mi = new MenuItem();
            mi.Header = "Выписать";
            mi.Click += PatientOutHospitalization;
            ctxMenu.Items.Add(mi);

            foreach (var hosp in hospitalizations)
            {
                if (hosp.BedNumber != null && hosp.RoomNumber != null)
                {
                    Ellipse ellipse = new Ellipse { Width = 20, Height = 20, Fill = Brushes.Green };
                    ellipse.MouseLeftButtonDown += EllipseDrag;

                    ellipse.ContextMenu = ctxMenu;

                    Canvas canvas = Canvases.FirstOrDefault(s => s.Name == $"{hosp.BedNumber}{hosp.RoomNumber}");
                    canvas.Children.Add(ellipse);


                }
            }
        }

        private void PatientOutHospitalization(object sender, RoutedEventArgs e)
        {
        }

        private void AddElips(object sender, RoutedEventArgs e)
        {

            if (HospitalizationCode == 0 && string.IsNullOrWhiteSpace(BedRoomNumber))
                return;

            Hospitalization hospitalization = new Hospitalization();
            hospitalization = DataBase.instance.Hospitalizations.FirstOrDefault(s => s.Id == HospitalizationCode);

            if (hospitalization == null)
            {
                MessageBox.Show("Неверно введен код госпитализации");
                return;
            }

            var Canvas = Canvases.FirstOrDefault(s => s.Name == BedRoomNumber);

            if (Canvas == null)
            {
                MessageBox.Show("Введите букву кровати и номер комнаты (пример Б112)");
                return;
            }

            if (Canvas.Children.Count > 0)
            {
                MessageBox.Show("Кровать уже занята");
                return;
            }

            if (hospitalization.RoomNumber != null && hospitalization.BedNumber != null)
            {
                MessageBox.Show($"Пациент уже добавлен на место {hospitalization.BedNumber}{hospitalization.RoomNumber}");
                return;
            }


            Canvas.Children.Add(new Ellipse { Width = 20, Height = 20, Fill = Brushes.Green });

            hospitalization.RoomNumber = BedRoomNumber.Substring(1);
            hospitalization.BedNumber = BedRoomNumber[0].ToString();
            DataBase.instance.Hospitalizations.Update(hospitalization);
            DataBase.instance.SaveChanges();
        }

        private void Drop(object sender, DragEventArgs e)
        {
            Ellipse elp = (Ellipse)e.Data.GetData(typeof(Ellipse));
            var p = elp.Parent as Panel;
            Canvas newParent = (Canvas)sender;

            if (newParent.Children.Count > 0)
            {
                e.Handled = true;
                return;
            }

            if (p == newParent)
            {
                e.Handled = true;
                return;
            }
            p.Children.Remove(elp);

            Hospitalization hospitalization = DataBase.instance.Hospitalizations.FirstOrDefault(s => s.RoomNumber == p.Name.Substring(1) && s.BedNumber == p.Name[0].ToString());

            hospitalization.RoomNumber = newParent.Name.Substring(1);
            hospitalization.BedNumber = newParent.Name[0].ToString();

            DataBase.instance.Hospitalizations.Update(hospitalization);
            DataBase.instance.SaveChanges();

            var pos = e.GetPosition(newParent);

            Canvas.SetLeft(elp, pos.X);
            Canvas.SetTop(elp, pos.Y);

            elp.MouseDown += EllipseDrag;
            newParent.Children.Add(elp);
        }
        private void EllipseDrag(object sender, MouseButtonEventArgs e)
        {
            Ellipse elp = (Ellipse)sender;
            DragDrop.DoDragDrop(elp, elp, DragDropEffects.Move);
        }
        private void Over(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
        }

        private void Check(object sender, RoutedEventArgs e)
        {
            if (HospitalizationCode == 0)
                return;

            Hospitalization hospitalization = new Hospitalization();
            hospitalization = DataBase.instance.Hospitalizations.Include(s => s.IdNavigation).FirstOrDefault(s => s.MedicCardCode == HospitalizationCode);

            if (hospitalization == null)
                return;

            FIO = hospitalization.RoomNumber == null ? $"{hospitalization.IdNavigation.LastName} {hospitalization.IdNavigation.FirstName} {hospitalization.IdNavigation.Patronymic}" : $"{hospitalization.IdNavigation.LastName} {hospitalization.IdNavigation.FirstName} {hospitalization.IdNavigation.Patronymic} место: {hospitalization.BedNumber}{hospitalization.RoomNumber}";
        }
    }
}
