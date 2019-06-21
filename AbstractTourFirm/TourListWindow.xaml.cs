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
using System.Windows.Shapes;
using AbstractTourFirm_ServiceDAL.BindingModel;
using AbstractTourFirm_ServiceDAL.Interfaces;
using AbstractTourFirm_ServiceDAL.ViewModel;
using Unity;

namespace AbstractTourFirm
{
    /// <summary>
    /// Логика взаимодействия для TourList.xaml
    /// </summary>
    public partial class TourList : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ITourService service;
        Random rnd = new Random();
        string[] mas_word = new string[7] { "Favella", "Sea Paradase", "Camping", "Chill out", "Don't move and relax", "without internet", "MayDay"};
        string[] country = new string[4] { "France", "Italy", "Spain", "USA" };
        static int num ;

        public TourList(ITourService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                List<TourViewModel> list = service.GetList();
                if (list != null)
                {
                    dataGridView.ItemsSource = list;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ButtonCreateTours_Click(object sender, RoutedEventArgs e)
        {
            num = rnd.Next(1, 6);
            service.AddElement(new TourBindingModel
            {

                TourName = mas_word[num]

            });
        }

        private void ButtonDeleteTour_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = ((TourViewModel)dataGridView.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
