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

    public partial class Form3 : Form
    {
        private string baseDirectory;
        private string dbPath;
        private string connectionString;
        private SQLiteConnection conexao;


        public Form3()
        {
            InitializeComponent();

            baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.Combine(baseDirectory, "assets", "lojapc.db.db");
            connectionString = $@"Data Source = {dbPath};version=3;";

        }


        //tabela chamada "utilizadores"
       



        private void button1_Click(object sender, EventArgs e) //salva como um novo usuário na database e atribui um idutilizador 
        {

            string login = textBox2.Text;
            string password = textBox3.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            using (SQLiteConnection conexao = new SQLiteConnection(connectionString))
            {
                try
                {
                    conexao.Open();

                    string query = "INSERT INTO utilizadores (login, password) VALUES (@login, @password)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Usuário cadastrado com sucesso!");
                            textBox2.Clear();
                            textBox3.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Erro ao cadastrar usuário.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}");
                }
            }
        }
      

        private void textBox2_TextChanged(object sender, EventArgs e) //caixa do login,
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e) // caixa da senha,
        {

        }




        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenNewForm<Form2>();
        }

        private void OpenNewForm<T>() where T : Form, new()
        {
            T newForm = new T();
            newForm.Show();
            this.Hide();
        }
    }

}
