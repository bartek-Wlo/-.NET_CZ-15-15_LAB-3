using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParallelWatki;


public class MatrixMultiplication
{
    private int size;
    private int maxThreads;
    private string ThreadsUsage;
    //private Stopwatch stopwatch;
    private long srednia;
    public int[,] tablica1 {get;}
    public int[,] tablica2 {get;}
    public int[,] wynik {get;}

    public MatrixMultiplication(int MatrixSize, int MaxThreads) 
    {
        size = MatrixSize;
        maxThreads = MaxThreads;
        ThreadsUsage = "null";
        //stopwatch = new Stopwatch(); // Do pomiaru czasu
        srednia = 0;

        tablica1 = new int[size, size];
        tablica2 = new int[size, size];
        wynik    = new int[size, size];
    }




    public string calculations() {
        Stopwatch stopwatch = new Stopwatch(); // Do pomiaru czasu
        Random random = new Random();
        const int liczba_pomiarow = 10;

        ParallelOptions options = new ParallelOptions() {MaxDegreeOfParallelism = maxThreads};
        int[] threadUsage = new int[Environment.ProcessorCount];

        for (int w = 0; w < size; w++) for (int k = 0; k < size; k++) {
                tablica1[w, k] = random.Next(0, 31);
                tablica2[w, k] = random.Next(0, 31);
        }


        for (int p = 0; p < liczba_pomiarow; ++p)
        {
            stopwatch.Restart();
            stopwatch.Start();
            Parallel.For(0, size, options, w =>
            {
                for (int k = 0; k < size; k++)
                {
                    int sum = 0;
                    for (int n = 0; n < size; n++) sum += tablica1[w, n] * tablica2[n, k];
                    wynik[w, k] = sum;
                }

                int threadId = Thread.CurrentThread.ManagedThreadId;

                // Sprawdzamy, czy indeks nie wychodzi poza zakres
                int index = threadId % threadUsage.Length;
                Interlocked.Increment(ref threadUsage[index]); // Operacja jest wykonywana atomowo
            });
            stopwatch.Stop();
            srednia += stopwatch.ElapsedMilliseconds;
        }
        srednia /= 10;

        ThreadsUsage = string.Join(" ", threadUsage);
        return ReportAfterCalculations();
    }






    public string ReportAfterCalculations() {
        var result = new System.Text.StringBuilder();
        result.Append("Liczba Wątków: " + maxThreads + Environment.NewLine);
        result.Append("Wiekość kwatratowych macierzy m*m: " + size.ToString() + Environment.NewLine);
        result.Append("Wykorzystanie wątków programie: " + ThreadsUsage + Environment.NewLine);
        //result.Append("Czas wykonania: " + stopwatch.ElapsedMilliseconds + "ms" + Environment.NewLine);
        result.Append("Średni czas wykonania: " + srednia + "ms" + Environment.NewLine);
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


public class test_class
{
    public static void test(/*string[] args*/)
    {
        int maxThreads = 4; // Liczba wątków max
        int size = 10;    // Rozmiar macierzy kwadratowej
        Stopwatch stopwatch = new Stopwatch(); // Do pomiaru czasu

        int[,] tablica1 = new int[size, size];
        int[,] tablica2 = new int[size, size];
        int[,] wynik = new int[size, size];
        Random random = new Random();

        ParallelOptions options = new ParallelOptions() {MaxDegreeOfParallelism = maxThreads};
        int[] threadUsage = new int[Environment.ProcessorCount];

        for (int w = 0; w < size; w++) for (int k = 0; k < size; k++) {
                tablica1[w, k] = random.Next(0, 101);
                tablica2[w, k] = random.Next(0, 101);
        }

stopwatch.Start();
        Parallel.For(0, size, options, w => {
            for (int k = 0; k < size; k++) {
                int sum = 0;
                for (int n = 0; n < size; n++) sum += tablica1[w, n] * tablica2[n, k];
                wynik[w, k] = sum;
            }

            int threadId = Thread.CurrentThread.ManagedThreadId;

            // Sprawdzamy, czy indeks nie wychodzi poza zakres
            int index = threadId % threadUsage.Length;
            Interlocked.Increment(ref threadUsage[index]); // Operacja jest wykonywana atomowo
        });
stopwatch.Stop();

        Console.WriteLine("Użycie wątków:");
        Console.WriteLine(string.Join(" ", threadUsage));
        Console.WriteLine("\n");
        Console.WriteLine(TabToString(tablica1));
        Console.WriteLine("\n");
        Console.WriteLine(TabToString(tablica2));
        Console.WriteLine("\n");
        Console.WriteLine(TabToString(wynik));
        Console.WriteLine("\n");
        Console.WriteLine($"Czas wykonania: {stopwatch.ElapsedMilliseconds} ms");
    }







    public static string TabToString(int[,] matrix)
    {
        // StringBuilder pozwala na efektywne budowanie dużych łańcuchów
        var result = new System.Text.StringBuilder();

        // Iterujemy przez wiersze i kolumny macierzy
        int rows = matrix.GetLength(0);  // Liczba wierszy
        int cols = matrix.GetLength(1);  // Liczba kolumn

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result.Append(matrix[i, j]); // Dodajemy element do wyniku
                if (j < cols - 1) result.Append(" "); // Dodajemy spację, jeśli to nie ostatnia kolumna
            }
            result.AppendLine(); // Przechodzimy do nowego wiersza
        }

        return result.ToString(); // Zwracamy wynik w postaci łańcucha
    }
}