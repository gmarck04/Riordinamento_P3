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

namespace Riordinamento_P3
{
    class Program
    {
        public static int Min; //Inizializzo la variabile Min
        public static int Max; //Inizializzo la variabile Max
        public static int Quantità = 0; //Inizializzo la variabile Quantità e le do valore 0
        public static int Collisioni = 0; //Inizializzo la variabile Collisioni e le do valore 0
        //public static int randomNumber; //Inizializzo la variabile randomNumber
        

        static int[] Generatore_numeri_Duplicati()
        {           
            Random random = new Random(); //Inizializza la classe random

            Console.WriteLine("Inserisci quanti numeri vuoi generare, devono essere almeno 123456 numeri");
            Quantità = Convert.ToInt32(Console.ReadLine());

            while (Quantità < 123456)
            {
                Console.WriteLine("\nDevi inserire almeno 123456 numeri");
                Console.WriteLine("Inserisci quanti numeri vuoi generare");
                Quantità = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Inserisci il numero massimo");
            Max = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Inserisci il numero minimo");
            Min = Convert.ToInt32(Console.ReadLine());
            
            while(Min > Max)
            {
                Console.WriteLine("\nDevi inserire un numero minore del valore massimo");
                Console.WriteLine("Inserisci il numero minimo");
                Min = Convert.ToInt32(Console.ReadLine());
            }

            while (Max < Min)
            {
                Console.WriteLine("\nDevi inserire un numero maggiore del valore minimo");
                Console.WriteLine("Inserisci il numero massimo");
                Max = Convert.ToInt32(Console.ReadLine());
            }

            int[] numeri = new int[Quantità]; //array

            for (int i = 0; i < Quantità; i++) //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici
            {
                int randomNumber = random.Next(Min, Max); //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici      

                numeri[i] = randomNumber; //Inserire nel file il valore nell'array numeri                                
            }
            return numeri;
        }

        static int[] Generatore_numeri_Non_Duplicati()
        {
            Random random = new Random(); //Inizializza la classe random

            Console.WriteLine("Inserisci quanti numeri vuoi generare, devono essere almeno 123456 numeri");
            Quantità = Convert.ToInt32(Console.ReadLine());

            while (Quantità < 123456)
            {
                Console.WriteLine("\nDevi inserire almeno 123456 numeri");
                Console.WriteLine("Inserisci quanti numeri vuoi generare");
                Quantità = Convert.ToInt32(Console.ReadLine());
            }

            Console.WriteLine("Inserisci il numero massimo");
            Max = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Inserisci il numero minimo");
            Min = Convert.ToInt32(Console.ReadLine());

            while (Min > Max)
            {
                Console.WriteLine("\nDevi inserire un numero minore del valore massimo");
                Console.WriteLine("Inserisci il numero minimo");
                Min = Convert.ToInt32(Console.ReadLine());
            }

            while (Max < Min)
            {
                Console.WriteLine("\nDevi inserire un numero maggiore del valore minimo");
                Console.WriteLine("Inserisci il numero massimo");
                Max = Convert.ToInt32(Console.ReadLine());
            }

            int[] numeri = new int[Quantità]; //array

            for (int i = 0; i < Quantità; i++) //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici
            {
                int randomNumber = random.Next(Min, Max); //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici      

                numeri[i] = randomNumber; //Inserire nel file il valore nell'array numeri                                
            }
            return numeri;
        }

        static void Scelta_generatore_numeri()
        {
            Console.WriteLine("Se vuoi generare dei numeri casuali che possono duplicarsi digita 0,");
            Console.WriteLine("se invece vuoi generare dei numeri casuali che non possono duplicarsi digita 1");
            string Scelta_genratore_numeri = Console.ReadLine();

            while (Scelta_genratore_numeri != "0" && Scelta_genratore_numeri != "1")
            {
                Console.Clear();
                Console.WriteLine("Hai inserito un valore diverso da '0' o da '1' ");
                Console.WriteLine("Se vuoi generare dei numeri casuali che possono duplicarsi digita 0,");
                Console.WriteLine("se invece vuoi generare dei numeri casuali che non possono duplicarsi digita 1");
                Scelta_genratore_numeri = Console.ReadLine();

            }
            switch (Scelta_genratore_numeri)
            {
                case "0":
                    {
                        int[] numeri_generati = Generatore_numeri_Duplicati();

                        for (int i = 0; i < Quantità; i++) //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici
                        {
                            Console.WriteLine(numeri_generati[i]);
                        }
                    }
                    break;

                case "1":
                    {
                        int[] numeri_generati = Generatore_numeri_Non_Duplicati();
                        for (int i = 0; i < Quantità; i++) //Creo un ciclo for per creare e inserire nel file i vari numeri pseudo-randomici
                        {
                            Console.WriteLine(numeri_generati[i]);
                        }
                    }
                    break;
            }
        }


        static void Main(string[] args)
        {
            Scelta_generatore_numeri();

            Console.ReadKey();
        }
    }
}
