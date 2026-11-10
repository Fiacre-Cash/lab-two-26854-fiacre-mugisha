namespace BicycleMushroomIdentification
{
    public class ChooseForm : BaseForm
    {
        public ChooseForm()
        {
            Text = "Choose";
            ClientSize = new System.Drawing.Size(320, 170);

            Label lbl = new Label { Text = "What do you want to do?", Left = 20, Top = 20, Width = 280 };

            Button btnBicycle = new Button { Text = "Bicycle", Left = 60, Top = 55, Width = 200, Height = 36 };
            btnBicycle.Click += (s, e) => GoTo(new BicycleForm());

            Button btnMushroom = new Button { Text = "Mushroom", Left = 60, Top = 105, Width = 200, Height = 36 };
            btnMushroom.Click += (s, e) => GoTo(new MushroomForm());

            Controls.AddRange(new Control[] { lbl, btnBicycle, btnMushroom });
        }
    }
}