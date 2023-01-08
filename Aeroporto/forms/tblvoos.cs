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
using Aeroporto.forms;

namespace Aeroporto
{
    public partial class tblvoos : Form
    {

        List<voos> lvoos;
        voos vous = new voos();
        List<voos> Voos = new List<voos>();
        List<aviao> laviao;
        aviao aviao = new aviao();

        public tblvoos()
        {
            InitializeComponent();
            vous.basedados();
            laviao = aviao.selectaviao();
            cbo_aviao.DataSource = laviao;
            cbo_aviao.DisplayMember = "nome";
            cbo_aviao.ValueMember = "idaviao";
            listavoos();
        }



        public void listavoos() ///fazer uma lista dos voos na datagrid
        {
            vous.criartbl();
            dataGridView1.Rows.Clear();

            lvoos = vous.selectvoo();


            foreach (voos voos in lvoos)
            {
                foreach(aviao aviao in laviao)
                {
                    if(voos.idaviao == aviao.idaviao)
                    {
                        dataGridView1.Rows.Add(voos.idvoo, voos.idaviao, voos.origem, voos.destino, voos.data, voos.hora, voos.quanteco, voos.quantclasse, voos.quantemp,aviao.nome);
                    }
                }

            }

        }

        private void btn_add_Click(object sender, EventArgs e) ///adicionar os valores ao voo
        {
            if (ValidaForm())
            {
                teste.Text = dateTimePicker2.Value.Hour.ToString() + ":";
                teste2.Text = dateTimePicker2.Value.Minute.ToString();


                voos novovoo;
                novovoo = new voos()
                {

                    idvoo = int.Parse(txt_codigovoo.Text),
                    idaviao = int.Parse(lbl_escondida.Text),
                    origem = cbo_partida.Text,
                    destino = cbo_chegada.Text,
                    data = dateTimePicker1.Text,
                    hora = teste.Text + teste2.Text,
                    quanteco = int.Parse(lbl_8.Text),
                    quantclasse = int.Parse(lbl_9.Text),
                    quantemp = int.Parse(lbl_0.Text)
                };

                Voos.Add(novovoo);
                vous.adicionarvoo(Voos);
                listavoos();
                Voos.Clear();
            }
        }

        private void tblvoos_Load(object sender, EventArgs e)
        {

        }

        private void btn_editar_Click(object sender, EventArgs e) ///editar os valores do voo
        {
            if (ValidaForm())
            {
                voos editarvoo;

                editarvoo = new voos()
                {
                    idvoo = int.Parse(txt_codigovoo.Text),
                    idaviao = int.Parse(lbl_escondida.Text),
                    origem = cbo_partida.Text,
                    destino = cbo_chegada.Text,
                    data = dateTimePicker1.Text,
                    hora = dateTimePicker2.Text,
                    quanteco = int.Parse(lbl_8.Text),
                    quantclasse = int.Parse(lbl_9.Text),
                    quantemp = int.Parse(lbl_0.Text)
                };

                editarvoo.editar(editarvoo);
                listavoos();
            }
        }

        private void btn_remover_Click(object sender, EventArgs e) ///remover o voo por o idvoo
        {
            voos deletevoo;
            deletevoo = new voos()
            {
                idvoo = int.Parse(txt_codigovoo.Text)
            };
            deletevoo.delete(deletevoo);
            listavoos();
        }

        private void btn_ver_Click(object sender, EventArgs e) ///ir para o menu ver voos
        {
            this.Hide();
            VerVoos form = new VerVoos();
            form.Show();
        }

        private void cbo_aviao_SelectedIndexChanged(object sender, EventArgs e)
        {
 
        }

        private void btn_yes_Click(object sender, EventArgs e) ///passagem dos lugares do aviao para o voo
        {
            

            //lbl_escondida.Text = "";
            lbl_escondida.Text = cbo_aviao.SelectedValue.ToString();
            string aviaoselecionado = lbl_escondida.Text;

            foreach (aviao aviao in laviao)
            {

                if (aviao.idaviao == int.Parse(aviaoselecionado))
                {
                    lbl_eco.Text = aviao.quantidadeeco.ToString();
                    lbl_classe.Text = aviao.quantidadeclasse.ToString();
                    lbl_emp.Text = aviao.quantidadeemp.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) ///adicionar os lugares e guardar o numero em label
        {
            if (cbo_tipo.Text == "Económico")
            {
                lbl_8.Text = numericUpDown1.Value.ToString();
            }
            if (cbo_tipo.Text == "Primeira Classe")
            {
                lbl_9.Text = numericUpDown1.Value.ToString();
            }
            if (cbo_tipo.Text == "Empresarial")
            {
                lbl_0.Text = numericUpDown1.Value.ToString();
            }
        }

        private void btn_voltar_Click(object sender, EventArgs e) ///voltar para o menui
        {
            menu form = new menu();
            form.Show();
            this.Hide();
        }

        private bool ValidaForm() ///fazer as comfirmações
        {
            bool output = true;
            if ((string.IsNullOrEmpty(txt_codigovoo.Text) || string.IsNullOrEmpty(cbo_aviao.Text) || string.IsNullOrEmpty(cbo_partida.Text) || string.IsNullOrEmpty(cbo_chegada.Text) || string.IsNullOrEmpty(cbo_tipo.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(dateTimePicker2.Text)))
            {
                MessageBox.Show("Preencha todos os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                output = false;
            }

            return output;
        }
    }
}
