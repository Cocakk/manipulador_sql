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
using static LinqToDB.Common.Configuration;
using System.IO;

namespace gggggggg
{
    public partial class Form1 : Form
    {
        private string baseDirection;
        private string dbPath;
        private string connectionString;
        private SQLiteConnection connection;

    public Form1()
    {
        InitializeComponent();

        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string dbPath = Path.Combine(baseDirectory, "assets", "Componentes.db");
        connectionString = $"Data Source={dbPath};Version=3;";
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                    
                // Aqui você pode realizar operações iniciais com o banco de dados, se necessário
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenNewForm<T>() where T : Form, new()
    {
        T newForm = new T();
        newForm.Show();
        this.Hide();
    }

    private void inserirToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenNewForm<Form4>();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        OpenNewForm<Form2>();
    }

    private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenNewForm<Form3>();
    }

    private void listarToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenNewForm<Form5>();
    }

    private void loginToolStripMenuItem_Click(object sender, EventArgs e)
    {
        OpenNewForm<Form2>();
    }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
