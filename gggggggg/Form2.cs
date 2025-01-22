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
using System.IO;

namespace gggggggg
{
    public partial class Form2 : Form
    {
        private string baseDirectory;
        private string dbPath;
        private string connectionString;
        private SQLiteConnection conexao;

        public Form2()
        {
            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.Combine(baseDirectory, "assets", "lojapc.db.db");
            connectionString = $@"Data Source = {dbPath};version=3;";
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void inserirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();

            form5.Show();
            this.Hide();
        }

        private void registrarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }
        //tabela chamada utilizadores
        private void button1_Click(object sender, EventArgs e) //verifica se o login e a senha estão corretos, se estiverem, abrir o form 5
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM utilizadores WHERE login = @login AND password = @password";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        if (count > 0)
                        {
                            Form5 form5 = new Form5();
                            form5.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Login ou senha incorretos.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar o login: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 Form3 = new Form3();
            Form3.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //caixa do login
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)//caixa da password
        {

        }
    }
}
