using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
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
            
            var buttonAll = new Button();
            Controls.Add(buttonAll);
            buttonAll.Text = @"Solve All";
            buttonAll.Location = new Point(26 / AmountPerCol * 3 * (ElementWidth + 20),
                10 + 26 % AmountPerCol * ElementHeight + 5);
            buttonAll.Size = new Size(ElementWidth, ElementHeight);
            
            var buttonClearAll = new Button();
            Controls.Add(buttonClearAll);
            buttonClearAll.Text = @"Clear All";
            buttonClearAll.Location = new Point(26 / AmountPerCol * 4 * (ElementWidth + 3),
                10 + 26 % AmountPerCol * ElementHeight + 5);
            buttonClearAll.Size = new Size(ElementWidth, ElementHeight);
            
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
                

                async void OnButtonAllOnClick(object sender, EventArgs args)
                {
                    var (ans1, ans2) = await Task.Run(() => GetSolutions(className));
                    textBox1.Text = ans1;
                    textBox2.Text = ans2;
                }

                button.Click += OnButtonAllOnClick;
                buttonAll.Click += OnButtonAllOnClick;
                buttonClearAll.Click += (sender, args) =>
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                };
            }
            
            AutoSize = true;
        }

        private (string, string) GetSolutions(string className)
        {
            Type.GetType(className)?
                .GetConstructor(BindingFlags.Static | BindingFlags.NonPublic, null, Type.EmptyTypes, null)
                ?.Invoke(null, null);
            
            var ans1 = Type.GetType(className)?
                .GetMethod("Part1")?
                .Invoke(this, Array.Empty<object>()).ToString();

            
            var ans2 = Type.GetType(className)?
                .GetMethod("Part2")?
                .Invoke(this, Array.Empty<object>()).ToString();


            return (ans1, ans2);
        }
    }
}