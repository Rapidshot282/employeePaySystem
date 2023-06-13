using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam2023_01_01
{
    public partial class Form1 : Form
    {
        private Worker worker;
        private int totalPaySum; // 추가: 총 지불 금액의 합을 저장하는 변수

        class Worker
        {
            public string workerName { get; set; }
            public int workertotalPay { get; set; }

            public int pay { get; set; }

            public int totalpay;

            public int workerPay(int workertime, int workerpay)
            {
                totalpay = workertime * workerpay;
                return totalpay;
            }

            public int workerPayLunch(int workertime, int workerpay)
            {
                totalpay = totalpay = (workertime * workerpay) - pay;
                return totalpay;
            }
        }

        public Form1()
        {
            InitializeComponent();
            worker = new Worker();
            totalPaySum = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hour_1 = dateTimePicker1.Value.Hour;
            int minute_1 = dateTimePicker1.Value.Minute;
            int hour_2 = dateTimePicker2.Value.Hour;
            int minute_2 = dateTimePicker2.Value.Minute;
            int calHour;

            worker.pay = int.Parse(textBox3.Text);

            calHour = hour_2 - hour_1;

            // 점심시간(오후 12시 ~ 오후 1시)에 해당하는지 확인
            bool isLunchTime = (hour_1 <= 12 && hour_2 >= 13);

            if (isLunchTime) // 점심시간에 해당하는 경우
            {
                // 점심시간에 해당하는 시간을 제외하고 총 근무 시간 계산
                if (hour_1 < 12 || hour_2 > 13)
                {
                    worker.workertotalPay = worker.workerPayLunch(calHour, worker.pay);
                }
            }
            else
            {
                if (calHour == 1)
                {
                    if (minute_1 == minute_2)
                    {
                        worker.workertotalPay = worker.workerPay(1, worker.pay);

                    }
                    else if (minute_1 > minute_2)
                    {
                        MessageBox.Show("1시간을 일하지 않아 시급이 0원으로 저장되었습니다.", "알림");
                        worker.workertotalPay = worker.workerPay(0, worker.pay);
                    }
                    else { worker.workertotalPay = worker.workerPay(calHour, worker.pay); }
                }
                else
                {
                    worker.workertotalPay = worker.workerPay(calHour, worker.pay);
                }
            }

            worker.workerName = textBox1.Text;
            totalPaySum += worker.workertotalPay;
            listBox1.Items.Add(worker.workerName + "," + worker.workertotalPay);

            totalPaySum = 0;

            foreach (var workerpay in listBox1.Items)
            {
                var input = workerpay.ToString();
                var s = input.Split(',');
                int s2 = int.Parse(s[1]);

                List<Worker> workerList = new List<Worker>();
                workerList.Add(new Worker { workertotalPay = s2 });

                totalPaySum += s2;

                textBox2.Text = totalPaySum.ToString();
            }

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}

