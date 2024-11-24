using System.IO;
using System.Windows.Forms;

public class TextOutput
{
    public string TextFile { get; private set; }

    public void SaveText(string text, string filePath)
    {
        File.WriteAllText(filePath, text);
    }

    public void DisplayText(RichTextBox richTextBox, string text)
    {
        richTextBox.Text = text;
    }
}
