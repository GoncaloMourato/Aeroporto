using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aeroporto.base_de_dados;

namespace Aeroporto.forms
{
    public partial class avioes : Form
    {

        List<aviao> laviao;
        aviao aviaos = new aviao();
        List<aviao> Aviaos = new List<aviao>();

        public avioes()
        {
            InitializeComponent();
            aviaos.basedados();
            listaaviaoes();
        }

        public void listaaviaoes() ///listar os avioes na datagrid
        {
            aviaos.criartbl();
            dataGridView1.Rows.Clear();

            laviao = aviaos.selectaviao();

            foreach(aviao aviao in laviao)
            {
                dataGridView1.Rows.Add(aviao.idaviao, aviao.nome, aviao.modelo,aviao.quantidadeeco, aviao.quantidadeclasse, aviao.quantidadeemp);
            }

        }

        private void button1_Click(object sender, EventArgs e) ///adicionar os campos  ao aviao
        {
            if(ValidaForm())
            { 
            aviao novoaviao;
            novoaviao = new aviao()
            {
                idaviao = int.Parse(txt_id.Text),
                nome = txt_nome.Text,
                modelo = txt_modelo.Text,
                quantidadeeco = int.Parse(label7.Text),
                quantidadeclasse = int.Parse(label8.Text),
                quantidadeemp = int.Parse(label9.Text)

            };

            Aviaos.Add(novoaviao);
            aviaos.adiconaraviao(Aviaos);
            listaaviaoes();
            Aviaos.Clear();
            }
        }

        private void btn_editar_Click(object sender, EventArgs e) ///adicionar os campos do aviao
        {

            aviao editaraviao;

            editaraviao = new aviao()
            {
                idaviao = int.Parse(txt_id.Text),
                nome = txt_nome.Text,
                modelo = txt_modelo.Text,
                quantidadeeco = int.Parse(label7.Text),
                quantidadeclasse = int.Parse(label8.Text),
                quantidadeemp = int.Parse(label9.Text)
            };

            editaraviao.editar(editaraviao);
            listaaviaoes();
        }

        private void btn_remove_Click(object sender, EventArgs e) ///remover o aviao
        {
            aviao deleteaviao;
            deleteaviao = new aviao()
            {
                idaviao = int.Parse(txt_id.Text)
            };
            deleteaviao.delete(deleteaviao);
            listaaviaoes();
        }

        private void avioes_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) ///mostrar os lugares
        {
            label6.Visible = true;
            numericUpDown1.Visible = true;
            button1.Visible = true;

        }

        private void button1_Click_1(object sender, EventArgs e) ///adicionar os lugares e guardar o numero em label
        {
            if (cbo_tipo.Text == "Económico")
            {
                label7.Text = numericUpDown1.Value.ToString();
            }
            if (cbo_tipo.Text == "Primeira Classe")
            {
                label8.Text = numericUpDown1.Value.ToString();
            }
            if (cbo_tipo.Text == "Empresarial")
            {
                label9.Text = numericUpDown1.Value.ToString();
            }
        }

        private void btn_voltar_Click(object sender, EventArgs e) ///voltar para o menu
        {
            this.Hide();
            menu form = new menu();
            form.Show();
        }

        private bool ValidaForm() ///fazer as comfirmações
        {
            bool output = true;
            if ((string.IsNullOrEmpty(txt_id.Text) || string.IsNullOrEmpty(txt_modelo.Text) || string.IsNullOrEmpty(txt_nome.Text)))
            {
                MessageBox.Show("Preencha todos os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                output = false;
            }

            return output;
        }
    }
}
