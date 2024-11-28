using AirPort_PRO_NuGet_Logger.Contracts.Models;
using DataGridAirPort;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace AirPort_PRO_NuGet_Logger
{
    /// <summary>
    /// Представляет форму для обработки данных о самолетах. 
    /// Этот класс создает либо новый объект <see cref="Plane"/>, либо инициализирует существующий, 
    /// настраивает элементы управления формы и устанавливает привязки данных между 
    /// элементами управления и свойствами объекта <see cref="Plane"/>.
    /// </summary>
    /// <param name="plane">
    /// Объект <see cref="Plane"/>, который будет инициализирован. 
    /// Если параметр равен <c>null</c>, создается новый экземпляр <see cref="Plane"/> с 
    /// установленными значениями по умолчанию.
    /// </param>
    public partial class AirPortForm : Form
    {
        private Plane plane;

        /// <summary>
        /// Отвечает за создание или инициализацию формы, которая обрабатывает данные о самолетах. Он создает либо новый объект Plane, либо инициализирует существующий объект, настраивает элементы управления на форме с возможностью выбора значений из перечисления, а также устанавливает привязки данных между элементами управления и свойствами объекта Plane
        /// </summary>
        /// <param name="plane"></param>
        public AirPortForm(Plane plane = null)
        {
            this.plane = plane == null
                ? DataGenerator.CreatePlane(x =>
                {
                    x.Id_Flight = Guid.NewGuid();
                    x.Type = Type_cs.Boeing;
                })
                : new Plane
                {
                    Id_Flight = plane.Id_Flight,
                    Number_Flight = plane.Number_Flight,
                    Number_passenger = plane.Number_passenger,
                    Passenger_fee = plane.Passenger_fee,
                    Number_crew = plane.Number_crew,
                    Crew_fee = plane.Crew_fee,
                    DTPArrival = plane.DTPArrival,
                    Present_ = plane.Present_,

                };

            InitializeComponent();

            foreach (var item in Enum.GetValues(typeof(Type_cs)))
            {
                comboBoxType.Items.Add(item);
            }
            if (comboBoxType.Items.Count > 0)
            {
                comboBoxType.SelectedIndex = 0;
            }

            comboBoxType.AddBinding(x => x.SelectedItem, this.plane, x => x.Type);
            textBoxNumber_Flight.AddBinding(x => x.Text, this.plane, x => x.Number_Flight, errorProvider1);
            textBoxNumber_passenger.AddBinding(x => x.Text, this.plane, x => x.Number_passenger, errorProvider1);
            textBoxPassenger_fee.AddBinding(x => x.Text, this.plane, x => x.Passenger_fee, errorProvider1);
            textBoxNumber_crew.AddBinding(x => x.Text, this.plane, x => x.Number_crew, errorProvider1);
            textBoxCrew_fee.AddBinding(x => x.Text, this.plane, x => x.Crew_fee, errorProvider1);
            textBoxPercent.AddBinding(x => x.Text, this.plane, x => x.Present_, errorProvider1);
            dateTimePickerArrival.AddBinding(x => x.Value, this.plane, x => x.DTPArrival, errorProvider1);

        }

        /// <summary>
        /// Получает объект <see cref="Plane"/>, ассоциированный с текущим экземпляром.
        /// </summary>
        /// <remarks>
        /// Это свойство предоставляет доступ к внутреннему полю <c>plane</c>, 
        /// позволяя извлекать данные о самолете из экземпляра формы.
        /// </remarks>
        public Plane Plane => plane;

        private void comboBoxType_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.Graphics.FillEllipse(Brushes.Red,
                new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Height - 4, e.Bounds.Height - 4));
            if (e.Index > -1)
            {
                var value = (Type_cs)(sender as ComboBox).Items[e.Index];
                e.Graphics.DrawString(GetDisplayValue(value),
                    e.Font,
                    new SolidBrush(e.ForeColor),
                    e.Bounds.X + 20,
                    e.Bounds.Y);
            }
        }
        private string GetDisplayValue(Type_cs value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes<DescriptionAttribute>(false);
            return attributes.FirstOrDefault()?.Description ?? "ММ";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Silver, 0, 0, Width, 0);
        }

        private bool AreAllFieldsFilled()
        {
            return !string.IsNullOrEmpty(comboBoxType.Text) &&
                   !string.IsNullOrEmpty(textBoxNumber_Flight.Text) &&
                   !string.IsNullOrEmpty(textBoxNumber_passenger.Text) &&
                   !string.IsNullOrEmpty(textBoxPassenger_fee.Text) &&
                   !string.IsNullOrEmpty(textBoxNumber_crew.Text) &&
                   !string.IsNullOrEmpty(textBoxCrew_fee.Text) &&
                   !string.IsNullOrEmpty(dateTimePickerArrival.Text) &&
                   !string.IsNullOrEmpty(textBoxPercent.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AreAllFieldsFilled())
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    MessageBox.Show("Пожалуйста, выберите тип самолета из списка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}