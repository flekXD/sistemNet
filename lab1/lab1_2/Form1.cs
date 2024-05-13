using System;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace lab1_2
{
    public partial class Form1 : Form
    {
        static Mutex mutex = new Mutex();
        static Timer timer;
        static string previousIterationResult = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 500; // 0.5 �������
            timer.Tick += DisplayData;
            timer.Start();
        }

        private void DisplayData(object sender, EventArgs e)
        {
            // �������� ��� � richTextBox1
            try
            {
                mutex.WaitOne();

                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("sort_iterations"))
                {
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {
                        BinaryReader reader = new BinaryReader(stream);
                        string iterationResult = reader.ReadString();

                        // ������ ����� �� richTextBox1, ���� �� ����������� �� ������������
                        if (iterationResult != previousIterationResult)
                        {
                            richTextBox1.AppendText(iterationResult + Environment.NewLine);
                            previousIterationResult = iterationResult;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //richTextBox1.AppendText($"������� ��� �������� �����: {ex.Message}" + Environment.NewLine);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ��� ��� ���
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // ��� ��� ���
        }
    }
}
