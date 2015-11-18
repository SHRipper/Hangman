using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class MainGame
    {
        static void Main(string[] args)
        {
            // Array der zu erratenen Wörter
            string[] wörter = new string[10];
            wörter[0] = "programmieren";
            wörter[1] = "informatik";
            wörter[2] = "tastatur";
            wörter[3] = "funktion";
            wörter[4] = "programmiersprache";
            wörter[5] = "variable";
            wörter[6] = "computer";
            wörter[7] = "debugging";
            wörter[8] = "methode";
            wörter[9] = "entwicklungsumgebung";

            // Zufallszahl erstellen
            Random rnd = new Random();
            int zufallsIndex = rnd.Next(0, wörter.Length);


            int wortlänge = wörter[zufallsIndex].Length;
            char[] buchstaben = new char[wortlänge];
            char[] falscheBuchstaben = new char[6]; // max. 6 Fehler erlaubt
            char[] verdecktesWort = new char[wortlänge];

            // Füllen des Char-Arrays "buchstaben"
            int index = 0;
            foreach (char c in wörter[zufallsIndex])
            {
                buchstaben[index] = c;
                index++;
            }

            // Füllen des Char-Arrays "verdeckesWort"
            for (int i = 0; i < wortlänge; i++)
            {
                verdecktesWort[i] = '-';
            }

            // Vorgeschichte bei Spielstart
            prolog();


            /*
             * Spiel Schleife
             */

            bool spielerHatGewonnen = false;
            int fehler = 0;
            while (!spielerHatGewonnen && fehler < 6)
            {
                bool istImWort = false;

                // Ausgabe
                zeichneHangman(fehler);
                Console.WriteLine("Gesucht ist ein Wort mit {0} Buchstaben. \n", wortlänge);
                Console.Write("Das gesuchte Wort ist: ");
                for (int i = 0; i < wortlänge; i++)
                {
                    Console.Write(verdecktesWort[i] + " ");
                }
                Console.Write("\n\n");
                Console.Write("Buchstaben eingeben: ");
                
                // Eingegebenen Buchstaben in Variable einlesen
                char buchstabe = Char.ToLower(Console.ReadKey().KeyChar);


                // Im Wort nach dem Buchstaben suchen
                for (int i = 0; i < wortlänge; i++)
                {
                    if (buchstabe == buchstaben[i])
                    {
                        // Der Buchstabe befindet sich im Wort
                        istImWort = true;
                        verdecktesWort[i] = buchstabe;
                    }
                }

                // Der Buchstabe befindet sich nicht im Wort
                if (!istImWort)
                {
                    bool buchstabeBereitsFalsch = false;
                    for (int i = 0; i < falscheBuchstaben.Length; i++)
                    {
                        if (buchstabe == falscheBuchstaben[i])
                        {
                            buchstabeBereitsFalsch = true;

                            // Ausgabe, wenn der Buchstabe sich bereits im "falscheBuchstaben" Array befindet
                            Console.WriteLine("\n\nDu hast den Buchstaben \"{0}\" bereits verwendet.", buchstabe);
                            Console.Write("Bereits verwendete falsche Buchstaben: ");
                            foreach (char c in falscheBuchstaben)
                            {
                                Console.Write(c + " ");
                            }
                            Console.Write("\n\n");

                        }
                    }

                    // Der Buchstabe wurde das erste mal falsch eingegeben
                    if (!buchstabeBereitsFalsch)
                    {
                        //Aufnehmen des Buchstabens in das Array
                        falscheBuchstaben[fehler] = buchstabe;

                        fehler++;
                        Console.WriteLine("\t--> {0} ist falsch!\n\n", buchstabe);
                    }
                    Console.WriteLine("{0} von 6 Fehlern", fehler);
                }
                // Der Buchstabe befindet sich im Wort
                else 
                {
                    Console.WriteLine("--> {0} ist richtig!\n\n", buchstabe);
                    
                    // Hilfsstring aus den Chars im "verdecktesWort" Array zusammenbauen 
                    string hilfsstring = "";
                    for (int i = 0; i < wortlänge; i++)
                    {
                        hilfsstring += verdecktesWort[i];
                    }

                    // Gewinnbedingung prüfen
                    if (hilfsstring == wörter[zufallsIndex])
                    {
                        spielerHatGewonnen = true;
                    }
                }

                Console.Write("\n\n------------------------------------------------------\n\n\n");
            }

            // Gewinnbedingung prüfen
            if (spielerHatGewonnen)
            {
                zeichneHangman(-1);
            }

            // Verlierbedingung prüfen
            if (fehler == 6)
            {
                zeichneHangman(6);
            }

            
            Console.ReadLine();
        }

        private static void zeichneHangman(int status)
        {
            // Zeichne den Hangman
            switch (status)
            {
                case -1: // Spieler hat gewonnen
                    Console.Clear();
                    Console.WriteLine("\nDu hast Paul vom Galgen gerettet!\n\n");
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|  \t\\O/  Hurra!");
                    Console.WriteLine("|\\ \t |");
                    Console.WriteLine("| \\\t/ \\");
                    break;
                case 0:
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
                case 1:
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|        O");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
                case 2:
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|        O");
                    Console.WriteLine("|        |");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
                case 3:
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|        O");
                    Console.WriteLine("|       /|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
                case 4:
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|        O");
                    Console.WriteLine("|       /|\\");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
                case 5:
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|        O");
                    Console.WriteLine("|       /|\\");
                    Console.WriteLine("|       / ");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
                case 6: // Spieler hat gewonnen
                    Console.Clear();
                    Console.WriteLine("\nWegen dir musste Paul sterben. Du Monster!\n");
                    Console.WriteLine("+--------+-");
                    Console.WriteLine("|        O");
                    Console.WriteLine("|       /|\\");
                    Console.WriteLine("|       / \\");
                    Console.WriteLine("|");
                    Console.WriteLine("|");
                    Console.WriteLine("|\\");
                    Console.WriteLine("| \\");
                    break;
            }
            Console.WriteLine();
        }

        private static void prolog()
        {
            // Vorgeschichte für das Hangman Spiel

            zeichnePaul();
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadLine();
            Console.Clear();

            zeichnePaulUndFred();
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadLine();
            Console.Clear();

            zeichnePaulUndFred();
            Console.WriteLine("\n\nSei ein guter Mensch. Hilf Paul!");
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadLine();
            Console.Clear();
        }
        private static void zeichnePaul()
        {
            Console.WriteLine("\nDas ist Paul.\n");
            Console.WriteLine("  \t\\o  Hi!");
            Console.WriteLine(" \t |\\");
            Console.WriteLine("\t/ \\");
        }
        private static void zeichnePaulUndFred()
        {
            Console.WriteLine("\nPaul möchte nicht am Galgen hängen wie sein Freund Fred...\n");
            Console.WriteLine("+--------+-");
            Console.WriteLine("|        O");
            Console.WriteLine("|       /|\\");
            Console.WriteLine("|       / \\");
            Console.WriteLine("|");
            Console.WriteLine("|  \t\t O  Oh Gott Fred!");
            Console.WriteLine("|\\ \t\t/|\\");
            Console.WriteLine("| \\\t\t/ \\");
        }
    }
}
