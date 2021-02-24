using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ivi.Visa.Interop;
using System.Globalization;
using System.Threading;


namespace TurnTableTest
{
    public partial class Form1 : Form
    {
        private ResourceManager RM = new ResourceManager();
        private FormattedIO488 INST_Table = new FormattedIO488();
        private FormattedIO488 INST_VNA = new FormattedIO488();
        private FormattedIO488 INST_Tower = new FormattedIO488();



        //VISA
        string E5071C;
        string Table;
        string Tower;

        //COMMAND
        string SPD;
        string POL = "PV";  // アンテナ向き

        //VALUE
        string MK;
        string VFRQ;
        string VSPN;
        string VPOW;
        string VPOINT;
        string[,] results = new string[361, 7];

        int CHKNUM;
        int INTVAL = 0;
        int MESPOINT;
        int MESPOS;
        int CHKPOS;
        int[] TESTMK = { 0, 0, 0, 0, 0, 0, 0 };

        //Table position
        string DISTSTR; //distination
        string CPOS;

        //結果のファイル
        StreamWriter data;

        //flag
        


        public Form1()
        {
            InitializeComponent();

            //VISA adress
            E5071C = textBox_visaVNA.Text;
            Table = textBox_visaTable.Text;
            Tower = textBox_visaPol.Text;

            //VNA settings
            VFRQ = textBox_FreqCenter.Text;
            VSPN = textBox_FreqBandwidth.Text;
            VPOW = textBox_Power.Text;
            VPOINT = textBox_PointNum.Text;

            //Tower
            POL = "PH";

            //table settings
            DISTSTR = String.Format("{0:00000.00}", double.Parse(textBox_Distination.Text));


        }

        //VISA address to combobox
        private void SETVISA()
        {
            string[] VISA = { Table, Tower, E5071C };
            comboBox_visaList.Items.AddRange(VISA);
            comboBox_visaList.SelectedIndex = 0;
        }

        //Speed of turn Table to combobox
        private void VELOCITY()
        {
            string[] VELO = { "0.25", "0.50", "0.75", "1.0", "1.5", "2.0" };
            comboBox_velo.Items.AddRange(VELO);
            comboBox_velo.SelectedIndex = 1;
            SPD = "SPD00000.50";
        }

        //Measurement segment
        private void SEGMENT()
        {
            string[] DIVE = { "1", "5", "10", "15" };
            comboBox_interval.Items.AddRange(DIVE);
            comboBox_interval.SelectedIndex = 0;
            INTVAL = 1;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            SETVISA();
            VELOCITY();
            SEGMENT();

        }

        private void button_VNASET_Click(object sender, EventArgs e)
        {

            if (INST_VNA.IO == null)
            {
                MessageBox.Show("VNA disconnected");
                return;
            }

            string FRQ = ":SENS1:FREQ:CENT " + VFRQ + "E6";
            string SPN = ":SENS1:FREQ:SPAN " + VSPN + "E6";
            string POINT = ":SENS1:SWE:POIN " + VPOINT;
            string POW = ":SOUR1:POW " + VPOW;

            try
            {
                INST_VNA.IO.Clear();
                INST_VNA.WriteString(FRQ);
                INST_VNA.WriteString(SPN);
                INST_VNA.WriteString(POINT);
                INST_VNA.WriteString(POW);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private async void button_Move2_Click(object sender, EventArgs e)
        {
            double POSITION;
            double DISTINATION;
            double SPAN;
            string DISTCOMMAND;

            
            try
            {
                INST_Table.IO.Clear();
                INST_Table.WriteString("SPD00002.00");

                INST_Table.WriteString("CP");
                CPOS = INST_Table.ReadString().Substring(0, 8);

                POSITION = double.Parse(CPOS.Substring(0, 8));
                DISTINATION = double.Parse(textBox_Distination.Text);

                //		POSISTR	"00000.00,0\r\r"	string


                SPAN = DISTINATION - POSITION;
                if (SPAN < 0)
                {
                    SPAN = SPAN + 360;
                }
                else if(SPAN == 0)
                {
                    return;
                }

                DISTCOMMAND = "CWP" + String.Format("{0:00000.00}", SPAN);
                INST_Table.WriteString(DISTCOMMAND);
                

                do
                {
                    await Task.Run(() => readposition());
                    textBox_Angle.Text = CPOS;
                    
                } while (DISTSTR != CPOS);

                INST_Table.WriteString(SPD);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void button_SENDCMD_Click(object sender, EventArgs e)
        {
            string CMD = textBox_CMD.Text;

            try
            {
                var session = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(comboBox_visaList.SelectedItem.ToString());

                session.FormattedIO.WriteLine(CMD);

                if (textBox_CMD.Text.Contains("?"))
                {
                    textBox_VALUE.Text = session.FormattedIO.ReadLine();
                }

                session.Dispose();
                session = null;
            }
            catch
            {

            }
        }


        private void button_SetPol_Click(object sender, EventArgs e)
        {

            if (POL == "PH")
            {
                POL = "PV";
                button_SetPol.Text = "Horizontal";
                           }
            else if (POL == "PV")
            {
                POL = "PH";
                button_SetPol.Text = "Vertical";
            }
            else
            {
                return;
            }

          try
            {
                var session = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(Tower);

                session.FormattedIO.WriteLine(POL);

                session.Dispose();
                session = null;
            }
            catch
            {

            }
        }


        private async void button_startMeas_Click(object sender, EventArgs e)
        {

            CHKNUM = Chkbox();

            if (checkBox_Hol.Checked == true)
            {
                POL = "PV";
                this.button_SetPol.PerformClick();
                await TEST();

                if (checkBox_Vel.Checked == true)
                {
                    POL = "PH";
                    this.button_SetPol.PerformClick();
                    await TEST();
                }

            }
            else
            {
                if (checkBox_Vel.Checked == true)
                {
                    POL = "PH";
                    this.button_SetPol.PerformClick();
                    await TEST();
                }
            }
            
        }



        public void readposition()
        {
            try
            {
                INST_Table.WriteString("CP");
                CPOS = INST_Table.ReadString().Substring(0, 8);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        //MEASUREMENT
        private async Task TEST()
        {

            try
            {



                //保存するファイルの処理
                string MeasFileName = null;

                if (POL == "PH")
                {
                    MeasFileName = "dataH.csv";
                }
                else if (POL == "PV")
                {
                    MeasFileName = "dataV.csv";
                }
                else
                {
                    return;
                }

                saveFileDialog1.FileName = MeasFileName;
                saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string file = saveFileDialog1.FileName;
                    data = new StreamWriter(file, true, Encoding.Default);
                    MessageBox.Show("Measurement START");
                }
                else
                {
                    MessageBox.Show("Measurement STOP");
                    return;
                }



                for (CHKPOS = 0; CHKPOS < 6; CHKPOS++)
                {
                    if (TESTMK[CHKPOS] == 1)
                    {
                        //現在位置
                        INST_Table.WriteString("CP");
                        CPOS = INST_Table.ReadString().Substring(0, 8);

                        //0位置移動
                        if (CPOS != "00000.00")
                        {
                            textBox_Distination.Text = "0";
                            this.button_Move2.PerformClick();
                        }

                        //逆回転
                        INST_Table.WriteString("CCP00010.00");
                        do
                        {
                            await Task.Run(() => readposition());
                            textBox_Angle.Text = CPOS;

                        } while (CPOS != "00350.00");

                        //VNA測定初期化
                        await Task.Run(() => INITVNA1()); //


                        //SPD設定
                        INST_Table.WriteString(SPD);


                        //回転開始
                        INST_Table.WriteString("CWP00370.00");


                        //測定開始
                        //Task t = Task.Run(() => { AsyncVNARead(); });


                        //回転中
                        MESPOS = 0;
                        do //(MESPOS = 0; MESPOS < MESPOINT; MESPOS++)
                        {
                            //回転位置
                            await Task.Run(() => readposition2());

                            //VNA
                            //Task t = Task.Run(() => { AsyncVNARead(); }); //
                            await Task.Run(() => { AsyncVNARead1(); });

                            int x = MESPOS * INTVAL;
                            textBox_Angle.Text = x.ToString();

                            Console.WriteLine(MESPOS);
                            MESPOS++;

                        } while (MESPOS < MESPOINT);

                        do
                        {
                            INST_Table.WriteString("CP");
                            CPOS = INST_Table.ReadString().Substring(0, 8);
                        } while (CPOS != "00000.00");
                        
                    }

                }




                //測定完了
                string temp = null;

                for (int k = 0; k < MESPOINT; k++)
                {
                    for (int l = 0; l < CHKNUM; l++)
                    {
                        if (l == 0)
                        {
                            temp = results[k, l];
                        }
                        else
                        {
                            temp = temp + "," + results[k, l];
                        }
                    }

                    data.WriteLine(temp);
                }

                data.Close();
                MessageBox.Show("END");
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void readposition2()
        {
            double temp;

            try
            {
                do
                {
                    INST_Table.WriteString("CP");
                    CPOS = INST_Table.ReadString().Substring(0, 8);
                    Console.WriteLine(CPOS);
                    

                    temp = Math.Abs(double.Parse(CPOS) - MESPOS*INTVAL);

                    //差分が10以上で
                    if(MESPOS == 0)
                    {
                        if(temp > 0.2)
                        {
                            temp = 1;
                        }
                    }
                    else 
                    {
                        if (temp > 20)
                        {
                            //強制終了
                            MESPOS = MESPOINT;
                            MessageBox.Show("ERROR");
                            return;
                        }
                           
                    }

                } while (temp > 0.1); //差分が0.1以下になるまでループ

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void INITVNA()
        {
            int MinF = 0, MaxF = 0;

            if (checkBox1.Checked == true)
            {
                TESTMK[0] = 1;
                MinF = int.Parse(textBox_MK1.Text);
                MaxF = int.Parse(textBox_MK1.Text);

            }
            else
            {
                INST_VNA.WriteString(":CALC1:MARK1 OFF");
            }
            if (checkBox2.Checked == true)
            {
                TESTMK[1] = 1;

                if (MinF > int.Parse(textBox_MK2.Text))
                {
                    MinF = int.Parse(textBox_MK2.Text);
                }
                if (MaxF < int.Parse(textBox_MK2.Text))
                {
                    MaxF = int.Parse(textBox_MK2.Text);
                }
            }
            else
            {
                INST_VNA.WriteString(":CALC1:MARK2 OFF");
            }
            if (checkBox3.Checked == true)
            {
                TESTMK[2] = 1;

                if (MinF > int.Parse(textBox_MK3.Text))
                {
                    MinF = int.Parse(textBox_MK3.Text);
                }
                if (MaxF < int.Parse(textBox_MK3.Text))
                {
                    MaxF = int.Parse(textBox_MK3.Text);
                }
            }
            else
            {
                INST_VNA.WriteString(":CALC1:MARK3 OFF");
            }
            if (checkBox4.Checked == true)
            {
                TESTMK[3] = 1;

                if (MinF > int.Parse(textBox_MK4.Text))
                {
                    MinF = int.Parse(textBox_MK4.Text);
                }
                if (MaxF < int.Parse(textBox_MK4.Text))
                {
                    MaxF = int.Parse(textBox_MK4.Text);
                }
            }
            else
            {
                INST_VNA.WriteString(":CALC1:MARK4 OFF");
            }
            if (checkBox5.Checked == true)
            {
                TESTMK[4] = 1;

                if (MinF > int.Parse(textBox_MK5.Text))
                {
                    MinF = int.Parse(textBox_MK5.Text);
                }
                if (MaxF < int.Parse(textBox_MK5.Text))
                {
                    MaxF = int.Parse(textBox_MK5.Text);
                }
            }
            else
            {
                INST_VNA.WriteString(":CALC1:MARK5 OFF");
            }
            if (checkBox6.Checked == true)
            {
                TESTMK[5] = 1;

                if (MinF > int.Parse(textBox_MK6.Text))
                {
                    MinF = int.Parse(textBox_MK6.Text);
                }
                if (MaxF < int.Parse(textBox_MK6.Text))
                {
                    MaxF = int.Parse(textBox_MK6.Text);
                }
            }
            else
            {
                INST_VNA.WriteString(":CALC1:MARK6 OFF");
            }
            if (MinF == 0 & MaxF == 0)
            {
                MessageBox.Show("No Test Point");
                return;

            }


            string START = ":SENS1:FREQ:STAR " + MinF.ToString() + "E6";
            string END = ":SENS1:FREQ:STOP " + MaxF.ToString() + "E6";
            //string SPN = ":SENS1:FREQ:SPAN " + "0" + "E6";
            string POINT = ":SENS1:SWE:POIN " + "2";
            string POW = ":SOUR1:VPOW " + VPOW;

            INST_VNA.WriteString(START);
            //System.Threading.Thread.Sleep(50);
            INST_VNA.WriteString(END);
            //System.Threading.Thread.Sleep(50);
            INST_VNA.WriteString(POINT);
            //System.Threading.Thread.Sleep(1000);



            if (checkBox1.Checked == true)
            {
                INST_VNA.WriteString(":CALC1:MARK1:X " + textBox_MK1.Text + "E6");

            }
            if (checkBox2.Checked == true)
            {
                INST_VNA.WriteString(":CALC1:MARK2:X " + textBox_MK2.Text + "E6");

            }
            if (checkBox3.Checked == true)
            {
                INST_VNA.WriteString(":CALC1:MARK3:X " + textBox_MK3.Text + "E6");

            }
            if (checkBox4.Checked == true)
            {
                INST_VNA.WriteString(":CALC1:MARK4:X " + textBox_MK4.Text + "E6");

            }
            if (checkBox5.Checked == true)
            {
                INST_VNA.WriteString(":CALC1:MARK5:X " + textBox_MK5.Text + "E6");

            }
            if (checkBox6.Checked == true)
            {
                INST_VNA.WriteString(":CALC1:MARK6:X " + textBox_MK6.Text + "E6");
            }


            for (int h = 1; h < 7; h++)
            {
                if (TESTMK[h - 1] == 1)
                {
                    INST_VNA.WriteString(":CALC1:MARK" + h.ToString() + " ON");
                    //System.Threading.Thread.Sleep(100);

                }
            }

        }

        private void textBox_Distination_TextChanged(object sender, EventArgs e)
        {
            DISTSTR = String.Format("{0:00000.00}", double.Parse(textBox_Distination.Text));
        }

        private void comboBox_velo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_velo.SelectedIndex)
            {
                case 0:
                    SPD = "SPD00000.25";
                    break;
                case 1:
                    SPD = "SPD00000.50";
                    break;
                case 2:
                    SPD = "SPD00000.75";
                    break;
                case 3:
                    SPD = "SPD00001.00";
                    break;
                case 4:
                    SPD = "SPD00001.50";
                    break;
                case 5:
                    SPD = "SPD00002.00";
                    break;

            }
            Console.WriteLine(SPD);
        }

        private void comboBox_interval_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_interval.SelectedIndex)
            {
                case 0:
                    INTVAL = 1;
                    break;
                case 1:
                    INTVAL = 5;
                    break;
                case 2:
                    INTVAL = 10;
                    break;
                case 3:
                    INTVAL = 15;
                    break;

            }

            MESPOINT = 360 / INTVAL;

            Console.WriteLine("INTERVAL,　NUMBER OF MEASUREMENT POINTS");
            Console.WriteLine(INTVAL);
            Console.WriteLine(MESPOINT);
        }

        private void button_Table_Click(object sender, EventArgs e)
        {
            if (INST_Table.IO == null)
            {
                INST_Table.IO = (IMessage)RM.Open(Table, AccessMode.NO_LOCK, 5000, "");
                INST_Table.IO.Timeout = 5000;
                INST_Table.IO.Clear();
                INST_Table.WriteString("DL0");
                INST_Table.WriteString("S1");
                INST_Table.WriteString("HD0");
                INST_Table.WriteString("VL1");
                INST_Table.WriteString("M0");

                button_Table.Text = "Table OFF";
            }
            else
            {
                INST_Table.IO.Clear();
                INST_Table.IO.Close();
                INST_Table.IO = null;

                button_Table.Text = "Table ON";
            }

        }

        private void button_VNA_Click(object sender, EventArgs e)
        {
            if (INST_VNA.IO == null)
            {
                INST_VNA.IO = (IMessage)RM.Open(E5071C, AccessMode.NO_LOCK, 5000, "");
                INST_VNA.IO.Timeout = 5000;
                button_VNA.Text = "VNA OFF";
            }
            else
            {
                INST_VNA.IO.Clear();
                INST_VNA.IO.Close();
                INST_VNA.IO = null;

                button_VNA.Text = "VNA ON";
            }
        }

        private void textBox_visaTable_TextChanged(object sender, EventArgs e)
        {
            comboBox_visaList.Items.Clear();
            SETVISA();
        }

        private void textBox_visaPol_TextChanged(object sender, EventArgs e)
        {
            comboBox_visaList.Items.Clear();
            SETVISA();
        }

        private void textBox_visaVNA_TextChanged(object sender, EventArgs e)
        {
            comboBox_visaList.Items.Clear();
            SETVISA();
        }

        private async void button_VNATEST_Click(object sender, EventArgs e)
        {
            
            if (INST_VNA.IO == null)
            {
                MessageBox.Show("VNA disconnected");
                return;
            }

            CHKNUM = Chkbox();

            await Task.Run(() => INITVNA1());


            //保存するファイルの処理
            string MeasFileName = null;

            if(POL == "PH")
            {
                MeasFileName = "dataH.csv";
            }
            else if (POL == "PV")
            {
                MeasFileName = "dataV.csv";
            }
            else
            {
                return;
            }

            saveFileDialog1.FileName = MeasFileName;
            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog1.FileName;
                data = new StreamWriter(file, true, Encoding.Default);
                MessageBox.Show("Measurement START");
            }
            else
            {
                MessageBox.Show("Measurement STOP");
                return;
            }

            //
            int omega = 0;
            double a = comboBox_velo.SelectedIndex;

            switch (a)
            {
                case 0:
                    omega = 333;
                    break;
                case 1:
                    omega = 222;
                    break;
                case 2:
                    omega = 166;
                    break;
                case 3:
                    omega = 111;
                    break;
                case 4:
                    omega = 83;
                    break;

            }

            //測定開始
            //Task t = Task.Run(() => { AsyncVNARead(); });

            try
            {
                for (MESPOS = 0; MESPOS < MESPOINT; MESPOS++)
                {
                    await Task.Run(() => readposition3(omega));
                    textBox_Angle.Text = MESPOS.ToString();
                    //Task t = Task.Run(() => { AsyncVNARead1(); });
                    AsyncVNARead1();
                }


                string temp = null;

                for (int k = 0; k < MESPOINT; k++)
                {
                    for (int l = 0; l < CHKNUM; l++)
                    {
                        if (l == 0)
                        {
                            temp = results[k, l];
                        }
                        else
                        {
                            temp = temp + "," + results[k, l];
                        }
                    }

                    data.WriteLine(temp);
                }

                data.Close();

                MessageBox.Show("END");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private int Chkbox()
        {
            int i = 0;

            if (checkBox1.Checked == true)
            {
                i++;
                TESTMK[0] = 1;
            }
            if (checkBox2.Checked == true)
            {
                i++;
                TESTMK[1] = 1;
            }
            if (checkBox3.Checked == true)
            {
                i++;
                TESTMK[2] = 1;
            }
            if (checkBox4.Checked == true)
            {
                i++;
                TESTMK[3] = 1;
            }
            if (checkBox5.Checked == true)
            {
                i++;
                TESTMK[4] = 1;
            }
            if (checkBox6.Checked == true)
            {
                i++;
                TESTMK[5] = 1;
            }
            if (i > 6)
            {
                i = 0;
            }



            return i;
        }

        private void readposition3(int speed)
        {

            System.Threading.Thread.Sleep(speed);

        }

        private void AsyncVNARead()
        {
            String[] ReadResults;

            for (int AY = 0; AY < CHKNUM; AY++)
            {
                if (TESTMK[AY] == 1)
                {
                    int measnum = AY + 1;
                    INST_VNA.WriteString(":CALC1:MARK" + measnum.ToString() + ":Y?");
                    ReadResults = INST_VNA.ReadString().Split(',');
                    Console.WriteLine(ReadResults[0]);
                    double num = double.Parse(ReadResults[0], NumberStyles.Float);
                    results[MESPOS, AY] = num.ToString();

                }
            }

        }

        public void INITVNA1()
        {
            

            if (CHKPOS == 0)
            {
                MK = textBox_MK1.Text;

            }
            else if (CHKPOS == 1)
            {
                MK = textBox_MK2.Text;

            }
            else if (CHKPOS == 2)
            {
                MK = textBox_MK3.Text;

            }
            else if (CHKPOS == 3)
            {
                MK = textBox_MK4.Text;

            }
            else if (CHKPOS == 4)
            {
                MK = textBox_MK5.Text;

            }
            else if (CHKPOS == 5)
            {
                MK = textBox_MK6.Text;

            }
            else
            {

            }


            string CENT = ":SENS1:FREQ:CENT " + MK + "E6";
            string SPN = ":SENS1:FREQ:SPAN " + "0E6";
            string POINT = ":SENS1:SWE:POIN " + "2";
            string MKX = ":CALC1:MARK1:X " + MK + "E6";
            //string POW = ":SOUR1:VPOW " + VPOW;

            INST_VNA.WriteString(CENT);
            INST_VNA.WriteString(SPN);
            INST_VNA.WriteString(POINT);

            INST_VNA.WriteString(MKX);
            INST_VNA.WriteString(":CALC1:MARK1 ON");


            //if (checkBox1.Checked == true)
            //{
            //    INST_VNA.WriteString(":CALC1:MARK1:X " + MK + "E6");
            //    INST_VNA.WriteString(":CALC1:MARK1 ON");
                

            //}
            //if (checkBox2.Checked == true)
            //{
            //    INST_VNA.WriteString(":CALC1:MARK2:X " + textBox_MK2.Text + "E6");

            //}
            //if (checkBox3.Checked == true)
            //{
            //    INST_VNA.WriteString(":CALC1:MARK3:X " + textBox_MK3.Text + "E6");

            //}
            //if (checkBox4.Checked == true)
            //{
            //    INST_VNA.WriteString(":CALC1:MARK4:X " + textBox_MK4.Text + "E6");

            //}
            //if (checkBox5.Checked == true)
            //{
            //    INST_VNA.WriteString(":CALC1:MARK5:X " + textBox_MK5.Text + "E6");

            //}
            //if (checkBox6.Checked == true)
            //{
            //    INST_VNA.WriteString(":CALC1:MARK6:X " + textBox_MK6.Text + "E6");
            //}



        }


        private void AsyncVNARead1()
        {
            String[] ReadResults;
            double num;      

            INST_VNA.WriteString(":CALC1:MARK1:Y?");
            ReadResults = INST_VNA.ReadString().Split(',');
            num = double.Parse(ReadResults[0], NumberStyles.Float);

            Console.WriteLine(ReadResults[0]);
            results[MESPOS, CHKPOS] = num.ToString();

        }

        private void textBox_FreqCenter_TextChanged(object sender, EventArgs e)
        {
            VFRQ = textBox_FreqCenter.Text;
        }

        private void textBox_PointNum_TextChanged(object sender, EventArgs e)
        {
            VPOINT = textBox_PointNum.Text;
        }

        private void textBox_FreqBandwidth_TextChanged(object sender, EventArgs e)
        {
            VSPN = textBox_FreqBandwidth.Text;
        }

        private void textBox_Power_TextChanged(object sender, EventArgs e)
        {
            VPOW = textBox_Power.Text;
        }

        //END
        private void button6_Click(object sender, EventArgs e)
        {
            if (data != null)
            {
                data.Close();
            }

            if (INST_Table.IO != null)
            {
                INST_Table.IO.Clear();
                INST_Table.IO.Close();
                INST_Table.IO = null;
            }

            if (INST_VNA.IO != null)
            {
                INST_VNA.IO.Clear();
                INST_VNA.IO.Close();
                INST_VNA.IO = null;
            }

            Application.Exit();
        }

    }
}
