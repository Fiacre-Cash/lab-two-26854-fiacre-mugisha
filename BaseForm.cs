using System;
using System.Drawing;
using System.Windows.Forms;

namespace BicycleMushroomIdentification
{
    public class BaseForm : Form
    {
        private bool _navigating;

        public BaseForm()
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Font = new Font("Segoe UI", 10F);
        }

        protected void GoTo(Form next)
        {
            _navigating = true;
            next.Show();
            Close();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (!_navigating)
                Application.Exit();
        }
    }
}