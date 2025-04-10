using System;
using System.Windows.Forms;


namespace ParallelWatki
{
    public partial class ButtonToInt : Form
    {
        private TextBox txtLiczba;
        private Button btnZatwierdz;
        private int WprowadzonaLiczba;
        public int GetLiczba() {return WprowadzonaLiczba;}

        public ButtonToInt()
        {
            txtLiczba = new TextBox();
            btnZatwierdz = new Button();
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.Text = "Wprowadź Liczbę";
            this.Size = new System.Drawing.Size(300, 155);
            this.StartPosition = FormStartPosition.CenterParent; // Wyświetlaj na środku okna nadrzędnego

            // Dodaj etykietę
            Label lblWprowadzLiczbe = new Label();
            lblWprowadzLiczbe.Text = "Wprowadź liczbę całkowitą:";
            lblWprowadzLiczbe.Location = new System.Drawing.Point(10, 10);
            lblWprowadzLiczbe.Size = new System.Drawing.Size(200, 20);
            Controls.Add(lblWprowadzLiczbe);

            // Dodaj pole tekstowe do wprowadzania liczby
            txtLiczba.Location = new System.Drawing.Point(10, 40);
            txtLiczba.Size = new System.Drawing.Size(260, 20);
            Controls.Add(txtLiczba);

            // Dodaj przycisk zatwierdzający
            btnZatwierdz.Text = "Zatwierdź";
            btnZatwierdz.Location = new System.Drawing.Point(10, 70);
            btnZatwierdz.Size = new System.Drawing.Size(100, 30);
            btnZatwierdz.Click += BtnZatwierdz_Click;
            Controls.Add(btnZatwierdz);

            // ENTER
            this.KeyPreview = true;
            this.KeyDown += FormWprowadzLiczbe_KeyDown;
        }


        private void BtnZatwierdz_Click(object? sender, EventArgs e)
        {
            if (int.TryParse(txtLiczba.Text, out int liczba))
            {
                WprowadzonaLiczba = liczba;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else MessageBox.Show("Wprowadź poprawną liczbę całkowitą.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        private void FormWprowadzLiczbe_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                BtnZatwierdz_Click(sender, e);
                e.Handled = true;          /* Informuje OS że Kod zają się obsługą zdarzenia */
                e.SuppressKeyPress = true; /* Znak nie zostanie wprowadzony (Nie doda \n do pola textowego) */
            } else if (e.KeyCode == Keys.Escape) {
                this.DialogResult = DialogResult.Cancel;  // Można zwrócić "Cancel" dla Escape
                this.Close();
            }
        }
    }
}