using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;

namespace App3
{
    public partial class MainPage : ContentPage
    {
        // すべてのボタンを保持する
        private List<Button> buttonContainer;

        // 問題（初期状態）
        // private List<string> randomNumbers = new List<string> { "8", "7", "6", "5", "4", "3", "2", "1" };
        private List<string> randomNumbers = new List<string> { "3", "1", "2", "6", "4", "5", "7", "8" };

        // 回答
        private List<string> answer = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8" };

        private Stopwatch stopWatch = new Stopwatch();

        // スタートを最初のクリックとして扱う
        private bool firstClick = true;

        public MainPage()
        {
            InitializeComponent();

            buttonContainer = new List<Button> {
                this.button1,
                this.button2,
                this.button3,
                this.button4,
                this.button5,
                this.button6,
                this.button7,
                this.button8,
                this.button9,
            };


            this.InitButtonaText(randomNumbers);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="numbers">サイズが8の文字列リスト</param>
        private void InitButtonaText(List<string> numbers)
        {
            if(numbers.Count == 8)
            {
                this.button1.Text = numbers[0]; 
                this.button2.Text = numbers[1];
                this.button3.Text = numbers[2];
                this.button4.Text = numbers[3];
                this.button5.Text = numbers[4];
                this.button6.Text = numbers[5];
                this.button7.Text = numbers[6];
                this.button8.Text = numbers[7];
                this.button9.Text = string.Empty;   // 左下は空
            }
            else
            {
                // Error
            }
        }

        /// <summary>
        /// 移動後の状態が正解(終了)かどうかを判定する
        /// </summary>
        /// <returns>true:正解, false:不正解</returns>
        private void CheckAnser()
        {
            var ans = this.button1.Text == string.Empty &&
                    this.button2.Text == "1" &&
                    this.button3.Text == "2" &&
                    this.button4.Text == "3" &&
                    this.button5.Text == "4" &&
                    this.button6.Text == "5" &&
                    this.button7.Text == "6" &&
                    this.button8.Text == "7" &&
                    this.button9.Text == "8";

            if (ans)
            {
                // 終了！
                stopWatch.Stop();
                var msg = "かかった時間は、" + stopWatch.ElapsedMilliseconds + "ミリ秒です。";
                DisplayAlert("終了！", msg, "OK");
            }
            else
            {
                // まだまだ続く
            }
        }

        /// <summary>
        /// 空白の位置を1～9で返す
        /// </summary>
        /// <returns></returns>
        private int BlankPanelIndex()
        {
            // 空白の位置を返す
            var blankPanelIndex = 1;
            foreach (var b in buttonContainer)
            {
                if (b.Text == string.Empty)
                {
                    break;
                }
                blankPanelIndex++;
            }
            return blankPanelIndex;
        }

        /// <summary>
        /// 空白と隣り合わせの場合、数字を入れ替える
        /// (クリックされたパネルを空に。空のパネルにクリックされた数字を)
        /// </summary> 
        private void SlidePanel(int clickedPanelIndex)
        {
            var clickedButton = buttonContainer[clickedPanelIndex -1];
            var clickedNumber = clickedButton.Text;

            var blankButton = buttonContainer[this.BlankPanelIndex() - 1];
            blankButton.Text = clickedNumber;

            clickedButton.Text = string.Empty;
        }

        /// <summary>
        /// クリックされたパネル位置の上下左右のいずれかに空白があるかをチェックする
        /// </summary>
        /// <param name="clickedPanelIndex">クリックされたパネル位置(表示されているテキストではない）</param>
        /// <returns>隣り合っている場合はtrue, そうでない場合はfalse</returns>
        private bool IsNeighborPanel(int clickedPanelIndex)
        {
            var blankPanelIndex = this.BlankPanelIndex();
            // クリックされたパネルの左側に空白があるか
            if(clickedPanelIndex != 1 || clickedPanelIndex != 4 || clickedPanelIndex != 7)
            {
                if(blankPanelIndex == clickedPanelIndex - 1)
                {
                    return true;
                }
            }

            // クリックされたパネルの右側に空白があるか
            if (clickedPanelIndex != 3 || clickedPanelIndex != 6 || clickedPanelIndex != 9)
            {
                if (blankPanelIndex == clickedPanelIndex + 1)
                {
                    return true;
                }
            }

            // クリックされたパネルの上側に空白があるか
            if (clickedPanelIndex != 1 || clickedPanelIndex != 2 || clickedPanelIndex != 3)
            {
                if (blankPanelIndex == clickedPanelIndex - 3)
                {
                    return true;
                }
            }

            // クリックされたパネルの下側に空白があるか
            if (clickedPanelIndex != 7 || clickedPanelIndex != 8 || clickedPanelIndex != 9)
            {
                if (blankPanelIndex == clickedPanelIndex + 3)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 各ボタン共通の処理
        /// </summary>
        /// <param name="panelIndex"></param>
        private void Click(int panelIndex)
        {
            if(firstClick)
            {
                stopWatch.Start();
                firstClick = false;
            }

            if (this.IsNeighborPanel(panelIndex))
            {
                this.SlidePanel(panelIndex);
                this.CheckAnser();
            }
        }

        public void Clicked1(object sender, EventArgs args)
        {
            this.Click(1);
        }

        public void Clicked2(object sender, EventArgs args)
        {
            this.Click(2);
        }

        public void Clicked3(object sender, EventArgs args)
        {
            this.Click(3);
        }

        public void Clicked4(object sender, EventArgs args)
        {
            this.Click(4);
        }

        public void Clicked5(object sender, EventArgs args)
        {
            this.Click(5);
        }

        public void Clicked6(object sender, EventArgs args)
        {
            this.Click(6);
        }

        public void Clicked7(object sender, EventArgs args)
        {
            this.Click(7);
        }

        public void Clicked8(object sender, EventArgs args)
        {
            this.Click(8);
        }

        public void Clicked9(object sender, EventArgs args)
        {
            this.Click(9);
        }


    }
}
