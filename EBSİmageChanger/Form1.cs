using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace EBSİmageChanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        Thread th; OpenFileDialog op;
        private void button1_Click(object sender, EventArgs e)
        {
            op = new OpenFileDialog();
            op.Multiselect = true;
            if (op.ShowDialog() == DialogResult.OK)
            {
                th = new Thread(Changer); th.Start();
            }
        }

        private void Changer()
        {
            if (listBox1.Items.Count != 0)
            {
                listBox1.Items.Clear();
            }
            else
            {
                for (int i = 0; i < op.FileNames.Length; i++)
                {
                    listBox1.Items.Add(op.FileNames[i].ToString());
                }
                for (int i = 0; i < op.FileNames.Length; i++)
                {
                    Image image = Image.FromFile(op.FileNames[i].ToString());
                    string Yol = op.FileNames[i].ToString().Replace(op.SafeFileNames[i].ToString(), "");
                    İmageSizeChanger(image, int.Parse(txt_Genislik.Text), int.Parse(txt_Yukseklik.Text), txt_Resimİsmi.Text);
                }
            }
        }

        public void İmageSizeChanger(Image img, int Genislik, int Yukseklik, string FolderOrName)
        {
            Bitmap Newİmage = new Bitmap(Genislik, Yukseklik);
            using (Graphics g = Graphics.FromImage((Image)Newİmage))
            {
                g.DrawImage(img, 0, 0, Genislik, Yukseklik);
            }
            Newİmage.Save(FolderOrName, ImageFormat.Jpeg);
        }
    }
}
