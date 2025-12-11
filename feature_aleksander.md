# ✨ Feature: Bot Aleksander (Livello Facile)

## 🎯 Obiettivo della feature
Aggiungere al gioco Othello un bot semplice e intuitivo, chiamato **Aleksander**, capace di effettuare una mossa valida senza utilizzare strategie complesse.

Lo scopo del bot è offrire un livello di difficoltà **Facile**, pensato per:

- utenti principianti,
- studenti che giocano per la prima volta,
- giocatori che vogliono apprendere gradualmente le logiche di Othello prima di affrontare i bot più difficili.

---

## 💡 Motivazione

Prima dell’introduzione di Aleksander, il gioco offriva solo modalità PvP e bot di livello più elevato.  
Serviva quindi un bot:

- semplice da implementare,
- rapido nelle decisioni,
- che giocasse mosse corrette senza previsioni complesse,
- ideale per chi si approccia per la prima volta al gioco.

Aleksander risolve questa esigenza offrendo un'intelligenza base ma coerente.

---

## 🧠 Strategia del bot

Aleksander segue la logica:

> **Scegli la mossa che cattura più pedine tra tutte quelle disponibili.**

Il suo comportamento è riassumibile così:

1. Controlla tutte le posizioni 8×8 del tabellone.  
2. Per ogni cella valida:
   - calcola quante pedine del colore avversario verrebbero catturate.
3. Tiene traccia della mossa che cattura più pedine.
4. Alla fine seleziona la mossa migliore.

È una strategia semplice, diretta e abbastanza efficace nelle prime fasi di apprendimento del gioco.

---

## 🧩 Funzionamento interno

Aleksander utilizza due metodi principali:

---

### ✔ `effettuaMossa(Tabellone tabellone, int colorePedina)`

- Scorre ogni cella della scacchiera.  
- Usa `tabellone.verificaMossa()` per capire se è valida.  
- Calcola le catture potenziali con `calcolaCatture()`.  
- Memorizza la mossa con il punteggio più alto.  
- Ritorna una tupla `(x, y)`.  
- Se non ci sono mosse valide → ritorna `(-1, -1)`.

---

### ✔ `calcolaCatture(int[,] statoTabellone, int colorePedina, int x, int y)`

Per ogni direzione (8 direzioni possibili):

- avanza fino a incontrare pedine avversarie,
- le conta,
- si ferma quando trova:
  - una pedina del proprio colore → catture valide, oppure  
  - un bordo/cella vuota → catture annullate.

Restituisce il numero totale di pedine catturate.

---

## ⚙️ Codice della classe (per riferimento)

```csharp
/// <summary>
/// Bot che sceglie la mossa catturando il maggior numero possibile di pedine
/// in base allo stato corrente del tabellone.
/// </summary>
public class Bot1
{
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
            int cattureTemp = 0;

            while (i >= 0 && i < 8 &&
                   j >= 0 && j < 8 &&
                   statoTabellone[i, j] == coloreAvversario)
            {
                cattureTemp++;
                i += dx[dir];
                j += dy[dir];
            }

            if (i >= 0 && i < 8 &&
                j >= 0 && j < 8 &&
                statoTabellone[i, j] == colorePedina)
            {
                catturate += cattureTemp;
            }
        }

        return catturate;
    }
}

