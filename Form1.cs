using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace kimmilyonerolmakister
{
	public partial class Form1 : Form
	{
		private List<Soru> _sorular = new List<Soru>();
		private Soru _suankiSoru;
		private int _seviye = 1;
		private Random _rnd = new Random();
		private bool _joker50Used = false;
		private bool _jokerPhoneUsed = false;
		private bool _jokerAudienceUsed = false;

		private readonly int[] _oduller = { 0, 1000, 2000, 3000, 5000, 7500, 10000, 15000, 30000, 60000, 125000, 250000, 1000000 };

		public Form1()
		{
			InitializeComponent();
			SetupUI();
		}

		private void SetupUI()
		{
			this.Text = "Kim Milyoner Olmak İster?";
			this.BackColor = Color.FromArgb(10, 10, 60);
			this.Size = new Size(900, 600);
			this.StartPosition = FormStartPosition.CenterScreen;

			// Question Label
			label1.ForeColor = Color.White;
			label1.Font = new Font("Segoe UI", 14, FontStyle.Bold);
			label1.TextAlign = ContentAlignment.MiddleCenter;
			label1.AutoSize = false;
			label1.Size = new Size(700, 80);
			label1.Location = new Point(100, 200);
			label1.BorderStyle = BorderStyle.FixedSingle;

			// Prize Label
			label2.ForeColor = Color.Gold;
			label2.Font = new Font("Segoe UI", 12, FontStyle.Bold);
			label2.Location = new Point(100, 150);
			label2.Size = new Size(300, 30);
			label2.Text = "Seviye: 1 | Ödül: 0 TL";

			// Answer Buttons
			ConfigureAnswerButton(button1, "A", new Point(100, 350));
			ConfigureAnswerButton(button2, "B", new Point(500, 350));
			ConfigureAnswerButton(button3, "C", new Point(100, 420));
			ConfigureAnswerButton(button4, "D", new Point(500, 420));

			// Joker Buttons
			ConfigureJokerButton(button5, "50:50", new Point(650, 50));
			ConfigureJokerButton(button6, "Telefon", new Point(730, 50));
			ConfigureJokerButton(button7, "Seyirci", new Point(810, 50));

			button1.Click += (s, e) => CevapVer("A");
			button2.Click += (s, e) => CevapVer("B");
			button3.Click += (s, e) => CevapVer("C");
			button4.Click += (s, e) => CevapVer("D");

			button5.Click += (s, e) => Joker50();
			button6.Click += (s, e) => JokerTelefon();
			button7.Click += (s, e) => JokerSeyirci();
		}

		private void ConfigureAnswerButton(Button btn, string label, Point loc)
		{
			btn.Size = new Size(300, 50);
			btn.Location = loc;
			btn.BackColor = Color.FromArgb(20, 20, 100);
			btn.ForeColor = Color.White;
			btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
			btn.FlatStyle = FlatStyle.Flat;
			btn.FlatAppearance.BorderColor = Color.Gold;
			btn.Text = label;
		}

		private void ConfigureJokerButton(Button btn, string text, Point loc)
		{
			btn.Size = new Size(70, 70);
			btn.Location = loc;
			btn.BackColor = Color.Gold;
			btn.ForeColor = Color.Black;
			btn.Font = new Font("Segoe UI", 8, FontStyle.Bold);
			btn.FlatStyle = FlatStyle.Flat;
			btn.Text = text;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			LoadQuestions();
			NewQuestion();
		}

		private void LoadQuestions()
		{
			try
			{
				string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sorular.txt");
				if (!File.Exists(path))
				{
					// Check project root if not in bin
					path = "sorular.txt";
				}

				if (File.Exists(path))
				{
					var lines = File.ReadAllLines(path);
					foreach (var line in lines)
					{
						var parts = line.Split('|');
						if (parts.Length == 7)
						{
							_sorular.Add(new Soru
							{
								SoruMetni = parts[0],
								A = parts[1],
								B = parts[2],
								C = parts[3],
								D = parts[4],
								DogruCevap = parts[5],
								Zorluk = int.Parse(parts[6])
							});
						}
					}
				}
				else
				{
					MessageBox.Show("Soru dosyası bulunamadı!");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Hata: " + ex.Message);
			}
		}

		private void NewQuestion()
		{
			// Original format difficulty mapping (approximate)
			int difficulty = 1;
			if (_seviye > 2) difficulty = 2;
			if (_seviye > 4) difficulty = 3;
			if (_seviye > 7) difficulty = 4;
			if (_seviye > 10) difficulty = 5;

			var filteredQuestions = _sorular.Where(q => q.Zorluk == difficulty).ToList();
			if (filteredQuestions.Count == 0)
			{
				MessageBox.Show("Bu zorluk seviyesinde soru bulunamadı!");
				return;
			}

			_suankiSoru = filteredQuestions[_rnd.Next(filteredQuestions.Count)];

			label1.Text = _suankiSoru.SoruMetni;
			button1.Text = "A: " + _suankiSoru.A;
			button2.Text = "B: " + _suankiSoru.B;
			button3.Text = "C: " + _suankiSoru.C;
			button4.Text = "D: " + _suankiSoru.D;

			button1.Visible = button2.Visible = button3.Visible = button4.Visible = true;
			button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = true;

			label2.Text = $"Seviye: {_seviye} | Ödül: {_oduller[_seviye - 1]} TL | Hedef: {_oduller[_seviye]} TL";
		}

		private void CevapVer(string cevap)
		{
			if (cevap == _suankiSoru.DogruCevap)
			{
				if (_seviye == 12)
				{
					MessageBox.Show("TEBRİKLER! 1 Milyon TL kazandınız!");
					Application.Restart();
				}
				else
				{
					MessageBox.Show("Doğru cevap! Bir sonraki soruya geçiliyor.");
					_seviye++;
					NewQuestion();
				}
			}
			else
			{
				int teselli = 0;
				if (_seviye > 7) teselli = 30000;
				else if (_seviye > 2) teselli = 7500; // Original format had milestones at 2nd and 7th question usually

				MessageBox.Show($"Yanlış cevap! Doğru cevap: {_suankiSoru.DogruCevap}\nKazandığınız ödül: {teselli} TL");
				Application.Restart();
			}
		}

		private void Joker50()
		{
			if (_joker50Used) return;
			_joker50Used = true;
			button5.Enabled = false;
			button5.BackColor = Color.Gray;

			List<Button> buttons = new List<Button> { button1, button2, button3, button4 };
			string[] answers = { "A", "B", "C", "D" };

			int removedCount = 0;
			while (removedCount < 2)
			{
				int index = _rnd.Next(4);
				if (answers[index] != _suankiSoru.DogruCevap && buttons[index].Visible)
				{
					buttons[index].Visible = false;
					removedCount++;
				}
			}
		}

		private void JokerTelefon()
		{
			if (_jokerPhoneUsed) return;
			_jokerPhoneUsed = true;
			button6.Enabled = false;
			button6.BackColor = Color.Gray;

			string[] names = { "Prof. Dr. Ahmet", "Teyzen Fatma", "Arkadaşın Mehmet", "Öğretmenin Ayşe" };
			string name = names[_rnd.Next(names.Length)];

			// Phone joker isn't always right
			string suggestedAnswer;
			if (_rnd.Next(100) < 70) // 70% chance of correct answer
				suggestedAnswer = _suankiSoru.DogruCevap;
			else
				suggestedAnswer = new[] { "A", "B", "C", "D" }[_rnd.Next(4)];

			MessageBox.Show($"{name} diyor ki: Bence cevap {suggestedAnswer} şıkkı.");
		}
		private void JokerSeyirci()
		{
			// 1. Kontrol: Eğer zaten kullanıldıysa fonksiyondan çık
			if (_jokerAudienceUsed) return;

			// 2. Durumu güncelle: Artık kullanıldı
			_jokerAudienceUsed = true;

			// 3. Görsel Değişiklikler: Butonu pasif yap ve rengini griye çevir
			button7.Enabled = false;
			button7.BackColor = Color.Gray;

			// 4. Mantık: Seyirci oylarını simüle et
			// Doğru cevaba %40 ile %70 arası bir ağırlık verelim
			int dogruPuan = _rnd.Next(40, 71);
			int kalan = 100 - dogruPuan;

			// Diğer 3 şıkka kalan puanı rastgele dağıtalım
			int s1 = _rnd.Next(0, kalan);
			int s2 = _rnd.Next(0, kalan - s1);
			int s3 = kalan - s1 - s2;

			string[] siklar = { "A", "B", "C", "D" };
			int[] oranlar = new int[4];
			int digerIndex = 0;
			int[] digerDagitim = { s1, s2, s3 };

			string sonucMesaji = "Seyirci Oylama Sonuçları:\n";

			for (int i = 0; i < 4; i++)
			{
				if (siklar[i] == _suankiSoru.DogruCevap)
					oranlar[i] = dogruPuan;
				else
					oranlar[i] = digerDagitim[digerIndex++];

				sonucMesaji += $"{siklar[i]}: %{oranlar[i]}\n";
			}

			// 5. Bilgiyi göster
			MessageBox.Show(sonucMesaji, "Seyirci Jokeri");
		}

		private void label1_Click(object sender, EventArgs e) { }
	}

	public class Soru
	{
		public string SoruMetni { get; set; }
		public string A { get; set; }
		public string B { get; set; }
		public string C { get; set; }
		public string D { get; set; }
		public string DogruCevap { get; set; }
		public int Zorluk { get; set; }
	}
}
