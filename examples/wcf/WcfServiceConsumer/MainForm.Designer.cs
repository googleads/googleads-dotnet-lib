namespace Google.Api.Ads.Dfa.Examples.Wcf {
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
      this.btnGetAdTypes = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnGetAdTypes
      // 
      this.btnGetAdTypes.Location = new System.Drawing.Point(76, 23);
      this.btnGetAdTypes.Name = "btnGetAdTypes";
      this.btnGetAdTypes.Size = new System.Drawing.Size(160, 33);
      this.btnGetAdTypes.TabIndex = 0;
      this.btnGetAdTypes.Text = "Get Ad Types";
      this.btnGetAdTypes.UseVisualStyleBackColor = true;
      this.btnGetAdTypes.Click += new System.EventHandler(this.btnGetAdTypes_Click);
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(317, 88);
      this.Controls.Add(this.btnGetAdTypes);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "WCF Service Consumer";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnGetAdTypes;
  }
}

