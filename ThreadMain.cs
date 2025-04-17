using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ParallelWatki;


public partial class MatrixMultiplication
{
    private volatile bool[] wiersze;
    private object locker = new object();
    public void calculations_Threads() {
        Array.Fill(wiersze, false); // Czyści tablice indeksów
        Stopwatch stopwatch = new Stopwatch(); // Do pomiaru czasu
        Thread[] threads = new Thread[maxThreads];

        //stopwatch.Restart();
        stopwatch.Start();
        for(int i = 0; i < maxThreads; ++i) {
            threads[i] = new Thread(Thread_Task);
            threads[i].Name = String.Format("Thread: {0}", i);
        }
        foreach(Thread x in threads) x.Start(); // Rozpoczęcie pracy wątków
        foreach(Thread x in threads) x.Join(); // Czekanie na zakończenie pracy wątków
        stopwatch.Stop();
        srednia_thread += stopwatch.ElapsedMilliseconds;
    }


    private void Thread_Task() {
        int wiersz;
        while(TryGetFreeIndex(out wiersz)) {
            for (int k = 0; k < size; k++) {
                int sum = 0;
                for (int n = 0; n < size; n++) sum += tablica1[wiersz, n] * tablica2[n, k];
                wynik_threads[wiersz, k] = sum;
            }
        }
    }


    bool TryGetFreeIndex(out int index) {
        lock (locker) for (int i = 0; i < wiersze.Length; i++) {
            if (!wiersze[i]) {
                wiersze[i] = true; // Zaznacza dany wiersz jako liczony
                index = i;
                return true;
            }
        }
        index = -1;
        return false;
    }
}




