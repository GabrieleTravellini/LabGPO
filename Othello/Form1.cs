using System.Diagnostics;
using System.Security.Cryptography;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

namespace Othello
{
    /// <summary>
    /// Form principale per il gioco Othello
    /// </summary>
    public partial class Form1 : Form
    {
        Dictionary<PictureBox, Tuple<int, int>> ciao;
        List<(int, int)> ciaoList;
        private int[,] matrice;
        private Bot1 Bot;
        private Jarvis Jarvis;
        private Silypall Silypall;
        private Tabellone tabellone = new Tabellone();
        private int pedina;
        private object bot;
        private List<int[,]> undo;
        private List<int[,]> replay1;

        /// <summary>
        /// Costruttore del form principale
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            ciao = new Dictionary<PictureBox, Tuple<int, int>>
                {
                    { pictureBox1, new Tuple<int, int>(0, 0) },
                    { pictureBox2, new Tuple<int, int>(0, 1) },
                    { pictureBox3, new Tuple<int, int>(0, 2) },
                    { pictureBox4, new Tuple<int, int>(0, 3) },
                    { pictureBox5, new Tuple<int, int>(0, 4) },
                    { pictureBox6, new Tuple<int, int>(0, 5) },
                    { pictureBox7, new Tuple<int, int>(0, 6) },
                    { pictureBox8, new Tuple<int, int>(0, 7) },
                    { pictureBox9, new Tuple<int, int>(1, 0) },
                    { pictureBox10, new Tuple<int, int>(1, 1) },
                    { pictureBox11, new Tuple<int, int>(1, 2) },
                    { pictureBox12, new Tuple<int, int>(1, 3) },
                    { pictureBox13, new Tuple<int, int>(1, 4) },
                    { pictureBox14, new Tuple<int, int>(1, 5) },
                    { pictureBox15, new Tuple<int, int>(1, 6) },
                    { pictureBox16, new Tuple<int, int>(1, 7) },
                    { pictureBox17, new Tuple<int, int>(2, 0) },
                    { pictureBox18, new Tuple<int, int>(2, 1) },
                    { pictureBox19, new Tuple<int, int>(2, 2) },
                    { pictureBox20, new Tuple<int, int>(2, 3) },
                    { pictureBox21, new Tuple<int, int>(2, 4) },
                    { pictureBox22, new Tuple<int, int>(2, 5) },
                    { pictureBox23, new Tuple<int, int> (2, 6) },
                    { pictureBox24, new Tuple<int, int>(2, 7) },
                    { pictureBox25, new Tuple<int, int>(3, 0) },
                    { pictureBox26, new Tuple<int, int>(3, 1) },
                    { pictureBox27, new Tuple<int, int>(3, 2) },
                    { pictureBox28, new Tuple<int, int>(3, 3) },
                    { pictureBox29, new Tuple<int, int>(3, 4) },
                    { pictureBox30, new Tuple<int, int>(3, 5) },
                    { pictureBox31, new Tuple<int, int>(3, 6) },
                    { pictureBox32, new Tuple<int, int>(3, 7) },
                    { pictureBox33, new Tuple<int, int>(4, 0) },
                    { pictureBox34, new Tuple<int, int>(4, 1) },
                    { pictureBox35, new Tuple<int, int>(4, 2) },
                    { pictureBox36, new Tuple<int, int>(4, 3) },
                    { pictureBox37, new Tuple<int, int>(4, 4) },
                    { pictureBox38, new Tuple<int, int>(4, 5) },
                    { pictureBox39, new Tuple<int, int>(4, 6) },
                    { pictureBox40, new Tuple<int, int>(4, 7) },
                    { pictureBox41, new Tuple<int, int>(5, 0) },
                    { pictureBox42, new Tuple<int, int>(5, 1) },
                    { pictureBox43, new Tuple<int, int>(5, 2) },
                    { pictureBox44, new Tuple<int, int>(5, 3) },
                    { pictureBox45, new Tuple<int, int>(5, 4) },
                    { pictureBox46, new Tuple<int, int>(5, 5) },
                    { pictureBox47, new Tuple<int, int>(5, 6) },
                    { pictureBox48, new Tuple<int, int>(5, 7) },
                    { pictureBox49, new Tuple<int, int>(6, 0) },
                    { pictureBox50, new Tuple<int, int>(6, 1) },
                    { pictureBox51, new Tuple<int, int>(6, 2) },
                    { pictureBox52, new Tuple<int, int>(6, 3) },
                    { pictureBox53, new Tuple<int, int>(6, 4) },
                    { pictureBox54, new Tuple<int, int>(6, 5) },
                    { pictureBox55, new Tuple<int, int>(6, 6) },
                    { pictureBox56, new Tuple<int, int>(6, 7) },
                    { pictureBox57, new Tuple<int, int>(7, 0) },
                    { pictureBox58, new Tuple<int, int> (7, 1) },
                    { pictureBox59, new Tuple<int, int>(7, 2) },
                    { pictureBox60, new Tuple<int, int>(7, 3) },
                    { pictureBox61, new Tuple<int, int>(7, 4) },
                    { pictureBox62, new Tuple<int, int>(7, 5) },
                    { pictureBox63, new Tuple<int, int>(7, 6) },
                    { pictureBox64, new Tuple<int, int>(7, 7) }
                };
            ciaoList = new List<(int, int)>();
            undo = new List<int[,]>();
            replay1 = new List<int[,]>();
            pictureBox66.BackgroundImage = Image.FromFile("samurai.png");
            pictureBox66.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox65.BackgroundImage = Image.FromFile("icona othello.png");
            pictureBox65.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox67.BackgroundImage = Image.FromFile("vs.png");
            pictureBox67.SizeMode = PictureBoxSizeMode.StretchImage;

            foreach (PictureBox pictureBox in ciao.Keys)
            {
                pictureBox.Click += PictureBox_Click;
            }
        }

        Dictionary<int, char> conversioniAsseX = new Dictionary<int, char>
        {
            {0, 'A'},
            {1, 'B'},
            {2, 'C'},
            {3, 'D'},
            {4, 'E'},
            {5, 'F'},
            {6, 'G'},
            {7, 'H'},
        };

        private bool isFirstClick = true;

        /// <summary>
        /// Salva lo storico delle mosse su file
        /// </summary>
        private void file()
        {
            string filePath = "gameData.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (int[,] matrice in undo)
                    {
                        for (int i = 0; i < matrice.GetLength(0); i++)
                        {
                            for (int j = 0; j < matrice.GetLength(1); j++)
                            {
                                writer.Write(matrice[i, j]);
                                if (j < matrice.GetLength(1) - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();
                        }
                        writer.WriteLine("---");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore: {ex.Message}");
            }
        }

        /// <summary>
        /// Aggiorna la ListBox con lo storico delle mosse
        /// </summary>
        private void AggiornaLB()
        {
            listBox1.Items.Clear();
            foreach ((int, int) b in ciaoList)
            {
                listBox1.Items.Add($"Posizionamento in ({conversioniAsseX[b.Item2]}, {b.Item1 + 1})");
            }
        }

        /// <summary>
        /// Imposta lo sfondo verde per tutte le caselle del tabellone
        /// </summary>
        private void SfondoPanel()
        {
            foreach (PictureBox p in ciao.Keys)
            {
                p.BorderStyle = BorderStyle.FixedSingle;
                p.BackColor = Color.Green;
            }
        }

        /// <summary>
        /// Gestore evento caricamento form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            SfondoPanel();
        }

        /// <summary>
        /// Gestore evento click sulla label1
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        /// <summary>
        /// Gestore evento click sulla pictureBox1
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Gestore evento paint del panel2
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        /// <summary>
        /// Gestore evento click sul controllo A
        /// </summary>
        private void A_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Gestore evento cambio selezione domainUpDown1
        /// </summary>
        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Gestore evento paint del panel1
        /// </summary>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private int difficolta;
        private bool bottonestart = false;

        /// <summary>
        /// Avvia una nuova partita contro il bot
        /// </summary>
        private void button_START_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            bottonestart = true;
            utenteVSutente = false;
            if (radioButton_Media.Checked)
            {
                difficolta = 2;
            }
            else if (radioButton_Difficile.Checked)
            {
                difficolta = 3;
            }
            else
            {
                difficolta = 1;
            }
            listBox1.Items.Clear();
            tabellone.pulisci();
            Avvio(difficolta);
        }

        /// <summary>
        /// Inizializza il gioco selezionando il bot in base alla difficoltà
        /// </summary>
        /// <param name="difficolta">Livello di difficoltà (1 = facile, 2 = medio, 3 = difficile)</param>
        private void Avvio(int difficolta)
        {
            if (difficolta == 1)
            {
                bot = new Bot1();
            }
            else if (difficolta == 2)
            {
                bot = new Silypall();
            }
            else if (difficolta == 3)
            {
                bot = new Jarvis(2, tabellone);
            }

            pedina = 1;
            EvidenziatoreMoltoBello();
        }

        /// <summary>
        /// Gestisce il click su una casella del tabellone
        /// </summary>
        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (pedina != 1 && utenteVSutente == false) return;

            PictureBox clickedBox = sender as PictureBox;
            (int x, int y) = ciao[clickedBox];

            bool risultato = tabellone.posiziona(pedina, x, y);
            if (!risultato)
            {
                MessageBox.Show("Mossa non valida! Riprova.");
                return;
            }

            AggiornaListBox(pedina, x, y);
            CambiaTurno();
            bool valido = mosseValide();
            if (!valido && tabellone.verificaVittoria() == 0)
            {
                MessageBox.Show("Passo turno perchè nessuna mossa valida");
                CambiaTurno();
            }
            EvidenziatoreMoltoBello();
        }

        /// <summary>
        /// Passa il turno al giocatore successivo
        /// </summary>
        private void CambiaTurno()
        {
            if (pedina == 1)
            {
                pedina = 2;
                label_giocatore.Text = "Tocca al Bianco";
            }
            else
            {
                pedina = 1;
                label_giocatore.Text = "Tocca al Nero";
            }

            int vincitore = tabellone.verificaVittoria();
            if (vincitore != 0)
            {
                EvidenziatoreMoltoBello();
                MostraVincitore(vincitore);
                return;
            }

            if (pedina == 2 && utenteVSutente == false)
            {
                EffettuaMossaBot();
            }
        }

        /// <summary>
        /// Verifica se ci sono mosse valide disponibili
        /// </summary>
        /// <returns>True se ci sono mosse valide, False altrimenti</returns>
        private bool mosseValide()
        {
            List<(int, int)> mosse = tabellone.getMosseDisponibili(pedina);
            if (mosse.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Esegue la mossa del bot con un ritardo di 1 secondo
        /// </summary>
        private async void EffettuaMossaBot()
        {
            int botX = -1, botY = -1;

            if (bot is Bot1)
            {
                await Task.Delay(1000);
                (botX, botY) = ((Bot1)bot).effettuaMossa(tabellone, pedina);
            }
            else if (bot is Silypall)
            {
                await Task.Delay(1000);
                (botX, botY) = ((Silypall)bot).mossa(tabellone, pedina);
            }
            else if (bot is Jarvis)
            {
                await Task.Delay(1000);
                (botX, botY) = ((Jarvis)bot).effettuaMossa();
            }

            if (botX != -1 && botY != -1)
            {
                tabellone.posiziona(pedina, botX, botY);
            }
            else
            {
                MessageBox.Show("Il bot non può effettuare mosse.");
            }

            AggiornaListBox(pedina, botX, botY);
            CambiaTurno();
            EvidenziatoreMoltoBello();
        }

        /// <summary>
        /// Aggiorna la ListBox con le coordinate della mossa effettuata
        /// </summary>
        /// <param name="pedina">Colore della pedina (1 = nero, 2 = bianco)</param>
        /// <param name="x">Coordinata X della mossa</param>
        /// <param name="y">Coordinata Y della mossa</param>
        private void AggiornaListBox(int pedina, int x, int y)
        {
            try
            {
                string turno;

                if (pedina == 1)
                {
                    turno = "Giocatore (Nero)";
                }
                else
                {
                    turno = "Bot (Bianco)";
                }

                string colonna = conversioniAsseX[y].ToString();
                int riga = x + 1;

                listBox1.Items.Add($"{turno}: ({colonna}, {riga})");
            }
            catch (Exception)
            {
                MessageBox.Show("nessuna coordinata stampata, turno saltato");
            }
        }

        /// <summary>
        /// Aggiorna la visualizzazione del tabellone sincronizzando con la matrice
        /// </summary>
        private void AggiornaTabellone()
        {
            int[,] matrice = tabellone.getTabellone();
            undo.Add((int[,])matrice.Clone());
            file();

            foreach (PictureBox box in ciao.Keys)
            {
                Tuple<int, int> coordinates = ciao[box];
                int x = coordinates.Item1;
                int y = coordinates.Item2;

                if (matrice[x, y] == 1)
                {
                    string percorso = "p_nera.png";
                    box.Image = Image.FromFile(percorso);
                }
                else if (matrice[x, y] == 2)
                {
                    string percorso = "p_bianca.png";
                    box.Image = Image.FromFile(percorso);
                }
                else
                {
                    box.BackColor = Color.Green;
                    box.Image = null;
                }
                box.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        /// <summary>
        /// Mostra un messaggio con il vincitore della partita
        /// </summary>
        /// <param name="vincitore">Codice del vincitore (1 = nero, 2 = bianco, 3 = pareggio)</param>
        private void MostraVincitore(int vincitore)
        {
            string messaggio = "errore nel calcolo della vincita";

            if (vincitore == 1)
            {
                if (utenteVSutente == false)
                {
                    messaggio = "Hai vinto! Complimenti!";
                }
                messaggio = "Ha vinto il Nero";
            }
            else if (vincitore == 2)
            {
                if (utenteVSutente == false)
                {
                    messaggio = "Ha vinto il bot!";
                }
                messaggio = "Ha vinto il Bianco";
            }
            else if (vincitore == 3)
            {
                messaggio = "Pareggio!";
            }

            MessageBox.Show(messaggio);
            panel1.Show();
        }

        /// <summary>
        /// Evidenzia le mosse valide disponibili sul tabellone
        /// </summary>
        private void EvidenziatoreMoltoBello()
        {
            AggiornaTabellone();

            if (tabellone.getMosseDisponibili(pedina).Count > 0)
            {
                foreach (PictureBox box in ciao.Keys)
                {
                    foreach ((int, int) tupla in tabellone.getMosseDisponibili(pedina))
                    {
                        if (ciao[box].Item1 == tupla.Item1 && ciao[box].Item2 == tupla.Item2)
                        {
                            string percorso = "p_gialla.png";
                            box.Image = Image.FromFile(percorso);
                        }
                        box.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
        }

        /// <summary>
        /// Carica lo storico delle partite dal file
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            string nome = "gameData.txt";
            List<int[,]> matriceList = new List<int[,]>();
            List<int[]> matriceTemp = new List<int[]>();

            try
            {
                string[] righe = File.ReadAllLines(nome);

                foreach (string riga in righe)
                {
                    if (riga.Trim() == "---")
                    {
                        if (matriceTemp.Count > 0)
                        {
                            int[,] matrice = ConvertiInMatrice(matriceTemp);
                            matriceList.Add(matrice);
                            MessageBox.Show($"ciao{matrice}");
                            matriceTemp.Clear();
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(riga))
                    {
                        try
                        {
                            int[] rigaNumeri = riga.Split(',').Select(int.Parse).ToArray();
                            matriceTemp.Add(rigaNumeri);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show($"Errore di formattazione nella riga: {riga}");
                        }
                    }
                }

                replay1 = matriceList;
                MessageBox.Show($"Caricate {replay1.Count} matrici dal file!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento: {ex.Message}");
            }
        }

        /// <summary>
        /// Converte una lista di array in una matrice bidimensionale
        /// </summary>
        /// <param name="matriceTemp">Lista di array da convertire</param>
        /// <returns>Matrice bidimensionale</returns>
        private int[,] ConvertiInMatrice(List<int[]> matriceTemp)
        {
            int righe = matriceTemp.Count;
            int colonne = matriceTemp[0].Length;
            int[,] matrice = new int[righe, colonne];

            for (int i = 0; i < righe; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    matrice[i, j] = matriceTemp[i][j];
                }
            }

            return matrice;
        }

        /// <summary>
        /// Nasconde il pannello iniziale all'avvio del gioco
        /// </summary>
        private void iniziaBTN_Click(object sender, EventArgs e)
        {
            panelIniziale.Visible = false;
        }

        /// <summary>
        /// Gestore evento click sulla pictureBox65
        /// </summary>
        private void pictureBox65_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Apre il sito web FNGO nel browser
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            string url = "https://www.fngo.it/";
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'apertura della pagina: {ex.Message}", "Errore");
            }
        }

        /// <summary>
        /// Gestore evento click sul titolo della ListBox
        /// </summary>
        private void titoloLB_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Carica le matrici salvate dal file per il replay
        /// </summary>
        private void button1_Click_1(object sender, EventArgs e)
        {
            string nome = "gameData.txt";
            List<int[,]> matriceList = new List<int[,]>();
            List<int[]> matriceTemp = new List<int[]>();

            try
            {
                string[] righe = File.ReadAllLines(nome);

                foreach (string riga in righe)
                {
                    if (riga.Trim() == "---")
                    {
                        if (matriceTemp.Count > 0)
                        {
                            int[,] matrice = ConvertiInMatrice(matriceTemp);
                            matriceList.Add(matrice);
                            matriceTemp.Clear();
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(riga))
                    {
                        try
                        {
                            int[] rigaNumeri = riga.Split(',').Select(int.Parse).ToArray();
                            matriceTemp.Add(rigaNumeri);
                        }
                        catch (FormatException ex)
                        {
                            MessageBox.Show($"Errore di formattazione nella riga: {riga}");
                        }
                    }
                }

                replay1 = matriceList;
                MessageBox.Show($"Caricate {replay1.Count} matrici dal file!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante il caricamento: {ex.Message}");
            }
        }

        /// <summary>
        /// Avvia la riproduzione automatica delle mosse salvate
        /// </summary>
        private async void button3_Click(object sender, EventArgs e)
        {
            foreach (int[,] c in replay1)
            {
                if (!bottonestart)
                {
                    matrice = c;
                    AggiornaTabellone2();
                    await Task.Delay(1000);
                }
            }
        }

        /// <summary>
        /// Aggiorna il tabellone durante il replay
        /// </summary>
        private void AggiornaTabellone2()
        {
            foreach (PictureBox box in ciao.Keys)
            {
                Tuple<int, int> coordinates = ciao[box];
                int x = coordinates.Item1;
                int y = coordinates.Item2;

                if (matrice[x, y] == 1)
                {
                    string percorso = "p_nera.png";
                    box.Image = Image.FromFile(percorso);
                }
                else if (matrice[x, y] == 2)
                {
                    string percorso = "p_bianca.png";
                    box.Image = Image.FromFile(percorso);
                }
                else
                {
                    box.BackColor = Color.Green;
                    box.Image = null;
                }
                box.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        bool utenteVSutente = false;

        /// <summary>
        /// Gestore evento click sul button4
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Gestore evento click sulla pictureBox66
        /// </summary>
        private void pictureBox66_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Avvia una partita tra due giocatori umani
        /// </summary>
        private void button_UTENTEVSUTENTE_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            tabellone.pulisci();
            Avvio(difficolta);
            panel1.Hide();
            bottonestart = true;
            utenteVSutente = true;
            label_giocatore.Visible = true;
        }

        /// <summary>
        /// Gestore evento cambio selezione nella listBox1
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Apre la pagina delle regole di Othello nel browser
        /// </summary>
        private void documentazioneBTN_Click(object sender, EventArgs e)
        {
            string url = "https://www.fngo.it/regole.asp";
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Errore durante l'apertura della pagina: {ex.Message}", "Errore");
            }
        }
    }
}