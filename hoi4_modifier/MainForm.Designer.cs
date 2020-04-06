using System.Windows.Forms;

public partial class mainForm
{
    /// <summary>
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components;

    /// <summary>
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    /// <summary>
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
            this.clearButton = new System.Windows.Forms.Button();
            this.inputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.modifyButton = new System.Windows.Forms.Button();
            this.printLabel = new System.Windows.Forms.Label();
            this.delectButton = new System.Windows.Forms.Button();
            this.ReadButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(460, 250);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(100, 60);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "清除";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // inputRichTextBox
            // 
            this.inputRichTextBox.Location = new System.Drawing.Point(10, 20);
            this.inputRichTextBox.Name = "inputRichTextBox";
            this.inputRichTextBox.Size = new System.Drawing.Size(550, 160);
            this.inputRichTextBox.TabIndex = 2;
            this.inputRichTextBox.Text = "";
            // 
            // modifyButton
            // 
            this.modifyButton.Location = new System.Drawing.Point(350, 250);
            this.modifyButton.Name = "modifyButton";
            this.modifyButton.Size = new System.Drawing.Size(100, 60);
            this.modifyButton.TabIndex = 3;
            this.modifyButton.Text = "修改";
            this.modifyButton.UseVisualStyleBackColor = true;
            this.modifyButton.Click += new System.EventHandler(this.ModifyButton_Click);
            // 
            // printLabel
            // 
            this.printLabel.AutoSize = true;
            this.printLabel.Location = new System.Drawing.Point(20, 200);
            this.printLabel.Name = "printLabel";
            this.printLabel.Size = new System.Drawing.Size(0, 15);
            this.printLabel.TabIndex = 6;
            // 
            // delectButton
            // 
            this.delectButton.Location = new System.Drawing.Point(240, 250);
            this.delectButton.Name = "delectButton";
            this.delectButton.Size = new System.Drawing.Size(100, 60);
            this.delectButton.TabIndex = 7;
            this.delectButton.Text = "删除";
            this.delectButton.UseVisualStyleBackColor = true;
            this.delectButton.Click += new System.EventHandler(this.DelectButton_Click);
            // 
            // ReadButton
            // 
            this.ReadButton.Location = new System.Drawing.Point(130, 250);
            this.ReadButton.Name = "ReadButton";
            this.ReadButton.Size = new System.Drawing.Size(100, 60);
            this.ReadButton.TabIndex = 8;
            this.ReadButton.Text = "读取";
            this.ReadButton.UseVisualStyleBackColor = true;
            this.ReadButton.Click += new System.EventHandler(this.ReadButton_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 353);
            this.Controls.Add(this.ReadButton);
            this.Controls.Add(this.delectButton);
            this.Controls.Add(this.printLabel);
            this.Controls.Add(this.modifyButton);
            this.Controls.Add(this.inputRichTextBox);
            this.Controls.Add(this.clearButton);
            this.Name = "mainForm";
            this.Text = "钢铁雄心4文本修改器";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private RichTextBox inputRichTextBox;
    private Button modifyButton;
    private Button clearButton;
    private Label printLabel;
    private Button delectButton;
    private Button ReadButton;
}