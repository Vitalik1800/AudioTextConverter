using System.Windows.Forms;

public class UserInterface
{
    public string GetUserInput(OpenFileDialog dialog)
    {
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.FileName;
        }
        return null;
    }

    public void ShowOutput(string message)
    {
        MessageBox.Show(message);
    }
}
