﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteDock
{
	public partial class NoteDock : Form
	{

		string path = @"";
		string history = "";
		public NoteDock()
		{
			InitializeComponent();
		}
		private void newToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 50;
			SaveFileDialog save = new SaveFileDialog();
			if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = save.FileName;
				StreamWriter write = new StreamWriter(File.Create(path));
				Note.Text = "";
				write.Close();
				loading.Value = 100;
				FilePath.Text = path;
				history = Note.Text;
			}
			loading.Value = 0;
		}

		private void openToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 50;
			OpenFileDialog open = new OpenFileDialog();
			if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = open.FileName;
				StreamReader reader = new StreamReader(path);
				string data = reader.ReadToEnd();
				Note.Text = data;
				reader.Close();
				loading.Value = 100;
				FilePath.Text = path;
				history = Note.Text;
			}
			loading.Value = 0;
		}

		private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 50;
			SaveFileDialog save = new SaveFileDialog();
			if (path == "")
			{
				if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					path = save.FileName;
					StreamWriter writer = new StreamWriter(File.Create(path));
					writer.Write(Note.Text);
					writer.Flush();
					writer.Close();
					loading.Value = 100;
					FilePath.Text = path;
					history = Note.Text;
				}
			}
			else
			{
				loading.Value = 50;
				StreamWriter writer = new StreamWriter(File.Create(path));
				writer.Write(Note.Text);
				writer.Flush();
				writer.Close();
				loading.Value = 100;
				history = Note.Text;
			}
			loading.Value = 0;
		}

		private void saveAsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			loading.Value = 50;
			SaveFileDialog save = new SaveFileDialog();
			if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				path = save.FileName;
				StreamWriter writer = new StreamWriter(File.Create(path));
				writer.Write(Note.Text);
				writer.Flush();
				writer.Close();
				loading.Value = 100;
				FilePath.Text = path;
				history = Note.Text;
			}
			loading.Value = 0;
		}

		private void NoteDock_FormClosing(object sender, FormClosingEventArgs e)
		{
			loading.Value = 25;
			if (history != Note.Text)
			{
				Console.Beep(500, 250);
				Console.Beep(1000, 250);
				string message = "Save All Changes?";
				string title = "NoteDock";
				MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show(message, title, buttons);
				loading.Value = 50;
				if(result == DialogResult.Yes)
				{
					SaveFileDialog save = new SaveFileDialog();
					if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						path = save.FileName;
						StreamWriter writer = new StreamWriter(File.Create(path));
						writer.Write(Note.Text);
						writer.Flush();
						writer.Close();
						loading.Value = 100;
						FilePath.Text = path;
					}
				}
			}
			else
			{
				Application.Exit();
			}
			loading.Value = 0;
		}

		private void aboutNoteDockToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("NoteDock Ver1.2.1-alpha", "About NoteDock");
		}

		private void fontToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FontDialog fontDlg = new FontDialog();
			fontDlg.ShowColor = true;
			fontDlg.ShowApply = true;
			fontDlg.ShowEffects = true;
			fontDlg.ShowHelp = true;
			if (fontDlg.ShowDialog() != DialogResult.Cancel)
			{
				Note.Font = fontDlg.Font;
				Note.ForeColor = fontDlg.Color;
			}
		}

		private void NoteDock_Load(object sender, EventArgs e)
		{
			this.KeyPreview = true;
		}

		private void NoteDock_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
			{
				SaveFileDialog save = new SaveFileDialog();
				if (path == "")
				{
					if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{
						path = save.FileName;
						StreamWriter writer = new StreamWriter(File.Create(save.FileName));
						writer.Write(Note.Text);
						writer.Flush();
						history = Note.Text;
						writer.Close();
					}
				}
				else
				{
					StreamWriter writer = new StreamWriter(File.Create(path));
					writer.Write(Note.Text);
					writer.Flush();
					history = Note.Text;
					writer.Close();
				}
				e.SuppressKeyPress = true;
			}
			else if (e.Control && e.KeyCode == Keys.O)
			{
				OpenFileDialog open = new OpenFileDialog();
				if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					path = open.FileName;
					StreamReader reader = new StreamReader(path);
					string data = reader.ReadToEnd();
					Note.Text = data;
					reader.Close();
					loading.Value = 100;
					FilePath.Text = path;
					history = Note.Text;
				}
				loading.Value = 0;
				e.SuppressKeyPress = true;
			}
			else if (e.Control && e.KeyCode == Keys.N)
			{
				SaveFileDialog save = new SaveFileDialog();
				if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					path = save.FileName;
					StreamWriter write = new StreamWriter(File.Create(path));
					Note.Text = "";
					write.Close();
					FilePath.Text = path;
					history = Note.Text;
				}
			}
		}
	}
}
