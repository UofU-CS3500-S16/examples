using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TipCalculator
{
    public partial class TipCalc : Form
    {
        public TipCalc()
        {
            InitializeComponent();
        }

        private void TipCalc_Load(object sender, EventArgs e)
        {

        }

        private void CalcButton_Click(object sender, EventArgs e)
        {

        }

        private void CalcButton_MouseEnter(object sender, EventArgs e)
        {
            CalcButton.BackColor = Color.Red;
        }
    }
}
