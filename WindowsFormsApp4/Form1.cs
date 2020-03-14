using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Win32;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        private ColorDialog chooseColorDialog = new ColorDialog();
        private ColorDialog chooseColorDialog1 = new ColorDialog();
        private FontDialog chooseFontStyleDialog = new FontDialog();
        public Form1()

        {
            InitializeComponent();
            buttonChooseColor.Click += new EventHandler(OnClickChooseColor);
            try
            {
                if (ReadSettings() == false)
                {
                    MessageBox.Show("В реестре нет информации...");
                }


                this.StartPosition = FormStartPosition.CenterScreen;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        void OnClickChooseColor(object Sender, EventArgs e)
        {
            if (chooseColorDialog.ShowDialog() == DialogResult.OK)
                this.BackColor = chooseColorDialog.Color;
        }

        bool ReadSettings()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey reg0 = currentUserKey.OpenSubKey(@"Software\Lesson9Task4app");
            if (reg0 != null)
            {
                
            RegistryKey softwareKey = currentUserKey.OpenSubKey(@"Software\Lesson9Task4app");
                int red = Convert.ToInt32(softwareKey.GetValue("BackGroundColor.R"));
                int green = Convert.ToInt32(softwareKey.GetValue("BackGroundColor.G"));
                int blue = Convert.ToInt32(softwareKey.GetValue("BackGroundColor.B"));

                //2. Цвет текста
                int redText = Convert.ToInt32(softwareKey.GetValue("TextColor.R"));
                int greedText = Convert.ToInt32(softwareKey.GetValue("TextColor.G"));
                int blueText = Convert.ToInt32(softwareKey.GetValue("TextColor.B"));

                string fontName = Convert.ToString(softwareKey.GetValue("TextStyleFont.Name"));
                float textSize = Convert.ToSingle(softwareKey.GetValue("TextStyleFont.Size"));

                FontStyle fontstyle = GetStyleType();

                FontStyle GetStyleType()
                {
                    if (Convert.ToString(softwareKey.GetValue("TextStyleFont.Style")) == "Bold")
                    {
                        return FontStyle.Bold;
                    }
                    else if (Convert.ToString(softwareKey.GetValue("TextStyleFont.Style")) == "Italic")
                    {
                        return FontStyle.Italic;
                    }
                    else if (Convert.ToString(softwareKey.GetValue("TextStyleFont.Style")) == "Regular")
                    {
                        return FontStyle.Regular;
                    }
                    else if (Convert.ToString(softwareKey.GetValue("TextStyleFont.Style")) == "Strikeout")
                    {
                        return FontStyle.Strikeout;
                    }
                    else
                    {
                        return FontStyle.Regular;
                    }


                }
                this.BackColor = Color.FromArgb(red, green, blue);
                label1.ForeColor = Color.FromArgb(redText, greedText, blueText);
                label1.Font = new Font(fontName, textSize, fontstyle);
            }
            reg0.Close();
            return (true);
        }

            void SaveSettings()
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey softwareKey = currentUserKey.OpenSubKey(@"Software", true);
            RegistryKey appkey = softwareKey.CreateSubKey("Lesson9Task4app");
            appkey.SetValue("BackGroundColor.R", BackColor.R.ToString());
            appkey.SetValue("BackGroundColor.G", BackColor.G.ToString());
            appkey.SetValue("BackGroundColor.B", BackColor.B.ToString());
            appkey.SetValue("TextColor.R", label1.ForeColor.R.ToString());
            appkey.SetValue("TextColor.G", label1.ForeColor.G.ToString());
            appkey.SetValue("TextColor.B", label1.ForeColor.B.ToString());
            appkey.SetValue("TextStyleFont.Name", label1.Font.Name.ToString());
            appkey.SetValue("TextStyleFont.Size", label1.Font.Size.ToString());
            appkey.SetValue("TextStyleFont.Style", label1.Font.Style.ToString());
            appkey.Close();
        }

            private void button2chooseTextColor_Click(object sender, EventArgs e)
        {
            if (chooseColorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // установка цвета формы
            label1.ForeColor = chooseColorDialog1.Color;
        }

        private void buttonFontParams_Click(object sender, EventArgs e)
        {
            if (chooseFontStyleDialog.ShowDialog() == DialogResult.Cancel)
                return;
            // установка шрифта
            label1.Font = chooseFontStyleDialog.Font;

            //MessageBox.Show(label1.Font.Size);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
