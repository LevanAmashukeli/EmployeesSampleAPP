using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace EmployeesSampleAPP
{

    public partial class MainWindow : Window
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataTable dt;
        DataRowView row;

        public MainWindow() { InitializeComponent(); Preparing(); }

        private void Preparing()
        {
            #region Init

            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "C:\\USERS\\NUCIK\\SOURCE\\REPOS\\EMPLOYEESSAMPLEAPP\\(LOCADB)LEOBASE.MDF"
            };
            con = new SqlConnection(connectionStringBuilder.ConnectionString);
            dt = new DataTable();
            da = new SqlDataAdapter();

            #endregion

            #region select


            var sql = @"SELECT * FROM Workers Order By Workers.id";
            da.SelectCommand = new SqlCommand(sql, con);

            #endregion

            #region insert

            sql = @"INSERT INTO Workers (workerName,  workerLastName,  role, salary, status) 
                                 VALUES (@workerName , @workerLastName, @role, @salary , @status); 
                     SET @id = @@IDENTITY;";

            da.InsertCommand = new SqlCommand(sql, con);

            da.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").Direction = ParameterDirection.Output;
            da.InsertCommand.Parameters.Add("@workerName", SqlDbType.NVarChar, 20, "workerName");
            da.InsertCommand.Parameters.Add("@workerLastName", SqlDbType.NVarChar, 20, "workerLastName");
            da.InsertCommand.Parameters.Add("@role", SqlDbType.NVarChar, 20, "role");
            da.InsertCommand.Parameters.Add("@salary", SqlDbType.Int, 4, "salary");
            da.InsertCommand.Parameters.Add("@status", SqlDbType.NVarChar, 20, "status");

            #endregion

            #region update

            sql = @"UPDATE Workers SET 
                           workerName = @workerName,
                           workerLastName = @workerLastName, 
                           role = @role,
                           salary = @salary, 
                           status = @status
                    WHERE id = @id";

            da.UpdateCommand = new SqlCommand(sql, con);
            da.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").SourceVersion = DataRowVersion.Original;
            da.UpdateCommand.Parameters.Add("@workerName", SqlDbType.NVarChar, 20, "workerName");
            da.UpdateCommand.Parameters.Add("@workerLastName", SqlDbType.NVarChar, 20, "workerLastName");
            da.UpdateCommand.Parameters.Add("@role", SqlDbType.NVarChar, 20, "role");
            da.UpdateCommand.Parameters.Add("@salary", SqlDbType.Int, 4, "salary");
            da.UpdateCommand.Parameters.Add("@status", SqlDbType.NVarChar, 20, "status");

            #endregion

            #region delete

            sql = "DELETE FROM Workers WHERE id = @id";

            da.DeleteCommand = new SqlCommand(sql, con);
            da.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");

            #endregion

            da.Fill(dt);

            gridView.DataContext = dt.DefaultView;
            LblCount.Content = "Rows: " + dt.Rows.Count.ToString();


        }

        /// <summary>
        /// Start Edition  record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            row = (DataRowView)gridView.SelectedItem;
            row.BeginEdit();
            da.Update(dt);
        }

        /// <summary>
        /// Editing  record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GVCurrentCellChanged(object sender, EventArgs e)
        {
            if (row == null) return;
            row.EndEdit();
            da.Update(dt);
        }

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {
            row = (DataRowView)gridView.SelectedItem;
            row.Row.Delete();
            da.Update(dt);
            LblCount.Content = "Rows: " + dt.Rows.Count.ToString();
        }

        /// <summary>
        /// Add new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {
            DataRow r = dt.NewRow();
            AddWindow add = new AddWindow(r);
            add.ShowDialog();


            if (add.DialogResult.Value)
            {
                dt.Rows.Add(r);
                da.Update(dt); 
            }
            LblCount.Content = "Rows: " + dt.Rows.Count.ToString();
        }

        private void AllViewShow(object sender, RoutedEventArgs e)
        {
            new AllView().ShowDialog();
        }

    }
}
