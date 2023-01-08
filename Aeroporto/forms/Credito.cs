using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aeroporto.forms;

namespace Aeroporto.forms
{
    public partial class Credito : Form
    {
        public Credito()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
            menu form = new menu();
            form.Show();
        }
    }
}
