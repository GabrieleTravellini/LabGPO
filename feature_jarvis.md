# ✨ Feature: Bot Jarvis (Livello Avanzato)

## 🎯 Obiettivo della feature
Aggiungere al gioco Othello un bot avanzato, denominato **Jarvis**, dotato di un sistema decisionale a più fasi.  
Al contrario di Aleksander (bot facile), Jarvis utilizza strategie progressivamente più intelligenti man mano che la partita avanza.

Lo scopo è fornire una modalità di difficoltà **Alta**, adatta a giocatori esperti o a chi vuole sfidare un’intelligenza più realistica.

---

## 💡 Motivazione

Il progetto richiedeva l’inserimento di nuove funzionalità significative.  
Il bot Jarvis risponde a questa necessità introducendo:

- una matrice di priorità strategica,
- una progressione in "fasi" legate al numero di turni,
- un’integrazione con il bot aggressivo (Bot1),
- logiche più vicine a un giocatore umano esperto.

Rispetto ad Aleksander, Jarvis ragiona in modo più complesso e cambia strategia durante la partita.

---

## 🧠 Strategia del bot

Jarvis utilizza **tre fasi strategiche**:

---

### 🟢 **1. Prima fase (Turni 0–4)**  
Utilizza una **matrice di priorità 8×8** che assegna un valore numerico a ogni cella:

- gli **angoli valgono molto** (100),
- le **caselle X / C** sono penalizzate (–50 / –10),
- l’interno vale moderatamente (5).

Jarvis sceglie sempre la mossa con il valore più alto nella matrice.

Questa fase serve a costruire la base del controllo.

---

### 🟡 **2. Seconda fase (Turno 5)**  
La matrice viene modificata con aumenti specifici:

- +10 per i bordi,
- +20 per le righe/colonne esterne,

evitando le zone pericolose.

Questo porta Jarvis a preferire mosse che migliorano il controllo degli angoli senza rischiare troppo.

---

### 🔴 **3. Terza fase (Turni ≥ 10)**  
Jarvis passa completamente al bot aggressivo:

> **Seleziona la mossa che cattura più pedine**, usando Bot1.

Questa fase simula un giocatore umano che, nella parte finale della partita, massimizza il guadagno immediato.

---

## 🧩 Funzionamento interno

Jarvis si basa sulle seguenti parti principali:

- `mosseValide()` → recupera le mosse legali.  
- `secondaFase()` → modifica la matrice di priorità.  
- `terzaFase()` → attiva Bot1.  
- `effettuaMossa()` → gestisce la logica delle 3 fasi.

---

## ⚙️ Codice completo della classe Jarvis

```csharp
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
        /// Recupera tutte le mosse valide per il bot nel turno corrente.
        /// </summary>
        private void mosseValide()
        {
            mosseDisponibili = tabelloneMatrice.getMosseDisponibili(colorePedina);
        }

        /// <summary>
        /// Modifica la matrice di priorità durante la seconda fase della partita,
        /// aumentando il valore delle mosse di bordo e quasi-bordo.
        /// </summary>
        private void secondaFase()
        {
            for (int riga = 0; riga < 8; riga++)
            {
                for (int colonna = 0; colonna < 8; colonna++)
                {
                    if (colonna == 0 || colonna == 1 || colonna == 6 || colonna == 7)
                        matricePriorita[riga, colonna] += 10;

                    if (riga == 0 || riga == 1 || riga == 6 || riga == 7)
                        matricePriorita[riga, colonna] += 20;
                }
            }
        }

        /// <summary>
        /// Attiva la terza fase, inizializzando il bot aggressivo.
        /// </summary>
        private void terzaFase()
        {
            botAggressivo = new Bot1();
        }

        /// <summary>
        /// Sceglie e applica la mossa di Jarvis in base alla fase della partita.
        /// </summary>
        /// <returns>La tupla (x,y) della mossa scelta, oppure (-1,-1) se nessuna mossa è valida.</returns>
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
