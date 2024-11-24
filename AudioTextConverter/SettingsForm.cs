using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AudioTextConverter
{
    public partial class SettingsForm : Form
    {
        public string SelectedLanguage { get; private set; }
        public string SelectedSaveFormat { get; private set; }

        public SettingsForm()
        {
            InitializeComponent();
            PopulateLanguages();
            PopulateSaveFormats();
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
        }

        private void PopulateLanguages()
        {
            List<string> languages = new List<string>
            {
                "Виберіть мову",
                "English",
                "Deutsch",
                "Français",
                "Español",
                "Italiano",
                "Українська"
            };

            comboBox1.Items.AddRange(languages.ToArray());
            comboBox1.SelectedIndex = 1; // Встановлюємо перший елемент як вибраний за замовчуванням
        }

        private void PopulateSaveFormats()
        {
            List<string> saveFormats = new List<string>
            {
                "Виберіть формат збереження",
                "TXT",
                "DOCX",
                "PDF"
            };

            comboBox2.Items.AddRange(saveFormats.ToArray());
            comboBox2.SelectedIndex = 1; // Встановлюємо перший елемент як вибраний за замовчуванням
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;

            if (selectedIndex == 0)
            {
                MessageBox.Show("Будь ласка, виберіть мову зі списку.");
            }
            else
            {
                SelectedLanguage = comboBox1.SelectedItem.ToString();
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox2.SelectedIndex;

            if (selectedIndex == 0)
            {
                MessageBox.Show("Будь ласка, виберіть формат збереження зі списку.");
            }
            else
            {
                SelectedSaveFormat = comboBox2.SelectedItem.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0 || comboBox2.SelectedIndex == 0)
            {
                MessageBox.Show("Будь ласка, виберіть мову та формат збереження.");
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
