using System.Reflection.Metadata;

namespace ParallelWatki
{
    public partial class Form3 : Form
    {
        private void progowanie() {
            int width;
            int height;
            Bitmap ObrazBitmap;
            lock(imageLock) {
                ObrazBitmap = new Bitmap(pictureBoxOriginal.Image !);
                width = ObrazBitmap.Width;
                height = ObrazBitmap.Height;
            }
            
            for(int h = 0; h < height; ++h) {
                for(int w = 0; w < width; ++w) {
                    Color oryginalKolor = ObrazBitmap.GetPixel(w, h);
                    double Luminancja = 0.3*oryginalKolor.R + 0.59*oryginalKolor.G + 0.11*oryginalKolor.B;
                    Color progowanieKolor = (Luminancja > 128) ? Color.White : Color.Black;
                    ObrazBitmap.SetPixel(w, h, progowanieKolor);
                }
            }
            pictureBoxResult1.Image = ObrazBitmap;
        }



        private void krawedz() {
            Bitmap ObrazBitmap;;
            int width;
            int height;
            Bitmap krawdzBitmap;
            lock(imageLock) {
                ObrazBitmap = new Bitmap(pictureBoxOriginal.Image !);
                width = ObrazBitmap.Width;
                height = ObrazBitmap.Height;
                krawdzBitmap = new Bitmap(width, height);
            }
            int[,] sobelX = new int[,] {
                { -1, 0, 1 },
                { -2, 0, 2 },
                { -1, 0, 1 }
            };
            int[,] sobelY = new int[,] {
                { -1, -2, -1 },
                {  0,  0,  0 },
                {  1,  2,  1 }
            };

            for(int h = 1; h < height-1; ++h) {
                for(int w = 1; w < width-1; ++w) {
                    int gx = 0;
                    int gy = 0;
                    for (int ky = -1; ky <= 1; ky++) {
                        for (int kx = -1; kx <= 1; kx++) {
                            Color pixelColor = ObrazBitmap.GetPixel(w + kx, h + ky);
                            int gray = (int)((pixelColor.R + pixelColor.G + pixelColor.B) / 3);

                            gx += sobelX[ky + 1, kx + 1] * gray;
                            gy += sobelY[ky + 1, kx + 1] * gray;
                        }
                    }

                    int magnitude = (int)Math.Sqrt(gx * gx + gy * gy);
                    magnitude = Math.Clamp(magnitude, 0, 255);

                    Color edgeColor = Color.FromArgb(magnitude, magnitude, magnitude);
                    krawdzBitmap.SetPixel(w, h, edgeColor);
                }
            }
            pictureBoxResult2.Image = krawdzBitmap;
        }



        private void negatyw() {
            Bitmap ObrazBitmap;
            int width;
            int height;
            lock(imageLock) {
                ObrazBitmap = new Bitmap(pictureBoxOriginal.Image !);
                width = ObrazBitmap.Width;
                height = ObrazBitmap.Height;
            }

            for(int h = 0; h < height; ++h) {
                for(int w = 0; w < width; ++w) {
                    Color oryginalKolor = ObrazBitmap.GetPixel(w, h);
                    int R = 255 - oryginalKolor.R;
                    int G = 255 - oryginalKolor.G;
                    int B = 255 - oryginalKolor.B;
                    Color negatywKolor = Color.FromArgb(oryginalKolor.A, R, G , B);
                    ObrazBitmap.SetPixel(w, h, negatywKolor);
                }
            }
            pictureBoxResult3.Image = ObrazBitmap;
        }



        private void lustrzane() { 
            Bitmap ObrazBitmap;
            int width;
            int height;
            Bitmap lustrzanyBitmap;
            lock(imageLock) {
                ObrazBitmap = new Bitmap(pictureBoxOriginal.Image !);
                width = ObrazBitmap.Width;
                height = ObrazBitmap.Height;
                lustrzanyBitmap = new Bitmap(width, height);
            }

            for(int h = 0; h < height; ++h) {
                for(int w = 0; w < width; ++w) {
                    Color lustrzanyKolor = ObrazBitmap.GetPixel(width-w-1, h);
                    lustrzanyBitmap.SetPixel(w, h, lustrzanyKolor);
                }
            }
            pictureBoxResult4.Image = lustrzanyBitmap;
        }
    }
}