using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace _19130930_practicaHilos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BuscadorHolas() {
            MatchCollection buscadorHolas = Regex.Matches(richTextBox1.Text.ToLower(), "hola");
            LabelContador.Text = buscadorHolas.Count.ToString();
        }

        private void CorregirOperativo() {
            if (Regex.IsMatch(richTextBox1.Text, "oprativo"))
            {
                richTextBox1.Text = richTextBox1.Text.Replace("oprativo", "operativo");
                richTextBox1.Select(richTextBox1.Text.Length, 0);
            }
        }

        private void PintarPalabra()
        {
            if (this.richTextBox1.Text.Contains("color"))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf("color", (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + 0), "color".Length);
                    this.richTextBox1.SelectionColor = Color.Yellow;
                    this.richTextBox1.Select(selectStart, 0);
                    this.richTextBox1.SelectionColor = Color.Black;
                }
            }
        }

        private void SubrayarPalabra() {       
            if (this.richTextBox1.Text.Contains("prueba"))
            {
                int index = -1;
                int selectStart = this.richTextBox1.SelectionStart;

                while ((index = this.richTextBox1.Text.IndexOf("prueba", (index + 1))) != -1)
                {
                    this.richTextBox1.Select((index + 0), "prueba".Length);
                    this.richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline);
                    this.richTextBox1.Select(selectStart, 0);
                    richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Regular);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread contadorHolas = new Thread(BuscadorHolas);
            contadorHolas.Start();
            Thread reemplazo = new Thread(CorregirOperativo);
            reemplazo.Start();
            Thread pintar = new Thread(new ThreadStart(PintarPalabra));
            pintar.Start();
            Thread subrayar = new Thread(SubrayarPalabra);
            subrayar.Start();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            BuscadorHolas();
            CorregirOperativo();
            PintarPalabra();
            SubrayarPalabra();
        }
    }
}
