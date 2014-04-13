﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary;

namespace WindowsFormsApplication {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void analyzeButton_Click(object sender, EventArgs e) {
			string text = cipherTextBox.Text;
			string key;

			try {
				// Выбор направления (шифрование / дешифрование)
				if (decryptionRadioButton.Checked) {
					// Выбор действия:(если ключа нет, то выполнить анализ, при наличии ключа, дешифровать текст с данным ключом
					if (keyTextBox.Text == String.Empty) {

						// Выбран метод Касиски
						if (kasiskiBox.Checked) {
							int kMin = Kasiski.FindKeyLength(cipherTextBox.Text)[0];
							int kMax = Kasiski.FindKeyLength(cipherTextBox.Text)[1];

							minUpDown.Value = kMin;
							maxUpDown.Value = kMax;
							key = Vigenere.CrackKey(text, kMin, kMax);
						} else {
							key = Vigenere.CrackKey(text, Convert.ToInt32(minUpDown.Value), Convert.ToInt32(maxUpDown.Value));
						}

						keyTextBox.Text = key;
						resultTextBox.Text = Vigenere.Decipher(text, key);
					} else {
						string result = Vigenere.Decipher(cipherTextBox.Text, keyTextBox.Text);
						resultTextBox.Text = result;
					}
				} else {
					string result = Vigenere.Encipher(cipherTextBox.Text, keyTextBox.Text);
					resultTextBox.Text = result;
				}
			} catch (Exception ex) {
				MessageBox.Show(ex.Message, "Ошибка!");
			}
		}

		private void minUpDown_ValueChanged(object sender, EventArgs e) {
			if (minUpDown.Value > maxUpDown.Value) {
				MessageBox.Show("Error");
				minUpDown.Value = maxUpDown.Value - 1;
			}
		}

		private void maxUpDown_ValueChanged(object sender, EventArgs e) {
			if (maxUpDown.Value < minUpDown.Value) {
				MessageBox.Show("Error");
				maxUpDown.Value = minUpDown.Value + 1;
			}
		}

		private void kasiskiBox_CheckedChanged(object sender, EventArgs e) {
			minUpDown.Enabled = !minUpDown.Enabled;
			maxUpDown.Enabled = !maxUpDown.Enabled;
			keyTextBox.Text = String.Empty;
		}

		private void decryptionRadioButton_CheckedChanged(object sender, EventArgs e) {
			cipherLabel.Text = "Шифротекст";
			analyzeButton.Text = "Дешифровать";
		}

		private void encryptionRadioButton_CheckedChanged(object sender, EventArgs e) {
			cipherLabel.Text = "Открытый текст";
			analyzeButton.Text = "Зашифровать";
		}

		private void cleanButton_Click(object sender, EventArgs e) {
			cipherTextBox.Text = String.Empty;
			resultTextBox.Text = String.Empty;
			keyTextBox.Text = String.Empty;
			minUpDown.Value = 5;
			maxUpDown.Value = 15;
		}

		private void enToolStripMenuItem_Click(object sender, EventArgs e) {
			Vigenere.SetLanguage("en");
		}

		private void ruToolStripMenuItem_Click(object sender, EventArgs e) {
			Vigenere.SetLanguage("ru");
		}

		private void esToolStripMenuItem_Click(object sender, EventArgs e) {
			Vigenere.SetLanguage("es");
		}

		private void deToolStripMenuItem_Click(object sender, EventArgs e) {
			Vigenere.SetLanguage("de");
		}

		private void frToolStripMenuItem_Click(object sender, EventArgs e) {
			Vigenere.SetLanguage("fr");
		}

		private void ptToolStripMenuItem1_Click(object sender, EventArgs e) {
			Vigenere.SetLanguage("pt");
		}
	}
}
