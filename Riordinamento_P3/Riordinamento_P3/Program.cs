/*
 * Autore: Giovanni Marchetto
 * Data 12/03/2021
 * Consegna: Creare un software che compia queste azioni:

1) Creare un elenco di numeri interi, con possibilità di scelta da parte dell'utente se possono essere ripetuti oppure no, 
    in un numero non minore di 123.456. (implementare i vari controlli)
2) L'elenco in output al punto 1 verrà ordinato con algoritmo A*
3) L'elenco in output al punto 1 verrà ordinato con algoritmo B*
4) L'elenco in output al punto 1 verrà ordinato con algoritmo C*
5) L'output del programma sarà la stampa a video di 3 valori, relativi alla "fatica" svolta dai 3 algoritmi utilizzati.     
    L'ordine di presentazione sarà l'ordine crescente e al visualizzazione dovrà essere simile a:"Al primo posto l'algoritmo A  
    con un punteggio di 333333.
    Al secondo posto l'algoritmo B con un punteggio di 444444.
    All terzo posto l'algoritmo C con un punteggio di 555555."
6) Ogni algoritmo produrrà un file su cui salvare il risultato dell'ordinamento. È necessario che i 3 file di output risultino identici.

________
* A, B, C sono 3 algoritmi di ordinamento diversi tra di loro. All'interno di essi deve essere presente un "contatore" che sommi il valore dei seguenti pesi:
1 se c'è un'operazione di "vista", come ad esempio controllare il valore di una variabile o di una posizione dell'array
2 se c'è un'operazione di assegnazione di un nuovo valore ad una variabile
3 se c'è un'operazione di "modifica", come ad esempio lo scambio di due variabili
Altri pesi dovranno essere assegnati con un criterio di buonsenso.
________
- Non occorre presentare relazione: il codice dovrà essere necessariamente molto chiaro.
- Giustificare eventuali ulteriori integrazioni o variazioni ai requisiti.
- Commentare accuratamente il codice.
- La realizzazione è da svolgere singolarmente, in autonomia.
________

La correttezza di ognuno dei 6 punti vale 1 punto.
Ulteriori punti per:
- scrittura corretta ed elegante del codice (1pt)
- commenti adeguati (1pt)
- soluzioni migliorative ed ottimizzazione (2pt)
*/

using System;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Riordinamento_P3
{
    class Program
    {
        public static int Min; //Inizializzo la variabile Min
        public static int Max; //Inizializzo la variabile Max
        public static int Quantità = 0; //Inizializzo la variabile Quantità e le do valore 0
        public static int Collisioni = 0; //Inizializzo la variabile Collisioni e le do valore 0  
        public static int[] numeri = new int[0]; //array

        static int[] Generatore_numeri_Duplicati()
        {
            Console.Clear();
            Random random = new Random(); //Inizializza la classe random

            Console.WriteLine("Inserisci quanti numeri vuoi generare, devono essere almeno 123456 numeri");          
            bool numero_intero_Quantità = Int32.TryParse(Console.ReadLine(), out Quantità);

            while (numero_intero_Quantità == false || Quantità < 123456)
            {
                Console.Clear();
                if (numero_intero_Quantità == false)
                {
                    Console.WriteLine("Devi inserire un valore intero");
                    Console.WriteLine("Inserisci quanti numeri vuoi generare");
                    numero_intero_Quantità = Int32.TryParse(Console.ReadLine(), out Quantità);
                }
                else
                {
                    Console.WriteLine("Devi inserire almeno 123456 numeri");
                    Console.WriteLine("Inserisci quanti numeri vuoi generare");
                    numero_intero_Quantità = Int32.TryParse(Console.ReadLine(), out Quantità);
                }
            }

            Console.WriteLine("Inserisci il numero massimo");
            bool numero_intero_Max = Int32.TryParse(Console.ReadLine(), out Max);

            while (numero_intero_Max == false)
            {
                Console.Clear();
                Console.WriteLine("Devi inserire un valore intero");
                Console.WriteLine("Inserisci il numero massimo");
                numero_intero_Max = Int32.TryParse(Console.ReadLine(), out Max);
            }

            Console.WriteLine("Inserisci il numero minimo");
            bool numero_intero_Min = Int32.TryParse(Console.ReadLine(), out Min);

            while (numero_intero_Min == false)
            {
                Console.Clear();
                Console.WriteLine("Devi inserire un valore intero");
                Console.WriteLine("Inserisci il numero minimo");
                numero_intero_Min = Int32.TryParse(Console.ReadLine(), out Min);                
            }

            while (Min > Max)
            {
                Console.Clear();
                Console.WriteLine("Devi inserire un numero minore del valore massimo");
                Console.WriteLine("Inserisci il numero minimo");
                Min = Convert.ToInt32(Console.ReadLine());
            }

            while (Max < Min)
            {
                Console.Clear();
                Console.WriteLine("Devi inserire un numero maggiore del valore minimo");
                Console.WriteLine("Inserisci il numero massimo");
                Max = Convert.ToInt32(Console.ReadLine());
            }

            Array.Resize(ref numeri, Quantità);

            for (int i = 0; i < Quantità; i++) //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici
            {
                int randomNumber = random.Next(Min, Max); //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici      

                numeri[i] = randomNumber; //Inserire nel file il valore nell'array numeri                                
            }
            return numeri;
        }

        static int[] Generatore_numeri_Non_Duplicati()
        {
            Console.Clear();
            Random random = new Random(); //Inizializza la classe random
            int Collisioni = 0;
            Console.WriteLine("Inserisci quanti numeri vuoi generare, devono essere almeno 123456 numeri");
            bool numero_intero_Quantità = Int32.TryParse(Console.ReadLine(),out Quantità);      

            while (numero_intero_Quantità == false || Quantità < 12)
            {
                Console.Clear();
                if (numero_intero_Quantità == false)
                {
                    Console.WriteLine("Devi inserire un valore intero");
                    Console.WriteLine("Inserisci quanti numeri vuoi generare");
                    numero_intero_Quantità = Int32.TryParse(Console.ReadLine(), out Quantità);
                }
                else
                {
                    Console.WriteLine("Devi inserire almeno 123456 numeri");
                    Console.WriteLine("Inserisci quanti numeri vuoi generare");
                    numero_intero_Quantità = Int32.TryParse(Console.ReadLine(), out Quantità);
                }                
            }

            Console.WriteLine("Inserisci il numero massimo");
            bool numero_intero_Max = Int32.TryParse(Console.ReadLine(), out Max);

            while (numero_intero_Max == false || Max < Quantità)
            {
                Console.Clear();
                if (numero_intero_Max == false)
                {
                    Console.WriteLine("Devi inserire un valore intero");
                    Console.WriteLine("Inserisci il numero massimo");
                    numero_intero_Max = Int32.TryParse(Console.ReadLine(), out Max);
                }
                else
                {
                    Console.WriteLine("Devi inserire un numero maggiore dei numeri richiesti");
                    Console.WriteLine("Inserisci il numero massimo");
                    numero_intero_Max = Int32.TryParse(Console.ReadLine(), out Max);
                }
            }

            Console.WriteLine("Inserisci il numero minimo");
            bool numero_intero_Min = Int32.TryParse(Console.ReadLine(), out Min);

            while (numero_intero_Min == false || Min > Max)
            {
                Console.Clear();
                if (numero_intero_Min == false)
                {
                    Console.WriteLine("Devi inserire un valore intero");
                    Console.WriteLine("Inserisci il numero minimo");
                    numero_intero_Min = Int32.TryParse(Console.ReadLine(), out Min);
                }
                else
                {
                    Console.WriteLine("Devi inserire un numero minore del valore massimo");
                    Console.WriteLine("Inserisci il numero minimo");
                    numero_intero_Min = Int32.TryParse(Console.ReadLine(), out Min);
                }
            }

            while (Max < Min)
            {
                Console.Clear();
                Console.WriteLine("Devi inserire un numero maggiore del valore minimo");
                Console.WriteLine("Inserisci il numero massimo");
                Max = Convert.ToInt32(Console.ReadLine());
            }

            Array.Resize(ref numeri, Quantità);

            for (int i = 0; i < Quantità; i++) //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici
            {
                int randomNumber = random.Next(Min, Max); //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici      
                                  
                bool controllo = false;
                for (int a = 0; a < numeri.Length; a++)
                {
                    if(numeri[a] == randomNumber)
                    {
                        controllo = true;
                    }                    
                }

                if (controllo == true)
                {
                    Collisioni++;
                    i--;
                }
                else
                {
                    numeri[i] = randomNumber;                    
                }
            }
            return numeri;
        }
            
        static string Scelta_multi_threading()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Se vuoi attivare il multi threading ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("digita 0");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(",");

            Console.Write("se invece non lo vuoi procedere ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("senza ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("multi threading ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("digita 1");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");

            string Scelta_multi_threading = Convert.ToString(Console.ReadKey(true).KeyChar);

            while(Scelta_multi_threading != "0" && Scelta_multi_threading != "1")
            {
                Console.Clear();

                Console.Write("Hai inserito un valore diverso da ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("'0' ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("o da ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("'1'");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(".");

                Console.Write("Se vuoi attivare il multi threading ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("digita 0");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(",");

                Console.Write("se invece non lo vuoi procedere ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("senza ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("multi threading ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("digita 1");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(".");

                Scelta_multi_threading = Convert.ToString(Console.ReadKey(true).KeyChar);
            }
            return Scelta_multi_threading;
        }

        static string Scelta_generatore_numeri()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Se vuoi generare dei numeri casuali che possono duplicarsi ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("digita 0");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(",");

            Console.Write("se invece vuoi generare dei numeri casuali che ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("non ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("possono duplicarsi ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("digita 1");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");

            string Scelta_generatore_numeri = Convert.ToString(Console.ReadKey(true).KeyChar);

            while (Scelta_generatore_numeri != "0" && Scelta_generatore_numeri != "1")
            {
                Console.Clear();

                Console.Write("Hai inserito un valore diverso da ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("'0' ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("o da ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("'1'");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(".");

                
                Console.Write("Se vuoi generare dei numeri casuali che possono duplicarsi ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("digita 0");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(",");

                Console.Write("se invece vuoi generare dei numeri casuali che ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("non ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("possono duplicarsi ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("digita 1");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(".");

                Scelta_generatore_numeri = Convert.ToString(Console.ReadKey(true).KeyChar);
            }
            return Scelta_generatore_numeri;
        }

        static void Swich(string Scelta_multi_thread, string Scelta_generatore)
        {
            switch (Scelta_multi_thread)
            {
                case "0":
                    {
                        switch (Scelta_generatore)
                        {
                            case "0":
                                {
                                    int[] numeri_generati = Generatore_numeri_Duplicati();

                                    for (int i = 0; i < Quantità; i++)
                                    {
                                        Console.WriteLine(numeri_generati[i]);
                                    }

                                    Thread selection_sort = new Thread(Thread_selection_sort);
                                    selection_sort.Start();

                                    Thread bubble_sort = new Thread(Thread_bubble_sort_con_sentinella);
                                    bubble_sort.Start();
                                }
                                break;

                            case "1":
                                {
                                    int[] numeri_generati = Generatore_numeri_Non_Duplicati();
                                    for (int i = 0; i < Quantità; i++)
                                    {
                                        Console.WriteLine(numeri_generati[i]);
                                    }

                                    Thread selection_sort = new Thread(Thread_selection_sort);
                                    selection_sort.Start();

                                    Thread bubble_sort = new Thread(Thread_bubble_sort_con_sentinella);
                                    bubble_sort.Start();
                                }
                                break;
                        }
                    }
                    break;
                case "1":
                    {
                        switch (Scelta_generatore)
                        {
                            case "0":
                                {                                    
                                    int[] numeri_generati = Generatore_numeri_Duplicati();

                                    for (int i = 0; i < Quantità; i++)
                                    {
                                        Console.WriteLine(numeri_generati[i]);
                                    }

                                    algoritmo_selection_sort(numeri);
                                    algoritmo_bubble_sort_con_sentinella(numeri);
                                }
                                break;

                            case "1":
                                {
                                    int[] numeri_generati = Generatore_numeri_Non_Duplicati();
                                    for (int i = 0; i < Quantità; i++)
                                    {
                                        Console.WriteLine(numeri_generati[i]);
                                    }

                                    algoritmo_selection_sort(numeri);
                                    algoritmo_bubble_sort_con_sentinella(numeri);
                                }
                                break;
                        }
                    }
                    break;
            }            
        }

        static void Thread_selection_sort()
        {
            algoritmo_selection_sort(numeri);
        }

        static void algoritmo_selection_sort(int[] numeri)
        {                    
            int temp; //Inizializzo la variabile temp
            int min; //Inizializzo la variabile min
            string filename = @"selection_sort.txt";
            StreamWriter File = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + filename);
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            for (int i = 0; i < Quantità - 1; i++) //Ciclo for per riordinare i numeri
            {
                min = i; //Pongo la variabile min uguale a i
                for (int j = i + 1; j < Quantità; j++) //Ciclo for per trovare il numeri più piccolo
                {
                    if (numeri[j] < numeri[min]) //Controllo se il numero preso è minore o meno del numero contenuto in min
                    {
                        min = j; //Pongo min uguale al valore di j
                    }
                }
                temp = numeri[min]; //Pongo temp uguale al numero posizionato al posto il cui valore corrisponde alla variabile min
                numeri[min] = numeri[i]; //Pongo il numero posizionato al posto il cui valore corrisponde alla variabile min
                                         //uguale al numero posizionato al posto il cui valore corrisponde alla variabile i
                numeri[i] = temp; //Pongo il numero posizionato al posto il cui valore corrisponde alla variabile i uguale alla variabile temp
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            File.WriteLine("I numeri riordinati sono: "); //Digito a schermo la seguente frase
            for (int i = 0; i < Quantità; i++) //Ciclo for per scrivere i numeri riordinati contenuti nell'array numeri
            {
                File.WriteLine(numeri[i]);
            }
            
            File.Close();          

            Console.WriteLine($"\nIl selection sort ha finito, ha impiegato {elapsedTime}");
        }

        static void Thread_bubble_sort_con_sentinella()
        {
            algoritmo_bubble_sort_con_sentinella(numeri);
        }

        static void algoritmo_bubble_sort_con_sentinella(int[] numeri)
        {
            int Sentinella = numeri.Length;
            int x = 0;
            int temp;
            int y = 0;
            string filename = @"bubble_sort_con_sentinella.txt";
            StreamWriter File = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + filename);
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            do
            {
                x = 0;
                for (int i = 0; i < Sentinella - 1; i++)
                {
                    if (numeri[i] > numeri[i + 1])
                    {
                        temp = numeri[i];
                        numeri[i] = numeri[i + 1];
                        numeri[i + 1] = temp;
                        x = 1;
                        y = i + 1;
                    }
                }
                Sentinella = y;
            } while (x == 1);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            
            File.WriteLine("I numeri riordinati sono: "); //Digito a schermo la seguente frase
            for (int i = 0; i < Quantità; i++) //Ciclo for per scrivere i numeri riordinati contenuti nell'array numeri
            {
                File.WriteLine(numeri[i]);
            }

            File.Close();

            Console.WriteLine($"\nIl bubble sort con sentinella ha finito, ha impiegato {elapsedTime}");
        }

        static void Thread_merge_sort()
        {
            algoritmo_merge_sort(numeri);
        }

        static void algoritmo_merge_sort(int[] numeri)
        {

        }

        static void Main(string[] args)
        {
            string Scelta_multi_thread = Scelta_multi_threading();
            Console.Clear();
            string Scelta_generatore = Scelta_generatore_numeri();
            Console.Clear();

            Swich(Scelta_multi_thread, Scelta_generatore);

            Console.ReadKey();
        }
    }
}
