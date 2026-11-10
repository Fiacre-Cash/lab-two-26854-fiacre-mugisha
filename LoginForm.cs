using System;
using System.Windows.Forms;

namespace BicycleMushroomIdentification
{
    public partial class LoginForm : BaseForm
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Label lblCredentials;
        private Button btnLogin;

        public LoginForm()
        {
            Text = "Login";
            ClientSize = new System.Drawing.Size(340, 270);

            Label lblUser = new Label { Text = "Username:", Left = 20, Top = 25, Width = 90 };
            txtUsername = new TextBox { Left = 120, Top = 22, Width = 190 };
            txtUsername.TextChanged += UpdateCredentials;

            Label lblPass = new Label { Text = "Password:", Left = 20, Top = 70, Width = 90 };
            txtPassword = new TextBox { Left = 120, Top = 67, Width = 190, UseSystemPasswordChar = true };
            txtPassword.TextChanged += UpdateCredentials;

            chkShowPassword = new CheckBox { Text = "Show password", Left = 120, Top = 100, Width = 190 };
            chkShowPassword.CheckedChanged += (s, e) =>
            {
                txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
                UpdateCredentials(s, e);
            };

            btnLogin = new Button { Text = "Login", Left = 120, Top = 135, Width = 100, Height = 32 };
            btnLogin.Click += btnLogin_Click;

            lblCredentials = new Label
            {
                Left = 20,
                Top = 185,
                Width = 300,
                Height = 65,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(6)
            };

            Controls.AddRange(new Control[] { lblUser, txtUsername, lblPass, txtPassword,
                                              chkShowPassword, btnLogin, lblCredentials });
            AcceptButton = btnLogin;

            UpdateCredentials(null, EventArgs.Empty);
        }

        // Shows what the user has typed so far
        private void UpdateCredentials(object? sender, EventArgs e)
        {
            string password = chkShowPassword.Checked
                ? txtPassword.Text
                : new string('*', txtPassword.Text.Length);

            lblCredentials.Text = "Username: " + txtUsername.Text + Environment.NewLine +
                                  "Password:" + password;
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username and Password cannot be empty.", "Login",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GoTo(new ChooseForm());
        }
    }
}