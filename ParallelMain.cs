using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParallelWatki;


public partial class MatrixMultiplication
{
    private int size;
    private int maxThreads;
    private string ThreadsUsage;
    //private Stopwatch stopwatch;
    private long srednia_parallel, srednia_thread;
    private const int liczba_pomiarow = 10;
    public int[,] tablica1 {get;}
    public int[,] tablica2 {get;}
    public int[,] wynik_paralel {get;}
    public int[,] wynik_threads {get;}

    public MatrixMultiplication(int MatrixSize, int MaxThreads) 
    {
        size = MatrixSize;
        maxThreads = MaxThreads;
        ThreadsUsage = "null";
        //stopwatch = new Stopwatch(); // Do pomiaru czasu
        srednia_parallel = 0;
        srednia_thread = 0;

        tablica1 = new int[size, size];
        tablica2 = new int[size, size];
        wynik_paralel = new int[size, size];
        wynik_threads = new int[size, size];
        wiersze = new bool[size];
    }




    public string calculations() {
        // Stopwatch stopwatch = new Stopwatch(); // Do pomiaru czasu
        Random random = new Random();

        // ParallelOptions options = new ParallelOptions() {MaxDegreeOfParallelism = maxThreads};
        // int[] threadUsage = new int[Environment.ProcessorCount];

        for (int p = 0; p < liczba_pomiarow; ++p)
        {
            for (int w = 0; w < size; w++) for (int k = 0; k < size; k++) {
                    tablica1[w, k] = random.Next(0, 31);
                    tablica2[w, k] = random.Next(0, 31);
            }
            calculations_Parallel();
            calculations_Threads();
            if( wynik_paralel.Cast<int>() .SequenceEqual (wynik_threads.Cast<int>()) ) 
                Console.WriteLine($"{p}. ok"); /*  Rzutowanie tablicy 2D na 1D i porównywanie ich metodą. */
            else {
                throw new Exception("Wątki zwróciły różne tablice wynikowe!");
                //Console.WriteLine("Wątki zwróciły różne tablice wynikowe!");
            }
        }
        srednia_parallel /= liczba_pomiarow;
        srednia_thread  /= liczba_pomiarow;
        return ReportAfterCalculations();
    }




    private void calculations_Parallel() {
        Stopwatch stopwatch = new Stopwatch(); // Do pomiaru czasu
        ParallelOptions options = new ParallelOptions() {MaxDegreeOfParallelism = maxThreads};
        int[] threadUsage = new int[Environment.ProcessorCount];

        // stopwatch.Restart();
        stopwatch.Start();
        Parallel.For(0, size, options, w =>
        {
            for (int k = 0; k < size; k++)
            {
                int sum = 0;
                for (int n = 0; n < size; n++) sum += tablica1[w, n] * tablica2[n, k];
                wynik_paralel[w, k] = sum;
            }

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Sprawdzamy, czy indeks nie wychodzi poza zakres
            int index = threadId % threadUsage.Length;
            Interlocked.Increment(ref threadUsage[index]); // Operacja jest wykonywana atomowo
        });
        stopwatch.Stop();
        srednia_parallel += stopwatch.ElapsedMilliseconds;
        ThreadsUsage = string.Join(" ", threadUsage); // Zapisuje użycie wątków dla ostatniego wykonania zadania
    }




    public string ReportAfterCalculations() {
        var result = new System.Text.StringBuilder();
        result.Append("Liczba Wątków: " + maxThreads + Environment.NewLine);
        result.Append("Wiekość kwatratowych macierzy m*m: " + size.ToString() + Environment.NewLine);
        result.Append("Wykorzystanie wątków programie: " + ThreadsUsage + Environment.NewLine);
        //result.Append("Czas wykonania: " + stopwatch.ElapsedMilliseconds + "ms" + Environment.NewLine);
        result.Append("Średni czas wykonania parallel: " + srednia_parallel + "ms" + Environment.NewLine);
        result.Append("Średni czas wykonania  threads: " + srednia_thread + "ms" + Environment.NewLine);
        return result.ToString();
    }




    public static string TabToString(int[,] matrix) {
        // StringBuilder pozwala na efektywne budowanie dużych łańcuchów
        var result = new System.Text.StringBuilder();

        // Iterujemy przez wiersze i kolumny macierzy
        int rows = matrix.GetLength(0);  // Liczba wierszy
        int cols = matrix.GetLength(1);  // Liczba kolumn

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result.Append(matrix[i, j].ToString().PadLeft(3)); // Dodajemy element do wyniku
                if (j < cols - 1) result.Append(" "); // Dodajemy spację, jeśli to nie ostatnia kolumna
            }
            result.AppendLine(); // Przechodzimy do nowego wiersza
        }

        return result.ToString(); // Zwracamy wynik w postaci łańcucha
    }
}
