using COVID.DB;
using COVID.MLP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID.Vista
{
    public partial class principal : Form
    {
        static Mlp red;

        public principal()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this.SetVisibleCore(false);
            database db = new database();
            red = entrenamiento.carga();
            
            new mlp(db,red).ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
       
            new som().ShowDialog();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }
    }
}
