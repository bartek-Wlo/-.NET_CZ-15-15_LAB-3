using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO; // Potrzebne dla obsługi plików

namespace ParallelWatki
{
    public partial class Form3 : Form
    {
        private Button buttonLoadFile;
        private Button buttonProcess;
        private PictureBox pictureBoxOriginal;
        private PictureBox pictureBoxResult1;
        private PictureBox pictureBoxResult2;
        private PictureBox pictureBoxResult3;
        private PictureBox pictureBoxResult4;
        private SplitContainer splitContainerMain;
        private TableLayoutPanel tableLayoutPanelResults;

        public Form3()
        {
            InitializeComponent(); // Ważne, aby wywołać metodę generowaną przez projektanta
            SetupLayout();         // Metoda do ustawienia kontrolek programowo
        }

        private void SetupLayout()
        {
            this.SuspendLayout();

            // --- Kontrolki Główne ---
            this.buttonLoadFile = new Button();
            this.buttonProcess = new Button();
            this.splitContainerMain = new SplitContainer();
            this.pictureBoxOriginal = new PictureBox();
            this.tableLayoutPanelResults = new TableLayoutPanel();
            this.pictureBoxResult1 = new PictureBox();
            this.pictureBoxResult2 = new PictureBox();
            this.pictureBoxResult3 = new PictureBox();
            this.pictureBoxResult4 = new PictureBox();

            // --- Konfiguracja Form2 ---
            this.ClientSize = new Size(1880, 890);
            this.Text = "Przetwarzanie Obrazu";
            this.Name = "Form2";

            // --- Przycisk Wczytywania Pliku (Góra) ---
            this.buttonLoadFile.Dock = DockStyle.Top;
            this.buttonLoadFile.Height = 40; // Wysokość przycisku
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Text = "Wczytaj plik";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new EventHandler(this.buttonLoadFile_Click);

            // --- Przycisk Przetwarzania (Dół) ---
            this.buttonProcess.Dock = DockStyle.Bottom;
            this.buttonProcess.Height = 40; // Wysokość przycisku
            this.buttonProcess.Name = "buttonProcess";
            this.buttonProcess.Text = "Uruchom przekształcanie";
            this.buttonProcess.UseVisualStyleBackColor = true;
            this.buttonProcess.Click += new EventHandler(this.buttonProcess_Click);

            // --- SplitContainer (Dzieli obszar na lewy i prawy) ---
            this.splitContainerMain.Dock = DockStyle.Fill; // Wypełnia przestrzeń między przyciskami
            this.splitContainerMain.Orientation = Orientation.Vertical; // Podział pionowy (lewo/prawo)
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.BorderStyle = BorderStyle.Fixed3D;
            // Ustawienie początkowej pozycji podziału (około połowy)
            this.splitContainerMain.SplitterDistance = this.ClientSize.Width / 2;


            // --- Panel Lewy (w SplitContainer) ---
            // PictureBox dla Oryginalnego Obrazu
            this.pictureBoxOriginal.Dock = DockStyle.Fill; // Wypełnia lewy panel
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxOriginal.SizeMode = PictureBoxSizeMode.Zoom; // Skaluje obraz zachowując proporcje
            this.splitContainerMain.Panel1.Controls.Add(this.pictureBoxOriginal);

            // --- Panel Prawy (w SplitContainer) ---
            // TableLayoutPanel do ułożenia 4 obrazów wynikowych
            this.tableLayoutPanelResults.Dock = DockStyle.Fill; // Wypełnia prawy panel
            this.tableLayoutPanelResults.Name = "tableLayoutPanelResults";
            this.tableLayoutPanelResults.ColumnCount = 2;
            this.tableLayoutPanelResults.RowCount = 2;
            // Ustawienie kolumn na równą szerokość (50%)
            this.tableLayoutPanelResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutPanelResults.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            // Ustawienie wierszy na równą wysokość (50%)
            this.tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.tableLayoutPanelResults.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            this.splitContainerMain.Panel2.Controls.Add(this.tableLayoutPanelResults);

            // --- PictureBoxy Wynikowe (w TableLayoutPanel) ---
            ConfigureResultPictureBox(this.pictureBoxResult1, "pictureBoxResult1", 0, 0);
            ConfigureResultPictureBox(this.pictureBoxResult2, "pictureBoxResult2", 1, 0);
            ConfigureResultPictureBox(this.pictureBoxResult3, "pictureBoxResult3", 0, 1);
            ConfigureResultPictureBox(this.pictureBoxResult4, "pictureBoxResult4", 1, 1);

            // --- Dodawanie Kontrolek do Form2 ---
            // Kolejność ma znaczenie dla DockStyle.Fill
            this.Controls.Add(this.splitContainerMain); // Najpierw Fill
            this.Controls.Add(this.buttonLoadFile);     // Potem Top
            this.Controls.Add(this.buttonProcess);      // Potem Bottom

            this.ResumeLayout(false);
        }

        // Metoda pomocnicza do konfiguracji PictureBoxów wynikowych
        private void ConfigureResultPictureBox(PictureBox pb, string name, int column, int row)
        {
            pb.Dock = DockStyle.Fill; // Wypełnia komórkę tabeli
            pb.Name = name;
            pb.BorderStyle = BorderStyle.FixedSingle;
            pb.SizeMode = PictureBoxSizeMode.Zoom; // Skaluje obraz zachowując proporcje
            this.tableLayoutPanelResults.Controls.Add(pb, column, row); // Dodaje do konkretnej komórki
        }


        // --- Obsługa Zdarzeń ---

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Pliki obrazów (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Wszystkie pliki (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Wczytaj wybrany plik obrazu
                        string filePath = openFileDialog.FileName;
                        Image loadedImage = Image.FromFile(filePath);
                        pictureBoxOriginal.Image = loadedImage;

                        // Wyczyść poprzednie wyniki (opcjonalnie)
                        pictureBoxResult1.Image = null;
                        pictureBoxResult2.Image = null;
                        pictureBoxResult3.Image = null;
                        pictureBoxResult4.Image = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas wczytywania obrazu: {ex.Message}", "Błąd Pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonProcess_Click(object sender, EventArgs e)
        {
            if (pictureBoxOriginal.Image == null)
            {
                MessageBox.Show("Najpierw wczytaj obraz oryginalny.", "Brak Obrazu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ---------------------------------------------------------------
            // TUTAJ UMIEŚĆ KOD DO PRZETWARZANIA OBRAZU
            // Pobierz obraz z: pictureBoxOriginal.Image
            // Wyniki (jako obiekty Image) umieść w:
            // pictureBoxResult1.Image = wynik1;
            // pictureBoxResult2.Image = wynik2;
            // pictureBoxResult3.Image = wynik3;
            // pictureBoxResult4.Image = wynik4;
            // ---------------------------------------------------------------

            MessageBox.Show("Tutaj nastąpi przetwarzanie obrazu (kod niezaimplementowany).");

            // Przykładowe placeholder'y (usuń lub zastąp prawdziwym kodem)
             // Create dummy images for testing layout
            /*
             Bitmap dummy = new Bitmap(200, 150);
             using(Graphics g = Graphics.FromImage(dummy)) { g.Clear(Color.LightBlue); g.DrawString("Wynik 1", SystemFonts.DefaultFont, Brushes.Black, 10, 10); }
             pictureBoxResult1.Image = (Bitmap)dummy.Clone();
             using(Graphics g = Graphics.FromImage(dummy)) { g.Clear(Color.LightCoral); g.DrawString("Wynik 2", SystemFonts.DefaultFont, Brushes.Black, 10, 10); }
             pictureBoxResult2.Image = (Bitmap)dummy.Clone();
             using(Graphics g = Graphics.FromImage(dummy)) { g.Clear(Color.LightGreen); g.DrawString("Wynik 3", SystemFonts.DefaultFont, Brushes.Black, 10, 10); }
             pictureBoxResult3.Image = (Bitmap)dummy.Clone();
             using(Graphics g = Graphics.FromImage(dummy)) { g.Clear(Color.LightGoldenrodYellow); g.DrawString("Wynik 4", SystemFonts.DefaultFont, Brushes.Black, 10, 10); }
             pictureBoxResult4.Image = (Bitmap)dummy.Clone();
             dummy.Dispose();
            */
        }


        // --- Metoda InitializeComponent (Generowana przez projektanta) ---
        // Pozostaw ją, jeśli używasz projektanta. Jeśli tworzysz UI tylko kodem,
        // upewnij się, że jest wywoływana w konstruktorze.
        // Zazwyczaj zawiera kod inicjalizujący komponenty dodane przez projektanta.
        // Jeśli dodajesz WSZYSTKO w kodzie (jak w SetupLayout), ta metoda może być pusta
        // lub zawierać podstawową inicjalizację formularza.
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // Form2
            //
            this.ClientSize = new System.Drawing.Size(282, 253); // Początkowy rozmiar, zostanie zmieniony w SetupLayout
            this.Name = "Form2";
            this.ResumeLayout(false);
            // Dodatkowa inicjalizacja z projektanta może tu być
        }

    }
}
