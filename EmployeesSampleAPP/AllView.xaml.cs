using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace EmployeesSampleAPP
{

    public partial class AllView : Window
    {
        public AllView()
        {
            InitializeComponent();
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "C:\\USERS\\NUCIK\\SOURCE\\REPOS\\EMPLOYEESSAMPLEAPP\\(LOCADB)LEOBASE.MDF"
            };

            SqlConnection con = new SqlConnection(connectionStringBuilder.ConnectionString);
            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();

            var sql = @"SELECT 
Workers.workerName as 'First Name',
Workers.workerLastName as 'Last Name',
Phone.PhoneNumber  as 'Phone Number',
Workers.role as 'Role',
Workers.salary as 'Salary',
Workers.status as 'Status'
FROM  Workers, Phone
WHERE Workers.id = Phone.id
Order By Workers.id";
            da.SelectCommand = new SqlCommand(sql, con);
            da.Fill(dt);

            gridAllView.DataContext = dt.DefaultView;
        }
    }
}
