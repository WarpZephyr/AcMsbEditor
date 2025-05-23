﻿using System.Windows.Forms;

namespace ACMsbEditor.Windows.Forms.Dialogs
{
    internal static class SimpleFormDialog
    {
        /// <summary>
        /// Shows an information dialog.
        /// </summary>
        /// <param name="message">The message to show in the dialog.</param>
        public static void ShowInformationDialog(string? message = null, string? caption = null)
            => MessageBox.Show(message ?? "There is something you should know.", caption ?? "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        /// <summary>
        /// Shows an error dialog.
        /// </summary>
        /// <param name="message">The message to show in the dialog.</param>
        /// <param name="caption">The caption to give this dialog.</param>
        public static void ShowErrorDialog(string? message = null, string? caption = null)
            => MessageBox.Show(message ?? "An error has occurred.", caption ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        /// <summary>
        /// Shows a question dialog.
        /// </summary>
        /// <param name="message">The message to show in the dialog.</param>
        /// <param name="caption">The caption to give this dialog.</param>
        /// <returns>A bool representing whether or not the user pressed yes.</returns>
        public static bool ShowQuestionDialog(string? message = null, string? caption = null)
            => MessageBox.Show(message ?? "Are you sure you want to do this?", caption ?? @"Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        /// <summary>
        /// Shows an input dialog.
        /// </summary>
        /// <param name="message">The message to show in the dialog.</param>
        /// <param name="caption">The caption to give this dialog.</param>
        /// <returns>What the user inputted or an empty string if cancelled.</returns>
        public static string? ShowInputDialog(string? message = null, string? caption = null)
        {
            var prompt = new Form
            {
                Width = 240,
                Height = 125,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption ?? "Input Dialog",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };

            var textLabel = new Label { Left = 8, Top = 8, Width = 200, Text = message ?? "Input something." };
            var textBox = new TextBox { Left = 10, Top = 28, Width = 200 };

            var cancelButton = new Button { Text = @"Cancel", Left = 9, Width = 100, Top = 55, DialogResult = DialogResult.Cancel };
            cancelButton.Click += (sender, e) => { prompt.Close(); };
            var confirmation = new Button { Text = @"OK", Left = 112, Width = 100, Top = 55, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(textBox);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

        /// <summary>
        /// Shows a numeric input dialog.
        /// </summary>
        /// <param name="message">The message to show in the dialog.</param>
        /// <param name="caption">The caption to give this dialog.</param>
        /// <returns>What the user inputted or an empty string if cancelled.</returns>
        public static decimal ShowNumericInputDialog(decimal defaultValue, string? message = null, string? caption = null)
        {
            var prompt = new Form
            {
                Width = 240,
                Height = 125,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption ?? "Input Dialog",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };

            var textLabel = new Label { Left = 8, Top = 8, Width = 200, Text = message ?? "Input something." };
            var numericInput = new NumericUpDown { Left = 10, Top = 28, Width = 200, Minimum = 0, Maximum = int.MaxValue, Value = defaultValue };

            var cancelButton = new Button { Text = @"Cancel", Left = 9, Width = 100, Top = 55, DialogResult = DialogResult.Cancel };
            cancelButton.Click += (sender, e) => { prompt.Close(); };
            var confirmation = new Button { Text = @"OK", Left = 112, Width = 100, Top = 55, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(numericInput);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? numericInput.Value : defaultValue;
        }

        /// <summary>
        /// Shows a ComboBox dialog.
        /// </summary>
        /// <param name="text">The text to show in the dialog.</param>
        /// <param name="title">The title to give this dialog.</param>
        /// <param name="options">The options to give the user.</param>
        /// <param name="style">The dropdown style of the combobox.</param>
        /// <returns>What option the user choose or an empty string if cancelled.</returns>
        public static string? ShowComboBoxDialog(string? text = null, string? title = null, string[]? options = null, ComboBoxStyle style = ComboBoxStyle.DropDownList)
        {
            var prompt = new Form
            {
                Width = 240,
                Height = 125,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = title ?? "ComboBox Dialog",
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false
            };

            var textLabel = new Label { Left = 8, Top = 8, Width = 200, Text = text ?? "Select something." };
            var combobox = new ComboBox { Left = 10, Top = 28, Width = 200 };
            combobox.Items.AddRange(options ?? []);
            combobox.DropDownStyle = style;
            combobox.SelectedIndex = 0;

            var cancelButton = new Button { Text = @"Cancel", Left = 9, Width = 100, Top = 55, DialogResult = DialogResult.Cancel };
            cancelButton.Click += (sender, e) => { prompt.Close(); };
            var confirmation = new Button { Text = @"OK", Left = 112, Width = 100, Top = 55, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(combobox);
            prompt.Controls.Add(cancelButton);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? combobox.Text : null;
        }
    }
}
