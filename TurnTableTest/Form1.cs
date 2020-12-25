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
        string POL;

        //VALUE
        string VFRQ;
        string VSPN;
        string VPOW;
        string VPOINT;

        int INTVAL = 0;
        int MESPOINT;
        int MESPOS;

        // アンテナ向き
        bool polarity = false;

        //Table position
        string DISTSTR; //distination
        string CPOS;

        //結果のファイル
        StreamWriter dataH = new StreamWriter("C:\\TEST\\test.csv", true, Encoding.Default);
        StreamWriter dataV = new StreamWriter("C:\\TEST\\testV.csv", true, Encoding.Default);

        string[] results = new string[500];

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
            string[] VELO = { "0.5", "0.75", "1.0", "1.5", "2.0" };
            comboBox_velo.Items.AddRange(VELO);
            comboBox_velo.SelectedIndex = 0;
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

        //---------------------------------------------------------------------------

        private void button_VNASET_Click(object sender, EventArgs e)
        {
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

        //--------------------------------------------------------------------------

        private void button_SENDCMD_Click(object sender, EventArgs e)
        {
            string CMD = textBox_CMD.Text;

            try
            {
                var session = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(comboBox_visaList.SelectedItem.ToString());

                session.FormattedIO.WriteLine(CMD);
                textBox_VALUE.Text= session.FormattedIO.ReadLine();    

                session.Dispose();
                session = null;
            }
            catch
            {

            }
        }


        private void button_SetPol_Click(object sender, EventArgs e)
        {

            if (polarity == false)
            {
                POL = "PH";
                button_SetPol.Text = "Horizontal";
                polarity = true;
            }
            else
            {
                POL = "PV";
                button_SetPol.Text = "Vertical";
                polarity = false;
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






        private void button_startMeas_Click(object sender, EventArgs e)
        {

             TEST();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(dataH != null)
            {
                dataH.Close();
            }

            if (dataV != null)
            {
                dataV.Close();
            }

            if (INST_Table.IO != null)
            {
                INST_Table.IO.Clear();
                INST_Table.IO.Close();
                INST_Table.IO = null;
            }

            if(INST_VNA.IO != null)
            {
                INST_VNA.IO.Clear();
                INST_VNA.IO.Close();
                INST_VNA.IO = null;
            }

            Application.Exit();
        }

        private async void button_startTest_Click(object sender, EventArgs e)
        {
            if(INST_Table.IO == null)
            {
                MessageBox.Show("Table disconnected");
                return;
            }

            if(INST_VNA.IO == null)
            {
                MessageBox.Show("VNA disconnected");
                return;
            }


            try
            {

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


                //回転開始
                INST_Table.WriteString("CWP00370.00");


                for (MESPOS = 0; MESPOS < MESPOINT; MESPOS++)
                {
                    await Task.Run(() => readposition2());
                    textBox_Angle.Text = CPOS;

                }

                MessageBox.Show("TEST END");
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

                    temp = Math.Abs(double.Parse(CPOS) - MESPOS);

                    //差分が10以上で強制終了
                    if(temp > 10)
                    {
                        MESPOS = MESPOINT;
                        return;
                    }

                } while (temp > 0.1);　//差分が0.1以下になるまでループ

                dataH.WriteLine(CPOS);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void TEST()
        {

            try
            {
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


                //回転開始
                INST_Table.WriteString("CWP00370.00");


                for (MESPOS = 0; MESPOS < MESPOINT; MESPOS++)
                {
                    await Task.Run(() => readposition2());
                    //Task<string> t1 = Task.Run(() => { return AsyncWork1(); });
                    Task t = Task.Run(() => { AsyncWork(); });
                    //textBox_Angle.Text = t1.Result;
                    textBox_Angle.Text = CPOS;

                }

                MessageBox.Show("TEST END");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void AsyncWork()
        {
            INST_VNA.WriteString(":SENS1:FREQ:CENT " + textBox_MK1.Text + "E6", true);
            INST_VNA.WriteString(":CALC1:MARK1 ON");
            INST_VNA.WriteString(":CALC1: MARK1:X " + textBox_MK1.Text + "E6");
            INST_VNA.WriteString(":CALC1:MARK1:Y?");
            String[] ReadMK = INST_VNA.ReadString().Split(',');
            double num = double.Parse(ReadMK[0], NumberStyles.Float);
            string resultMK = num.ToString();

            if(POL == "PH")
            {
                dataH.WriteLine(resultMK);
            }
        }


        public string AsyncWork1()
        {

            double temp;

            do
            {
                INST_Table.WriteString("CP");
                CPOS = INST_Table.ReadString().Substring(0, 8);
                Console.WriteLine(CPOS);

                temp = Math.Abs(double.Parse(CPOS) - MESPOS);

                if (temp > 1)
                {
                    MESPOS = MESPOINT;
                    return CPOS;
                }

            } while (temp > 0.1);

            dataH.WriteLine(CPOS);
            return CPOS;

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
                    SPD = "SPD00000.50";
                    break;
                case 1:
                    SPD = "SPD00000.75";
                    break;
                case 2:
                    SPD = "SPD00001.00";
                    break;
                case 3:
                    SPD = "SPD00001.50";
                    break;
                case 4:
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
            string START = ":SENS1:FREQ:STAR " + textBox_MK1.Text + "E6";
            string END = ":SENS1:FREQ:STOP " + textBox_MK2.Text + "E6";
            string SPN = ":SENS1:FREQ:SPAN " + "0" + "E6";
            string POINT = ":SENS1:SWE:POIN " + "201";
            string POW = ":SOUR1:VPOW " + VPOW;
            

            if (INST_VNA.IO == null)
            {
                MessageBox.Show("VNA disconnected");
                return;
            }

            INST_VNA.WriteString(START);
            INST_VNA.WriteString(END);
            INST_VNA.WriteString(POINT);
            INST_VNA.WriteString(":CALC1:MARK1 ON");
            INST_VNA.WriteString(":CALC1:MARK1:X " + textBox_MK1.Text + "E6");
            INST_VNA.WriteString(":CALC1:MARK2:X " + textBox_MK2.Text + "E6");


            System.Threading.Thread.Sleep(1000);
            MessageBox.Show("START");

            try
            {

                for (MESPOS = 0; MESPOS < MESPOINT; MESPOS++)
                {
                    await Task.Run(() => readposition3());
                    textBox_Angle.Text = MESPOS.ToString();
                    Task t = Task.Run(() => { AsyncVNARead(); });

                }

                for (int k = 0; k < 360; k++)
                {

                    dataH.WriteLine(results[k]);

                }

                MessageBox.Show("END");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void readposition3()
        {
            System.Threading.Thread.Sleep(100);
        }

        private void AsyncVNARead()
        {
            INST_VNA.WriteString(":CALC1:MARK1:Y?");
           
            String[] ReadResults = INST_VNA.ReadString().Split(',');
            Console.WriteLine(ReadResults[0]);
            double num = double.Parse(ReadResults[0], NumberStyles.Float);

            INST_VNA.WriteString(":CALC1:MARK2:Y?");

            String[] ReadResults2 = INST_VNA.ReadString().Split(',');
            Console.WriteLine(ReadResults2[0]);
            double num2 = double.Parse(ReadResults2[0], NumberStyles.Float);

            results[MESPOS] = num.ToString() + "," + num2.ToString();
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

    }
}
