using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchTreeView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //SearchTreeviewControl treeControl = new SearchTreeviewControl
            //{
            //    Dock = DockStyle.Fill,
            //    ExibirLogo = true
            //};

            // Inscreva-se no evento do UserControl
            //searchTreeviewControlTest.FormSelected += AtualizarLabel;

            searchTreeviewControlTest.FormSelected += formName =>
            {
                //MessageBox.Show($"Abrir formulário: {formName}", "Selecionado");
                label1.Text = formName.ToString();
                // Aqui você pode usar reflexão ou outro mecanismo para abrir o form
            };

            //this.Controls.Add(treeControl);

            searchTreeviewControlTest.LoadTree(new List<(string, List<(string, string)>)>
        {
            ("Animais", new List<(string, string)>
            {
                ("Cavalo", "FormCavalo"),
                ("Gato", "FormGato")
            }),
            ("Equipamentos", new List<(string, string)>
            {
                ("Escavadora", "FormEscavadora"),
                ("Retroescavadora", "FormRetro")
            })
        });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Nome", typeof(string));
            dt.Columns.Add("Email", typeof(string));

            for (int i = 1; i <= 123; i++)
            {
                dt.Rows.Add(i.ToString(), $"Nome {i}", $"email{i}@teste.com");
            }

            //var dados = new DataTable();
            //dados.Columns.Add("ID", typeof(int));
            //dados.Columns.Add("Nome", typeof(string));
            //dados.Columns.Add("Email", typeof(string));

            //for (int i = 1; i <= 123; i++)
            //    dados.Rows.Add(i, $"Nome {i}", $"email{i}@dominio.com");

            dataGridViewSearch1.CarregarDados(dt);
        }
    }
}
