using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace WebApplication
{
    public partial class Company : System.Web.UI.Page
    {
        string connection = @"Data Source=DESKTOP-07J0DCC\SQLEXPRESS;Integrated Security = true; Initial Catalog=AspNetWebApplication";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnDelete.Enabled = false;
                PopulateGridView();
            }
           
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            hfID.Value = "";
            txtCompanyName.Text = "";
            txtDepartmentLocation.Text = "";
            txtDepartmentName.Text = "";
            txtEmployeeName.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
            lblErrorMessage.Text = lblSuccessMessage.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(btnSave.Text == "Save")
            {
                SqlConnection conn = new SqlConnection(connection);
                string insertCommand = "insert into organization values(@DepartmentName,@DepartmentLocation,@CompanyName,@EmployeeName)";
                string existCommand = @"select * from organization where CompanyName = @CompanyName or EmployeeName = @EmployeeName";
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlCommand command = new SqlCommand(existCommand, conn);

                //command.Parameters.AddWithValue("@ID", hfID.Value == "" ? 0 : Convert.ToInt32(hfID));
                command.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text);
                command.Parameters.AddWithValue("@DepartmentLocation", txtDepartmentLocation.Text);
                command.Parameters.AddWithValue("@EmployeeName", txtEmployeeName.Text);
                command.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);

                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    lblSuccessMessage.Text = "Already Exist In Database";
                    conn.Close();
                    PopulateGridView();
                }
                else
                {
                    conn.Close();
                    SqlCommand command2 = new SqlCommand(insertCommand, conn);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                        //command2.Parameters.AddWithValue("@ID", hfID.Value == "" ? 1 : Convert.ToInt32(hfID));
                        command2.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text.Trim());
                        command2.Parameters.AddWithValue("@DepartmentLocation", txtDepartmentLocation.Text.Trim());
                        command2.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
                        command2.Parameters.AddWithValue("@EmployeeName", txtEmployeeName.Text.Trim());
                        command2.ExecuteNonQuery();
                        conn.Close();
                        Clear();
                        if (hfID.Value == "")
                        {
                            lblSuccessMessage.Text = "Saved Successfully";
                        }
                        else
                            lblErrorMessage.Text = "Not Saved";

                        PopulateGridView();
                    }


                }
            }
            if(btnSave.Text == "Update")
            {
                //int id = Convert.ToInt32(((sender as LinkButton).CommandArgument));
                int id = int.Parse(hfID.Value);

                SqlConnection conn = new SqlConnection(connection);
                //string insertCommand = "insert into organization values(@DepartmentName,@DepartmentLocation,@CompanyName,@EmployeeName)";
                string updateCommand = "update organization set DepartmentName = @DepartmentName, DepartmentLocation = @DepartmentLocation, EmployeeName = @EmployeeName where ID = @ID";

                SqlCommand command2 = new SqlCommand(updateCommand, conn);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    //command2.Parameters.AddWithValue("@ID", hfID.Value == "" ? 1 : Convert.ToInt32(hfID));
                    command2.Parameters.AddWithValue("@ID", id);
                    command2.Parameters.AddWithValue("@DepartmentName", txtDepartmentName.Text.Trim());
                    command2.Parameters.AddWithValue("@DepartmentLocation", txtDepartmentLocation.Text.Trim());
                    command2.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text.Trim());
                    command2.Parameters.AddWithValue("@EmployeeName", txtEmployeeName.Text.Trim());
                    command2.ExecuteNonQuery();
                    Clear();
                    if (hfID.Value == "")
                    {
                        lblSuccessMessage.Text = "Updated Successfully";
                    }
                    else
                        lblErrorMessage.Text = "Not Saved";

                    PopulateGridView();
                }


            }
            
        }

        protected void lnk_OnClick(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(((sender as LinkButton).CommandArgument));

            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    string command = "select * from organization where ID = @ID";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, connection);
                    sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ID", id);
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            hfID.Value = id.ToString();
            txtCompanyName.Text = dataTable.Rows[0]["CompanyName"].ToString();
            txtDepartmentLocation.Text = dataTable.Rows[0]["DepartmentLocation"].ToString();
            txtDepartmentName.Text = dataTable.Rows[0]["DepartmentName"].ToString();
            txtEmployeeName.Text = dataTable.Rows[0]["EmployeeName"].ToString();
            btnSave.Text = "Update";
            btnDelete.Enabled = true;
            
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(connection);
            var deleteCommand = "delete from organization where ID = @ID";
            int id = int.Parse(hfID.Value);

            SqlCommand command2 = new SqlCommand(deleteCommand, conn);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                //command2.Parameters.AddWithValue("@ID", hfID.Value == "" ? 1 : Convert.ToInt32(hfID));
                command2.Parameters.AddWithValue("@ID", id);
                command2.ExecuteNonQuery();
                conn.Close();
                Clear();
                PopulateGridView();
                lblSuccessMessage.Text = "Deleted Successfully";
            }
        }

        //**PopulateGridView
        public void PopulateGridView()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connection))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    string command = "select * from organization";
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command, connection);
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            gvCompany.DataSource = dataTable;
            gvCompany.DataBind();
        }
    }
}