using System;
using System.Windows.Forms;

namespace AudioTextConverter
{
    public partial class EditForm : Form
    {
        public string EditedText { get; private set; }
        public EditForm(string textToEdit)
        {
            InitializeComponent();
            richTextBox1.Text = textToEdit;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditedText = richTextBox1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            DialogResult = DialogResult.OK;
            Close();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {

        }
    }
}
