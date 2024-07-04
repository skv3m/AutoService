using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoService
{
    public partial class MainForm : Form
    {
        private List<ServiceRequest> serviceRequests = new List<ServiceRequest>();

        public MainForm()
        {
            InitializeComponent();
        }

        private bool ValidateFields()
        {
            if (string.IsNullOrWhiteSpace(txtOwnerName.Text))
            {
                MessageBox.Show("Введите ФИО владельца автомобиля.", "Недостающие данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCarModel.Text))
            {
                MessageBox.Show("Введите марку автомобиля.", "Недостающие данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtServiceType.Text))
            {
                MessageBox.Show("Введите вид работы.", "Недостающие данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtServiceCost.Text))
            {
                MessageBox.Show("Введите стоимость работы.", "Недостающие данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Дополнительная проверка на валидность стоимости
            if (!decimal.TryParse(txtServiceCost.Text, out _))
            {
                MessageBox.Show("Некорректно введена стоимость работы. Введите числовое значение.", "Недопустимое значение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void btnAddRequest_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            // Создание заявки и добавление в список
            var request = new ServiceRequest
            {
                OwnerName = txtOwnerName.Text,
                CarModel = txtCarModel.Text,
                ServiceType = txtServiceType.Text,
                ServiceDate = dtpServiceDate.Value,
                ServiceCost = decimal.Parse(txtServiceCost.Text)
            };

            serviceRequests.Add(request);
            lstRequests.Items.Add(request);
            MessageBox.Show("Заявка добавлена!");
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            if (lstRequests.SelectedItem is ServiceRequest selectedRequest)
            {
                var receipt = new Receipt
                {
                    OwnerName = selectedRequest.OwnerName,
                    CarModel = selectedRequest.CarModel,
                    ServiceType = selectedRequest.ServiceType,
                    ServiceDate = selectedRequest.ServiceDate,
                    ServiceCost = selectedRequest.ServiceCost
                };

                var receiptForm = new ReceiptForm(receipt);
                receiptForm.Show();
            }
            else
            {
                MessageBox.Show("Выберите заявку для печати квитанции.", "Не выбрана заявка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAddRequest_Click_1(object sender, EventArgs e)
        {
            var serviceRequest = new ServiceRequest
            {
                OwnerName = txtOwnerName.Text,
                CarModel = txtCarModel.Text,
                ServiceType = txtServiceType.Text,
                ServiceDate = dtpServiceDate.Value,
                ServiceCost = decimal.Parse(txtServiceCost.Text)
            };

            serviceRequests.Add(serviceRequest);
            MessageBox.Show("Заявка добавлена!");
            lstRequests.Items.Add(serviceRequest);
        }
    }

    public class ServiceRequest
    {
        public string OwnerName { get; set; }
        public string CarModel { get; set; }
        public string ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal ServiceCost { get; set; }

        public override string ToString()
        {
            return $"{OwnerName} - {CarModel} - {ServiceType}";
        }
    }

    public class Receipt
    {
        public string OwnerName { get; set; }
        public string CarModel { get; set; }
        public string ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public decimal ServiceCost { get; set; }
    }
}
