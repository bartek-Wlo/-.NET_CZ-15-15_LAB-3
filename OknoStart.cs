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
            InitializeComponent();
            SetupForm1Controls();
        }

        private void SetupForm1Controls()
        {
            // Konfiguracja przycisku do otwierania istniejącego okna
            this.buttonOpenExisting = new Button();
            this.buttonOpenExisting.Location = new System.Drawing.Point(12, 12);
            this.buttonOpenExisting.Name = "buttonOpenExisting";
            this.buttonOpenExisting.Size = new System.Drawing.Size(260, 30);
            this.buttonOpenExisting.TabIndex = 0;
            this.buttonOpenExisting.Text = "Otwórz istniejące okno (komentarz)";
            this.buttonOpenExisting.UseVisualStyleBackColor = true;
            this.buttonOpenExisting.Click += new System.EventHandler(this.buttonOpenExisting_Click);

            // Konfiguracja przycisku do otwierania okna przetwarzania
            this.buttonOpenProcessing = new Button();
            this.buttonOpenProcessing.Location = new System.Drawing.Point(12, 52);
            this.buttonOpenProcessing.Name = "buttonOpenProcessing";
            this.buttonOpenProcessing.Size = new System.Drawing.Size(260, 30);
            this.buttonOpenProcessing.TabIndex = 1;
            this.buttonOpenProcessing.Text = "Otwórz okno przetwarzania obrazów";
            this.buttonOpenProcessing.UseVisualStyleBackColor = true;
            this.buttonOpenProcessing.Click += new System.EventHandler(this.buttonOpenProcessing_Click);

            // Konfiguracja Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 100); // Rozmiar okna Form1
            this.Controls.Add(this.buttonOpenProcessing);
            this.Controls.Add(this.buttonOpenExisting);
            this.Name = "Form1";
            this.Text = "Główne Okno";
            this.ResumeLayout(false);
        }


        private void buttonOpenExisting_Click(object sender, EventArgs e)
        {
            // ---------------------------------------------------------------
            // TUTAJ UMIEŚĆ KOD DO OTWARCIA ISTNIEJĄCEGO OKNA
            // Np.
            // ExistingForm existingForm = new ExistingForm();
            // existingForm.Show();
            // ---------------------------------------------------------------
            MessageBox.Show("Tutaj zostanie otwarte istniejące okno (kod niezaimplementowany).");
        }

        private void buttonOpenProcessing_Click(object sender, EventArgs e)
        {
            // Otwieranie nowego okna Form2
            Form3 form3 = new Form3();
            form3.Show(); // Użyj ShowDialog() jeśli chcesz, aby było modalne
        }

        // Uwaga: Poniższa metoda InitializeComponent jest zwykle generowana
        // automatycznie przez projektanta Windows Forms. Jeśli dodajesz kontrolki
        // ręcznie w kodzie (jak w SetupForm1Controls), upewnij się, że nie
        // koliduje to z kodem projektanta. Dla prostoty, tutaj InitializeComponent
        // jest puste, a kontrolki tworzone są w SetupForm1Controls.
        // W typowym projekcie kontrolki dodaje się w projektancie.
        private void InitializeComponent()
        {
             // Automatycznie generowana zawartość przez Projektanta Windows Forms
             // Zwykle inicjalizuje komponenty dodane w widoku projektanta.
             // W tym przykładzie kontrolki dodano ręcznie w SetupForm1Controls
             // więc ta metoda może pozostać pusta lub zawierać inicjalizację
             // wygenerowaną przez projektanta, jeśli go używasz.
            this.SuspendLayout();
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "Form1";
            this.ResumeLayout(false);
        }
    }
}
