# ✨ Feature: Bot Silypall (Livello Intermedio – Strategia Difensiva)

## 🎯 Obiettivo della feature
Aggiungere al gioco Othello un nuovo bot chiamato **Silypall**, caratterizzato da una strategia orientata alla difesa.

Silypall non tenta di catturare il massimo numero di pedine, né applica strategie avanzate basate su matrici o fasi (come Jarvis).  
Il suo obiettivo principale è:

> **effettuare una mossa che non permetta all’avversario di catturare pedine al turno successivo.**

È quindi un bot di **difficoltà intermedia**, collocato tra Aleksander (facile) e Jarvis (difficile).

---

## 💡 Motivazione

Nel gioco serviva un livello aggiuntivo di difficoltà:

- più “intelligente” rispetto ad Aleksander, che pensa solo al guadagno immediato,
- meno complesso di Jarvis, che applica strategie multi-fase e un ragionamento avanzato.

Silypall rappresenta quindi un bot **equilibrato**, ideale per chi vuole una sfida moderata.

---

## 🧠 Strategia del bot

La strategia di Silypall si articola in due fasi:

---

### 🟢 **1. Ricerca della mossa sicura**
Per ogni mossa valida:

1. crea una copia del tabellone;
2. simula la mossa del bot nella copia;
3. controlla se dopo quella mossa l’avversario ha mosse valide.

Se **l’avversario non può catturare pedine**, allora:

> ✔ Silypall sceglie quella mossa immediatamente.

---

### 🔴 **2. Nessuna mossa è sicura → scelta casuale**
Se tutte le mosse permettono all’avversario di catturare:

> Silypall seleziona **casualmente una mossa** tra quelle disponibili.

---

## 🧩 Funzionamento interno

Silypall utilizza tre elementi principali:

- ricerca di tutte le mosse valide;
- simulazione del tabellone tramite `copiaDa()` e `posiziona()`;
- controllo delle mosse dell’avversario dopo la simulazione.

Il risultato è un comportamento *prudente* e coerente.

---

## ⚙️ Codice completo della classe Silypall

```csharp
using System;
using System.Collections.Generic;

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
        /// <returns>
        /// La mossa scelta sotto forma di coordinate (riga, colonna)
        /// oppure (-1, -1) se nessuna mossa è disponibile.
        /// </returns>
        public (int, int) mossa(Tabellone t, int coloreBot)
        {
            int coloreAvversario = (coloreBot == 1) ? 2 : 1;

            List<(int, int)> mosseValide = new List<(int, int)>();

            // Recupero tutte le mosse valide
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

            // Controllo se esiste una mossa sicura
            foreach ((int, int) mossa in mosseValide)
            {
                Tabellone copiaTabellone = new Tabellone();
                copiaTabellone.copiaDa(t);

                // Simula la mossa
                copiaTabellone.posiziona(coloreBot, mossa.Item1, mossa.Item2);

                bool avversarioPuòMangiare = false;

                // Verifica se dopo la simulazione l'avversario ha mosse valide
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

                // Se l'avversario NON può catturare, è una mossa sicura
                if (!avversarioPuòMangiare)
                {
                    return mossa;
                }
            }

            // Se nessuna mossa è sicura, sceglie una mossa casuale
            int indiceCasuale = random.Next(mosseValide.Count);
            return mosseValide[indiceCasuale];
        }
    }
}
