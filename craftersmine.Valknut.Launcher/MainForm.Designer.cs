
namespace craftersmine.Valknut.Launcher
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.emailBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.passwordBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.rememberMeCheck = new MaterialSkin.Controls.MaterialCheckBox();
            this.loginButton = new MaterialSkin.Controls.MaterialRaisedButton();
            this.registerButton = new MaterialSkin.Controls.MaterialFlatButton();
            this.loginForm = new System.Windows.Forms.Panel();
            this.waitAnim = new MaterialSkin.Controls.MaterialProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.launcherPanel = new System.Windows.Forms.Panel();
            this.loggedInAs = new MaterialSkin.Controls.MaterialLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.clientSelect = new MaterialSkin.Controls.MaterialLabel();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.loginForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.launcherPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            resources.ApplyResources(this.materialLabel1, "materialLabel1");
            this.materialLabel1.Depth = 0;
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            // 
            // emailBox
            // 
            resources.ApplyResources(this.emailBox, "emailBox");
            this.emailBox.Depth = 0;
            this.emailBox.Hint = "example@example.com";
            this.emailBox.MaxLength = 32767;
            this.emailBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.emailBox.Name = "emailBox";
            this.emailBox.PasswordChar = '\0';
            this.emailBox.SelectedText = "";
            this.emailBox.SelectionLength = 0;
            this.emailBox.SelectionStart = 0;
            this.emailBox.TabStop = false;
            this.emailBox.UseSystemPasswordChar = false;
            // 
            // materialLabel2
            // 
            resources.ApplyResources(this.materialLabel2, "materialLabel2");
            this.materialLabel2.Depth = 0;
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            // 
            // passwordBox
            // 
            resources.ApplyResources(this.passwordBox, "passwordBox");
            this.passwordBox.Depth = 0;
            this.passwordBox.Hint = "Your password";
            this.passwordBox.MaxLength = 32767;
            this.passwordBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '\0';
            this.passwordBox.SelectedText = "";
            this.passwordBox.SelectionLength = 0;
            this.passwordBox.SelectionStart = 0;
            this.passwordBox.TabStop = false;
            this.passwordBox.UseSystemPasswordChar = true;
            // 
            // rememberMeCheck
            // 
            resources.ApplyResources(this.rememberMeCheck, "rememberMeCheck");
            this.rememberMeCheck.Depth = 0;
            this.rememberMeCheck.MouseLocation = new System.Drawing.Point(-1, -1);
            this.rememberMeCheck.MouseState = MaterialSkin.MouseState.HOVER;
            this.rememberMeCheck.Name = "rememberMeCheck";
            this.rememberMeCheck.Ripple = true;
            this.rememberMeCheck.UseVisualStyleBackColor = true;
            // 
            // loginButton
            // 
            resources.ApplyResources(this.loginButton, "loginButton");
            this.loginButton.Depth = 0;
            this.loginButton.Icon = null;
            this.loginButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.loginButton.Name = "loginButton";
            this.loginButton.Primary = true;
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // registerButton
            // 
            resources.ApplyResources(this.registerButton, "registerButton");
            this.registerButton.Depth = 0;
            this.registerButton.Icon = null;
            this.registerButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.registerButton.Name = "registerButton";
            this.registerButton.Primary = false;
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // loginForm
            // 
            this.loginForm.BackColor = System.Drawing.Color.White;
            this.loginForm.Controls.Add(this.waitAnim);
            this.loginForm.Controls.Add(this.tableLayoutPanel1);
            this.loginForm.Controls.Add(this.rememberMeCheck);
            this.loginForm.Controls.Add(this.passwordBox);
            this.loginForm.Controls.Add(this.materialLabel2);
            this.loginForm.Controls.Add(this.emailBox);
            this.loginForm.Controls.Add(this.materialLabel1);
            resources.ApplyResources(this.loginForm, "loginForm");
            this.loginForm.Name = "loginForm";
            // 
            // waitAnim
            // 
            this.waitAnim.Depth = 0;
            resources.ApplyResources(this.waitAnim, "waitAnim");
            this.waitAnim.MouseState = MaterialSkin.MouseState.HOVER;
            this.waitAnim.Name = "waitAnim";
            this.waitAnim.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.loginButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.registerButton, 0, 1);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // launcherPanel
            // 
            this.launcherPanel.BackColor = System.Drawing.Color.White;
            this.launcherPanel.Controls.Add(this.materialFlatButton1);
            this.launcherPanel.Controls.Add(this.materialRaisedButton1);
            this.launcherPanel.Controls.Add(this.clientSelect);
            this.launcherPanel.Controls.Add(this.comboBox1);
            this.launcherPanel.Controls.Add(this.loggedInAs);
            resources.ApplyResources(this.launcherPanel, "launcherPanel");
            this.launcherPanel.Name = "launcherPanel";
            // 
            // loggedInAs
            // 
            this.loggedInAs.Depth = 0;
            resources.ApplyResources(this.loggedInAs, "loggedInAs");
            this.loggedInAs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.loggedInAs.MouseState = MaterialSkin.MouseState.HOVER;
            this.loggedInAs.Name = "loggedInAs";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Name = "comboBox1";
            // 
            // clientSelect
            // 
            resources.ApplyResources(this.clientSelect, "clientSelect");
            this.clientSelect.Depth = 0;
            this.clientSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.clientSelect.MouseState = MaterialSkin.MouseState.HOVER;
            this.clientSelect.Name = "clientSelect";
            // 
            // materialRaisedButton1
            // 
            resources.ApplyResources(this.materialRaisedButton1, "materialRaisedButton1");
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            // 
            // materialFlatButton1
            // 
            resources.ApplyResources(this.materialFlatButton1, "materialFlatButton1");
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.Icon = global::craftersmine.Valknut.Launcher.Properties.Resources.outline_settings_black_24dp;
            this.materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = false;
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            // 
            // materialLabel3
            // 
            resources.ApplyResources(this.materialLabel3, "materialLabel3");
            this.materialLabel3.Depth = 0;
            this.materialLabel3.ForeColor = System.Drawing.Color.Silver;
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.launcherPanel);
            this.Controls.Add(this.loginForm);
            this.Name = "MainForm";
            this.Sizable = false;
            this.loginForm.ResumeLayout(false);
            this.loginForm.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.launcherPanel.ResumeLayout(false);
            this.launcherPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialSingleLineTextField emailBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialSingleLineTextField passwordBox;
        private MaterialSkin.Controls.MaterialCheckBox rememberMeCheck;
        private MaterialSkin.Controls.MaterialRaisedButton loginButton;
        private MaterialSkin.Controls.MaterialFlatButton registerButton;
        private System.Windows.Forms.Panel loginForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialProgressBar waitAnim;
        private System.Windows.Forms.Panel launcherPanel;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
        private MaterialSkin.Controls.MaterialLabel clientSelect;
        private System.Windows.Forms.ComboBox comboBox1;
        private MaterialSkin.Controls.MaterialLabel loggedInAs;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
    }
}

