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
using AbstractTourFirm_ServiceDAL.Interfaces;
using AbstractTourFirm_ServiceDAL.ViewModel;
using Unity;

namespace AbstractTourFirm
{
    /// <summary>
    /// Логика взаимодействия для TourForTravelWindow.xaml
    /// </summary>
    public partial class TourForTravelWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public TourForTravelViewModel Model { set { model = value; } get { return model; } }
        private readonly ITourService service;
        private TourForTravelViewModel model;

        public TourForTravelWindow(ITourService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (comboBoxTourForTravel.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new TourForTravelViewModel
                    {
                        TourId = Convert.ToInt32(comboBoxTourForTravel.SelectedValue),
                        TourName = comboBoxTourForTravel.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                List<TourViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxTourForTravel.DisplayMemberPath = "BlankName";
                    comboBoxTourForTravel.SelectedValuePath = "Id";
                    comboBoxTourForTravel.ItemsSource = list;
                    comboBoxTourForTravel.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (model != null)
            {
                comboBoxTourForTravel.IsEnabled = false;
                comboBoxTourForTravel.SelectedValue = model.TourId;
                textBoxCount.Text = model.Count.ToString();
            }
        }
    }
}
