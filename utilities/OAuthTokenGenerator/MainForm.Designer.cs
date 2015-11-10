namespace Google.Api.Ads.Common.Utilities.OAuthTokenGenerator {
  partial class MainForm {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
      this.panel1 = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.txtClientID = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.txtClientSecret = new System.Windows.Forms.TextBox();
      this.chkScopes = new System.Windows.Forms.CheckedListBox();
      this.label3 = new System.Windows.Forms.Label();
      this.btnOK = new System.Windows.Forms.Button();
      this.btnCancel = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.txtExtraScopes = new System.Windows.Forms.TextBox();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.txtExtraScopes);
      this.panel1.Controls.Add(this.label4);
      this.panel1.Controls.Add(this.label1);
      this.panel1.Controls.Add(this.txtClientID);
      this.panel1.Controls.Add(this.label2);
      this.panel1.Controls.Add(this.txtClientSecret);
      this.panel1.Controls.Add(this.chkScopes);
      this.panel1.Controls.Add(this.label3);
      this.panel1.Location = new System.Drawing.Point(12, 108);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(493, 226);
      this.panel1.TabIndex = 11;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 10);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(47, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Client ID";
      // 
      // txtClientID
      // 
      this.txtClientID.Location = new System.Drawing.Point(141, 10);
      this.txtClientID.Name = "txtClientID";
      this.txtClientID.Size = new System.Drawing.Size(349, 20);
      this.txtClientID.TabIndex = 1;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 36);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(67, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Client Secret";
      // 
      // txtClientSecret
      // 
      this.txtClientSecret.Location = new System.Drawing.Point(141, 36);
      this.txtClientSecret.Name = "txtClientSecret";
      this.txtClientSecret.Size = new System.Drawing.Size(349, 20);
      this.txtClientSecret.TabIndex = 3;
      // 
      // chkScopes
      // 
      this.chkScopes.FormattingEnabled = true;
      this.chkScopes.Location = new System.Drawing.Point(141, 62);
      this.chkScopes.Name = "chkScopes";
      this.chkScopes.Size = new System.Drawing.Size(349, 49);
      this.chkScopes.TabIndex = 4;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 62);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(77, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "OAuth2 Scope";
      // 
      // btnOK
      // 
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(351, 340);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(75, 23);
      this.btnOK.TabIndex = 12;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler(this.btnOk_Click);
      // 
      // btnCancel
      // 
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(432, 340);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 23);
      this.btnCancel.TabIndex = 13;
      this.btnCancel.Text = "Close";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(11, 12);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(494, 90);
      this.textBox1.TabIndex = 15;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 119);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(90, 26);
      this.label4.TabIndex = 6;
      this.label4.Text = "Addtional Scopes\r\n(One per line)";
      // 
      // txtExtraScopes
      // 
      this.txtExtraScopes.Location = new System.Drawing.Point(141, 119);
      this.txtExtraScopes.Multiline = true;
      this.txtExtraScopes.Name = "txtExtraScopes";
      this.txtExtraScopes.Size = new System.Drawing.Size(349, 98);
      this.txtExtraScopes.TabIndex = 7;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(517, 375);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOK);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "MainForm";
      this.Text = "OAuth2 Token Generator";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtClientID;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox txtClientSecret;
    private System.Windows.Forms.CheckedListBox chkScopes;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox txtExtraScopes;
    private System.Windows.Forms.Label label4;
  }
}

