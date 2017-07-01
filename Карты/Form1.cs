using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Карты
{
    public partial class Form1 : Form
    {

        static Random _random = new Random();
        Timer timer = null;
        Card[] cardArray = { new Card("button1", 1, 0), new Card("button2", 2, 0), new Card("button3", 3, 0), new Card("button4", 4, 0),
                               new Card("button5", 5, 0), new Card("button6", 5, 0), new Card("button7", 7, 0), new Card("button8", 7, 0),
                               new Card("button9", 1, 0), new Card("button10", 2, 0), new Card("button11", 3, 0), new Card("button12", 4, 0),
                               new Card("button13", 8, 0), new Card("button14", 9, 0), new Card("button15", 10, 0), new Card("button16", 11, 0),
                               new Card("button17", 12, 0), new Card("button18", 13, 0), new Card("button19", 8, 0), new Card("button20", 14, 0),
                               new Card("button21", 15, 0), new Card("button22", 13, 0), new Card("button23", 14, 0), new Card("button24", 10, 0),
                               new Card("button25", 15, 0), new Card("button26", 9, 0), new Card("button27", 16, 0), new Card("button28", 11, 0),
                               new Card("button29", 12, 0), new Card("button30", 16, 0)
                           };



        private Card[] dealСards(Card[] array)
        {
            int n = array.Length;
            
            for (int i = 0; i < n; i++)
            {

                // NextDouble returns a random number between 0 and 1.
                // ... It is equivalent to Math.random() in Java.
                int r = i + (int)(_random.NextDouble() * (n - i));
                
                int c = array[i].CardValue;
                array[i].CardValue = array[r].CardValue;
                array[r].CardValue=c;
            }
            
            return array;
        }

        DateTime date;
        int tempOne = 0, tempTwo = 0;
        
        
 
       //Card[] cardArray = null;
        
        public Form1()
        {
            InitializeComponent();
            this.closeAllButtons();
            cardArray = this.dealСards(cardArray);
            //cardArray = this.Shuffle(cardArray);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string btnName = ((Button)sender).Name;
            string value = this.getValueByName(btnName).ToString();

            if (tempOne == 0)
            {
                tempOne = this.getValueByName(btnName);
                Controls[btnName].Text = "";
                Image img = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(value);
                Controls[btnName].BackgroundImage = resizeImage(img, new Size(75, 65));

            }
            else
            {
                if (tempTwo == 0)
                {
                    tempTwo = this.getValueByName(btnName);
                    Controls[btnName].Text = "";
                    Image img = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(value);
                    Controls[btnName].BackgroundImage = resizeImage(img, new Size(75, 65));

                    if (tempOne == tempTwo)
                    {
                        this.changeStatusByValue(this.getValueByName(btnName));
                    }

                }else
                {
                    tempOne = tempTwo = 0;
                    foreach (Button btn in this.Controls.OfType<Button>())
                    {

                        if (this.getStatusByButtonName(btn.Name) == 0)
                        {
                            btn.Text = "Closed";
                            btn.BackgroundImage = null;
                        }
                    }
                }
            }
            if (this.areWiner())
            {
                timer.Stop();
                MessageBox.Show("You wins");
                
            }
        }


        private void tickTimer(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - date.Ticks;
            DateTime stopWatch = new DateTime();

            stopWatch = stopWatch.AddTicks(tick);
            label1.Text = String.Format("{0:HH:mm:ss:ff}", stopWatch);
        }

        //new game
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cardArray = this.dealСards(cardArray);
            date = DateTime.Now;
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(tickTimer);
            timer.Start();
            this.closeAllButtons();
            for (int i = 0; i < cardArray.Length; i++)
            {
                cardArray[i].CardStatus = 0;
            }
            closeAllButtons();
        }

        private void closeAllButtons()
        {
            try
            {
                foreach (Control control in this.Controls)
                {
                    if (control.GetType() == typeof(Button))
                        control.Text = "Closed";
                }
            }
            catch (System.Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        //open all cards with status = 0
        private void blaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach ( Button btn in this.Controls.OfType<Button>() )
            {
                
                if (this.getStatusByButtonName(btn.Name) == 0)
                {
                    btn.Text = this.getValueByName(btn.Name).ToString();
                }
                

            }

        }

        //извежда статус на картата по нейното име
        private int getStatusByButtonName(string buttonName)
        {
            int result = -1;
            for (int i = 0; i < cardArray.Length; i++)
            {
                if (cardArray[i].ButtonName == buttonName)
                {
                    result = cardArray[i].CardStatus;
                    break;
                }
            }
            return result;

        }

        //извежда значение на картата по нейното име
        private int getValueByName(string btnName)
        {

            int result = -1;
            for (int i = 0; i < cardArray.Length; i++)
            {
                if (cardArray[i].ButtonName == btnName)
                {
                    result = cardArray[i].CardValue;
                    break;
                }
            }
            return result;
        }

        //close all cards with status = 0
        private void closeZeroStToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Button btn in this.Controls.OfType<Button>())
            {

                if (this.getStatusByButtonName(btn.Name) == 0)
                {
                    btn.Text = "Closed";
                    btn.BackgroundImage = null;
                }


            }
        }


        private void changeStatusByValue(int value)
        {
            for (int i = 0; i < cardArray.Length; i++)
            {
                if (cardArray[i].CardValue == value)
                {
                    cardArray[i].CardStatus=1;

                }
            }
        }

        private bool areWiner()
        {
            for (int i = 0; i < cardArray.Length; i++)
            {
                if (cardArray[i].CardStatus==0)
                {
                    return false;
                }
            }
            return true;
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright © All Rights Reserved.");
        }
    }
}
