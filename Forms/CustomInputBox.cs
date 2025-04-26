using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace PasswordManager
{
    public static class CustomInputBox
    {
        static string fontPath = Path.Combine(Application.StartupPath, "Fonts", "PressStart2P-Regular.ttf");

        private static Font LoadFont(float size)
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile(fontPath);
            return new Font(pfc.Families[0], size, FontStyle.Regular);
        }

        public static string Show(string prompt, string title)
        {
            //diseño ventana
            Form promptForm = new Form()
            {
                Width = 500,
                Height = 270,
                Text = title,
                BackColor = Color.FromArgb(30, 30, 45),
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            //diseño label
            Label textLabel = new Label()
            {
                Left = 20,
                Top = 20,
                Text = prompt,
                ForeColor = Color.MediumTurquoise,
                Width = 440,
                Font = LoadFont(8f),
                BackColor = Color.Transparent
            };
            //diseño textbox
            TextBox textBox = new TextBox()
            {
                Left = 20,
                Top = 80,
                Width = 440,
                BackColor = Color.FromArgb(40, 40, 60),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = LoadFont(8f),
                PasswordChar = '*'
            };

            //diseño boton
            Button confirmation = new Button()
            {
                Text = "ACEPTAR",
                Left = 190,
                Width = 120,
                Top = 140,
                BackColor = Color.FromArgb(227, 0, 128),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = LoadFont(8f),
                DialogResult = DialogResult.OK
            };
            confirmation.FlatAppearance.BorderSize = 0;
            confirmation.Click += (sender, e) => { promptForm.Close(); };

            promptForm.Controls.Add(textLabel);
            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(confirmation);
            promptForm.AcceptButton = confirmation;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }
    }
}
