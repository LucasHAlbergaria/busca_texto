using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuscaTexto
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.Text = "";

        }


        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO
            // Complete com seu nome e código de matrícula
            MessageBox.Show(this,
               "Busca em Texto - 2025/1\n\nDesenvolvido por:\n79999999 - NOME DO ALUNO\nProf. Virgílio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.",
               "Sobre o trabalho...",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Caixa de diálogo de abrir arquivo com filtro para extensão txt e rtf
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Texto (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf";
                openFileDialog.Title = "Abrir Arquivo";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoArquivo = openFileDialog.FileName;

                    if (Path.GetExtension(caminhoArquivo).ToLower() == ".rtf")
                    {
                        texto.LoadFile(caminhoArquivo, RichTextBoxStreamType.RichText);
                    }
                    else
                    {
                        // Lê o conteúdo do arquivo como UTF-8 e carrega no RichTextBox
                        string conteudo = File.ReadAllText(caminhoArquivo, Encoding.UTF8);
                        texto.Text = conteudo;
                    }
                }
            }

            MessageBox.Show(texto.Text);

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

            string texto = toolStripTextBox1.Text.Trim();

            if (string.IsNullOrEmpty(texto))
            {
                MessageBox.Show("Por favor, digite algo antes de continuar.");
            }
            else
            {
                // Texto válido, continuar
                MessageBox.Show("Texto digitado: " + texto);
                // Aqui você pode salvar o valor ou prosseguir com a lógica
            }

        }

        private void forçaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.SelectAll();
            texto.SelectionBackColor = Color.White;
            texto.DeselectAll();
            string textoBase = texto.Text;
            string palavra = toolStripTextBox1.Text;
            List<int> resultados = BuscaForcaBruta.ForcaBrutaTodasOcorrencias(palavra, textoBase);

            Color[] cores = new Color[]
            {
    Color.Red,
    Color.Green,
    Color.Blue,
    Color.Orange,
    Color.Purple,
    Color.Teal,
    Color.Brown,
    Color.Magenta
            };

            if (resultados.Count > 0)
            {
                // Destaca todas as ocorrências com cores diferentes
                for (int i = 0; i < resultados.Count; i++)
                {
                    int pos = resultados[i];
                    Color cor = cores[i % cores.Length];
                    texto.Select(pos, palavra.Length);
                    texto.SelectionBackColor = cor;
                }

                // Pergunta se o usuário deseja substituir
                DialogResult confirmar = MessageBox.Show("Foram encontradas " + resultados.Count + " ocorrências.\nDeseja substituir todas elas?", "Substituir", MessageBoxButtons.YesNo);

                if (confirmar == DialogResult.Yes)
                {
                    // Criar formulário inline para digitar nova palavra
                    Form prompt = new Form()
                    {
                        Width = 350,
                        Height = 150,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "Substituir Palavra",
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    Label textoLabel = new Label() { Left = 10, Top = 20, Text = "Nova palavra:", AutoSize = true };
                    TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 300 };
                    Button okButton = new Button() { Text = "OK", Left = 230, Width = 80, Top = 80, DialogResult = DialogResult.OK };

                    okButton.Click += (s, args) => { prompt.Close(); };

                    prompt.Controls.Add(textoLabel);
                    prompt.Controls.Add(inputBox);
                    prompt.Controls.Add(okButton);
                    prompt.AcceptButton = okButton;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        string novaPalavra = inputBox.Text;

                        if (!string.IsNullOrEmpty(novaPalavra))
                        {
                            // Substituir todas as ocorrências manualmente
                            texto.Text = texto.Text.Replace(palavra, novaPalavra);
                            MessageBox.Show("Substituição concluída!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Padrão não encontrado.");
            }

        }

        private void buscaKMPToolStripMenuItem_Click(object sender, EventArgs e)
        {

            texto.SelectAll();
            texto.SelectionBackColor = Color.White;
            texto.DeselectAll();
            string textoBase = texto.Text;
            string palavra = toolStripTextBox1.Text;
            List<int> resultados = BuscaKMP.KMPTodasOcorrencias(palavra, textoBase);

            Color[] cores = new Color[]
            {
    Color.Red,
    Color.Green,
    Color.Blue,
    Color.Orange,
    Color.Purple,
    Color.Teal,
    Color.Brown,
    Color.Magenta
            };

            if (resultados.Count > 0)
            {
                // Destaca todas as ocorrências com cores diferentes
                for (int i = 0; i < resultados.Count; i++)
                {
                    int pos = resultados[i];
                    Color cor = cores[i % cores.Length];
                    texto.Select(pos, palavra.Length);
                    texto.SelectionBackColor = cor;
                }

                // Pergunta se o usuário deseja substituir
                DialogResult confirmar = MessageBox.Show("Foram encontradas " + resultados.Count + " ocorrências.\nDeseja substituir todas elas?", "Substituir", MessageBoxButtons.YesNo);

                if (confirmar == DialogResult.Yes)
                {
                    // Criar formulário inline para digitar nova palavra
                    Form prompt = new Form()
                    {
                        Width = 350,
                        Height = 150,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "Substituir Palavra",
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    Label textoLabel = new Label() { Left = 10, Top = 20, Text = "Nova palavra:", AutoSize = true };
                    TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 300 };
                    Button okButton = new Button() { Text = "OK", Left = 230, Width = 80, Top = 80, DialogResult = DialogResult.OK };

                    okButton.Click += (s, args) => { prompt.Close(); };

                    prompt.Controls.Add(textoLabel);
                    prompt.Controls.Add(inputBox);
                    prompt.Controls.Add(okButton);
                    prompt.AcceptButton = okButton;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        string novaPalavra = inputBox.Text;

                        if (!string.IsNullOrEmpty(novaPalavra))
                        {
                            // Substituir todas as ocorrências manualmente
                            texto.Text = texto.Text.Replace(palavra, novaPalavra);
                            MessageBox.Show("Substituição concluída!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Padrão não encontrado.");
            }
        }

        private void buscaBoyerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texto.SelectAll();
            texto.SelectionBackColor = Color.White;
            texto.DeselectAll();
            string textoBase = texto.Text;
            string palavra = toolStripTextBox1.Text;
            List<int> resultados = BuscaBoyerMoore.BoyerTodasOcorrencias(palavra, textoBase);

            Color[] cores = new Color[]
            {
    Color.Red,
    Color.Green,
    Color.Blue,
    Color.Orange,
    Color.Purple,
    Color.Teal,
    Color.Brown,
    Color.Magenta
            };

            if (resultados.Count > 0)
            {
                // Destaca todas as ocorrências com cores diferentes
                for (int i = 0; i < resultados.Count; i++)
                {
                    int pos = resultados[i];
                    Color cor = cores[i % cores.Length];
                    texto.Select(pos, palavra.Length);
                    texto.SelectionBackColor = cor;
                }

                // Pergunta se o usuário deseja substituir
                DialogResult confirmar = MessageBox.Show("Foram encontradas " + resultados.Count + " ocorrências.\nDeseja substituir todas elas?", "Substituir", MessageBoxButtons.YesNo);

                if (confirmar == DialogResult.Yes)
                {
                    // Criar formulário inline para digitar nova palavra
                    Form prompt = new Form()
                    {
                        Width = 350,
                        Height = 150,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "Substituir Palavra",
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    Label textoLabel = new Label() { Left = 10, Top = 20, Text = "Nova palavra:", AutoSize = true };
                    TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 300 };
                    Button okButton = new Button() { Text = "OK", Left = 230, Width = 80, Top = 80, DialogResult = DialogResult.OK };

                    okButton.Click += (s, args) => { prompt.Close(); };

                    prompt.Controls.Add(textoLabel);
                    prompt.Controls.Add(inputBox);
                    prompt.Controls.Add(okButton);
                    prompt.AcceptButton = okButton;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        string novaPalavra = inputBox.Text;

                        if (!string.IsNullOrEmpty(novaPalavra))
                        {
                            // Substituir todas as ocorrências manualmente
                            texto.Text = texto.Text.Replace(palavra, novaPalavra);
                            MessageBox.Show("Substituição concluída!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Padrão não encontrado.");
            }
        }

        private void buscaRabinKarpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            texto.SelectAll();
            texto.SelectionBackColor = Color.White;
            texto.DeselectAll();
            string textoBase = texto.Text;
            string palavra = toolStripTextBox1.Text;
            List<int> resultados = BuscaRabinKarp.TodasOcorrenciasRK(palavra, textoBase);

            Color[] cores = new Color[]
            {
    Color.Red,
    Color.Green,
    Color.Blue,
    Color.Orange,
    Color.Purple,
    Color.Teal,
    Color.Brown,
    Color.Magenta
            };

            if (resultados.Count > 0)
            {
                // Destaca todas as ocorrências com cores diferentes
                for (int i = 0; i < resultados.Count; i++)
                {
                    int pos = resultados[i];
                    Color cor = cores[i % cores.Length];
                    texto.Select(pos, palavra.Length);
                    texto.SelectionBackColor = cor;
                }

                // Pergunta se o usuário deseja substituir
                DialogResult confirmar = MessageBox.Show("Foram encontradas " + resultados.Count + " ocorrências.\nDeseja substituir todas elas?", "Substituir", MessageBoxButtons.YesNo);

                if (confirmar == DialogResult.Yes)
                {
                    // Criar formulário inline para digitar nova palavra
                    Form prompt = new Form()
                    {
                        Width = 350,
                        Height = 150,
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        Text = "Substituir Palavra",
                        StartPosition = FormStartPosition.CenterScreen
                    };

                    Label textoLabel = new Label() { Left = 10, Top = 20, Text = "Nova palavra:", AutoSize = true };
                    TextBox inputBox = new TextBox() { Left = 10, Top = 50, Width = 300 };
                    Button okButton = new Button() { Text = "OK", Left = 230, Width = 80, Top = 80, DialogResult = DialogResult.OK };

                    okButton.Click += (s, args) => { prompt.Close(); };

                    prompt.Controls.Add(textoLabel);
                    prompt.Controls.Add(inputBox);
                    prompt.Controls.Add(okButton);
                    prompt.AcceptButton = okButton;

                    if (prompt.ShowDialog() == DialogResult.OK)
                    {
                        string novaPalavra = inputBox.Text;

                        if (!string.IsNullOrEmpty(novaPalavra))
                        {
                            // Substituir todas as ocorrências manualmente
                            texto.Text = texto.Text.Replace(palavra, novaPalavra);
                            MessageBox.Show("Substituição concluída!");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Padrão não encontrado.");
            }
        }
    }
}
