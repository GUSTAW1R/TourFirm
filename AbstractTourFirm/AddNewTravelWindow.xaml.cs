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
    /// Логика взаимодействия для CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly ITravelService service;
        private int? id;
        private int final_cost { get; set; }
        private List<TourForTravelViewModel> tourForTravels;

        public CreateOrderWindow(ITravelService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void LoadData()
        {
            try
            {
                if (tourForTravels != null)
                {
                    dataGridView.ItemsSource = null;
                    dataGridView.ItemsSource = tourForTravels;
                    dataGridView.Columns[0].Visibility = Visibility.Hidden;
                    dataGridView.Columns[1].Visibility = Visibility.Hidden;
                    dataGridView.Columns[2].Visibility = Visibility.Hidden;
                    dataGridView.Columns[3].Visibility = Visibility.Hidden;
                    dataGridView.Columns[4].Visibility = Visibility.Hidden;
                    dataGridView.Columns[5].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TravelViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxTravelName.Text = view.TravelName;
                        textBoxSum.Text = view.Final_Cost.ToString();
                        tourForTravels = view.TourForTravel;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                tourForTravels = new List<TourForTravelViewModel>();
            }
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<TourForTravelWindow>();
            if (window.ShowDialog() == true)
            {
                if (window.Model != null)
                {
                    if (id.HasValue)
                    {
                        window.Model.TravelId = id.Value;
                    }
                    tourForTravels.Add(window.Model);
                }
                LoadData();
            }
        }

        private void buttonDel_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        tourForTravels.RemoveAt(dataGridView.SelectedIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTravelName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxSum.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tourForTravels == null || tourForTravels.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<TourForTravelBindingModel> documentsComponentBM = new List<TourForTravelBindingModel>();
                for (int i = 0; i < tourForTravels.Count; ++i)
                {
                    documentsComponentBM.Add(new TourForTravelBindingModel
                    {
                        Id = tourForTravels[i].Id,
                        TravelId = tourForTravels[i].TravelId,
                        TourId = tourForTravels[i].TourId,
                        Count = tourForTravels[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new TravelBindingModel
                    {
                        Id = id.Value,
                        TravelName = textBoxTravelName.Text,
                        Price = Convert.ToInt32(textBoxSum.Text),
                        DocumentBlank = documentsComponentBM
                    });
                }
                else
                {
                    service.AddElement(new TravelBindingModel
                    {
                        TravelName = textBoxTravelName.Text,
                        Price = Convert.ToInt32(textBoxSum.Text),
                        DocumentBlank = documentsComponentBM
                    });
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

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void buttonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridView.SelectedItems.Count == 1)
            {
                var window = Container.Resolve<TourForTravelWindow>();
                window.Model = tourForTravels[dataGridView.SelectedIndex];
                if (window.ShowDialog() == true)
                {
                    tourForTravels[dataGridView.SelectedIndex] = window.Model;
                    LoadData();
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
