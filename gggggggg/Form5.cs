using System;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace gggggggg
{
    public partial class Form5 : Form
    {
        private string connectionString;
        private string dbPath;
        private SQLiteConnection connection;

        public Form5()
        {
            InitializeComponent();
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
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

        private void inserirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LoadComponents();
        }
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedComponentName = listBox1.SelectedItem.ToString();
                DisplayComponentDetails(selectedComponentName);
            }
        }

        private void alterarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void DisplayComponentDetails(string componentName)
        {
            string query = "SELECT * FROM componentes WHERE nomecomponente = @name";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", componentName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string descricao = reader["descricao"].ToString();
                            string dataCriacao = reader["data_criacao"].ToString();
                            string estado = reader["estado"].ToString();

                            // Assumindo que você tem TextBoxes para exibir essas informações
                            textBoxDescricao.Text = descricao;
                            textBoxDataCriacao.Text = dataCriacao;
                            textBoxEstado.Text = estado;
                        }
                    }
                }
            }
        }




        private void LoadComponents()
        {
            string query = "SELECT * FROM componentes";

            // Set dbPath to the correct location of your database file
            dbPath = @"D:\Os Meus Documentos\Nova pasta (3)\gggggggg\gggggggg\basedados\lojapc.db.db";            // Full path to your database file
            connectionString = $"Data Source={dbPath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader()) // This should no longer throw an error
                    {
                        while (reader.Read())
                        {
                            string componenteName = reader["nomecomponente"].ToString();
                            listBox1.Items.Add(componenteName);
                        }
                    }
                }
            }
        }

        private void removebutton_Click(object sender, EventArgs e)//botão responsável por remover um item da tabela
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedComponentName = listBox1.SelectedItem.ToString();

                DialogResult result = MessageBox.Show($"Tem certeza que deseja remover o componente '{selectedComponentName}'?",
                                                      "Confirmar Remoção",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    string query = "DELETE FROM componentes WHERE nomecomponente = @name";

                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@name", selectedComponentName);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Componente removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                listBox1.Items.Remove(selectedComponentName);
                                ClearTextBoxes();
                            }
                            else
                            {
                                MessageBox.Show("Falha ao remover o componente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um componente para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearTextBoxes()
        {
            textBoxDescricao.Clear();
            textBoxDataCriacao.Clear();
            textBoxEstado.Clear();
        }

        private void changebutton_Click(object sender, EventArgs e)//botão feito para alterar informações de um item da tabela
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedComponentName = listBox1.SelectedItem.ToString();

                string query = "UPDATE componentes SET descricao = @descricao, estado = @estado WHERE nomecomponente = @name";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@descricao", textBoxDescricao.Text);
                        command.Parameters.AddWithValue("@estado", textBoxEstado.Text);
                        command.Parameters.AddWithValue("@name", selectedComponentName);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Componente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Atualiza a exibição do componente
                            DisplayComponentDetails(selectedComponentName);
                        }
                        else
                        {
                            MessageBox.Show("Falha ao atualizar o componente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um componente para alterar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void inserirToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }
    }
}
