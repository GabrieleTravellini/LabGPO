using Othello;
using System;
using System.Collections.Generic;

namespace Othello
{
    /// <summary>
    /// Implementa il bot "Jarvis", un giocatore automatico dotato di più strategie
    /// che si attivano in base al turno. Utilizza una matrice di priorità per
    /// guidare le prime mosse e passa in fasi successive a logiche più aggressive.
    /// </summary>
    public class Jarvis
    {
        private int colorePedina;
        private Tabellone tabelloneMatrice;
        private List<(int, int)> mosseDisponibili;
        private int contaTurno;
        private Bot1 botAggressivo;
        private Random random;

        /// <summary>
        /// Matrice di priorità che assegna un valore strategico a ciascuna posizione.
        /// Valori alti corrispondono a mosse migliori da preferire.
        /// </summary>
        private int[,] matricePriorita =
        {
            { 100, -10, 10,10,10,10,-10, 100 },
            { -10, -50, 0, 0, 0, 0, -50, -10 },
            { 10,    0, 5, 5, 5, 5,  0,   10 },
            { 10,    0, 5, 5, 5, 5,  0,   10 },
            { 10,    0, 5, 5, 5, 5,  0,   10 },
            { 10,    0, 5, 5, 5, 5,  0,   10 },
            { -10, -50, 0, 0, 0, 0,-50,  -10 },
            { 100, -10,10,10,10,10,-10,  100 }
        };

        /// <summary>
        /// Crea un nuovo bot Jarvis.
        /// </summary>
        /// <param name="colorePedina">Colore della pedina del bot (es. 1 o -1).</param>
        /// <param name="tabellone">Riferimento al tabellone su cui giocare.</param>
        public Jarvis(int colorePedina, Tabellone tabellone)
        {
            this.colorePedina = colorePedina;
            tabelloneMatrice = tabellone;
            contaTurno = 0;
            mosseDisponibili = new List<(int, int)>();
            random = new Random();
            botAggressivo = new Bot1();
        }

        /// <summary>
        /// Calcola tutte le mosse valide disponibili per Jarvis nel turno corrente.
        /// </summary>
        private void mosseValide()
        {
            mosseDisponibili = tabelloneMatrice.getMosseDisponibili(colorePedina);
        }

        /// <summary>
        /// Applica modifiche alla matrice di priorità
        /// per migliorare le mosse esterne durante la seconda fase del gioco.
        /// </summary>
        /// <remarks>
        /// Questa fase scatta quando <c>contaTurno == 5</c>.
        /// Incrementa il valore di bordo e quasi-bordo evitando le caselle pericolose.
        /// </remarks>
        private void secondaFase()
        {
            for (int riga = 0; riga < 8; riga++)
            {
                for (int colonna = 0; colonna < 8; colonna++)
                {
                    if (colonna == 0 || colonna == 1 || colonna == 6 || colonna == 7)
                    {
                        if (!(colonna == 6 && riga == 1) || (colonna == 1 && riga == 6) ||
                            (colonna == 1 && riga == 1) || (colonna == 6 && riga == 6) ||
                            (colonna == 1 && riga == 0) || (colonna == 1 && riga == 7) ||
                            (colonna == 0 && riga == 6) || (colonna == 6 && riga == 7))
                        {
                            matricePriorita[riga, colonna] += 10;
                        }
                    }

                    if (riga == 0 || riga == 1 || riga == 6 || riga == 7)
                    {
                        if (!(colonna == 6 && riga == 1) || (colonna == 1 && riga == 6) ||
                            (colonna == 1 && riga == 1) || (colonna == 6 && riga == 6) ||
                            (colonna == 1 && riga == 0) || (colonna == 1 && riga == 7) ||
                            (colonna == 0 && riga == 6) || (colonna == 6 && riga == 7))
                        {
                            matricePriorita[riga, colonna] += 20;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Inizializza il bot aggressivo utilizzato durante la terza fase.
        /// </summary>
        /// <remarks>
        /// La terza fase inizia quando <c>contaTurno >= 10</c>.
        /// </remarks>
        private void terzaFase()
        {
            botAggressivo = new Bot1();
        }

        /// <summary>
        /// Determina e applica la mossa scelta da Jarvis in base alla fase di gioco.
        /// </summary>
        /// <returns>
        /// Una tupla contenente la coordinata della mossa scelta (riga, colonna).
        /// Restituisce <c>(-1, -1)</c> se non esistono mosse valide.
        /// </returns>
        public (int, int) effettuaMossa()
        {
            mosseValide();

            if (mosseDisponibili.Count == 0)
            {
                contaTurno++;
                return (-1, -1);
            }

            int posX = -1;
            int posY = -1;

            if (contaTurno == 5)
            {
                secondaFase();
            }
            else if (contaTurno >= 10)
            {
                if (contaTurno == 10)
                    terzaFase();

                var mossa = botAggressivo.effettuaMossa(tabelloneMatrice, colorePedina);

                if (mossa.Item1 == -1)
                {
                    contaTurno++;
                    return (posX, posY);
                }

                contaTurno++;
                tabelloneMatrice.posiziona(colorePedina, mossa.Item1, mossa.Item2);
                return mossa;
            }

            int valoreMax = 0;

            foreach (var tupla in mosseDisponibili)
            {
                int riga = tupla.Item1;
                int colonna = tupla.Item2;

                if (matricePriorita[riga, colonna] > valoreMax)
                {
                    valoreMax = matricePriorita[riga, colonna];
                    posX = riga;
                    posY = colonna;
                }

                if (matricePriorita[riga, colonna] == valoreMax && random.Next(1, 3) == 1)
                {
                    posX = riga;
                    posY = colonna;
                }
            }

            contaTurno++;
            tabelloneMatrice.posiziona(colorePedina, posX, posY);
            return (posX, posY);
        }
    }
}