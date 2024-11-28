using AirPort_PRO_NuGet_Logger.Contracts;
using AirPort_PRO_NuGet_Logger.Contracts.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirPort_PRO_NuGet_Logger
{
    public partial class Form1 : Form
    {
        private IAirPort planeManager;
        private BindingSource bindingSource;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="planeManager"></param>
        public Form1(IAirPort planeManager)
        {
            this.planeManager = planeManager;
            bindingSource = new BindingSource();

            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingSource;
        }

        /// <summary>
        /// Метод SetStats асинхронно получает статистику и обновляет элементы интерфейса на основании полученных данных
        /// </summary>
        /// <returns></returns>
        public async Task SetStats()
        {
            var stats = await planeManager.GetStatsAsync();

            toolStripLabel1.Text = $"Всего рейсов: {stats.Count}";
            toolStripLabel2.Text = $"Всего пассажиров: {stats.Total_passengers}";
            toolStripLabel3.Text = $"Всего экипажа: {stats.Entire_crew}";
            toolStripLabel4.Text = $"Всего монет: {stats.Total_coins}";

        }

        private async void toolStripAdd_Click(object sender, EventArgs e)
        {
            var airPortForm = new AirPortForm();

            if (airPortForm.ShowDialog(this) == DialogResult.OK)
            {
                await planeManager.AddAsync(airPortForm.Plane);
                bindingSource.ResetBindings(false);
                await SetStats();
            }
        }

        private async void toolStripEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var data = (Plane)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;
                var airPortForm = new AirPortForm(data);

                if (airPortForm.ShowDialog(this) == DialogResult.OK)
                {
                    await planeManager.EditAsync(airPortForm.Plane);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }
        }

        private async void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var data = (Plane)dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].DataBoundItem;

                if (MessageBox.Show($"Вы действительно хотите удалить {data.Type}?", "Удаление записи", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    await planeManager.DeleteAsync(data.Id_Flight);
                    bindingSource.ResetBindings(false);
                    await SetStats();
                }
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            bindingSource.DataSource = await planeManager.GetAllAsync();
            await SetStats();
        }
    }
}