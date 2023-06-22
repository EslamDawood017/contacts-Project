using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmShowAllContact : Form
    {
        public frmShowAllContact()
        {
            InitializeComponent();
        }
        private void _RefreshContactList()
        {
            dgvAllContacts.DataSource = clsContact.GetAllConatcts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshContactList();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddandEditeContacts frm = new frmAddandEditeContacts((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete contact [" + dgvAllContacts.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsContact.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Contact Deleted Successfully.");
                    _RefreshContactList();
                }

                else
                    MessageBox.Show("Contact is not deleted.");

            }
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            frmAddandEditeContacts frm = new frmAddandEditeContacts(-1);
            frm.ShowDialog();
            _RefreshContactList();
        }
    }
}
