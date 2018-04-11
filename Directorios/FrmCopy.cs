using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Directorios
{
    public partial class FrmCopy : Form
    {
        public static UtilEvents _tool = new UtilEvents();
        public delegate void MyDelegado(int progressId);

        public FrmCopy()
        {
            InitializeComponent();
            _tool.ProgressChanged += Tool_ProgressChanged;
        }

        private void btnCopiar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtOrigen.Text) && !string.IsNullOrEmpty(txtDestino.Text))
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(TxtOrigen.Text);
                    var total = Task.Run(() => { return dir.GetFiles("*", SearchOption.AllDirectories).Length; }).Result;
                    progressBarDir.Visible = true;
                    progressBarDir.Minimum = 1;
                    progressBarDir.Maximum = total;
                    progressBarDir.Value = 1;
                    progressBarDir.Step = 1;

                    Directorios.DirectoryCopy(TxtOrigen.Text, txtDestino.Text, true);
                    MessageBox.Show("Finalizo", "Copiar", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                catch (Exception)
                {

                    MessageBox.Show("Error Creando Archivos", "My Application",
MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }

            }
        }
        #region eventos
        private void Tool_ProgressChanged(int progress)
        {
            if (progressBarDir.InvokeRequired)
            {
                MyDelegado MD = new MyDelegado(Mostrar);
                //progressBarDir.Invoke(MD, new object[] { progress });
                try
                {
                    BeginInvoke((Action)delegate
                    {
                        if (progress <= progressBarDir.Maximum)
                            progressBarDir.Value = progress;
                        LblContador.Text = "Registros Leidos = " + progressBarDir.Value.ToString();
                        LblTotal.Text = "Registros Total = " + progressBarDir.Maximum.ToString();
                        //Application.DoEvents();
                    });// progressBarDir.Value++;
                    //this.Invoke((Action)delegate { Mostrar(progress); });
                }
                catch (Exception ex)
                {

                    throw;
                }

                // progressBarDir.BeginInvoke(new Action<StatusEventArgs>(OnChangeStatus), new[] { e });
            }
            else
            {
                if (progress <= progressBarDir.Maximum)
                    progressBarDir.Value = progress;
                LblContador.Text = "Registros Leidos = " + progressBarDir.Value.ToString();
                LblTotal.Text = "Registros Total = " + progressBarDir.Maximum.ToString();
            }
        }
        public void Mostrar(int progress)
        {
            progressBarDir.Value = progress;
            LblContador.Text = "Registros Leidos = " + progressBarDir.Value.ToString();
            LblTotal.Text = "Registros Total = " + progressBarDir.Maximum.ToString();
        }
        #endregion

        private void LblTotal_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
//this.Invoke(new Action(() => Progress_Bar_Loading_Images.Value++));
//Task.Create(delegate
//        {  
//            Parallel.For(0, iterations, i => 
//            {  
//                Thread.SpinWait(50000000); // do work here  
//                BeginInvoke((Action)delegate { pb.Value++; });  
//            });  
//        });  
//public delegate void InvokeDelegate();

//private void Invoke_Click(object sender, EventArgs e)
//{
//    myTextBox.BeginInvoke(new InvokeDelegate(InvokeMethod));
//}
//public void InvokeMethod()
//{
//    myTextBox.Text = "Executed the given delegate";
//}


