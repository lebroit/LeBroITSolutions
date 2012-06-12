using System.Windows.Controls;
using ApplicationB.Controllers;
using DomainModel;

namespace ApplicationB.Views
{
    /// <summary>
    /// Interaction logic for MasterGrid.xaml
    /// </summary>
    public partial class MasterGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterGrid"/> class.
        /// </summary>
        public MasterGrid()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the datagrid customers selection changed.
        /// By the way, could have done this with a command, but this solution
        /// is to demonstrate the usage of connectors not WPF in general.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void DgCustomersSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers.SelectedValue == null ) return;
            ((MasterDetailController) (DataContext)).GetCustomer(((Customer) (dgCustomers.SelectedValue)).CustomerId);
        }
    }
}
