using System;
using System.Windows.Forms;

namespace BicycleMushroomIdentification
{
    public class MushroomForm : BaseForm
    {
        private Label lblQuestion;
        private Button btnStart;
        private Button btnYes;
        private Button btnNo;

        private int step = 0;

        public MushroomForm()
        {
            Text = "Mushroom";
            ClientSize = new System.Drawing.Size(420, 300);

            Label lblInfo = new Label
            {
                Text = "Think of one of: Agaric jaunissant, Amanite tue-mouche, Cepe de bordeaux, " +
                       "Coprin chevelu, Girolle, Pied bleu.",
                Left = 20,
                Top = 15,
                Width = 380,
                Height = 55
            };

            lblQuestion = new Label
            {
                Text = "Click Start to begin.",
                Left = 20,
                Top = 85,
                Width = 380,
                Height = 40,
                Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold)
            };

            btnYes = new Button { Text = "Yes", Left = 60, Top = 135, Width = 120, Height = 34, Enabled = false };
            btnYes.Click += (s, e) => Answer(true);

            btnNo = new Button { Text = "No", Left = 240, Top = 135, Width = 120, Height = 34, Enabled = false };
            btnNo.Click += (s, e) => Answer(false);

            btnStart = new Button { Text = "Start", Left = 150, Top = 185, Width = 120, Height = 34 };
            btnStart.Click += (s, e) => { step = 1; ShowStep(); };

            Button btnBack = new Button { Text = "Back", Left = 60, Top = 245, Width = 120, Height = 34 };
            btnBack.Click += (s, e) => GoTo(new ChooseForm());

            Button btnLogout = new Button { Text = "Logout", Left = 240, Top = 245, Width = 120, Height = 34 };
            btnLogout.Click += (s, e) => GoTo(new LoginForm());

            Controls.AddRange(new Control[] { lblInfo, lblQuestion, btnYes, btnNo, btnStart, btnBack, btnLogout });
        }

        // Steps:
        //   1 ring?    yes -> 2, no -> 4
        //   2 forest?  yes -> Amanite tue-mouche, no -> 3
        //   3 cup?     yes -> Agaric jaunissant, no -> Coprin chevelu
        //   4 gills?   no  -> Cepe de bordeaux, yes -> 5
        //   5 cup?     yes -> Pied bleu, no -> Girolle
        private void ShowStep()
        {
            switch (step)
            {
                case 1: lblQuestion.Text = "Does your mushroom have a ring?"; break;
                case 2: lblQuestion.Text = "Does your mushroom grow in a forest?"; break;
                case 3:
                case 5: lblQuestion.Text = "Does your mushroom have a convex cup?"; break;
                case 4: lblQuestion.Text = "Does your mushroom have gills?"; break;
            }

            btnYes.Enabled = true;
            btnNo.Enabled = true;
            btnStart.Enabled = false;
        }

        private void Answer(bool yes)
        {
            switch (step)
            {
                case 1: step = yes ? 2 : 4; break;
                case 2:
                    if (yes) { Finish("Amanite tue-mouche"); return; }
                    step = 3; break;
                case 3: Finish(yes ? "Agaric jaunissant" : "Coprin chevelu"); return;
                case 4:
                    if (!yes) { Finish("Cepe de bordeaux"); return; }
                    step = 5; break;
                case 5: Finish(yes ? "Pied bleu" : "Girolle"); return;
            }
            ShowStep();
        }

        private void Finish(string mushroom)
        {
            lblQuestion.Text = "Your mushroom is: " + mushroom;
            MessageBox.Show("Your mushroom is: " + mushroom, "Result",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            step = 0;
            btnYes.Enabled = false;
            btnNo.Enabled = false;
            btnStart.Enabled = true;
            btnStart.Text = "Start again";
        }
    }
}