using System.Linq;
using System.Windows.Forms;
using ApplicationA.Controllers;

namespace ApplicationA
{
    /// <summary>
    /// Windows Main form that presents the views
    /// </summary>
    public partial class MasterDetailfrm : Form
    {
        private readonly MasterDetailController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterDetailfrm"/> class.
        /// </summary>
        public MasterDetailfrm()
        {
            InitializeComponent();
            //just to show it's possible to give a custom url on the controllers constructor
            controller = new MasterDetailController
                             {
                                 UrlY = "http://localhost:2316/WebServiceY.asmx",
                                 UrlX = "http://localhost:1965/ServiceX.svc",
                             };
            masterGrid.Customersdgv.DataSource = controller.Customers;
        }

        /// <summary>
        /// handles the row enter event of Master grid(Customersdgvs)
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void CustomersdgvRowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                var customerId =
                    controller.Customers.Find(
                        c => c.CustomerId == masterGrid.Customersdgv.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString()).CustomerId;
                controller.GetCustomer(customerId);
            }
            if (Equals(controller.Customer, null)) return;
            AddBindingsForTextboxes();
        }


        /// <summary>
        /// Adds the bindings for textboxes, clears them first.
        /// </summary>
        private void AddBindingsForTextboxes()
        {
            foreach (var control in detailOverview.Controls.Cast<object>().Where(control => control.GetType() == typeof (TextBox)))
            {
                ((TextBox)control).DataBindings.Clear();
            }
            //Here we bind the customer properties to the textbox text properties
            //this gives us a WPF like binding, not as strong binding but is works.
            detailOverview.txtAddress.DataBindings.Add("Text", controller.Customer, "Address", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtCustomerName.DataBindings.Add("Text", controller.Customer,"CustomerId", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtCity.DataBindings.Add("Text", controller.Customer, "City", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtPostalCode.DataBindings.Add("Text", controller.Customer, "PostalCode", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtPhone.DataBindings.Add("Text", controller.Customer, "Phone", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtFax.DataBindings.Add("Text", controller.Customer, "Fax", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtCompanyName.DataBindings.Add("Text", controller.Customer, "CompanyName", false, DataSourceUpdateMode.OnPropertyChanged);
            detailOverview.txtContactName.DataBindings.Add("Text", controller.Customer, "ContactName", false, DataSourceUpdateMode.OnPropertyChanged);
        }
    }
}
