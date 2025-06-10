using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SearchTreeView
{
    public partial class SearchTreeviewControl : UserControl
    {
        private TreeView treeView;
        private TextBox searchBox;
        private PictureBox pictureBoxLogo;

        private List<TreeNode> originalNodes = new List<TreeNode>();
        private bool exibirLogo = false;

        [Category("Configurações"), Description("Indica se o logo será exibido no topo.")]
        public bool ExibirLogo
        {
            get => exibirLogo;
            set
            {
                exibirLogo = value;
                AtualizarLogoVisibilidade();
            }
        }

        [Category("Custom Properties"), Description("The logo displayed above the TreeView.")]
        public Image Logo
        {
            get => pictureBoxLogo.Image;
            set => pictureBoxLogo.Image = value;
        }

        public event Action<string> FormSelected;

        public SearchTreeviewControl()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            pictureBoxLogo = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Height = 100,
                Dock = DockStyle.Top,
                Visible = false
            };

            treeView = new TreeView { Dock = DockStyle.Fill };
            searchBox = new TextBox { Dock = DockStyle.Top };

            searchBox.TextChanged += SearchBox_TextChanged;
            treeView.AfterSelect += TreeView_AfterSelect;

            Controls.Add(treeView);
            Controls.Add(searchBox);
            Controls.Add(pictureBoxLogo);
        }

        private void AtualizarLogoVisibilidade()
        {
            pictureBoxLogo.Visible = exibirLogo;
            //btnEscolherLogo.Visible = exibirLogo;
        }

        public void LoadTree(List<(string Parent, List<(string Child, string FormName)> Children)> structure)
        {
            treeView.Nodes.Clear();
            originalNodes.Clear();

            foreach (var parent in structure)
            {
                TreeNode parentNode = new TreeNode(parent.Parent);
                foreach (var child in parent.Children)
                {
                    TreeNode childNode = new TreeNode(child.Child)
                    {
                        Tag = child.FormName
                    };
                    parentNode.Nodes.Add(childNode);
                }
                treeView.Nodes.Add(parentNode);
            }

            foreach (TreeNode node in treeView.Nodes)
            {
                originalNodes.Add((TreeNode)node.Clone());
            }

            //treeView.ExpandAll();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string searchText = searchBox.Text.Trim().ToLower();
            treeView.BeginUpdate();
            treeView.Nodes.Clear();

            if (string.IsNullOrEmpty(searchText))
            {
                // Se a caixa estiver vazia, restaura os nós originais e recolhe tudo
                foreach (var original in originalNodes)
                {
                    treeView.Nodes.Add((TreeNode)original.Clone());
                }
                CollapseAllNodes(); // recolhe todos os nós
            }
            else
            {
                // Caso haja texto, filtra e expande os nós que correspondem
                foreach (var original in originalNodes)
                {
                    if (original.Text.ToLower().Contains(searchText)) 
                    {
                        TreeNode newNode = (TreeNode)original.Clone();
                        newNode.Nodes.Clear();

                        foreach (TreeNode child in original.Nodes)
                        {
                            newNode.Nodes.Add((TreeNode)child.Clone());
                        }
                        treeView.Nodes.Add(newNode);
                    }
                    else 
                    {
                        TreeNode match = FilterNode((TreeNode)original.Clone(), searchText);
                        if (match != null)
                        {
                            treeView.Nodes.Add(match);
                        }
                    }
                }
                // Agora, expande os nós que têm correspondência
                ExpandMatchingNodes(treeView.Nodes, searchText);
            }

            //foreach (TreeNode original in originalNodes)
            //{
            //    TreeNode match = FilterNode((TreeNode)original.Clone(), searchText);
            //    if (match != null)
            //    {
            //        treeView.Nodes.Add(match);
            //    }
            //}

            //treeView.ExpandAll();
            treeView.EndUpdate();
        }

        private void ExpandMatchingNodes(TreeNodeCollection nodes, string searchText)
        {
            foreach (TreeNode node in nodes)
            {
                // Se o texto do nó contém a pesquisa, expanda
                if (node.Text.ToLower().Contains(searchText))
                {
                    bool flag = false;
                    if (node.Nodes.Count > 0) 
                    {
                        foreach (TreeNode child in node.Nodes)
                        {
                            if (child.Text.ToLower().Contains(searchText))
                            {
                                if (!node.IsExpanded)
                                {
                                    flag = true;
                                }
                            }
                            
                        }
                    }

                    if(flag) node.Expand();
                    else node.Collapse();
                }
                else 
                {
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeNode child in node.Nodes)
                        {
                            if (child.Text.ToLower().Contains(searchText))
                            {
                                if (!node.IsExpanded)
                                {
                                    node.Expand();
                                }
                            }
                        }
                    }
                }   
            }
        }

        private TreeNode FilterNode(TreeNode node, string searchText)
        {
            TreeNode newNode = (TreeNode)node.Clone();
            newNode.Nodes.Clear();

            bool hasMatch = node.Text.ToLower().Contains(searchText);

            foreach (TreeNode child in node.Nodes)
            {
                if (child.Text.ToLower().Contains(searchText))
                {
                    newNode.Nodes.Add((TreeNode)child.Clone());
                    hasMatch = true;
                }
            }

            return hasMatch ? newNode : null;
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is string formName)
            {
                FormSelected?.Invoke(formName);
            }
        }

        private void CollapseAllNodes()
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                node.Collapse();
                CollapseChildNodes(node);
            }
        }

        private void CollapseChildNodes(TreeNode node)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Collapse();
                CollapseChildNodes(child);
            }
        }
    }
}
