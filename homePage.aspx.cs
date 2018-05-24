using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class homePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearEmpForm();
            PopulateEmpList();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Int32 empId = Convert.ToInt32(btnSave.CommandArgument);

        if (empId == 0)
        {
            //INSERT
            LearnEFEntities db = new LearnEFEntities(); 
            Employee objEmp = new Employee();
            objEmp.HREmpId = txtHREmpId.Text;
            objEmp.FirstName = txtFirstName.Text;
            objEmp.LastName = txtLastName.Text;
            objEmp.Address = txtAddress.Text;
            objEmp.City = txtCity.Text;

            db.Employees.Add(objEmp);
            db.SaveChanges();
        }
        else
        {
            //UPDATE
            LearnEFEntities db = new LearnEFEntities();

            var empQuery = from emp in db.Employees
                           where emp.EmpId == empId
                           select emp;
            Employee objEmp = empQuery.Single();

            objEmp.HREmpId = txtHREmpId.Text;
            objEmp.FirstName = txtFirstName.Text;
            objEmp.LastName = txtLastName.Text;
            objEmp.Address = txtAddress.Text;
            objEmp.City = txtCity.Text;

            db.SaveChanges(); 
        }

        ClearEmpForm();
        PopulateEmpList();
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        PopulateEmpList();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Int32 empId = Convert.ToInt32(btnSave.CommandArgument);
        if (empId != 0)
        {
            LearnEFEntities db = new LearnEFEntities();

            Employee objEmp = new Employee() { EmpId = empId };
            db.Employees.Attach(objEmp);
            db.Employees.Remove(objEmp);
            db.SaveChanges();

            //reset the form and grid
            ClearEmpForm();
            PopulateEmpList();
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 empId = Convert.ToInt32(ddlEmployee.SelectedValue);

        if (empId == 0)
        {
            ClearEmpForm();
            return;
        }

        btnSave.CommandArgument = empId.ToString();
        btnSave.Text = "UPDATE";
        btnDelete.CommandArgument = empId.ToString();

        LearnEFEntities db = new LearnEFEntities();

        var empQuery = from emp in db.Employees
                       where emp.EmpId == empId
                       select emp;

        Employee objEmp = empQuery.Single();

        txtHREmpId.Text = objEmp.HREmpId;
        txtFirstName.Text = objEmp.FirstName;
        txtLastName.Text = objEmp.LastName;
        txtAddress.Text = objEmp.Address;
        txtCity.Text = objEmp.City;
    }

    private void PopulateEmpList()
    {
        LearnEFEntities db = new LearnEFEntities();
        //string ab = txtSrchFirstName.Text;
        //string ac = txtSrchCity.Text;
        //List<Employee> empList = db.SearchEmployee(txtSrchFirstName.Text, txtSrchCity.Text).ToList();
        List<Employee> empList = new List<Employee>();

        ddlEmployee.DataSource = empList;
        ddlEmployee.DataValueField = "EmpId";
        ddlEmployee.DataTextField = "FirstName";
        ddlEmployee.DataBind();

        ddlEmployee.Items.Insert(0, new ListItem("--Add New--", "0"));

        //bind grid
        grdEmployees.DataSource = empList;
        grdEmployees.DataBind();
    }

    private void ClearEmpForm()
    {
        txtHREmpId.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtAddress.Text = "";
        txtCity.Text = "";

        btnSave.CommandArgument = "0";
        btnSave.Text = "SAVE";
    }
}