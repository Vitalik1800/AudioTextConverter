using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioTextConverter
{
    public partial class Form1 : Form
    {
        SettingsForm settingsForm;
        private AudioInput audioInput;
        private TextOutput textOutput;
        private AudioProcessor audioProcessor;
        private SpeechRecognition speechRecognition;
        private UserInterface userInterface;
        private FileHandler fileHandler;
        private string selectedLanguage = "de";
        private string selectedSaveFormat = "TXT";

        public Form1()
        {
            InitializeComponent();
            audioInput = new AudioInput();
            textOutput = new TextOutput();
            audioProcessor = new AudioProcessor();
            speechRecognition = new SpeechRecognition();
            userInterface = new UserInterface();
            fileHandler = new FileHandler();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(audioInput.AudioFile))
            {
                await ConvertAudioToTextAsync(audioInput.AudioFile);
            }
            else
            {
                userInterface.ShowOutput("Спочатку оберіть аудіо-файл для конвертації.");
            }
        }

        private async Task ConvertAudioToTextAsync(string audioFilePath)
        {
            try
            {
                if (Path.GetExtension(audioFilePath).ToLower() != ".wav")
                {
                    userInterface.ShowOutput("Для конвертації підтримуються тільки WAV-файли.");
                    return;
                }

                string transcript = await speechRecognition.RecognizeSpeechAsync(audioFilePath, GetLanguageCode(settingsForm.SelectedLanguage));
                textOutput.DisplayText(richTextBox1, Path.GetFileName(audioFilePath) + ":\n" + transcript);
                button1.Text = "Завантажити файл \r\n[Обрати файл]\r\n";
            }
            catch (Exception ex)
            {
                userInterface.ShowOutput($"Помилка конвертації: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "Аудіо файли|*.wav|Всі файли|*.*",
                Title = "Виберіть файл для конвертації"
            };

            string selectedFile = userInterface.GetUserInput(dialog);
            if (selectedFile != null)
            {
                audioInput.LoadAudioFile(selectedFile);
                userInterface.ShowOutput($"Обрано файл: {audioInput.AudioFile}");
                button1.Text = Path.GetFileName(audioInput.AudioFile);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                SaveTranscriptToFile(richTextBox1.Text);
            }
            else
            {
                userInterface.ShowOutput("Немає тексту для збереження");
            }
        }

        private void SaveTranscriptToFile(string transcript)
        {
            if (string.IsNullOrEmpty(audioInput.AudioFile))
            {
                userInterface.ShowOutput("Аудіо-файл не обрано.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = selectedSaveFormat == "TXT" ? "Text Files (*.txt)|*.txt" : "All Files (*.*)|*.*",
                Title = "Save Transcript",
                FileName = Path.GetFileNameWithoutExtension(audioInput.AudioFile) + "." + selectedSaveFormat.ToLower()
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textOutput.SaveText(transcript, saveFileDialog.FileName);
                    userInterface.ShowOutput($"Файл збережено: {saveFileDialog.FileName}");
                }
                catch (Exception ex)
                {
                    userInterface.ShowOutput($"Помилка збереження файлу: {ex.Message}");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            settingsForm = new SettingsForm();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(settingsForm.SelectedLanguage))
                {
                    selectedLanguage = settingsForm.SelectedLanguage;
                }

                if (!string.IsNullOrEmpty(settingsForm.SelectedSaveFormat))
                {
                    selectedSaveFormat = settingsForm.SelectedSaveFormat;
                }

                userInterface.ShowOutput($"Налаштування збережено:\nМова: {selectedLanguage}\nФормат збереження: {selectedSaveFormat}");
            }
        }

        private void вихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void редагуватиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditForm editForm = new EditForm(richTextBox1.Text);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = editForm.EditedText;
            }
        }

        private string GetLanguageCode(string language)
        {
            switch (language)
            {
                case "English":
                    return "en";
                case "Deutsch":
                    return "de";
                case "Français":
                    return "fr";
                case "Español":
                    return "es";
                case "Italiano":
                    return "it";
                case "Українська":
                    return "uk";
                default:
                    throw new ArgumentException("Unsupported language");
            }
        }
    }
}
