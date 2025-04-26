using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text.Json;
using PasswordManager.Helpers;

namespace PasswordManager
{
    public partial class Login : Form
    {
        private const string passwordsFile = "passwords.json"; 
        private static List<UserData> users = new List<UserData>(); 

        public UserData User { get; private set; }

        public Login()
        {
            InitializeComponent();

            //Diseño
            DesignHelper.StyleFormGeneral(this);
            DesignHelper.StyleTextBox(txtUser);
            DesignHelper.StyleTextBox(txtMasterPassword);
            DesignHelper.StyleLabel(lblUsername);
            DesignHelper.StyleLabel(lblPassword);
            DesignHelper.StyleButtonSimple(btnLogin);
            DesignHelper.StyleButtonRegistro(btnRegistro);
            DesignHelper.PositionButton(btnLogin, 170, 215);
            DesignHelper.PositionButton(btnRegistro, 150, 285);
            List<TextBox> textBoxes = new List<TextBox> {  txtUser, txtMasterPassword };
            List<Label> labels = new List<Label> { lblUsername, lblPassword };
            DesignHelper.ArrangeTextBoxesAndLabels(this, textBoxes, labels, 10, 50, 100);
            DesignHelper.StyleTitle(lblManager);
        }

      
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtUser.Text.Trim();  // Obtener el correo electrónico
            string password = txtMasterPassword.Text.Trim();

            // Cargar los usuarios del archivo JSON si es la primera vez que se ejecuta
            LoadUsers();

            // Verificar si el correo está registrado
            UserData user = users.Find(u => u.User == email);

            if (user != null && user.Password == password)
            {
                this.Hide();
                MainForm main = new MainForm(user); 
                main.Show();
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMasterPassword.Clear();
                txtMasterPassword.Focus();
            }
        }


        // Cargar los usuarios del archivo JSON
        private void LoadUsers()
        {
            if (File.Exists(passwordsFile))
            {
                string json = File.ReadAllText(passwordsFile);
                users = JsonSerializer.Deserialize<List<UserData>>(json) ?? new List<UserData>();
            }
        }

        // Guardar los usuarios en el archivo JSON
        private void SaveUsers()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(users, options);
            File.WriteAllText(passwordsFile, json);
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
        string user = txtUser.Text.Trim();
        string password = txtMasterPassword.Text.Trim();

        // Cargar los usuarios del archivo JSON si es la primera vez que se ejecuta
        LoadUsers();

            // Verificar si el correo ya está registrado
            if (users.Exists(u => u.User == user))
            {
                MessageBox.Show("El correo ya está registrado. Usa la opción de login.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (user.Length==0 & password.Length ==0) {
                MessageBox.Show("Debes indicar usuario y contraseña", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        // Si no está registrado, agregarlo a la lista de usuarios
        UserData newUser = new UserData
        {
            User = user,
            Password = password,
            PasswordSites = new List<PasswordEntry>()
               
        };

            // Agregar el nuevo usuario a la lista
            users.Add(newUser);

            // Guardar los usuarios en el archivo JSON
            SaveUsers();

            MessageBox.Show("¡Te has registrado con éxito! Ahora puedes iniciar sesión.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }

  
    

   
}
