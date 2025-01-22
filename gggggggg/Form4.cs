using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace gggggggg
{
    public partial class Form4 : Form
    {
        private string connectionString;
        private string dbPath;
        private SQLiteConnection connection;

        public Form4()
        {
            InitializeComponent();
        }

        private void registrarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void listarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();

            form5.Show();
            this.Hide();
        }

        private void inserirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) //insere os dados e abre o form5
        {

            if (!ValidateInputs()) return;

            try
            {
                // Get the values from the form controls
                string nomeComponente = textBox1.Text;
                string descricao = textBox2.Text;
                DateTime dataCriacao = DateTime.Parse(textBox4.Text);
                string estado = GetSelectedEstado();

                // Insert the data into the database
                InsertComponente(nomeComponente, descricao, dataCriacao, estado);

                MessageBox.Show("Componente inserido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open Form5 and close this form
                Form5 form = new Form5();
                form.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inserir componente: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedEstado()
        {
            if (checkBox1.Checked) return "Ativo";
            if (checkBox2.Checked) return "Inativo";
            if (checkBox3.Checked) return "Reparo";
            return "Desconhecido";
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        private void InsertComponente(string nome, string descricao, DateTime dataCriacao, string estado)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO componentes (nomecomponente, descricao, data_criacao, estado) 
                       VALUES (@nome, @descricao, @dataCriacao, @estado)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@descricao", descricao);
                    cmd.Parameters.AddWithValue("@dataCriacao", dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@estado", estado);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dbPath = @"D:\Os Meus Documentos\Nova pasta (3)\gggggggg\gggggggg\basedados\lojapc.db.db";            // Full path to your database file
            connectionString = $"Data Source={dbPath};Version=3;";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)//nomecomponente
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//Descricao
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)//Data_criacao  
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//estado (ativo)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//estado (inativo)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)//estado (reparo)
        {

        }
    }
}
