using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace prac12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Таймер для обновления времени на экране
        /// </summary>
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            TabControlSelectionChanged();
            InitializeTimer();
            
        }
        

        /// <summary>
        /// Метод блокировки кнопки в неактивной вкладке
        /// </summary>
        private void TabControlSelectionChanged()
        {
            var selectedTab = tControl.SelectedItem as TabItem;

            if (selectedTab != null)
            {
                if (selectedTab.Header.ToString() == "Расстояние")
                {
                    btnFirstDigit.IsEnabled = false;
                    btnCalculate.IsEnabled = true;
                }
                else if (selectedTab.Header.ToString() == "Первая цифра")
                {
                    btnCalculate.IsEnabled = false;
                    btnFirstDigit.IsEnabled = true;
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения выбранной вкладки
        /// </summary>
        private void tControl_Changed(object sender, SelectionChangedEventArgs e)
        {
            TabControlSelectionChanged();
            ClearResults();
        }
        /// <summary>
        /// Метод таймера для обновления времени и даты
        /// </summary>
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        /// <summary>
        /// Обработчик события тика таймера
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime d = DateTime.Now;
            tbTime.Text = d.ToString("HH:mm:ss");
            tbData.Text = d.ToString("dd.MM.yyyy");
        }
        /// <summary>
        /// Метод очищения результатов
        /// </summary>
        private void ClearResults()
        {
            tbDistance.Clear();
            tbFirstDigitResult.Clear();
        }
        /// <summary>
        /// Кнопка рассчета растояния
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (double.TryParse(tbX1.Text, out double x1) &&
                double.TryParse(tbY1.Text, out double y1) &&
                double.TryParse(tbX2.Text, out double x2) &&
                double.TryParse(tbY2.Text, out double y2))
            {
                double distance = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                tbDistance.Text = distance.ToString();
            }
            else
            {
                MessageBox.Show("Введите корректные координаты!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Кнопка нахождения первой цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirtsDigit_Click(object sender, RoutedEventArgs e)
        {
            ClearResults();

            if (int.TryParse(tbThreeDigit.Text, out int number) && number >= 100 && number <= 999)
            {
                int firstDigit = number / 100;
                tbFirstDigitResult.Text = firstDigit.ToString();
            }
            else
            {
                MessageBox.Show("Введите трехзначное число!", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Очистка координатов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmClearCoordinates_Click(object sender, RoutedEventArgs e)
        {
                tbX1.Clear();
                tbY1.Clear();
                tbX2.Clear();
                tbY2.Clear();
        }
        /// <summary>
        /// Очистка результата в нахождении расстояния
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmClearCalcRez_Click(object sender, RoutedEventArgs e)
        {
            tbDistance.Clear();
        }
        /// <summary>
        /// Очистка трехзначного числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmThreeDigitClear_Click(object sender, RoutedEventArgs e)
        {
            tbThreeDigit.Clear();
        }
        /// <summary>
        /// Очистка результата первой цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmClearFirtsDigit_Click(object sender, RoutedEventArgs e)
        {
            tbFirstDigitResult.Clear();
        }
        /// <summary>
        /// Кнопка вывода информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Демьяхин Роман ИСП-31\n12 вариант\n1) Найти расстояние между двумя точками с заданными координатами на плоскости\n2) Дано трехзначное число. Используя одну операцию деления нацело, вывести первую цифру данного числа (сотни).", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// Кнопка выхода из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}