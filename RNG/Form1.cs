using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RNG
{
    public partial class Main : Form
    {
        Stack<bool> randBits = new Stack<bool>();
        List<int> randNums = new List<int>();
        string min;
        string max;

        public Main()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        { 
            using (OpenFileDialog open = new OpenFileDialog())
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    this.randBits = RNGController.GetRandomBitsStack(open.FileName);
                }
            }
        }

       

        private void txtMin_TextChanged(object sender, EventArgs e)
        {
            this.min = this.txtMin.Text;
        }

        private void txtMax_TextChanged(object sender, EventArgs e)
        {
            this.max = this.txtMax.Text;
        }
        private void btnGenerateOne_Click(object sender, EventArgs e)
        {

            try
            {
                int n = RNGController.GetRandomNumber(this.min, this.max, this.randBits);
                randNums.Add(n);
                this.labelRandomNum.Text = n.ToString();
            }
            catch
            {
                this.labelRandomNum.Text = "Out of bits.";
            }
        }
        private void btnGenerateAll_Click(object sender, EventArgs e)
        {
            for (; ; )
            {
                try
                {
                    int n = RNGController.GetRandomNumber(this.min, this.max, this.randBits);
                    randNums.Add(n);
                }
                catch
                {
                    MessageBox.Show("Check output for results.");
                    break;
                }
            }

            System.Diagnostics.Debug.WriteLine(this.randNums.Average());
        }
    }
}
