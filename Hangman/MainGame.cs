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
            string ergebnisAusgabe = "";
            string buchstabeBereitsFalschAusgabe = "";
            bool spielerHatGewonnen = false;
            int fehler = 0;

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

            string gesuchtesWort = wörter[zufallsIndex];
            int wortlänge = gesuchtesWort.Length;
            char[] buchstaben = new char[wortlänge];
            char[] falscheBuchstaben = new char[6]; // max. 6 Fehler erlaubt
            char[] verdecktesWort = new char[wortlänge];

            // Füllen des Char-Arrays "buchstaben"
            int index = 0;
            foreach (char c in gesuchtesWort)
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
            while (!spielerHatGewonnen && fehler < 6)
            {
                // Ausgabe
                Console.Clear();
                zeichneHangman(fehler);
                Console.WriteLine("Gesucht ist ein Wort mit {0} Buchstaben.\n", wortlänge);
                Console.Write("Das gesuchte Wort ist: ");
                for (int i = 0; i < wortlänge; i++)
                {
                    Console.Write(verdecktesWort[i] + " ");
                }
                Console.Write("\n\n");
                Console.Write("Bereits verwendete falsche Buchstaben: ");
                foreach (char c in falscheBuchstaben)
                {
                    Console.Write(c + " ");
                }
                Console.Write("\n\n");
                Console.WriteLine("{0} von 6 Fehlern\n", fehler);
                Console.Write(buchstabeBereitsFalschAusgabe);
                buchstabeBereitsFalschAusgabe = "";
                Console.Write(ergebnisAusgabe);
                Console.Write("Buchstaben eingeben: ");

                // Prüfung der Eingabe
                bool istBuchstabe = false;
                char buchstabe = ' ';
                while (!istBuchstabe)
                {
                    // Cursor immer hinter "Buchstabe eingeben: " setzen
                    Console.CursorLeft = 21;

                    char eingabe = Char.ToLower(Console.ReadKey().KeyChar);
                    int ascii = Convert.ToInt32(eingabe);

                    // Prüfung ob sich der eingegebene Buchstabe a - z oder ä,ü,ö ist
                    if ((ascii >= 97 && ascii <= 122) || ascii == 228 || ascii == 252 || ascii == 246)
                    {
                        istBuchstabe = true;
                        buchstabe = eingabe;
                    }
                    else
                    {
                        // "eingabe" ist kein Buchstabe zwischen a und z oder A und Z

                        // Überschreiben der möglichen Fragezeichenboxen,
                        // die z.B. bei der Eingabe von "ESC" entstehen
                        Console.CursorLeft = 21;
                        Console.Write(" "); 
                    }
                }

                // Im gesuchten Wort nach dem Buchstaben suchen
                bool istImWort = false;
                for (int i = 0; i < wortlänge; i++)
                {
                    if (buchstabe == buchstaben[i])
                    {
                        // Der Buchstabe befindet sich im Wort
                        istImWort = true;
                        verdecktesWort[i] = buchstabe;
                    }
                }

                if (!istImWort)
                {
                    // Der Buchstabe befindet sich nicht im Wort

                    bool buchstabeBereitsFalsch = false;
                    for (int i = 0; i < falscheBuchstaben.Length; i++)
                    {
                        if (buchstabe == falscheBuchstaben[i])
                        {
                            // eingegebener Buchstabe befindet sich schon im 
                            // falsche Buchstaben Array

                            buchstabeBereitsFalsch = true;
                            buchstabeBereitsFalschAusgabe = "Du hast den Buchstaben \"" + 
                                buchstabe + "\" bereits verwendet.\n";
                        }
                    }

                    if (!buchstabeBereitsFalsch)
                    {
                        // Der Buchstabe wurde das erste mal falsch eingegeben

                        //Aufnehmen des Buchstabens in das Array
                        falscheBuchstaben[fehler] = buchstabe;

                        fehler++;
                        ergebnisAusgabe = "--> " + buchstabe + " ist falsch!\n\n";
                    }
                }
                else 
                {
                    // Der Buchstabe befindet sich im Wort

                    ergebnisAusgabe = "--> " + buchstabe + " ist richtig!\n\n";
                    
                    // Hilfsstring aus den Chars im "verdecktesWort" Array zusammenbauen 
                    string hilfsstring = "";
                    for (int i = 0; i < wortlänge; i++)
                    {
                        hilfsstring += verdecktesWort[i];
                    }

                    // Gewinnbedingung prüfen
                    if (hilfsstring == gesuchtesWort)
                    {
                        spielerHatGewonnen = true;
                        zeichneHangman(-1);
                    }
                }
            }

            // Verlierbedingung prüfen
            if (fehler == 6)
            {
                zeichneHangman(6);
            }
            Console.ReadKey();
        }

        private static void zeichneHangman(int status)
        {
            // Zeichne den Hangman
            switch (status)
            {
                case -1: // Spieler hat gewonnen
                    Console.Clear();
                    Console.WriteLine("\nDu hast Paul vom Galgen gerettet!\n\n");
                    Console.WriteLine(" +--------+-");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |  \t\\O/  Hurra!");
                    Console.WriteLine(" |\\ \t |");
                    Console.WriteLine(" | \\\t/ \\");
                    Console.WriteLine("\nWeiter mit beliebiger Taste.");
                     break;
                case 0:
                    Console.WriteLine("\n +--------+-");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |\\");
                    Console.WriteLine(" | \\");
                    break;
                case 1:
                    Console.WriteLine("\n +--------+-");
                    Console.WriteLine(" |        O");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |\\");
                    Console.WriteLine(" | \\");
                    break;
                case 2:
                    Console.WriteLine("\n +--------+-");
                    Console.WriteLine(" |        O");
                    Console.WriteLine(" |        |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |\\");
                    Console.WriteLine(" | \\");
                    break;
                case 3:
                    Console.WriteLine("\n +--------+-");
                    Console.WriteLine(" |        O");
                    Console.WriteLine(" |       /|");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |\\");
                    Console.WriteLine(" | \\");
                    break;
                case 4:
                    Console.WriteLine("\n +--------+-");
                    Console.WriteLine(" |        O");
                    Console.WriteLine(" |       /|\\");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |\\");
                    Console.WriteLine(" | \\");
                    break;
                case 5:
                    Console.WriteLine("\n +--------+-");
                    Console.WriteLine(" |        O");
                    Console.WriteLine(" |       /|\\");
                    Console.WriteLine(" |       / ");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |");
                    Console.WriteLine(" |\\");
                    Console.WriteLine(" | \\");
                    break;
                case 6: // Spieler hat gewonnen
                    Console.Clear();
                    abspann();
                    break;
            }
            Console.WriteLine();
        }

        private static void prolog()
        {
            // Vorgeschichte für das Hangman Spiel

            zeichnePaul();
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadKey();
            Console.Clear();

            zeichnePaulUndFred();
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadKey();
            Console.Clear();

            zeichnePaulUndFred();
            Console.WriteLine("\n\nSei ein guter Mensch. Hilf Paul!");
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadKey();
            Console.Clear();
        }
        private static void abspann()
        {
            zeichnePaulAmGalgen();
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadKey();
            Console.Clear();

            zeichneGrab();
            Console.WriteLine("\nWeiter mit beliebiger Taste.");
            Console.ReadKey();
        }
        private static void zeichnePaulAmGalgen()
        {
            Console.WriteLine("\nOh mein Gott! Paul ist tot!\n\n");
            Console.WriteLine(" +--------+-");
            Console.WriteLine(" |        O");
            Console.WriteLine(" |       /|\\");
            Console.WriteLine(" |       / \\");
            Console.WriteLine(" |");
            Console.WriteLine(" |");
            Console.WriteLine(" |\\");
            Console.WriteLine(" | \\");
        }
        private static void zeichneGrab()
        {
            Console.WriteLine("    __________________________");
            Console.WriteLine("   /                          \\");
            Console.WriteLine("  /    In Gedenken an Paul.    \\");
            Console.WriteLine(" /                              \\");
            Console.WriteLine(" |    Sie haben es versucht...   |");
            Console.WriteLine(" |                               |");
            Console.WriteLine(" |                               |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |        ==============         |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |              ||               |");
            Console.WriteLine(" |_______________________________|");
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
            Console.WriteLine(" +--------+-");
            Console.WriteLine(" |        O");
            Console.WriteLine(" |       /|\\");
            Console.WriteLine(" |       / \\");
            Console.WriteLine(" |");
            Console.WriteLine(" |  \t\t O  Oh Gott Fred!");
            Console.WriteLine(" |\\ \t\t/|\\");
            Console.WriteLine(" | \\\t\t/ \\");
        }
    }
}
