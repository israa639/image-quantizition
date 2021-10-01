using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        graph g;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                g = new graph();
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath,g);

                //int isl=g.findDistinctColor( ImageMatrix, OpenedFilePath);


            

                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
            textBox1.Enabled = true;
             
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            g.constructGraph();

            MST mst = new MST(g);
           double x = mst.kruskal(g.GetEdge(), g);
            clusters cluster = new clusters(mst.getkruskalArray(), g);
           
            
            string k=textBox1.Text;
            cluster.detectCluster(mst.getkruskalArray(),int.Parse(k) , g);
           

          
           cluster.findRepColors(ImageMatrix,g);
          //  //double sigma = double.Parse(txtGaussSigma.Text);
          //  //int maskSize = (int)nudMaskSize.Value ;
          ////  ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);

            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void nudMaskSize_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

       
        private void txtWidth_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}