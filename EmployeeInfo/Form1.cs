using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeInfo
{
    public partial class EmployeeInfoUi : Form
    {
        public EmployeeInfoUi()
        {
            InitializeComponent();
        }



        Employee anEmployee=new Employee();
       string connectionstring = ConfigurationManager.ConnectionStrings["local"].ToString();
        
        private void saveButton_Click(object sender, EventArgs e)
        {
            anEmployee.name = nameTextBox.Text;
            anEmployee.address = addressTextBox.Text;
            anEmployee.email = emailTextBox.Text;
            anEmployee.salary = Convert.ToDouble(salaryTextBox.Text);

            

            string query = @"INSERT INTO tbl_employee(Name,Address,Email,Salary) VALUES('"+anEmployee.name+"','"+anEmployee.address+"','"+anEmployee.email+"','"+anEmployee.salary+"')";

            SqlConnection con=new SqlConnection(connectionstring);

            con.Open();
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Successfully Saved");


            
        }

        public void LoadEmployeeListView(List<Employee> employees )
        {
            showListView.Items.Clear();
            foreach (var employee in employees)
            {
                ListViewItem item=new ListViewItem(anEmployee.name);
                item.SubItems.Add(anEmployee.address);
                item.SubItems.Add(anEmployee.email);
                item.SubItems.Add(anEmployee.salary.ToString());

                showListView.Items.Add(item);
            }
        }
        private void showAllButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionstring);

            string search = @"SELECT * from tbl_employee";

            SqlCommand cmd=new SqlCommand(search,con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            List<Employee> employeelist=new List<Employee>();

            while (dr.Read())
            {
                anEmployee.name = dr["name"].ToString();
                anEmployee.address = dr["address"].ToString();
                anEmployee.email = dr["email"].ToString();
                anEmployee.salary = int.Parse(dr["salary"].ToString());

                employeelist.Add(anEmployee);
            }
            dr.Close();
            con.Close();
        }
    }

}
