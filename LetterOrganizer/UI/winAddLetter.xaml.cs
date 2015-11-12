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

namespace LetterOrganizer
{
    /// <summary>
    /// Interaction logic for winAddLetter.xaml
    /// </summary>
    public partial class winAddLetter : Window
    {
        public winAddLetter()
        {
            InitializeComponent();
        }

        public Letter ToEdit
        {
            get;
            set;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UnitOfWork unit = Global.UnitOfWork;
            var owners = unit.LetterRepository.GetAll().Select(x => x.Owner);
            var ringBinders = unit.RingBinderRepository.GetAll();

            txtLOwner.ItemsSource = owners;
            txtRingBinder.ItemsSource = ringBinders;

            if (ToEdit != null)
            {
                txtLComments.Text = ToEdit.Comment;
                txtLNumber.Text = ToEdit.LetterID.ToString();
                txtLOwner.Text = ToEdit.Owner;
                txtLResiveNumber.Text = ToEdit.ReceivedLetterId.ToString();
                txtLTitle.Text = ToEdit.Title;
                txtRingBinder.Text = ToEdit.RingerBinder.Title;
                txtRingBinderComment.Text = ToEdit.RingerBinder.Comments;

                rbHasAttachment.IsChecked = ToEdit.Attachment;
                rbHasNotAttachment.IsChecked = !ToEdit.Attachment;
                rbResive.IsChecked = ToEdit.ReceivedLetterId != 0;
                rbSend.IsChecked = ToEdit.ReceivedLetterId == 0;

                datePicker.SelectedDate = new Arash.PersianDate(ToEdit.Date);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (saveNewLetter())
            {
                ResetComponents();
            }
        }

        private bool saveNewLetter()
        {
            bool newrb = false;
            bool attach = bool.Parse(rbHasAttachment.IsChecked.ToString());
            int resiveID = 0;
            int id;

            if ((bool)rbResive.IsChecked && !int.TryParse(txtLResiveNumber.Text, out resiveID))
            {
                System.Windows.Forms.MessageBox.Show("نامه دریافتی باید دارای شماره رسید باشد.");
                return false;
            }
            if (!int.TryParse(txtLNumber.Text, out id))
            {
                System.Windows.Forms.MessageBox.Show("نامه باید دارای شماره باشد.");
                return false;
            }

            string comment = txtLComments.Text;
            string owner = txtLOwner.Text;
            string rBinder = txtRingBinder.Text;
            string title = txtLTitle.Text;

            if (string.IsNullOrWhiteSpace(txtRingBinder.Text))
            {
                System.Windows.Forms.MessageBox.Show("زوکن نامه باید مشخص باشد");
                return false;
            }

            RingBinder ringBinder = Global.UnitOfWork.RingBinderRepository.GetByQuery(x => x.Title == rBinder).FirstOrDefault();
            if (ringBinder == null)
            {
                newrb = true;
                ringBinder = new RingBinder()
                {
                    Title = txtRingBinder.Text,
                    Comments = txtRingBinderComment.Text,
                };
            }

            Letter newLetter = new Letter()
            {
                Attachment = attach,
                Comment = comment,
                LetterID = id,
                Owner = owner,
                ReceivedLetterId = resiveID,
                RingerBinder = ringBinder,
                Title = title,
                Date = datePicker.SelectedDate.ToDateTime(),
            };

            if (newrb)
            {
                Global.UnitOfWork.RingBinderRepository.Insert(ringBinder);
            }

            if (ToEdit != null)
            {
                ToEdit.Attachment = newLetter.Attachment;
                ToEdit.Comment = newLetter.Comment;
                ToEdit.Date = newLetter.Date;
                ToEdit.LetterID = newLetter.LetterID;
                ToEdit.Owner = newLetter.Owner;
                ToEdit.ReceivedLetterId = newLetter.ReceivedLetterId;
                ToEdit.RingerBinder = newLetter.RingerBinder;
                ToEdit.Title = newLetter.Title;


                Global.UnitOfWork.LetterRepository.Update(ToEdit);

            }
            else
            {

                Global.UnitOfWork.LetterRepository.Insert(newLetter);
            }

            return true;
        }

        private void ResetComponents()
        {
            rbHasAttachment.IsChecked = false;
            rbHasNotAttachment.IsChecked = true;

            rbResive.IsChecked = true;
            rbSend.IsChecked = false;

            txtLComments.Text = string.Empty;
            txtLNumber.Text = string.Empty;
            txtLOwner.Text = string.Empty;
            txtLResiveNumber.Text = string.Empty;
            txtLTitle.Text = string.Empty;
            txtRingBinder.Text = string.Empty;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Global.UnitOfWork.Commit();
        }
    }
}
