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
    }
}
