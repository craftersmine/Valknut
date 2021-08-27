﻿
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.waitAnim = new MaterialSkin.Controls.MaterialProgressBar();
            this.loginForm.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.loginButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.registerButton, 0, 1);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // waitAnim
            // 
            this.waitAnim.Depth = 0;
            resources.ApplyResources(this.waitAnim, "waitAnim");
            this.waitAnim.MouseState = MaterialSkin.MouseState.HOVER;
            this.waitAnim.Name = "waitAnim";
            this.waitAnim.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.loginForm);
            this.Name = "MainForm";
            this.Sizable = false;
            this.loginForm.ResumeLayout(false);
            this.loginForm.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

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
    }
}
