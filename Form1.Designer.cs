namespace ParallelWatki
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1880, 890);  // Zwiększyłem rozmiar formularza

            // TextBoxy dla macierzy
            this.matrixTextBox1 = new System.Windows.Forms.TextBox();
            this.matrixTextBox2 = new System.Windows.Forms.TextBox();
            this.matrixTextBox3 = new System.Windows.Forms.TextBox();

            this.threadCountButton = new System.Windows.Forms.Button(); // Przycisk Liczba wątków
            this.sizeButton = new System.Windows.Forms.Button();        // Przycisk Size
            this.calculateButton = new System.Windows.Forms.Button();   // Przycisk Oblicz
            this.FinalRaportTextBox = new System.Windows.Forms.TextBox();

            // Inicjalizacja kontrolek
            // Matrix TextBox1
            this.matrixTextBox1.Location = new System.Drawing.Point(20, 20);
            this.matrixTextBox1.Multiline = true;
            this.matrixTextBox1.Size = new System.Drawing.Size(600, 600);  // Ustawiłem większy rozmiar
            this.matrixTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.matrixTextBox1.WordWrap = false;
            this.matrixTextBox1.ReadOnly = true;
            this.matrixTextBox1.Font = new System.Drawing.Font("Courier New", 10);

            // Matrix TextBox2
            this.matrixTextBox2.Location = new System.Drawing.Point(640, 20);
            this.matrixTextBox2.Multiline = true;
            this.matrixTextBox2.Size = new System.Drawing.Size(600, 600);
            this.matrixTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.matrixTextBox2.WordWrap = false;
            this.matrixTextBox2.ReadOnly = true;
            this.matrixTextBox2.Font = new System.Drawing.Font("Courier New", 10);

            // Matrix TextBox3
            this.matrixTextBox3.Location = new System.Drawing.Point(1260, 20);
            this.matrixTextBox3.Multiline = true;
            this.matrixTextBox3.Size = new System.Drawing.Size(600, 600);
            this.matrixTextBox3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.matrixTextBox3.WordWrap = false;
            this.matrixTextBox3.ReadOnly = true;
            this.matrixTextBox3.Font = new System.Drawing.Font("Courier New", 10);




            // Przycisk Liczba wątków
            this.threadCountButton.Location = new System.Drawing.Point(20, 620);
            this.threadCountButton.Size = new System.Drawing.Size(600, 30);
            this.threadCountButton.Text = "Liczba wątków";
            this.threadCountButton.Click += new System.EventHandler(this.threadCountButton_Click);

            // Przycisk Size
            this.sizeButton.Location = new System.Drawing.Point(640, 620);
            this.sizeButton.Size = new System.Drawing.Size(600, 30);
            this.sizeButton.Text = "Size";
            this.sizeButton.Click += new System.EventHandler(this.sizeButton_Click);

            // Przycisk Oblicz
            this.calculateButton.Location = new System.Drawing.Point(1260, 620);
            this.calculateButton.Size = new System.Drawing.Size(600, 30);
            this.calculateButton.Text = "Oblicz";
            this.calculateButton.Click += new System.EventHandler(this.calculateButton_Click);




            // Matrix FinalRaportTextBox
            this.FinalRaportTextBox.Location = new System.Drawing.Point(20, 670);
            this.FinalRaportTextBox.Multiline = true;
            this.FinalRaportTextBox.Size = new System.Drawing.Size(1840, 200);
            this.FinalRaportTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FinalRaportTextBox.WordWrap = false;
            this.FinalRaportTextBox.ReadOnly = true;
            //this.FinalRaportTextBox.Font = new System.Drawing.Font("Courier New", 10);




            // Dodanie kontrolek do formularza
            this.Controls.Add(this.matrixTextBox1);
            this.Controls.Add(this.matrixTextBox2);
            this.Controls.Add(this.matrixTextBox3);
            this.Controls.Add(this.FinalRaportTextBox);
            this.Controls.Add(this.threadCountButton);
            this.Controls.Add(this.sizeButton);
            this.Controls.Add(this.calculateButton);

            this.Text = "Form1";
        }

        #endregion

        private System.Windows.Forms.TextBox matrixTextBox1;
        private System.Windows.Forms.TextBox matrixTextBox2;
        private System.Windows.Forms.TextBox matrixTextBox3;
        private System.Windows.Forms.TextBox FinalRaportTextBox;
        private System.Windows.Forms.Button threadCountButton;
        private System.Windows.Forms.Button sizeButton;
        private System.Windows.Forms.Button calculateButton;
        private int ThreadNum = 0, MatrixSize = 0;


        // Event handler dla przycisków
        private void threadCountButton_Click(object sender, EventArgs e)
        {
            ButtonToInt Read = new ButtonToInt();
            Read.ShowDialog();
            ThreadNum = Read.GetLiczba();
            FinalRaportTextBox.Text += "Thread number = "+ThreadNum.ToString() +Environment.NewLine;
        }

        private void sizeButton_Click(object sender, EventArgs e)
        {
            ButtonToInt Read = new ButtonToInt();
            Read.ShowDialog();
            MatrixSize = Read.GetLiczba();
            FinalRaportTextBox.Text += "Matrix Size = "+MatrixSize.ToString() +Environment.NewLine;
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            if (MatrixSize <= 0 || ThreadNum <= 0) {
                MessageBox.Show("Złe wartości dla Liczby Wątków oraz Rozmiaru kwadratowej macierzy.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MatrixMultiplication multiplication = new MatrixMultiplication(MatrixSize, ThreadNum);
            FinalRaportTextBox.Text += multiplication.calculations();
            matrixTextBox1.Text = MatrixMultiplication.TabToString(multiplication.tablica1);
            matrixTextBox2.Text = MatrixMultiplication.TabToString(multiplication.tablica2);
            matrixTextBox3.Text = MatrixMultiplication.TabToString(multiplication.wynik);
        }
    }
}
