using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class Bot1
    {
        /*
         * il Bot1 calcola, ad ogni turno, il numero
         * di pedine che può trasformare con ogni mossa,
         * eseguendo quella che permette di acquisire 
         * più pedine
         */

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
                        //calcola quante pedine trasforma questa mossa
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



        private int calcolaCatture(int[,] statoTabellone, int colorePedina, int x, int y)
        {
            int catturate = 0;

            int coloreAvversario;
            if (colorePedina == 1)
            {
                coloreAvversario = 2;
            }
            else
            {
                coloreAvversario = 1;
            }

            
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int i = x + dx[dir];
                int j = y + dy[dir];
                int cattureTemporanee = 0;

                //conta le pedine dell'avversario in questa direzione
                while (i >= 0 && i < 8 && j >= 0 && j < 8 && statoTabellone[i, j] == coloreAvversario)
                {
                    cattureTemporanee++;
                    i += dx[dir];
                    j += dy[dir];
                }

                //aggiunge le pedine al conteggio totale se la sequenza è valida
                if (i >= 0 && i < 8 && j >= 0 && j < 8 && statoTabellone[i, j] == colorePedina)
                {
                    catturate += cattureTemporanee;
                }
            }

            return catturate;
        }
    }

}
