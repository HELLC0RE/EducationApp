namespace EducationApp
{
    partial class DealGenerate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridPersons = new System.Windows.Forms.DataGridView();
            this.dataGridPrograms = new System.Windows.Forms.DataGridView();
            this.buttonDealGenerateTxt = new System.Windows.Forms.Button();
            this.buttonDealGeneratePDF = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonQrPdf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPersons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrograms)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPersons
            // 
            this.dataGridPersons.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPersons.Location = new System.Drawing.Point(12, 97);
            this.dataGridPersons.Name = "dataGridPersons";
            this.dataGridPersons.Size = new System.Drawing.Size(573, 520);
            this.dataGridPersons.TabIndex = 0;
            // 
            // dataGridPrograms
            // 
            this.dataGridPrograms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPrograms.Location = new System.Drawing.Point(591, 97);
            this.dataGridPrograms.Name = "dataGridPrograms";
            this.dataGridPrograms.Size = new System.Drawing.Size(583, 520);
            this.dataGridPrograms.TabIndex = 1;
            // 
            // buttonDealGenerateTxt
            // 
            this.buttonDealGenerateTxt.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDealGenerateTxt.Location = new System.Drawing.Point(16, 47);
            this.buttonDealGenerateTxt.Name = "buttonDealGenerateTxt";
            this.buttonDealGenerateTxt.Size = new System.Drawing.Size(130, 28);
            this.buttonDealGenerateTxt.TabIndex = 2;
            this.buttonDealGenerateTxt.Text = "TxT";
            this.buttonDealGenerateTxt.UseVisualStyleBackColor = true;
            this.buttonDealGenerateTxt.Click += new System.EventHandler(this.buttonDealGenerateTXT_Click);
            // 
            // buttonDealGeneratePDF
            // 
            this.buttonDealGeneratePDF.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDealGeneratePDF.Location = new System.Drawing.Point(152, 48);
            this.buttonDealGeneratePDF.Name = "buttonDealGeneratePDF";
            this.buttonDealGeneratePDF.Size = new System.Drawing.Size(130, 27);
            this.buttonDealGeneratePDF.TabIndex = 3;
            this.buttonDealGeneratePDF.Text = "Pdf";
            this.buttonDealGeneratePDF.UseVisualStyleBackColor = true;
            this.buttonDealGeneratePDF.Click += new System.EventHandler(this.buttonDealGeneratePDF_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Составить договор в формате:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(375, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(278, 21);
            this.label2.TabIndex = 5;
            this.label2.Text = "Составить квитанцию в формате:";
            // 
            // buttonQrPdf
            // 
            this.buttonQrPdf.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonQrPdf.Location = new System.Drawing.Point(379, 47);
            this.buttonQrPdf.Name = "buttonQrPdf";
            this.buttonQrPdf.Size = new System.Drawing.Size(130, 27);
            this.buttonQrPdf.TabIndex = 6;
            this.buttonQrPdf.Text = "Pdf";
            this.buttonQrPdf.UseVisualStyleBackColor = true;
            this.buttonQrPdf.Click += new System.EventHandler(this.buttonQrPdf_Click);
            // 
            // DealGenerate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 629);
            this.Controls.Add(this.buttonQrPdf);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonDealGeneratePDF);
            this.Controls.Add(this.buttonDealGenerateTxt);
            this.Controls.Add(this.dataGridPrograms);
            this.Controls.Add(this.dataGridPersons);
            this.Name = "DealGenerate";
            this.Text = "Составление договора";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPersons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPrograms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPersons;
        private System.Windows.Forms.DataGridView dataGridPrograms;
        private System.Windows.Forms.Button buttonDealGenerateTxt;
        private System.Windows.Forms.Button buttonDealGeneratePDF;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonQrPdf;
    }
}