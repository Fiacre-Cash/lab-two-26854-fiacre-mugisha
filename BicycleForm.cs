using System;
using System.Windows.Forms;

namespace BicycleMushroomIdentification
{
    public class BicycleForm : BaseForm
    {
        private TextBox txtStart;
        private TextBox txtEnd;
        private TextBox txtTotal;

        public BicycleForm()
        {
            Text = "Bicycle Rental";
            ClientSize = new System.Drawing.Size(360, 280);

            Label lblStart = new Label { Text = "Start hour (0-23):", Left = 20, Top = 25, Width = 140 };
            txtStart = new TextBox { Left = 180, Top = 22, Width = 140 };

            Label lblEnd = new Label { Text = "End hour (1-24):", Left = 20, Top = 70, Width = 140 };
            txtEnd = new TextBox { Left = 180, Top = 67, Width = 140 };

            Button btnCalc = new Button { Text = "Calculate", Left = 180, Top = 110, Width = 140, Height = 32 };
            btnCalc.Click += btnCalc_Click;

            Label lblTotal = new Label { Text = "Total (RWF):", Left = 20, Top = 163, Width = 140 };
            txtTotal = new TextBox { Left = 180, Top = 160, Width = 140, ReadOnly = true };

            Button btnBack = new Button { Text = "Back", Left = 20, Top = 220, Width = 140, Height = 32 };
            btnBack.Click += (s, e) => GoTo(new ChooseForm());

            Button btnLogout = new Button { Text = "Logout", Left = 180, Top = 220, Width = 140, Height = 32 };
            btnLogout.Click += (s, e) => GoTo(new LoginForm());

            Controls.AddRange(new Control[] { lblStart, txtStart, lblEnd, txtEnd, btnCalc,
                                              lblTotal, txtTotal, btnBack, btnLogout });
            AcceptButton = btnCalc;
        }

        private static int RateForHour(int hour)
        {
            if (hour >= 14 && hour < 19) return 1500;
            if ((hour >= 7 && hour < 14) || (hour >= 19 && hour < 21)) return 1000;
            return 500;
        }

        private void btnCalc_Click(object? sender, EventArgs e)
        {
            txtTotal.Clear();

            if (!int.TryParse(txtStart.Text.Trim(), out int start) ||
                !int.TryParse(txtEnd.Text.Trim(), out int end))
            {
                MessageBox.Show("Please enter whole numbers only (no decimals, no letters).",
                    "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (start < 0 || start > 23)
            {
                MessageBox.Show("Start hour must be between 0 and 23.", "Invalid input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStart.Focus();
                return;
            }

            if (end < 1 || end > 24)
            {
                MessageBox.Show("End hour must be between 1 and 24.", "Invalid input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEnd.Focus();
                return;
            }

            if (start >= end)
            {
                MessageBox.Show("Start hour must be less than end hour.", "Invalid input",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int total = 0;
            for (int h = start; h < end; h++)
                total += RateForHour(h);

            txtTotal.Text = total.ToString();
            MessageBox.Show("Total to pay: " + total + " RWF", "Rental total",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}