using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriangleAreaCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Настройка формы
            this.Text = "Калькулятор площади треугольника";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 245, 255);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Заголовок
            var titleLabel = new Label
            {
                Text = "Калькулятор площади треугольника",
                Font = new Font("Times New Roman", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 40, 85),
                Location = new Point(80, 20),
                Size = new Size(350, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // TabControl для разных методов расчета
            var tabControl = new TabControl
            {
                Location = new Point(30, 70),
                Size = new Size(440, 250),
                Name = "mainTabControl"
            };

            // Вкладка 1: По основанию и высоте
            var tabPage1 = new TabPage("По основанию и высоте");
            tabPage1.BackColor = Color.White;
            InitializeBaseHeightTab(tabPage1);
            tabControl.TabPages.Add(tabPage1);

            // Вкладка 2: По трем сторонам (формула Герона)
            var tabPage2 = new TabPage("По трем сторонам");
            tabPage2.BackColor = Color.White;
            InitializeThreeSidesTab(tabPage2);
            tabControl.TabPages.Add(tabPage2);

            // Поле результата
            var resultLabel = new Label
            {
                Text = "Результат:",
                Font = new Font("Times New Roman", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 40, 85),
                Location = new Point(30, 330),
                Size = new Size(100, 30)
            };

            var resultTextBox = new TextBox
            {
                Location = new Point(140, 330),
                Size = new Size(200, 30),
                Font = new Font("Times New Roman", 11),
                ReadOnly = true,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(0, 80, 0),
                Name = "resultText"
            };

            // Кнопка расчета
            var calculateButton = new Button
            {
                Text = "Вычислить площадь",
                Location = new Point(350, 330),
                Size = new Size(120, 35),
                Font = new Font("Times New Roman", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Name = "calculateButton"
            };
            calculateButton.FlatAppearance.BorderSize = 0;

            // Кнопка очистки
            var clearButton = new Button
            {
                Text = "Очистить",
                Location = new Point(350, 370),
                Size = new Size(120, 35),
                Font = new Font("Times New Roman", 10),
                BackColor = Color.FromArgb(230, 230, 230),
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Name = "clearButton"
            };
            clearButton.FlatAppearance.BorderSize = 0;

            // Обработчики событий
            calculateButton.Click += (sender, e) =>
            {
                CalculateArea(resultTextBox, tabControl);
            };

            clearButton.Click += (sender, e) =>
            {
                ClearAllFields(tabControl, resultTextBox);
            };

            // Добавление элементов на форму
            this.Controls.Add(titleLabel);
            this.Controls.Add(tabControl);
            this.Controls.Add(resultLabel);
            this.Controls.Add(resultTextBox);
            this.Controls.Add(calculateButton);
            this.Controls.Add(clearButton);
        }

        private void InitializeBaseHeightTab(TabPage tabPage)
        {
            var baseLabel = new Label
            {
                Text = "Основание (a):",
                Location = new Point(50, 50),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10)
            };

            var baseTextBox = new TextBox
            {
                Location = new Point(200, 50),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10),
                Name = "baseText"
            };

            var heightLabel = new Label
            {
                Text = "Высота (h):",
                Location = new Point(50, 100),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10)
            };

            var heightTextBox = new TextBox
            {
                Location = new Point(200, 100),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10),
                Name = "heightText"
            };

            // Изображение формулы
            var formulaLabel = new Label
            {
                Text = "Формула: S = ½ × a × h",
                Location = new Point(50, 150),
                Size = new Size(300, 60),
                Font = new Font("Times New Roman", 12, FontStyle.Italic),
                ForeColor = Color.FromArgb(0, 80, 160)
            };

            tabPage.Controls.Add(baseLabel);
            tabPage.Controls.Add(baseTextBox);
            tabPage.Controls.Add(heightLabel);
            tabPage.Controls.Add(heightTextBox);
            tabPage.Controls.Add(formulaLabel);
        }

        private void InitializeThreeSidesTab(TabPage tabPage)
        {
            var sideALabel = new Label
            {
                Text = "Сторона a:",
                Location = new Point(50, 50),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10)
            };

            var sideATextBox = new TextBox
            {
                Location = new Point(200, 50),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10),
                Name = "sideAText"
            };

            var sideBLabel = new Label
            {
                Text = "Сторона b:",
                Location = new Point(50, 100),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10)
            };

            var sideBTextBox = new TextBox
            {
                Location = new Point(200, 100),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10),
                Name = "sideBText"
            };

            var sideCLabel = new Label
            {
                Text = "Сторона c:",
                Location = new Point(50, 150),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10)
            };

            var sideCTextBox = new TextBox
            {
                Location = new Point(200, 150),
                Size = new Size(150, 25),
                Font = new Font("Times New Roman", 10),
                Name = "sideCText"
            };

            // Формула Герона
            var formulaLabel = new Label
            {
                Text = "Формула Герона:\nS = √[p(p-a)(p-b)(p-c)]\nгде p = (a+b+c)/2",
                Location = new Point(50, 180),
                Size = new Size(300, 80),
                Font = new Font("Times New Roman", 11, FontStyle.Italic),
                ForeColor = Color.FromArgb(0, 80, 160)
            };

            tabPage.Controls.Add(sideALabel);
            tabPage.Controls.Add(sideATextBox);
            tabPage.Controls.Add(sideBLabel);
            tabPage.Controls.Add(sideBTextBox);
            tabPage.Controls.Add(sideCLabel);
            tabPage.Controls.Add(sideCTextBox);
            tabPage.Controls.Add(formulaLabel);
        }

        private void CalculateArea(TextBox resultTextBox, TabControl tabControl)
        {
            try
            {
                double area = 0;

                if (tabControl.SelectedIndex == 0) // По основанию и высоте
                {
                    var baseText = tabControl.SelectedTab.Controls.Find("baseText", true)[0] as TextBox;
                    var heightText = tabControl.SelectedTab.Controls.Find("heightText", true)[0] as TextBox;

                    if (string.IsNullOrWhiteSpace(baseText.Text) || string.IsNullOrWhiteSpace(heightText.Text))
                    {
                        throw new ArgumentException("Заполните все поля");
                    }

                    double a = Convert.ToDouble(baseText.Text);
                    double h = Convert.ToDouble(heightText.Text);

                    if (a <= 0 || h <= 0)
                    {
                        throw new ArgumentException("Значения должны быть положительными");
                    }

                    area = 0.5 * a * h;
                }
                else if (tabControl.SelectedIndex == 1) // По трем сторонам
                {
                    var sideAText = tabControl.SelectedTab.Controls.Find("sideAText", true)[0] as TextBox;
                    var sideBText = tabControl.SelectedTab.Controls.Find("sideBText", true)[0] as TextBox;
                    var sideCText = tabControl.SelectedTab.Controls.Find("sideCText", true)[0] as TextBox;

                    if (string.IsNullOrWhiteSpace(sideAText.Text) || string.IsNullOrWhiteSpace(sideBText.Text) ||
                        string.IsNullOrWhiteSpace(sideCText.Text))
                    {
                        throw new ArgumentException("Заполните все поля");
                    }

                    double a = Convert.ToDouble(sideAText.Text);
                    double b = Convert.ToDouble(sideBText.Text);
                    double c = Convert.ToDouble(sideCText.Text);

                    if (a <= 0 || b <= 0 || c <= 0)
                    {
                        throw new ArgumentException("Значения должны быть положительными");
                    }

                    // Проверка на существование треугольника
                    if (a + b <= c || a + c <= b || b + c <= a)
                    {
                        throw new ArgumentException("Треугольник с такими сторонами не существует");
                    }

                    double p = (a + b + c) / 2;
                    area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
                }

                resultTextBox.Text = $"Площадь треугольника: {area:F4} кв.ед.";
            }
            catch (FormatException)
            {
                MessageBox.Show("Пожалуйста, введите корректные числовые значения",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearAllFields(TabControl tabControl, TextBox resultTextBox)
        {
            // Очистка всех текстовых полей
            foreach (TabPage tabPage in tabControl.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is TextBox textBox)
                    {
                        textBox.Clear();
                    }
                }
            }

            // Очистка результата
            resultTextBox.Clear();
        }
    }
}