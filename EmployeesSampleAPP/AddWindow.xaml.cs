using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace EmployeesSampleAPP
{
    public partial class AddWindow : Window
    {
        private AddWindow() { InitializeComponent(); }

        public AddWindow(DataRow row) : this()
        {
            cancelBtn.Click += delegate { this.DialogResult = false; };
            okBtn.Click += delegate
            {
                row["workerName"] = txtworkerName.Text;
                row["workerLastName"] = txtworkerLastName.Text;
                row["role"] = txtRole.Text;
                row["salary"] = txtSalary.Text;
                row["status"] = txtStatus.Text;
                this.DialogResult = !false;
            };

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
