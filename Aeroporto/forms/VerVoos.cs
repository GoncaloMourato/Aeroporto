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
using System.Data.SQLite;
using Aeroporto.forms;

namespace Aeroporto
{
    public partial class VerVoos : Form
    {

        int idvoo = 0; 
        int idaviao = 0;
        string origem = "";
        string destino = "";
        string data = "";
        string hora = "";

        private SQLiteConnection connection;

        List<voos> voosencontrados;
        List<voos> lvoos;
        List<voos> viis = new List<voos>();
        voos vous = new voos();

        public VerVoos()
        {
            InitializeComponent();
            vous.basedados();
            listavoos();

        }

        public void listavoos() ///listar os voos 
        {
            vous.criartbl();
            dataGridView1.Rows.Clear();

            lvoos = vous.selectvoo();

            foreach (voos voos in lvoos)
            {
                dataGridView1.Rows.Add(voos.idvoo, voos.idaviao, voos.origem, voos.destino, voos.data, voos.hora);
            }

        }



        private void btn_update_Click(object sender, EventArgs e) ///pesquisar os voos
        {
            dataGridView1.Rows.Clear ();
            List<string> voosdisponiveis = new List<string>();

            voos pesquisarvoo;
            pesquisarvoo = new voos()
            {
                origem = cbo_partida.Text,
                destino = cbo_chegada.Text,
                data = dateTimePicker1.Text
            };

            var path = @"Data\aeronautica.sqlite";
            connection = new SQLiteConnection("DataSource=" + path);
            connection.Open();
            SQLiteCommand comm = new SQLiteCommand("SELECT * FROM voos WHERE origem ='" + pesquisarvoo.origem + "'or destino ='" + pesquisarvoo.destino + "'or data ='" + pesquisarvoo.data + "'", connection);
            using (SQLiteDataReader read = comm.ExecuteReader())
            {
                while (read.Read())
                {
                    dataGridView1.Rows.Add(new object[]
                    {
                    read.GetValue(0), 
                    read.GetValue(1),
                    read.GetValue(2),
                    read.GetValue(3),
                    read.GetValue(4),
                    read.GetValue(5)
                    });
                }
            }
        }

            public void listavoosdisponiveis() ///apresentar os voos disponiveis criados na datagrid 
            {
                vous.criartbl();
                dataGridView1.Rows.Clear();

                foreach (voos voos in voosencontrados)
                {
                    dataGridView1.Rows.Add(voos.idvoo, voos.idaviao, voos.origem, voos.destino, voos.data, voos.hora);
                }


            }

        private void btn_comprar_Click(object sender, EventArgs e) ///ir para o menu tickets
        {
            this.Hide();
            tickets form = new tickets(idvoo,idaviao,origem,destino,data,hora);
            form.Show();
        }

        public void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) ///apresentar os valores na datagrid
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
             idvoo = int.Parse(row.Cells[0].Value.ToString());
            idaviao = int.Parse(row.Cells[1].Value.ToString());
            origem = row.Cells[2].Value.ToString();
            destino = row.Cells[3].Value.ToString();
            data = row.Cells[4].Value.ToString();
            hora = row.Cells[5].Value.ToString();
        }

        private void btn_voltar_Click(object sender, EventArgs e) ///voltar para o menu
        {
            this.Hide();
            menu form = new menu();
            form.Show();
        }
    }
    } 

