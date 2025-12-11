using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello
{
    /// <summary>
    /// Rappresenta il tabellone di gioco per Othello
    /// </summary>
    public class Tabellone
    {
        int[,] tabelloneMatrice;
        int colorePedina;

        /// <summary>
        /// Costruttore che inizializza il tabellone 8x8
        /// </summary>
        public Tabellone()
        {
            tabelloneMatrice = new int[8, 8];
            pulisci();
        }

        /// <summary>
        /// Pulisce il tabellone e imposta la configurazione iniziale di gioco
        /// </summary>
        public void pulisci()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tabelloneMatrice[i, j] = 0;
                }
                tabelloneMatrice[3, 3] = 2;
                tabelloneMatrice[3, 4] = 1;
                tabelloneMatrice[4, 3] = 1;
                tabelloneMatrice[4, 4] = 2;
            }
        }

        /// <summary>
        /// Copia lo stato del tabellone da un altro oggetto Tabellone
        /// </summary>
        /// <param name="altro">Il tabellone da copiare</param>
        public void copiaDa(Tabellone altro)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.tabelloneMatrice[i, j] = altro.tabelloneMatrice[i, j];
                }
            }
        }

        /// <summary>
        /// Restituisce la matrice del tabellone
        /// </summary>
        /// <returns>Array bidimensionale rappresentante il tabellone</returns>
        public int[,] getTabellone()
        {
            return tabelloneMatrice;
        }

        /// <summary>
        /// Posiziona una pedina sul tabellone e cattura le pedine avversarie
        /// </summary>
        /// <param name="colorePedina">Colore della pedina da posizionare (1 = nero, 2 = bianco)</param>
        /// <param name="posizioneX">Coordinata X della posizione</param>
        /// <param name="posizioneY">Coordinata Y della posizione</param>
        /// <returns>True se la mossa è valida e la pedina è stata posizionata, False altrimenti</returns>
        public bool posiziona(int colorePedina, int posizioneX, int posizioneY)
        {
            if (posizioneX == -1 || posizioneY == -1)
            {
                return false;
            }

            if (tabelloneMatrice[posizioneX, posizioneY] != 0)
                return false;

            int coloreAvversario;
            if (colorePedina == 1)
            {
                coloreAvversario = 2;
            }
            else
            {
                coloreAvversario = 1;
            }

            bool mossaValida = false;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int x = posizioneX + dx[dir];
                int y = posizioneY + dy[dir];
                bool trovatoAvversario = false;

                while (x >= 0 && x < 8 && y >= 0 && y < 8 && tabelloneMatrice[x, y] == coloreAvversario)
                {
                    trovatoAvversario = true;
                    x += dx[dir];
                    y += dy[dir];
                }

                if (trovatoAvversario && x >= 0 && x < 8 && y >= 0 && y < 8 && tabelloneMatrice[x, y] == colorePedina)
                {
                    mossaValida = true;
                    x = posizioneX + dx[dir];
                    y = posizioneY + dy[dir];

                    while (tabelloneMatrice[x, y] == coloreAvversario)
                    {
                        tabelloneMatrice[x, y] = colorePedina;
                        x += dx[dir];
                        y += dy[dir];
                    }
                }
            }

            if (mossaValida)
            {
                tabelloneMatrice[posizioneX, posizioneY] = colorePedina;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica se una mossa è valida senza effettuarla
        /// </summary>
        /// <param name="colorePedina">Colore della pedina (1 = nero, 2 = bianco)</param>
        /// <param name="posizioneX">Coordinata X della posizione</param>
        /// <param name="posizioneY">Coordinata Y della posizione</param>
        /// <returns>True se la mossa è valida, False altrimenti</returns>
        public bool verificaMossa(int colorePedina, int posizioneX, int posizioneY)
        {
            if (posizioneX == -1 || posizioneY == -1)
            {
                return false;
            }

            if (tabelloneMatrice[posizioneX, posizioneY] != 0)
                return false;

            int coloreAvversario;
            if (colorePedina == 1)
            {
                coloreAvversario = 2;
            }
            else
            {
                coloreAvversario = 1;
            }

            bool mossaValida = false;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int dir = 0; dir < 8; dir++)
            {
                int x = posizioneX + dx[dir];
                int y = posizioneY + dy[dir];
                bool trovatoAvversario = false;

                while (x >= 0 && x < 8 && y >= 0 && y < 8 && tabelloneMatrice[x, y] == coloreAvversario)
                {
                    trovatoAvversario = true;
                    x += dx[dir];
                    y += dy[dir];
                }

                if (trovatoAvversario && x >= 0 && x < 8 && y >= 0 && y < 8 && tabelloneMatrice[x, y] == colorePedina)
                {
                    mossaValida = true;
                }
            }

            if (mossaValida)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Verifica lo stato della partita e determina il vincitore
        /// </summary>
        /// <returns>0 = partita in corso, 1 = vittoria nero, 2 = vittoria bianco, 3 = pareggio</returns>
        public int verificaVittoria()
        {
            bool conteggio = false;
            int contatoreNero = 0;
            int contatoreBianco = 0;

            for (int riga = 0; riga < 8; riga++)
            {
                for (int colonna = 0; colonna < 8; colonna++)
                {
                    if (tabelloneMatrice[riga, colonna] == 0)
                    {
                        conteggio = verificaMossa(1, riga, colonna);
                        if (!conteggio)
                        {
                            conteggio = verificaMossa(2, riga, colonna);
                        }
                        if (conteggio == true)
                        {
                            return 0;
                        }
                    }
                }
            }

            if (conteggio == false)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (tabelloneMatrice[i, j] == 1)
                        {
                            contatoreNero++;
                        }
                        else if (tabelloneMatrice[i, j] == 2)
                        {
                            contatoreBianco++;
                        }
                    }
                }
            }
            if (contatoreNero > contatoreBianco)
            {
                return 1;
            }
            else if (contatoreNero < contatoreBianco)
            {
                return 2;
            }
            return 3;
        }

        /// <summary>
        /// Restituisce l'elenco di tutte le mosse disponibili per il colore specificato
        /// </summary>
        /// <param name="colorePedina">Colore della pedina (1 = nero, 2 = bianco)</param>
        /// <returns>Lista di tuple contenenti le coordinate (riga, colonna) delle mosse valide</returns>
        public List<(int, int)> getMosseDisponibili(int colorePedina)
        {
            List<(int, int)> mosseDisponibili = new List<(int, int)>();
            mosseDisponibili.Clear();
            for (int riga = 0; riga < 8; riga++)
            {
                for (int colonna = 0; colonna < 8; colonna++)
                {
                    if (verificaMossa(colorePedina, riga, colonna))
                    {
                        mosseDisponibili.Add((riga, colonna));
                    }
                }
            }
            return mosseDisponibili;
        }
    }
}