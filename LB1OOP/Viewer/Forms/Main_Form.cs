using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LB1OOP.Controller;
using LB1OOP.Viewer.Interfaces;

namespace LB1OOP
{
    public partial class Main_Form : Form, IMainForm
    {
        private readonly MainController _controller;

        public int UserCount
        {
            get => int.TryParse(userCountTextBox.Text, out int result) ? result : 0;
            set => userCountTextBox.Text = value.ToString();
        }

        public float SpeedLimit
        {
            get => float.TryParse(speedLimitTextBox.Text, out float result) ? result : 0;
            set => speedLimitTextBox.Text = value == 0 ? "Не задан" : value.ToString();
        }

        public float Area
        {
            get => float.TryParse(areaTextBox.Text, out float result) ? result : 0;
            set => areaTextBox.Text = value.ToString();
        }

        public int ContractNumber
        {
            get => int.TryParse(contractNumberTextBox.Text, out int result) ? result : 0;
            set => contractNumberTextBox.Text = value.ToString();
        }

        public string ProviderName
        {
            get => NameTextBox.Text;
            set => NameTextBox.Text = value;
        }

        public string TarifName
        {
            get => tarifNameTextBox.Text;
            set => tarifNameTextBox.Text = value;
        }

        public float TarifCoast
        {
            get => float.TryParse(tarifCoastTextBox.Text, out float result) ? result : 0;
            set => tarifCoastTextBox.Text = value.ToString();
        }

        public string UserCountHex
        {
            get => UserCountHextextBox.Text;
            set => UserCountHextextBox.Text = value;
        }

        public IEnumerable<string> ProviderNames
        {
            set
            {
                listBoxProviders.Items.Clear();
                if (value != null)
                {
                    foreach (var name in value)
                    {
                        listBoxProviders.Items.Add(name);
                    }
                }
            }
        }

        public IEnumerable<string> EventLogs
        {
            set
            {
                listBoxEvents.Items.Clear();
                if (value != null)
                {
                    foreach (var log in value)
                    {
                        listBoxEvents.Items.Add(log);
                    }
                }
            }
        }

        public string SelectedProviderName
        {
            get => listBoxProviders.SelectedItem?.ToString();
            set
            {
                if (value != null && listBoxProviders.Items.Contains(value))
                {
                    listBoxProviders.SelectedItem = value;
                }
            }
        }

        public event EventHandler CreateProviderRequested;
        public event EventHandler EditProviderRequested;
        public event EventHandler RemoveProviderRequested;
        public event EventHandler DensityRequested;
        public event EventHandler TestingRequested;
        public event EventHandler<ProviderSelectedEventArgs> ProviderSelected;

        public Main_Form(MainController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.SetView(this);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            add_button.Click += (s, e) => CreateProviderRequested?.Invoke(s, e);
            change_button.Click += (s, e) => EditProviderRequested?.Invoke(s, e);
            remove_button.Click += (s, e) => RemoveProviderRequested?.Invoke(s, e);
            btnTestException.Click += (s, e) => DensityRequested?.Invoke(s, e);
            test_button.Click += (s, e) => TestingRequested?.Invoke(s, e);
            listBoxProviders.SelectedIndexChanged += (s, e) =>
            {
                if (listBoxProviders.SelectedItem != null)
                {
                    ProviderSelected?.Invoke(this, new ProviderSelectedEventArgs(listBoxProviders.SelectedItem.ToString()));
                }
            };
        }

        public void RefreshDisplay()
        {
            Refresh();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(this, message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(this, message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CloseForm()
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Application.Exit();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
    }
}