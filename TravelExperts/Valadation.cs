using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelExperts
{
    // Created by Garrett Chepil
    // a class top valadate the data being entered into the forms
    public static class Valadation
    {
        // check if the string is empty
        public static bool IsPresent(this TextBox textbox)
        {

            if (textbox.Text == "")
            {
                MessageBox.Show(textbox.Tag + " is a required field.", "Entry Error");
                textbox.Focus();
                return false;
            }
            return true;
        }

        public static bool IsPresent(this ComboBox comboBox)
        {
            if (comboBox.SelectedIndex == -1)
            {
                MessageBox.Show(comboBox.Tag + " is a required field.", "Entry Error");
                comboBox.Focus();
                return false;
            }
            return true;
        }

        // check if the entry is decimal
        public static bool IsDecimal(this TextBox textbox)
        {
            try
            {
                Convert.ToDecimal(textbox.Text);
                return true;

            }
            catch (FormatException)
            {
                MessageBox.Show(textbox.Tag + " must be a decimal number.", "Entry Error");
                textbox.Focus();
                return false;
            }
        }

        // checks if the entry is an int
        public static bool IsInt(this TextBox textbox)
        {
            try
            {
                Convert.ToInt32(textbox.Text);
                return true;

            }
            catch (FormatException)
            {
                MessageBox.Show(textbox.Tag + " must be an integer.", "Entry Error");
                textbox.Focus();
                return false;
            }
        }

        // checks if the entry is an Double
        public static bool IsDouble(this TextBox textbox)
        {
            try
            {
                Convert.ToDouble(textbox.Text);
                return true;

            }
            catch (FormatException)
            {
                MessageBox.Show(textbox.Tag + " must be a decimal number.", "Entry Error");
                textbox.Focus();
                return false;
            }
        }
    }
}
