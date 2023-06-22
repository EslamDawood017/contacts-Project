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
    public partial class frmAddandEditeContacts : Form
    {
        public enum enMode { Uppdate = 1, AddNew = 2 }
        private enMode _Mode;

        int _contactID;
        clsContact _Contact;

        public frmAddandEditeContacts(int contactID)
        {
            InitializeComponent();
            _contactID = contactID;

            if (_contactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Uppdate;
        }
        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountry.GetAllCountries();
            foreach (DataRow dt in dtCountries.Rows)
            {
                cbCountry.Items.Add(dt["CountryName"]);
            }
        }
        private void _LoadData()
        {
            _FillCountriesInComboBox();
            cbCountry.SelectedIndex = 0;
            if (_Mode == enMode.AddNew)
            {
                MainLabel.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }
            _Contact = clsContact.Find(_contactID);

            if (_Contact == null)
            {
                MessageBox.Show("this Form will be Closed because the Contact Not Found");
                this.Close();
                return;
            }
            MainLabel.Text = "Edite Contatct with ID = " + _contactID;
            txtContactId.Text = _contactID.ToString();
            txtFristName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtPhone.Text = _Contact.Phone;
            txtAdress.Text = _Contact.Address;
            dateTimePicker1.Value = _Contact.DateOfBirth;
            if (_Contact.ImageBath != "")
            {
                pictureBox1.Load(_Contact.ImageBath);
            }

            lbRemove.Visible = (_Contact.ImageBath != "");

            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.Find(_Contact.CountryID).CountryName);


        }

        private void frmAddandEditeContacts_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int counteryId = clsCountry.Find(cbCountry.Text).CountryId;

            _Contact.FirstName = txtFristName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Address = txtAdress.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.CountryID = counteryId;
            _Contact.DateOfBirth = dateTimePicker1.Value;
            
            if (pictureBox1.ImageLocation != null)
            {
                _Contact.ImageBath = pictureBox1.ImageLocation;
            }
            else
            {
                _Contact.ImageBath = "";
            }
            if (_Contact.Save())
            {
                MessageBox.Show("Data Safed Successfully");
            }
            else
            {
                MessageBox.Show("Error:Data Was Not Safed Successfully");
            }
            _Mode = enMode.Uppdate;
            MainLabel.Text = "Edite Contatct with ID = " + _Contact.Id;
            txtContactId.Text = _Contact.Id.ToString();
        }
    }
}
