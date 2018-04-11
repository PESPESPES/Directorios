namespace Directorios
{
    partial class FrmCopy
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtOrigen = new System.Windows.Forms.TextBox();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopiar = new System.Windows.Forms.Button();
            this.progressBarDir = new System.Windows.Forms.ProgressBar();
            this.LblContador = new System.Windows.Forms.Label();
            this.LblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TxtOrigen
            // 
            this.TxtOrigen.Location = new System.Drawing.Point(75, 9);
            this.TxtOrigen.Name = "TxtOrigen";
            this.TxtOrigen.Size = new System.Drawing.Size(274, 20);
            this.TxtOrigen.TabIndex = 0;
            // 
            // txtDestino
            // 
            this.txtDestino.Location = new System.Drawing.Point(75, 35);
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(274, 20);
            this.txtDestino.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Origen";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destino";
            // 
            // btnCopiar
            // 
            this.btnCopiar.Location = new System.Drawing.Point(75, 133);
            this.btnCopiar.Name = "btnCopiar";
            this.btnCopiar.Size = new System.Drawing.Size(75, 23);
            this.btnCopiar.TabIndex = 4;
            this.btnCopiar.Text = "Copiar";
            this.btnCopiar.UseVisualStyleBackColor = true;
            this.btnCopiar.Click += new System.EventHandler(this.btnCopiar_Click);
            // 
            // progressBarDir
            // 
            this.progressBarDir.Location = new System.Drawing.Point(75, 104);
            this.progressBarDir.Name = "progressBarDir";
            this.progressBarDir.Size = new System.Drawing.Size(274, 23);
            this.progressBarDir.TabIndex = 6;
            this.progressBarDir.Visible = false;
            // 
            // LblContador
            // 
            this.LblContador.AutoSize = true;
            this.LblContador.Location = new System.Drawing.Point(72, 79);
            this.LblContador.Name = "LblContador";
            this.LblContador.Size = new System.Drawing.Size(31, 13);
            this.LblContador.TabIndex = 7;
            this.LblContador.Text = "Valor";
            // 
            // LblTotal
            // 
            this.LblTotal.AutoSize = true;
            this.LblTotal.Location = new System.Drawing.Point(295, 79);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(31, 13);
            this.LblTotal.TabIndex = 8;
            this.LblTotal.Text = "Total";
            this.LblTotal.Click += new System.EventHandler(this.LblTotal_Click);
            // 
            // FrmCopy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 170);
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.LblContador);
            this.Controls.Add(this.progressBarDir);
            this.Controls.Add(this.btnCopiar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDestino);
            this.Controls.Add(this.TxtOrigen);
            this.Name = "FrmCopy";
            this.Text = "Copiar Archivos";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtOrigen;
        private System.Windows.Forms.TextBox txtDestino;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopiar;
        private System.Windows.Forms.ProgressBar progressBarDir;
        private System.Windows.Forms.Label LblContador;
        private System.Windows.Forms.Label LblTotal;
    }
}

