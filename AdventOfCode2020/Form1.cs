using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdventOfCode2020
{
    public sealed partial class Form1 : Form
    {
        private const int AmountPerCol = 9;
        private const int ElementWidth = 100;
        private const int ElementHeight = 30;

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 25; i++)
            {
                var className = $"AdventOfCode2020.Days.Day{i + 1}";
                var posX = i / AmountPerCol * 3 * (ElementWidth + 10);
                var button = new Button();
                Controls.Add(button);
                button.Text = $@"Day {i + 1}";
                button.Location = new Point(posX + 10, 10 + i % AmountPerCol * 30);
                button.Size = new Size(ElementWidth, ElementHeight);

                var textBoxY = 10 + i % AmountPerCol * ElementHeight + 5;
                var textBox1 = new TextBox();
                Controls.Add(textBox1);
                textBox1.Location = new Point(posX + ElementWidth + 20, textBoxY);
                textBox1.Size = new Size(ElementWidth, ElementHeight);
                
                var textBox2 = new TextBox();
                Controls.Add(textBox2);
                textBox2.Location = new Point(posX + 2 * ElementWidth + 30, textBoxY);
                textBox2.Size = new Size(ElementWidth, ElementHeight);
                
                button.Click += (sender, args) => { GetSolutions(textBox1, className, textBox2); };
                GetSolutions(textBox1, className, textBox2);
            }
            
            AutoSize = true;
        }

        private void GetSolutions(TextBox textBox1, string className, TextBox textBox2)
        {
            textBox1.Text = Type.GetType(className)?
                .GetMethod("Part1")?
                .Invoke(this, Array.Empty<object>())
                .ToString();
            textBox2.Text = Type.GetType(className)?
                .GetMethod("Part2")?
                .Invoke(this, Array.Empty<object>())
                .ToString();
        }
    }
}