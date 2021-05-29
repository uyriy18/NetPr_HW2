
namespace AsyncClient_Task2
{
    partial class ClientForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.Screen_txbx = new System.Windows.Forms.TextBox();
            this.Date_btn = new System.Windows.Forms.Button();
            this.Time_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Screen_txbx
            // 
            this.Screen_txbx.Location = new System.Drawing.Point(13, 13);
            this.Screen_txbx.Multiline = true;
            this.Screen_txbx.Name = "Screen_txbx";
            this.Screen_txbx.Size = new System.Drawing.Size(481, 310);
            this.Screen_txbx.TabIndex = 0;
            // 
            // Date_btn
            // 
            this.Date_btn.Location = new System.Drawing.Point(13, 328);
            this.Date_btn.Name = "Date_btn";
            this.Date_btn.Size = new System.Drawing.Size(75, 23);
            this.Date_btn.TabIndex = 1;
            this.Date_btn.Text = "Date";
            this.Date_btn.UseVisualStyleBackColor = true;
            this.Date_btn.Click += new System.EventHandler(this.Date_btn_Click);
            // 
            // Time_btn
            // 
            this.Time_btn.Location = new System.Drawing.Point(94, 328);
            this.Time_btn.Name = "Time_btn";
            this.Time_btn.Size = new System.Drawing.Size(75, 23);
            this.Time_btn.TabIndex = 2;
            this.Time_btn.Text = "Time";
            this.Time_btn.UseVisualStyleBackColor = true;
            this.Time_btn.Click += new System.EventHandler(this.Time_btn_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 363);
            this.Controls.Add(this.Time_btn);
            this.Controls.Add(this.Date_btn);
            this.Controls.Add(this.Screen_txbx);
            this.Name = "ClientForm";
            this.Text = "Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClientForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Screen_txbx;
        private System.Windows.Forms.Button Date_btn;
        private System.Windows.Forms.Button Time_btn;
    }
}

