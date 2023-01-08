using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aeroporto.forms
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            tblvoos form = new tblvoos(); ///vai para o form do registro de voos
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            avioes form = new avioes();  ///vai para o form do registro de a avioes
            form.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit(); ///fecha a aplicação
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

            this.Hide();
            VerVoos form = new VerVoos(); ///vai para o form de ver os voos
            form.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Credito form = new Credito(); ///vai para o form dos creditos
            form.Show();
        }
    }
}
