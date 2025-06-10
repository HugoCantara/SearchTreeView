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
    public partial class DataGridViewSearch : UserControl
    {
        private Panel panelFiltro;
        private DataGridView dgv;
        private TextBox[] filtros;

        private ComboBox cbPagina;
        private Button btnAnterior;
        private Button btnProximo;
        private Label lblPagina;

        private DataTable _dadosOriginais;
        private int _paginaAtual = 1;
        private int _regPorPagina = 10;
        private int _totalPaginas = 1;

        //private Panel panelFiltros;
        //private TextBox[] filtrosColunas;
        //private DataTable _data = new DataTable();
        //private int _currentPage = 1;
        //private int _pageSize = 10;
        //private int _totalPages = 1;


        public DataGridViewSearch()
        {
            InitializeComponent();
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Dock = DockStyle.Fill;

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = false,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;

            cbPagina = new ComboBox { Width = 60, DropDownStyle = ComboBoxStyle.DropDownList };
            cbPagina.Items.AddRange(new string[] { "5", "10", "20", "50" });
            cbPagina.SelectedIndex = 1;

            cbPagina.SelectedIndexChanged += (s, e) =>
            {
                _regPorPagina = int.Parse(cbPagina.SelectedItem.ToString());
                _paginaAtual = 1;
                AtualizarGrid();
            };

            btnAnterior = new Button { Text = "<" };
            btnProximo = new Button { Text = ">" };
            lblPagina = new Label { Width = 100 };

            btnAnterior.Click += (s, e) =>
            {
                if (_paginaAtual > 1)
                {
                    _paginaAtual--;
                    AtualizarGrid();
                }
            };

            btnProximo.Click += (s, e) =>
            {
                if (_paginaAtual < _totalPaginas)
                {
                    _paginaAtual++;
                    AtualizarGrid();
                }
            };

            var rodape = new FlowLayoutPanel { Height = 30, Dock = DockStyle.Bottom };
            rodape.Controls.Add(new Label { Text = "Registros por página:" });
            rodape.Controls.Add(cbPagina);
            rodape.Controls.Add(btnAnterior);
            rodape.Controls.Add(btnProximo);
            rodape.Controls.Add(lblPagina);

            this.Controls.Add(dgv);
            this.Controls.Add(rodape);
        }

        //private void InicializarComponentes()
        //{
        //    this.Dock = DockStyle.Fill;

        //    panelFiltro = new Panel() { Height = 25, Dock = DockStyle.Top };

        //    dgv = new DataGridView()
        //    {
        //        Dock = DockStyle.Fill,
        //        ReadOnly = true,
        //        AllowUserToAddRows = false,
        //        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        //    };

        //    dgv.ColumnWidthChanged += (s, e) => AtualizarLarguraFiltros();
        //    dgv.Scroll += (s, e) => AtualizarPosicaoFiltros();
        //    dgv.CellValueChanged += dgv_CellValueChanged;
        //    dgv.CurrentCellDirtyStateChanged += dgv_CurrentCellDirtyStateChanged;

        //    cbPagina = new ComboBox() { Width = 60, DropDownStyle = ComboBoxStyle.DropDownList };
        //    cbPagina.Items.AddRange(new string[] { "5", "10", "20", "50" });
        //    cbPagina.SelectedIndex = 1;

        //    cbPagina.SelectedIndexChanged += (s, e) =>
        //    {
        //        _regPorPagina = int.Parse(cbPagina.SelectedItem.ToString());
        //        _paginaAtual = 1;
        //        AtualizarGrid();
        //    };

        //    btnAnterior = new Button() { Text = "<" };
        //    btnProximo = new Button() { Text = ">" };
        //    lblPagina = new Label() { Width = 100 };

        //    btnAnterior.Click += (s, e) => { if (_paginaAtual > 1) { _paginaAtual--; AtualizarGrid(); } };
        //    btnProximo.Click += (s, e) => { if (_paginaAtual < _totalPaginas) { _paginaAtual++; AtualizarGrid(); } };

        //    var rodape = new FlowLayoutPanel() { Height = 30, Dock = DockStyle.Bottom };
        //    rodape.Controls.Add(new Label() { Text = "Registros por página:" });
        //    rodape.Controls.Add(cbPagina);
        //    rodape.Controls.Add(btnAnterior);
        //    rodape.Controls.Add(btnProximo);
        //    rodape.Controls.Add(lblPagina);

        //    this.Controls.Add(dgv);
        //    this.Controls.Add(panelFiltro);
        //    this.Controls.Add(rodape);
        //}

        public void CarregarDados(DataTable dt)
        {
            _dadosOriginais = dt.Copy();
            _paginaAtual = 1;
            AtualizarGrid();
        }

        //public void CarregarDados(DataTable dt)
        //{
        //    _dadosOriginais = dt.Copy();
        //    _paginaAtual = 1;

        //    // Criar filtros
        //    panelFiltro.Controls.Clear();
        //    filtros = new TextBox[dt.Columns.Count];

        //    for (int i = 0; i < dt.Columns.Count; i++)
        //    {
        //        var txt = new TextBox
        //        {
        //            Height = panelFiltro.Height - 2,
        //            BorderStyle = BorderStyle.FixedSingle,
        //            Tag = i
        //        };
        //        txt.TextChanged += (s, e) => { _paginaAtual = 1; AtualizarGrid(); };
        //        filtros[i] = txt;
        //        panelFiltro.Controls.Add(txt);
        //    }

        //    dgv.DataSource = null;
        //    AtualizarGrid();
        //}

        private void AtualizarGrid()
        {
            if (_dadosOriginais == null) return;

            var dados = _dadosOriginais.AsEnumerable();

            // Aplica filtros da linha 0 (linha de filtros)
            if (dgv.Rows.Count > 0 && dgv.Rows[0].Tag?.ToString() == "filtro")
            {
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    string texto = dgv.Rows[0].Cells[i].Value?.ToString()?.Trim().ToLower();
                    if (!string.IsNullOrEmpty(texto))
                    {
                        string coluna = _dadosOriginais.Columns[i].ColumnName;
                        dados = dados.Where(r =>
                        {
                            var valor = r[coluna];
                            if (valor == DBNull.Value) return false;
                            return valor.ToString().ToLower().Contains(texto);
                        });
                    }
                }
            }

            var lista = dados.ToList();
            _totalPaginas = (int)Math.Ceiling((double)lista.Count / _regPorPagina);
            if (_totalPaginas == 0) _totalPaginas = 1;
            if (_paginaAtual > _totalPaginas) _paginaAtual = _totalPaginas;

            var pageData = lista.Skip((_paginaAtual - 1) * _regPorPagina).Take(_regPorPagina).ToList();
            DataTable tabelaFinal;

            if (pageData.Any())
            {
                tabelaFinal = pageData.CopyToDataTable();
            }
            else
            {
                tabelaFinal = _dadosOriginais.Clone();
            }

            // Cria e insere linha de filtro
            DataRow linhaFiltro = tabelaFinal.NewRow();
            foreach (DataColumn col in tabelaFinal.Columns)
                linhaFiltro[col.ColumnName] = "";
            tabelaFinal.Rows.InsertAt(linhaFiltro, 0);

            dgv.DataSource = tabelaFinal;

            // Configura a linha de filtro
            dgv.Rows[0].ReadOnly = false;
            dgv.Rows[0].DefaultCellStyle.BackColor = Color.LightYellow;
            dgv.Rows[0].Tag = "filtro";

            for (int i = 1; i < dgv.Rows.Count; i++)
                dgv.Rows[i].ReadOnly = true;

            lblPagina.Text = $"Página {_paginaAtual} / {_totalPaginas}";
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0) // Linha de filtro
            {
                _paginaAtual = 1;
                AtualizarGrid();
            }
        }

        private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv.IsCurrentCellDirty)
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        //private void AtualizarGrid()
        //{
        //    if (_dadosOriginais == null) return;

        //    var dados = _dadosOriginais.AsEnumerable();

        //    // Aplica filtros baseados na linha de filtro (se já exibida)
        //    if (dgv.Rows.Count > 0 && dgv.Rows[0].Tag?.ToString() == "filtro")
        //    {
        //        for (int i = 0; i < dgv.Columns.Count; i++)
        //        {
        //            string texto = dgv.Rows[0].Cells[i].Value?.ToString()?.ToLower() ?? "";
        //            if (!string.IsNullOrEmpty(texto))
        //            {
        //                string coluna = _dadosOriginais.Columns[i].ColumnName;
        //                dados = dados.Where(r => r[coluna].ToString().ToLower().Contains(texto));
        //            }
        //        }
        //    }

        //    var lista = dados.ToList();
        //    _totalPaginas = (int)Math.Ceiling((double)lista.Count / _regPorPagina);
        //    if (_totalPaginas == 0) _totalPaginas = 1;
        //    if (_paginaAtual > _totalPaginas) _paginaAtual = _totalPaginas;

        //    var pageData = lista.Skip((_paginaAtual - 1) * _regPorPagina).Take(_regPorPagina).ToList();
        //    DataTable tabelaFinal = pageData.Any() ? pageData.CopyToDataTable() : _dadosOriginais.Clone();

        //    for (int i = 0; i < filtros.Length; i++)
        //    {
        //        string texto = dgv.Rows[0].Cells[i].Value?.ToString()?.Trim().ToLower();

        //        if (!string.IsNullOrEmpty(texto))
        //        {
        //            string coluna = _dadosOriginais.Columns[i].ColumnName;

        //            // Tenta filtrar comparando como string
        //            dados = dados.Where(r =>
        //            {
        //                var valor = r[coluna];
        //                if (valor == DBNull.Value) return false;

        //                return valor.ToString().ToLower().Contains(texto);
        //            });
        //        }
        //    }

        //    //// Insere linha de filtro no topo
        //    DataRow linhaFiltro = tabelaFinal.NewRow();
        //    //for (int i = 0; i < tabelaFinal.Columns.Count; i++)
        //    //    linhaFiltro[i] = "";

        //    tabelaFinal.Rows.InsertAt(linhaFiltro, 0);
        //    dgv.DataSource = tabelaFinal;

        //    dgv.Rows[0].ReadOnly = false;
        //    dgv.Rows[0].DefaultCellStyle.BackColor = Color.LightYellow;
        //    dgv.Rows[0].Tag = "filtro";

        //    for (int i = 1; i < dgv.Rows.Count; i++)
        //        dgv.Rows[i].ReadOnly = true;

        //    lblPagina.Text = $"Página {_paginaAtual} / {_totalPaginas}";
        //}

        //private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex == 0) // Linha de filtro
        //    {
        //        _paginaAtual = 1;
        //        AtualizarGrid();
        //    }
        //}

        //private void dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        //{
        //    if (dgv.IsCurrentCellDirty)
        //    {
        //        dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    }
        //}

        //private void AtualizarGrid()
        //{
        //    if (_dadosOriginais == null) return;

        //    var dados = _dadosOriginais.AsEnumerable();

        //    for (int i = 0; i < filtros.Length; i++)
        //    {
        //        var texto = filtros[i].Text.Trim().ToLower();
        //        if (!string.IsNullOrEmpty(texto))
        //        {
        //            var coluna = _dadosOriginais.Columns[i].ColumnName;
        //            dados = dados.Where(r => r[coluna].ToString().ToLower().Contains(texto));
        //        }
        //    }

        //    var listaFinal = dados.ToList();
        //    _totalPaginas = (int)Math.Ceiling((double)listaFinal.Count / _regPorPagina);
        //    if (_totalPaginas == 0) _totalPaginas = 1;
        //    if (_paginaAtual > _totalPaginas) _paginaAtual = _totalPaginas;

        //    var dadosPagina = listaFinal
        //        .Skip((_paginaAtual - 1) * _regPorPagina)
        //        .Take(_regPorPagina)
        //        .ToList();

        //    if (dadosPagina.Any())
        //        dgv.DataSource = dadosPagina.CopyToDataTable();
        //    else
        //        dgv.DataSource = _dadosOriginais.Clone(); // Grid vazia

        //    lblPagina.Text = $"Página {_paginaAtual} / {_totalPaginas}";
        //    AtualizarLarguraFiltros();
        //}

        private void AtualizarLarguraFiltros()
        {
            if (dgv.Columns.Count != filtros.Length) return;

            int x = -dgv.HorizontalScrollingOffset;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                var col = dgv.Columns[i];
                filtros[i].SetBounds(x, 0, col.Width, panelFiltro.Height);
                x += col.Width;
            }
        }

        private void AtualizarPosicaoFiltros()
        {
            AtualizarLarguraFiltros();
        }

        //private DataGridView dgv;
        //private ComboBox cbColuna;
        //private ComboBox cbPagina;
        //private TextBox txtPesquisa;
        //private Button btnAnterior;
        //private Button btnProximo;
        //private Label lblPagina;

        //private void InicializarComponentes()
        //{
        //    this.Dock = DockStyle.Fill;

        //    dgv = new DataGridView()
        //    {
        //        Dock = DockStyle.Fill,
        //        ReadOnly = true,
        //        SelectionMode = DataGridViewSelectionMode.FullRowSelect,
        //        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        //    };

        //    cbPagina = new ComboBox() { Width = 60, DropDownStyle = ComboBoxStyle.DropDownList };
        //    cbPagina.Items.AddRange(new string[] { "5", "10", "20", "50" });
        //    cbPagina.SelectedIndex = 1;

        //    btnAnterior = new Button() { Text = "< Anterior" };
        //    btnProximo = new Button() { Text = "Próximo >" };
        //    lblPagina = new Label() { Width = 100, TextAlign = ContentAlignment.MiddleLeft };

        //    cbPagina.SelectedIndexChanged += (s, e) => {
        //        _pageSize = int.Parse(cbPagina.SelectedItem.ToString());
        //        _currentPage = 1;
        //        AtualizarGrid();
        //    };

        //    btnAnterior.Click += (s, e) => { if (_currentPage > 1) { _currentPage--; AtualizarGrid(); } };
        //    btnProximo.Click += (s, e) => { if (_currentPage < _totalPages) { _currentPage++; AtualizarGrid(); } };

        //    var panelRodape = new FlowLayoutPanel() { Dock = DockStyle.Bottom, Height = 30 };
        //    panelRodape.Controls.Add(new Label() { Text = "Por página:" });
        //    panelRodape.Controls.Add(cbPagina);
        //    panelRodape.Controls.Add(btnAnterior);
        //    panelRodape.Controls.Add(btnProximo);
        //    panelRodape.Controls.Add(lblPagina);

        //    panelFiltros = new Panel() { Dock = DockStyle.Top, Height = 30, AutoScroll = true };

        //    this.Controls.Add(dgv);
        //    this.Controls.Add(panelFiltros);
        //    this.Controls.Add(panelRodape);

        //    //this.Dock = DockStyle.Fill;

        //    //dgv = new DataGridView() { Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };

        //    //cbColuna = new ComboBox() { Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
        //    //cbPagina = new ComboBox() { Width = 60, DropDownStyle = ComboBoxStyle.DropDownList };
        //    //cbPagina.Items.AddRange(new string[] { "5", "10", "20", "50" });
        //    //cbPagina.SelectedIndex = 1;

        //    //txtPesquisa = new TextBox() { Width = 150 };
        //    //btnAnterior = new Button() { Text = "< Anterior" };
        //    //btnProximo = new Button() { Text = "Próximo >" };
        //    //lblPagina = new Label() { Width = 100, TextAlign = ContentAlignment.MiddleLeft };

        //    //cbPagina.SelectedIndexChanged += (s, e) => {
        //    //    _pageSize = int.Parse(cbPagina.SelectedItem.ToString());
        //    //    _currentPage = 1;
        //    //    AtualizarGrid();
        //    //};

        //    //txtPesquisa.TextChanged += (s, e) => { _currentPage = 1; AtualizarGrid(); };
        //    //cbColuna.SelectedIndexChanged += (s, e) => { _currentPage = 1; AtualizarGrid(); };

        //    //btnAnterior.Click += (s, e) => { if (_currentPage > 1) { _currentPage--; AtualizarGrid(); } };
        //    //btnProximo.Click += (s, e) => { if (_currentPage < _totalPages) { _currentPage++; AtualizarGrid(); } };

        //    //var panelTopo = new FlowLayoutPanel() { Dock = DockStyle.Top, Height = 30 };
        //    //panelTopo.Controls.Add(new Label() { Text = "Coluna:" });
        //    //panelTopo.Controls.Add(cbColuna);
        //    //panelTopo.Controls.Add(new Label() { Text = "Pesquisar:" });
        //    //panelTopo.Controls.Add(txtPesquisa);
        //    //panelTopo.Controls.Add(new Label() { Text = "Por página:" });
        //    //panelTopo.Controls.Add(cbPagina);

        //    //var panelRodape = new FlowLayoutPanel() { Dock = DockStyle.Bottom, Height = 30 };
        //    //panelRodape.Controls.Add(btnAnterior);
        //    //panelRodape.Controls.Add(btnProximo);
        //    //panelRodape.Controls.Add(lblPagina);

        //    //this.Controls.Add(dgv);
        //    //this.Controls.Add(panelTopo);
        //    //this.Controls.Add(panelRodape);
        //}

        //public void CarregarDados(DataTable dados)
        //{
        //    _data = dados.Copy();
        //    cbColuna.Items.Clear();
        //    foreach (DataColumn col in _data.Columns)
        //        cbColuna.Items.Add(col.ColumnName);

        //    if (cbColuna.Items.Count > 0)
        //        cbColuna.SelectedIndex = 0;

        //    // Criar filtros por coluna
        //    panelFiltros.Controls.Clear();
        //    filtrosColunas = new TextBox[_data.Columns.Count];

        //    for (int i = 0; i < _data.Columns.Count; i++)
        //    {
        //        var txt = new TextBox
        //        {
        //            Width = dgv.Width / _data.Columns.Count - 2,
        //            Tag = i,
        //            Margin = new Padding(0)
        //        };

        //        txt.TextChanged += Filtro_TextChanged;
        //        filtrosColunas[i] = txt;
        //        panelFiltros.Controls.Add(txt);
        //    }

        //    AtualizarGrid();
        //}

        //private void AtualizarGrid()
        //{
        //    if (_data == null || _data.Rows.Count == 0) return;

        //    var coluna = cbColuna.SelectedItem?.ToString();
        //    var filtro = txtPesquisa.Text.ToLower();

        //    var linhasFiltradas = _data.AsEnumerable();

        //    if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrEmpty(coluna))
        //    {
        //        linhasFiltradas = linhasFiltradas.Where(row => row[coluna].ToString().ToLower().Contains(filtro));
        //    }

        //    var listaFinal = linhasFiltradas.ToList();
        //    int total = listaFinal.Count;
        //    _totalPages = (int)Math.Ceiling((double)total / _pageSize);
        //    if (_currentPage > _totalPages) _currentPage = _totalPages;

        //    var pagina = listaFinal.Skip((_currentPage - 1) * _pageSize).Take(_pageSize);

        //    if (pagina.Any())
        //        dgv.DataSource = pagina.CopyToDataTable();
        //    else
        //        dgv.DataSource = null;

        //    lblPagina.Text = $"Página {_currentPage} / {_totalPages}";
        //}
    }
}
