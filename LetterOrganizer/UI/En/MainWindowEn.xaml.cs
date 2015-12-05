namespace LetterOrganizer
{
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

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowEn : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindowEn()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

        }


        /// <summary>
        /// Gets or sets the letter owners.
        /// </summary>
        /// <value>
        /// The letter owners.
        /// </value>
        public IEnumerable<string> LetterOwners
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the letters.
        /// </summary>
        /// <value>
        /// The letters.
        /// </value>
        public IEnumerable<Letter> Letters
        {
            get;
            set;
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            winAddLetterEn add = new winAddLetterEn();
            add.ShowDialog();

            Init();
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// Handles the Click event of the btnEdit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lsvLetters.SelectedItem != null)
            {
                winAddLetter add = new winAddLetter();
                add.ToEdit = (Letter)lsvLetters.SelectedItem;
                add.ShowDialog();
                Init();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lsvLetters.SelectedItem != null)
            {
                Global.UnitOfWork.LetterRepository.Delete((Letter)lsvLetters.SelectedItem);
                Init();
            }
        }

        /// <summary>
        /// Handles the Checked event of the RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            DoFillter();
        }

        /// <summary>
        /// Handles the KeyUp event of the txtLNumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtLNumber_KeyUp(object sender, KeyEventArgs e)
        {
            DoFillter();
        }

        /// <summary>
        /// Handles the CheckedChange event of the CheckBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void CheckBox_CheckedChange(object sender, RoutedEventArgs e)
        {
            DoFillter();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private async void Init()
        {
            loading.Visibility = System.Windows.Visibility.Visible;
            await Task.Run(() =>
            {
                UnitOfWork unit = Global.UnitOfWork;
                var owners = unit.LetterRepository.GetAll().Select(x => x.Owner);
                var Items = unit.LetterRepository.GetAll().ToList();

                Dispatcher.Invoke(() =>
                {
                    txtLOwner.ItemsSource = owners;
                    lsvLetters.ItemsSource = Items;
                });

            });
            loading.Visibility = System.Windows.Visibility.Hidden;
        }

        /// <summary>
        /// Does the fillter.
        /// </summary>
        private void DoFillter()
        {
            if (!IsLoaded)
            {
                return;
            }
            lsvLetters.Items.Filter = x =>
            {
                Letter item = (Letter)x;
                bool result = true;
                if ((bool)chType.IsChecked)
                {
                    if ((bool)rbResive.IsChecked)
                    {
                        result &= item.ReceivedLetterId != 0;
                    }
                    else
                    {
                        result &= item.ReceivedLetterId == 0;
                    }
                }
                if ((bool)chDetails.IsChecked)
                {
                    result &= item.Title.Contains(txtLTitle.Text);
                    result &= item.ReceivedLetterId.ToString().Contains(txtLResiveNumber.Text);
                    result &= item.LetterID.ToString().Contains(txtLNumber.Text);
                    result &= item.Owner.Contains(txtLOwner.Text);
                }
                if ((bool)chAttachment.IsChecked)
                {
                    result &= item.Attachment == rbHasAttachment.IsChecked;
                }
                if ((bool)chDate.IsChecked)
                {
                    if (item.Date.CompareTo(dpFrom.SelectedDate) < 0)
                    {
                        result &= false;
                    }
                    if (item.Date.CompareTo(dpTo.SelectedDate) > 0)
                    {
                        result &= false;
                    }

                }

                return result;
            };
        }

        /// <summary>
        /// Handles the SelectedDateChanged event of the dpFrom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void dpFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DoFillter();
        }

    }
}
