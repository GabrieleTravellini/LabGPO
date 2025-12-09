using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//TABELLONE CONSOLE
//TABELLONE CONSOLE
//TABELLONE CONSOLE
//TABELLONE CONSOLE
//TABELLONE CONSOLE
namespace Othello
{
    public class Tabellone
    {
        int[,] tabelloneMatrice;
        int colorePedina; //0 - vuoto, 1 nero, 2 bianco 


        public Tabellone()
        {
            tabelloneMatrice = new int[8, 8];
            pulisci();
        }



        public void pulisci()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    tabelloneMatrice[i, j] = 0;
                }
                tabelloneMatrice[3, 3] = 2;  //posizioni default per inizio partita
                tabelloneMatrice[3, 4] = 1;
                tabelloneMatrice[4, 3] = 1;
                tabelloneMatrice[4, 4] = 2;
            }
        }

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

        public int[,] getTabellone()
        {
            return tabelloneMatrice;
        }





        public bool posiziona(int colorePedina, int posizioneX, int posizioneY)
        {
            {

                if (posizioneX == -1 || posizioneY == -1)
                {
                    return false;
                }


                // Controlla che la casella sia vuota
                if (tabelloneMatrice[posizioneX, posizioneY] != 0)
                    return false;

                // Identifica il colore avversario
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

                // Direzioni di movimento (dx, dy) (es: dx=1, dy=0 è orizzontale verso destra)
                int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

                // Scorri tutte le direzioni
                for (int dir = 0; dir < 8; dir++)
                {
                    int x = posizioneX + dx[dir];
                    int y = posizioneY + dy[dir];
                    bool trovatoAvversario = false;

                    // Continua nella direzione finché trovi pedine dell'avversario
                    while (x >= 0 && x < 8 && y >= 0 && y < 8 && tabelloneMatrice[x, y] == coloreAvversario)
                    {
                        trovatoAvversario = true;
                        x += dx[dir];
                        y += dy[dir];
                    }

                    // Se trovi una pedina del giocatore, chiudi la cattura e cambia i colori delle pedine
                    if (trovatoAvversario && x >= 0 && x < 8 && y >= 0 && y < 8 && tabelloneMatrice[x, y] == colorePedina)
                    {
                        mossaValida = true;
                        x = posizioneX + dx[dir];
                        y = posizioneY + dy[dir];

                        // Cambia colore delle pedine catturate
                        while (tabelloneMatrice[x, y] == coloreAvversario)
                        {
                            tabelloneMatrice[x, y] = colorePedina;
                            x += dx[dir];
                            y += dy[dir];
                        }
                    }
                }

                // Se la mossa è valida, posiziona la pedina
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
        }

        public bool verificaMossa(int colorePedina, int posizioneX, int posizioneY)
        {
            {
                if (posizioneX == -1 || posizioneY == -1)
                {
                    return false;
                }
                // Controlla che la casella sia vuota
                if (tabelloneMatrice[posizioneX, posizioneY] != 0)
                    return false;

                // Identifica il colore avversario
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

                // Direzioni di movimento (dx, dy) (es: dx=1, dy=0 è orizzontale verso destra)
                int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
                int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

                // Scorri tutte le direzioni
                for (int dir = 0; dir < 8; dir++)
                {
                    int x = posizioneX + dx[dir];
                    int y = posizioneY + dy[dir];
                    bool trovatoAvversario = false;

                    // Continua nella direzione finché trovi pedine dell'avversario
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

                // Se la mossa è valida, posiziona la pedina
                if (mossaValida)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public int verificaVittoria()  //0 partita non ancora finita - 1 ha vinto nero, 2 ha vinto bianco, 3 pareggio
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

       
        public List<(int,int)> getMosseDisponibili(int colorePedina)
        {
            List<(int, int)> mosseDisponibili = new List<(int,int)>();
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






