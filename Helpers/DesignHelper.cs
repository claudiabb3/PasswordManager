using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PasswordManager.Helpers
{
    using FontAwesome.Sharp;
    using System.Drawing;
    using System.Drawing.Text;
    using System.IO;
    using System.Windows.Forms;

    public static class DesignHelper
    {
        private static PrivateFontCollection privateFonts = new PrivateFontCollection();
        private static bool fontLoaded = false;
        private static string fontPath = Path.Combine(Application.StartupPath, "Fonts", "PressStart2P-Regular.ttf");
      
        //Estilo para los botones con icono
       public static void StyleButtonIcon(IconButton btn, IconChar icon)
        {
            btn.IconChar = icon;
            btn.IconColor = Color.White;
            btn.IconSize = 28;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.BackColor = Color.FromArgb(40, 40, 60);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = LoadFont(9);
            btn.Size = new Size(190, 52);
            
        }

        //Estilo para el formulario de inicio
        public static void StyleFormInicio(Form form)
        {
            form.BackColor = Color.FromArgb(30, 30, 45); // Color general del fondo
            form.FormBorderStyle = FormBorderStyle.FixedDialog; // No redimensionable
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Size = new Size(870, 610);
        }

        //Estilo para datagridview
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Color.FromArgb(30, 30, 45);
            dgv.ForeColor = Color.White;
            dgv.GridColor = Color.Gray;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor= Color.FromArgb(40, 40, 60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.MediumTurquoise;
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 60);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 70, 90);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgv.BorderStyle = BorderStyle.None;
            dgv.Size = new Size(770, 410);
            dgv.DefaultCellStyle.Font = LoadFont(9);
            dgv.ColumnHeadersDefaultCellStyle.Font = LoadFont(12);
        }

        //Ubicacion de los botones con icono, venatan inicio
        public static void ArrangeButtonsHorizontally(Form form, List<IconButton> buttons, int buttonWidth, int spacing)
        {
            int totalWidth = buttons.Count * buttonWidth + (buttons.Count + 1) * spacing;
            int startX = (form.ClientSize.Width - totalWidth) / 2 + spacing;
            int y = 480;

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Location = new Point(startX + i * (buttonWidth + spacing), y);
            }
        }

        //Estilo para la ventana de login
        public static void StyleFormGeneral(Form form)
        {
            form.BackColor = Color.FromArgb(30, 30, 45);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
        }
        
        //Estilo para la venatana añadir
        public static void StyleFormAdd(Form form)
        {
            form.BackColor = Color.FromArgb(30, 30, 45);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Size = new Size(452, 497);
        }

        //Estilo textBox (usados en login)
        public static void StyleTextBox(TextBox txt)
        {
            txt.BackColor = Color.FromArgb(40, 40, 60);
            txt.ForeColor = Color.White;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = LoadFont(10);
            txt.Size = new Size(234, 50);
        }
        //Estilo para textBox (usados en ventana añadir)
        public static void StyleTextBox2(TextBox txt)
        {
            txt.BackColor = Color.FromArgb(40, 40, 60);
            txt.ForeColor = Color.MediumTurquoise;
            txt.BorderStyle = BorderStyle.Fixed3D;
            txt.Font = LoadFont(12);
            txt.Size = new Size(377, 60);
           
            
        }
        //Estilo Label (usados en login)
        public static void StyleLabel(Label lbl)
        {
            lbl.ForeColor = Color.White;
            lbl.Font = LoadFont(10);
            lbl.Size = new Size(234, 50);
        }
        //Estilo para label (usados en ventana añadir)
        public static void StyleLabel2(Label lbl)
        {
            lbl.ForeColor = Color.White;
            lbl.Font = LoadFont(12);
            lbl.Size = new Size(377, 34);
        }
        //Estilo titulo (venanta login)
        public static void StyleTitle(Label lbl) {
            lbl.ForeColor = Color.Pink;
            lbl.Font = LoadFont(18);
            lbl.Size = new Size(377, 50);
            lbl.Location = new Point(38, 36);
        }
        //Estilo boton (para ventana login y añadir)
        public static void StyleButtonSimple(Button btn)
        {
            btn.BackColor = Color.FromArgb(113,1,174);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Standard;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = LoadFont( 10);
            btn.Size = new Size(120, 40);
        }
        //Estilo boton registro de login
        public static void StyleButtonRegistro(Button btn)
        {
            btn.BackColor = Color.FromArgb(30, 30, 45);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = LoadFont(7);
            btn.Size = new Size(170, 30);
        }
        //Posicionamiento de boton (ventana login y añadir)
        public static void PositionButton(Button btn, int x, int y)
        {
            btn.Location = new Point(x, y);
        }

        // Método para cargar la fuente pixelada
        public static Font LoadFont(float size)
        {
            if (!fontLoaded)
            {
                if (File.Exists(fontPath))
                {
                    privateFonts.AddFontFile(fontPath);
                    fontLoaded = true;
                }
                else
                {
                    throw new FileNotFoundException($"No se encontró la fuente: {fontPath}");
                }
            }

            return new Font(privateFonts.Families[0], size);
        }
        //Posicionamiento label y textbox de login
        public static void ArrangeTextBoxesAndLabels(Form form, List<TextBox> textBoxes, List<Label> labels, int spacing, int startX, int startY)
        {
            // Validar que haya el mismo número de labels que de textboxes
            if (textBoxes.Count != labels.Count)
            {
                throw new ArgumentException("El número de TextBox y Label deben coincidir.");
            }

            // Ajustamos la posición de los Labels y TextBox
            for (int i = 0; i < textBoxes.Count; i++)
            {
                // Posicionar el Label
                labels[i].Location = new Point(startX, startY + i * (spacing + 30)); // Espaciado y tamaño de label

                // Posicionar el TextBox
                textBoxes[i].Location = new Point(startX + labels[i].Width + 15, startY + i * (spacing +30)); // 10 de margen entre Label y TextBox

                // ajustar el tamaño de los TextBox
                textBoxes[i].Size = new Size(200, 40); 
            }
        }
    }
}

