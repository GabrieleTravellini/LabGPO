using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    /// <summary>
    /// Bot che sceglie la mossa catturando il maggior numero possibile di pedine
    /// in base allo stato corrente del tabellone.
    /// </summary>
    public class Bot1
    {
        /*
         * Il Bot1 calcola, ad ogni turno, il numero
         * di pedine che può trasformare con ogni mossa,
         * eseguendo quella che permette di acquisire 
         * più pedine.
         */

        /// <summary>
        /// Determina e restituisce la mossa migliore disponibile per il bot,
        /// ovvero quella che cattura il maggior numero di pedine.
        /// </summary>
        /// <param name="tabellone">Istanza del tabellone di gioco.</param>
        /// <param name="colorePedina">Colore del bot (1 o 2).</param>
        /// <returns>
        /// Una tupla (x, y) contenente la coordinata della mossa scelta.
        /// Restituisce (-1, -1) se non ci sono mosse valide.
        /// </returns>
        public (int, int) effettuaMossa(Tabellone tabellone, int colorePedina)
        {
            int[,] statoTabellone = tabellone.getTabellone();
            int mossaX = -1, mossaY = -1;
            int maxPedine = 0;

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (tabellone.verificaMossa(colorePedina, x, y))
                    {
                        int catturate = calcolaCatture(statoTabellone, colorePedina, x, y);
                        if (catturate > maxPedine)
                        {
                            maxPedine = catturate;
                            mossaX = x;
                            mossaY = y;
                        }
                    }
                }
            }
            return (mossaX, mossaY);
        }

        /// <summary>
        /// Calcola quante pedine verrebbero catturate posizionando
        /// una pedina del colore specificato nella cella indicata.
        /// </summary>
        /// <param name="statoTabellone">Matrice 8x8 che rappresenta il tabellone corrente.</param>
        /// <param name="colorePedina">Colore del giocatore.</param>
        /// <param name="x">Coordinata X della mossa.</param>
        /// <param name="y">Coordinata Y della mossa.</param>
        /// <returns>Il numero totale di pedine catturate con quella mossa.</returns>
        private int calcolaCatture(int[,] statoTabellone, int colorePedina, int x, int y)
        {
            int catturate = 0;

            int coloreAvversario = colorePedina == 1 ? 2 : 1;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int i = x + dx[dir];
                int j = y + dy[dir];
                int cattureTemporanee = 0;

                while (i >= 0 && i < 8 && j >= 0 && j < 8 && statoTabellone[i, j] == coloreAvversario)
                {
                    cattureTemporanee++;
                    i += dx[dir];
                    j += dy[dir];
                }

                if (i >= 0 && i < 8 && j >= 0 && j < 8 && statoTabellone[i, j] == colorePedina)
                {
                    catturate += cattureTemporanee;
                }
            }

            return catturate;
        }
    }
}