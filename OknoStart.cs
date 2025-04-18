using System;
using System.Windows.Forms;

namespace ParallelWatki
{
    public partial class Form0 : Form
    {
        private Button buttonOpenExisting;
        private Button buttonOpenProcessing;

        public Form0()
        {
            this.buttonOpenExisting = new Button();
            this.buttonOpenProcessing = new Button();
            InitializeComponent();
            SetupForm1Controls();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout(); // Wstrzymuje odświerzanie okienka
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 300); // Rozmiar okna Form1
            this.Name = "Wybor";
            this.Text = "Wybierz Aplikację do uruchomienia.";
        }

        private void SetupForm1Controls()
        {
            // Konfiguracja przycisku do otwierania istniejącego okna
            this.buttonOpenExisting.Location = new System.Drawing.Point(20, 20);
            this.buttonOpenExisting.Name = "buttonOpenExisting";
            this.buttonOpenExisting.Size = new System.Drawing.Size(560, 120);
            this.buttonOpenExisting.TabIndex = 0;
            this.buttonOpenExisting.Text = "Otwórz okno Porównanie pracy Thread i Parallel";
            this.buttonOpenExisting.UseVisualStyleBackColor = true;
            this.buttonOpenExisting.Click += new System.EventHandler(this.buttonOpenExisting_Click);

            // Konfiguracja przycisku do otwierania okna przetwarzania
            this.buttonOpenProcessing.Location = new System.Drawing.Point(20, 160);
            this.buttonOpenProcessing.Name = "buttonOpenProcessing";
            this.buttonOpenProcessing.Size = new System.Drawing.Size(560, 120);
            this.buttonOpenProcessing.TabIndex = 1;
            this.buttonOpenProcessing.Text = "Otwórz okno przetwarzania obrazów";
            this.buttonOpenProcessing.UseVisualStyleBackColor = true;
            this.buttonOpenProcessing.Click += new System.EventHandler(this.buttonOpenProcessing_Click);

            this.Controls.Add(this.buttonOpenProcessing);
            this.Controls.Add(this.buttonOpenExisting);
            this.ResumeLayout(false); // Wznawaia odświerzanie okienka
        }


        private void buttonOpenExisting_Click(object? sender, EventArgs e)
        {
            Form12 form12 = new Form12();
            form12.Show();
        }

        private void buttonOpenProcessing_Click(object? sender, EventArgs e)
        {
            // Otwieranie nowego okna Form2
            Form3 form3 = new Form3();
            form3.Show(); // showDialog() <-- Zablokuje oryginalne okno (modalne)
        }
    }
}
