namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator {
  partial class ResultDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.txtConfig = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btkOK = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // txtConfig
      // 
      this.txtConfig.BackColor = System.Drawing.SystemColors.Window;
      this.txtConfig.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtConfig.Location = new System.Drawing.Point(13, 40);
      this.txtConfig.Multiline = true;
      this.txtConfig.Name = "txtConfig";
      this.txtConfig.ReadOnly = true;
      this.txtConfig.Size = new System.Drawing.Size(557, 199);
      this.txtConfig.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(309, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Copy the snippet shown below to your App.config / Web.config.";
      // 
      // btkOK
      // 
      this.btkOK.Location = new System.Drawing.Point(495, 263);
      this.btkOK.Name = "btkOK";
      this.btkOK.Size = new System.Drawing.Size(75, 23);
      this.btkOK.TabIndex = 2;
      this.btkOK.Text = "&OK";
      this.btkOK.UseVisualStyleBackColor = true;
      this.btkOK.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // ResultDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(582, 298);
      this.Controls.Add(this.btkOK);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtConfig);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ResultDialog";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "OAuth2 Token Generator - Config outpot";
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtConfig;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btkOK;
  }
}