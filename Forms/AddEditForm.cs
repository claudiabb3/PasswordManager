using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PasswordManager;
using PasswordManager.Helpers;

namespace PasswordManager
{
    public partial class AddEditForm : Form
    {
        public string userPassword;
        public PasswordEntry PasswordEntry { get; set; }
        public AddEditForm(string userPassword,PasswordEntry entry = null)
        {
            InitializeComponent();

            //diseño
            DesignHelper.StyleFormAdd(this);
            DesignHelper.StyleTextBox2(txtUser);
            DesignHelper.StyleTextBox2(txtPassword);
            DesignHelper.StyleTextBox2(txtSite);
            DesignHelper.StyleLabel2(lbSitio);
            DesignHelper.StyleLabel2(lbContraseña);
            DesignHelper.StyleLabel2(lbUsuario);
            DesignHelper.StyleButtonSimple(btnSave);
            DesignHelper.PositionButton(btnSave, 140,345);
            //fin diseño

            this.userPassword = userPassword;
            if (entry != null)
            {
               
                PasswordEntry = entry;
                txtSite.Text = entry.Site;
                txtUser.Text = entry.Username;
                txtPassword.Text = entry.Password;
            }
            else
            {
                PasswordEntry = new PasswordEntry();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
          
            // Validación 
            if (string.IsNullOrEmpty(txtSite.Text) || string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Asignar los valores al objeto PasswordEntry
            PasswordEntry.Site = txtSite.Text;
            PasswordEntry.Username = txtUser.Text;
            PasswordEntry.Password = txtPassword.Text;

            DialogResult = DialogResult.OK; 
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }
    }
}
