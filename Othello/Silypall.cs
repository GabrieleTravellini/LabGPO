using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    public class Silypall
    {
        private Random random = new Random();

        /// <summary>
        /// Determina la mossa da effettuare per il bot.
        /// Esamina tutte le mosse valide, simula ciascuna di esse e sceglie
        /// una mossa "sicura" (che non permette catture immediate all’avversario).
        /// Se nessuna mossa è sicura, ne seleziona una casuale.
        /// </summary>
        /// <param name="t">Il tabellone corrente di gioco.</param>
        /// <param name="coloreBot">Il colore del bot (1 o 2).</param>
        /// <returns>La mossa scelta sotto forma di coordinate (riga, colonna) oppure (-1, -1) se nessuna mossa è disponibile.</returns>
        public (int, int) mossa(Tabellone t, int coloreBot)
        {
            int coloreAvversario;
            if (coloreBot == 1)
            {
                coloreAvversario = 2;
            }
            else
            {
                coloreAvversario = 1;
            }

            List<(int, int)> mosseValide = new List<(int, int)>();

            for (int riga = 0; riga < 8; riga++)
            {
                for (int colonna = 0; colonna < 8; colonna++)
                {
                    if (t.verificaMossa(coloreBot, riga, colonna))
                    {
                        mosseValide.Add((riga, colonna));
                    }
                }
            }

            if (mosseValide.Count == 0)
            {
                return (-1, -1);
            }

            foreach ((int, int) mossa in mosseValide)
            {
                Tabellone copiaTabellone = new Tabellone();
                copiaTabellone.copiaDa(t);

                copiaTabellone.posiziona(coloreBot, mossa.Item1, mossa.Item2);

                bool avversarioPuòMangiare = false;

                for (int riga = 0; riga < 8; riga++)
                {
                    for (int colonna = 0; colonna < 8; colonna++)
                    {
                        if (copiaTabellone.verificaMossa(coloreAvversario, riga, colonna))
                        {
                            avversarioPuòMangiare = true;
                        }
                    }
                }

                if (!avversarioPuòMangiare)
                {
                    return mossa;
                }
            }

            int indiceCasuale = random.Next(mosseValide.Count);
            return mosseValide[indiceCasuale];
        }
    }
}
