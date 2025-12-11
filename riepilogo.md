# 📄 Riepilogo del Workflow e dell’Organizzazione del Lavoro

## 👥 Suddivisione del lavoro
Il progetto è stato sviluppato da un team composto da tre membri, ognuno dei quali ha realizzato un bot differente:

- **Hegi Duhanxhiu** – Bot *Aleksander*  
- **Luigi Mascioli** – Bot *Jarvis*  
- **Gabriele Travellini** – Bot *Silypall*  

Ogni sviluppatore ha progettato e implementato la propria feature in modo indipendente, seguendo un workflow Git strutturato.

---

## 🌿 GitFlow adottato
Per il versionamento è stato utilizzato un workflow **GitFlow**, che ha permesso:
- sviluppo parallelo,  
- integrazione controllata delle feature,  
- preparazione di una release stabile del progetto.  

### 1️⃣ Stato iniziale del repository
- Il branch `master` conteneva la versione originale del gioco *Othello* senza bot, considerata la base stabile del progetto.

### 2️⃣ Creazione del branch di sviluppo
- È stato creato il branch `develop`, punto centrale per integrare progressivamente tutte le nuove funzionalità.

### 3️⃣ Feature branch per ogni bot
Per ogni nuova funzionalità (bot) è stato creato un branch dedicato, derivato da `develop`:


develop
├── feature - aleksander 
├── feature - jarvis 
└── feature - silypall



Ogni branch conteneva esclusivamente:
- codice relativo al bot,  
- documentazione XML per Doxygen,  
- eventuali test o file aggiuntivi.  

**Vantaggi della strategia:**
- isolamento tra le implementazioni,  
- sviluppo parallelo senza conflitti,  
- facilità nei merge successivi.  

### 4️⃣ Merge delle feature nel develop
Completata la programmazione di ciascun bot, i tre feature branch sono stati mergiati in `develop`.  
In questa fase si è proceduto a:
- test funzionali delle nuove logiche di gioco,  
- risoluzione di conflitti minori,  
- verifica del corretto funzionamento dell’intero progetto.  

### 5️⃣ Creazione del branch di release
Una volta integrate tutte le feature, è stato creato il branch `release/v1.0`.  
In questo ramo sono state eseguite le attività di rifinitura:
- aggiornamento del `README.md`,  
- creazione dei file di documentazione delle feature,  
- generazione della documentazione automatica tramite Doxygen,  
- verifica globale del comportamento del gioco e dei tre bot.  

### 6️⃣ Merge del branch di release nel master
Terminata la preparazione della release:
- il branch `release/v1.0` è stato mergiato in `master`, ottenendo una versione stabile e completa del progetto;  
- successivamente è stato mergiato anche in `develop`, per mantenere allineato il ramo di sviluppo con lo stato finale del codice.  

---

## 📚 Documentazione prodotta

### 📄 README.md
È stato creato un README completo per l’utente, contenente:
- descrizione del gioco,  
- istruzioni per l’avvio,  
- spiegazione dei bot disponibili,  
- requisiti software e note utili.  

### 🧩 File markdown per le feature
Per ogni bot è stato realizzato un file di documentazione dedicato:
- `feature_aleksander.md`  
- `feature_jarvis.md`  
- `feature_silypall.md`  

Ciascuno include:
- overview della strategia adottata,  
- dettagli implementativi,  
- commenti XML,  
- eventuali test effettuati.  

### 🔧 Documentazione tramite Doxygen
È stato configurato Doxygen mediante un file `link doxygen sito.txt`, e sono stati generati:
- documentazione HTML,  
- diagrammi,  
- analisi delle classi e dei metodi.  

Il codice C# è stato commentato con **XML documentation tags**, compatibili con il sistema di generazione automatica.
