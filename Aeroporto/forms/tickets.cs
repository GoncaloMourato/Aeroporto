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
    public partial class tickets : Form
    {
        aviao aviaos = new aviao();
        List<aviao> laviao;
        List<bilhetes> lbilhete;
        bilhetes bilhetis = new bilhetes();
        List<bilhetes> Bilhetes = new List<bilhetes>();
        List<voos> voos = new List<voos>();
        List<voos> voo;
        voos voos1 = new voos();

        public tickets(int idvoo,int idaviao,string origem,string destino,string data,string hora) ///passar os parametros para dentro do form dos tickets
        {
            InitializeComponent();
            bilhetis.basedados();
            listabilhetes();
            txt_idvoo.Text = idvoo.ToString();
            txt_origem.Text = origem.ToString();
            txt_destino.Text = destino.ToString();
            dateTimePicker1.Text = data.ToString();
            txt_hora.Text = hora.ToString();
            voo = voos1.selectvoo();

            voos vooselecionado;
            vooselecionado = new voos()
            {
                idvoo = int.Parse(txt_idvoo.Text),
                origem = txt_origem.Text,
                destino = txt_destino.Text,
                data = dateTimePicker1.Text,
                hora = txt_hora.Text

            };

            voos.Add(vooselecionado);

            foreach (voos voos in voo )
            {
                if (voos.idvoo == idvoo)
                {
                    txt_idaviao.Text = voos.idaviao.ToString();
                    lbl_quanteco.Text = voos.quanteco.ToString();
                    lbl_quantclasse.Text = voos.quantclasse.ToString();
                    lbl_quantemp.Text = voos.quantemp.ToString();
                }
            }
        }

        public void listabilhetes() ///listar os voos e avioes para a datagridview depois da compra
        {
            laviao = aviaos.selectaviao();
            bilhetis.criartbl();
            dataGridView1.Rows.Clear();

            lbilhete = bilhetis.selectbilhete();


            foreach (bilhetes bilhetes in lbilhete)
            {

                foreach (voos vou in voos)
                {

                    foreach (aviao aviao in laviao)
                    {
                        if(bilhetes.idvoo == vou.idvoo && bilhetes.idaviao == aviao.idaviao)

                        dataGridView1.Rows.Add(bilhetes.idbilhete,aviao.idaviao, vou.idvoo,bilhetes.titular,aviao.nome + " " + aviao.modelo, vou.origem, vou.destino, vou.data, vou.hora,
                                               bilhetes.bilheteeco, bilhetes.bilheteprimeriaclasse, bilhetes.bilheteempresarial,bilhetes.precototal);
                    }
                }

            }

        }

        private void tickets_Load(object sender, EventArgs e)
        {

        }

        private void btn_add_Click(object sender, EventArgs e) ///adicionar valores ao bilhete
        {
            if(ValidaForm())
            {
            int precototal = (int.Parse(num_eco.Text) * int.Parse(lbl_precoeco.Text)) + (int.Parse(num_classe.Text) * int.Parse(lbl_precoclasse.Text)) + (int.Parse(num_emp.Text) * int.Parse(lbl_precoempresarial.Text));

            bilhetes novobilhete;
            novobilhete = new bilhetes()
            {
                idbilhete = int.Parse(txt_idticket.Text),
                idvoo = int.Parse(txt_idvoo.Text),
                idaviao = int.Parse(txt_idaviao.Text),
                titular = txt_titular.Text,
                bilheteeco = int.Parse(num_eco.Text),
                bilheteprimeriaclasse = int.Parse(num_classe.Text),
                bilheteempresarial = int.Parse(num_emp.Text),
                precototal = precototal
            };

            Bilhetes.Add(novobilhete);
            bilhetis.adicionarbilhete(Bilhetes);
            listabilhetes();
            Bilhetes.Clear();

            int calceco = int.Parse(lbl_quanteco.Text) - novobilhete.bilheteeco;
            int calcclasse = int.Parse(lbl_quantclasse.Text) - novobilhete.bilheteprimeriaclasse;
            int calcemp = int.Parse(lbl_quantemp.Text) - novobilhete.bilheteempresarial;

            voos editarvoo;

            editarvoo = new voos()
            {
                idvoo = int.Parse(txt_idvoo.Text),
                idaviao = int.Parse(txt_idaviao.Text),
                origem = txt_origem.Text,
                destino = txt_destino.Text,
                data = dateTimePicker1.Text,
                hora = txt_hora.Text,
                quanteco = calceco,
                quantclasse = calcclasse,
                quantemp = calcemp
            };

            editarvoo.editar(editarvoo);
            }
        }

        private void btn_editar_Click(object sender, EventArgs e) ///editar a compra do ticket
        {
            if (ValidaForm())
            {
                int precototal = int.Parse(num_eco.Text) + int.Parse(num_classe.Text) + int.Parse(num_emp.Text);

            bilhetes editarbilhete;

            editarbilhete = new bilhetes()
            {
                idbilhete = int.Parse(txt_idticket.Text),
                titular = txt_titular.Text,
                idvoo = int.Parse(txt_idvoo.Text),
                idaviao = int.Parse(txt_idaviao.Text),
                bilheteeco = int.Parse(num_eco.Text),
                bilheteprimeriaclasse = int.Parse(num_classe.Text),
                bilheteempresarial = int.Parse(num_emp.Text),
                precototal = precototal
            };

            editarbilhete.editar(editarbilhete);
            listabilhetes();
            }
        }

        private void btn_remove_Click(object sender, EventArgs e) ///remover o bilhete
        {
            bilhetes deletebilhete;
            deletebilhete = new bilhetes()
            {
                idbilhete = int.Parse(txt_idticket.Text)
            };
            deletebilhete.delete(deletebilhete);
            listabilhetes();
        }

        private void btn_voltar_Click(object sender, EventArgs e) ///voltar para o menu
        {
            this.Hide();
            menu form = new menu();
            form.Show();
        }

        private bool ValidaForm() ///fazer as validações
        {
            bool output = true;
            if ((string.IsNullOrEmpty(txt_titular.Text) || string.IsNullOrEmpty(txt_idticket.Text)))
            {
                MessageBox.Show("Preencha todos os campos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                output = false;
            }

            return output;
        }
    }
}
