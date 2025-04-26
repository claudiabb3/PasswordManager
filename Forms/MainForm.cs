using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FontAwesome.Sharp;
using PasswordManager.Helpers;
using PasswordManager.Services;


namespace PasswordManager
{
    public partial class MainForm : Form
    {
        private List<UserData> users;
        private UserData currentUser;

        public MainForm(UserData user)
        {
            InitializeComponent();

            //diseño
            DesignHelper.StyleFormInicio(this);              
            DesignHelper.StyleButtonIcon(btnAdd, IconChar.Plus);
            DesignHelper.StyleButtonIcon(btnEdit, IconChar.Edit);
            DesignHelper.StyleButtonIcon(btnDelete, IconChar.Trash);
            DesignHelper.StyleButtonIcon(btnRecover, IconChar.UnlockAlt);
            DesignHelper.StyleDataGridView(dgvPass);    
            DesignHelper.ArrangeButtonsHorizontally(this, new List<IconButton> { btnAdd, btnEdit, btnDelete, btnRecover }, 200,7);
            //FIN DISEÑO


            currentUser = user;
            users = UserService.LoadUsers();
            generarTabla();
            LoadPasswords();

       
        }

        private void generarTabla()
        {
            dgvPass.AutoGenerateColumns = false;
            dgvPass.Columns.Clear();

            dgvPass.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Sitio",
                DataPropertyName = "Site",
                Name = "Site"
            });

            dgvPass.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Usuario",
                DataPropertyName = "Username",
                Name = "Username"
            });

            dgvPass.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Contraseña",
                DataPropertyName = "Password",
                Name = "Password",
                Visible = false
            });
        }

        private void LoadPasswords()
        {
            dgvPass.DataSource = null;
            dgvPass.DataSource = currentUser.PasswordSites;
        }
    
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var addForm = new AddEditForm(currentUser.Password);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                PasswordService.AddPassword(currentUser, addForm.PasswordEntry, currentUser.Password);
                UserService.UpdateUser(users, currentUser);
                LoadPasswords();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPass.SelectedRows.Count > 0)
            {
                var selected = (PasswordEntry)dgvPass.SelectedRows[0].DataBoundItem;
                var editForm = new AddEditForm(currentUser.Password, selected);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    PasswordService.UpdatePassword(currentUser, selected, currentUser.Password);
                    UserService.UpdateUser(users, currentUser);
                    LoadPasswords();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPass.SelectedRows.Count > 0)
            {
                var selected = (PasswordEntry)dgvPass.SelectedRows[0].DataBoundItem;
                PasswordService.DeletePassword(currentUser, selected);
                UserService.UpdateUser(users, currentUser);
                LoadPasswords();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRecover_Click(object sender, EventArgs e)
        {
            if (dgvPass.SelectedRows.Count > 0)
            {
                var selected = (PasswordEntry)dgvPass.SelectedRows[0].DataBoundItem;
                string enteredPassword = CustomInputBox.Show("Introduce tu contraseña maestra:", "Verificación de identidad");

                if (string.IsNullOrEmpty(enteredPassword))
                {
                    MessageBox.Show("Operación cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (enteredPassword == currentUser.Password)
                {
                    string decryptedPassword = PasswordService.RecoverPassword(currentUser, selected, currentUser.Password);
                    MessageBox.Show($"Contraseña recuperada: {decryptedPassword}", "Recuperación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Contraseña incorrecta. No se puede mostrar la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un sitio para recuperar la contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
