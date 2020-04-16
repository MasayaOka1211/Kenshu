using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace MultiThread
{
    public partial class Form1 : Form
    {
        Stopwatch myStopWatch = new Stopwatch();

        //スタート・ストップボタン用
        Boolean sw = false;

        // 非同期実行するためのデリゲート
        delegate void SampleDelegate(string filePath, string WriteValue);

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (sw == false)
            {

                //計測開始

                myStopWatch.Start();

                //表示更新タイマー開始

                timer2.Start();

                //スイッチon

                sw = true;

                //リセットボタン使用不可

                btnReset.Enabled = false;

                //「スタート」だったボタンの表示を「ストップ」に変更

                btnStart.Text = "ストップ";

            }
          //スイッチがoff以外のとき（つまりはonのとき
            else
            {
                // 実行するデリゲートを作成
                SampleDelegate sampleDelegate =
                    new SampleDelegate(this.DelegatingMethod);

                //計測終了

                //myStopWatch.Stop();

                //表示固定

                //timer2.Stop();

                label3.Text = myStopWatch.Elapsed.ToString();

                 // 非同期で呼び出す
                IAsyncResult ar =
                    sampleDelegate.BeginInvoke(@"C:\Test\TimeRecord.txt", label3.Text + System.Environment.NewLine 
                    , null, null);

                //スイッチoff

                sw = false;

                //リセットボタン使用可

                btnReset.Enabled = true;

                //「ストップ」だったボタンの表示を「スタート」に変更

                btnStart.Text = "スタート";

            }            

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //ストップウォッチの内容をゼロにする

            myStopWatch.Reset();

            //リセットした状態をlabel2に表示する

            label2.Text = myStopWatch.Elapsed.ToString();
            label3.Text = myStopWatch.Elapsed.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label2.Text = myStopWatch.Elapsed.ToString();
        }

        private void DelegatingMethod(string filePath, string writeValue)
        {
            using(StreamWriter sWrite = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                sWrite.Write(writeValue);
            }
        }

    }
}
