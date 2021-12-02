using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsSystem
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            this.notifyIcon1.ContextMenuStrip.Items.Add("Close", null, this.MenuCloseClick);
            this.notifyIcon1.ContextMenuStrip.Items.Add("Show", null, this.MenuShowClick);
        }

       

        private void TextBoxWhere_Click(object sender, EventArgs e)
        {
            this.TextBoxWhere.Text = "";
        }

        private void TextBoxPattern_Click(object sender, EventArgs e)
        {
            this.TextBoxPattern.Text = "";
        }

        private void BtnWhere_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    this.TextBoxWhere.Text = Path.GetDirectoryName(file.FileName);
                }

            }
        }

        private void btnPattern_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();


            if (this.TextBoxPattern.Text.Length > 0 && this.TextBoxWhere.Text.Length > 0)
            {
                if (this.checkedListBox1.CheckedItems.Count > 0)
                {

                    foreach (var item in this.checkedListBox1.CheckedItems)
                    {
                        foreach (var dir in Directory.GetFiles(this.TextBoxWhere.Text))
                        {
                            if (item.ToString().ToLower().Contains("name"))
                            {

                                if (Path.GetFileName(dir).Contains(this.TextBoxPattern.Text))
                                {
                                    this.listBox1.Items.Add(Path.GetFileName(dir));
                                }
                            }
                            if (item.ToString().ToLower().Contains("date"))
                            {

                                if (File.GetCreationTime(dir).Date.ToShortDateString() == this.dateTimePicker1.Value.Date.ToShortDateString())
                                {
                                    this.listBox1.Items.Add(Path.GetFileName(dir));
                                }
                            }
                            if (item.ToString().ToLower().Contains("ext"))
                            {

                                if (Path.GetExtension(dir).Contains(this.TextBoxPattern.Text))
                                {
                                    this.listBox1.Items.Add(Path.GetFileName(dir));
                                }
                            }
                            if (item.ToString().ToLower().Contains("open"))
                            {

                                if (File.GetLastAccessTime(dir).Date.ToShortDateString() == this.dateTimePicker1.Value.Date.ToShortDateString())
                                {
                                    this.listBox1.Items.Add(Path.GetFileName(dir));
                                }
                            }
                            if (item.ToString().ToLower().Contains("write"))
                            {

                                if (File.GetLastWriteTime(dir).ToShortDateString() == this.dateTimePicker1.Value.Date.ToShortDateString())
                                {
                                    this.listBox1.Items.Add(Path.GetFileName(dir));
                                }
                            }
                        }
                        GC.Collect();

                    }

                }
                else
                {
                    foreach (var item in Directory.GetFiles(this.TextBoxWhere.Text))
                    {
                        if (Path.GetFileName(item).Contains(this.TextBoxPattern.Text))
                        {
                            this.listBox1.Items.Add(Path.GetFileName(item));
                        }
                        if (this.listBox1.Items.Count <= 0)
                        {
                            MessageBox.Show("No one found");
                        }
                        GC.Collect();
                    }
                }

            }
            else
            {
                MessageBox.Show("Fill all text");
            }
        }

        private void checkedListBox1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.Items[i].ToString().ToLower().Contains("in date") || checkedListBox1.Items[i].ToString().ToLower().Contains("in last"))
                {
                    this.dateTimePicker1.Visible = true;
                }
            }
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.Items[i].ToString().ToLower().Contains("in date") || checkedListBox1.Items[i].ToString().ToLower().Contains("in last"))
                {
                    this.dateTimePicker1.Visible = true;
                    this.TextBoxPattern.Text = "If you need->enter pattern";
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            this.TextBoxPattern.Text = this.dateTimePicker1.Value.Date.ToShortDateString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
            }
        }
      
        private void MenuShowClick(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        private void MenuCloseClick(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
