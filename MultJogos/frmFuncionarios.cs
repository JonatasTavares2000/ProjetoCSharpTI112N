using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MosaicoSolutions.ViaCep;

namespace MultJogos
{
    public partial class frmFuncionarios : Form
    {
        //Criando variáveis para controle do menu
        const int MF_BYCOMMAND = 0X400;
        [DllImport("user32")]
        static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);
        [DllImport("user32")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32")]
        static extern int GetMenuItemCount(IntPtr hWnd);

        public frmFuncionarios()
        {
            InitializeComponent();
            desabilitarCampos();
        }

        private void frmFuncionarios_Load(object sender, EventArgs e)
        {
            IntPtr hMenu = GetSystemMenu(this.Handle, false);
            int MenuCount = GetMenuItemCount(hMenu) - 1;
            RemoveMenu(hMenu, MenuCount, MF_BYCOMMAND);
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            frmMenuPrincipal abrir = new frmMenuPrincipal();
            abrir.Show();
            this.Hide();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            frmPesquisar abrir = new frmPesquisar();
            abrir.Show();
        }
        //Método para desabilitar os campos e botoes!
        public void desabilitarCampos()
        {
            txtCodigo.Enabled = false;
            txtEndereco.Enabled = false;
            txtEmail.Enabled = false;
            txtNum.Enabled = false;
            txtNome.Enabled = false;
            txtCidade.Enabled = false;
            txtBairro.Enabled = false;
            mskCEP.Enabled = false;
            mskCPF.Enabled = false;
            mskTelefone.Enabled = false;
            cbbEstado.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnLimpar.Enabled = false;
            btnCadastrar.Enabled = false;
        }

        //Método para desabilitar os campos e botoes!
        public void habilitarCampos()
        {
            txtCodigo.Enabled = true;
            txtEndereco.Enabled = true;
            txtEmail.Enabled = true;
            txtNum.Enabled = true;
            txtNome.Enabled = true;
            txtCidade.Enabled = true;
            txtBairro.Enabled = true;
            mskCEP.Enabled = true;
            mskCPF.Enabled = true;
            mskTelefone.Enabled = true;
            cbbEstado.Enabled = true;
            btnLimpar.Enabled = true;
            btnCadastrar.Enabled = true;

            txtNome.Focus();
        }

        //Método para limpar campos
        public void limparCampos()
        {
            txtCodigo.Clear();
            txtEndereco.Clear();
            txtNum.Clear();
            txtEmail.Clear();
            txtNome.Clear();
            txtCidade.Clear();
            txtBairro.Clear();
            mskCEP.Clear();
            mskCPF.Clear();
            mskTelefone.Clear();
            cbbEstado.Items.Clear();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            habilitarCampos();
            btnNovo.Enabled = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Equals("") && txtEmail.Text.Equals("") && txtNum.Text.Equals("")
                && txtEndereco.Text.Equals("")
                && txtBairro.Text.Equals("") && txtCidade.Text.Equals("") && mskCEP.Text.Equals("     ") && mskCPF.Text.Equals("   .   .   -")
                && mskTelefone.Text.Equals("     -"))
            {
                MessageBox.Show("Não deixar campos vazios.");
            }
            else
            {
                MessageBox.Show("Cadastrado com sucesso!");
                desabilitarCampos();
                limparCampos();
            }
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            habilitarCampos();
            btnNovo.Enabled = false;
        }

        public void buscaCEP(string cep)
        {
            var viaCEPService = ViaCepService.Default();

            try
            {
            var endereco = viaCEPService.ObterEndereco(cep);

            txtEndereco.Text = endereco.Logradouro;
            txtBairro.Text = endereco.Bairro;
            txtCidade.Text = endereco.Localidade;
            cbbEstado.Text = endereco.UF;
            }
            catch (Exception)
            {
                MessageBox.Show("CEP não encontrado.");
                
            }
        }

        private void mskCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscaCEP(mskCEP.Text);
                mskCEP.Focus();
                txtNum.Focus();
            }
        }
    }
}
