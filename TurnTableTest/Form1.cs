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


namespace TurnTableTest
{
    public partial class Form1 : Form
    {
        private ResourceManager RM = new ResourceManager();
        private FormattedIO488 INST = new FormattedIO488();

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

        // アンテナ向き
        bool polarity = false;

        //結果のファイル名
        string FileName;

        public Form1()
        {
            InitializeComponent();

            //VISA
            E5071C = textBox_visaVNA.Text;
            Table = textBox_visaTable.Text;
            Tower = textBox_visaPol.Text;

            VFRQ = textBox_FreqCenter.Text;
            VSPN = textBox_FreqBandwidth.Text;
            VPOW = textBox_Power.Text;
            VPOINT = textBox_PointNum.Text;
            

        }
        private void SETVISA()
        {
            string[] VISA = { Table, Tower, E5071C };
            comboBox_visaList.Items.AddRange(VISA);
            comboBox_visaList.SelectedIndex = 2;
        }
        private void SETVELO()
        {
            string[] VELO = { "0.5", "0.75", "1.0", "1.5", "2.0" };
            comboBox_velo.Items.AddRange(VELO);
            comboBox_velo.SelectedIndex = 0;
            SPD = "SPD00000.50";
        }

        private void DIVIDE()
        {
            string[] DIVE = { "1", "5", "10", "15" };
            comboBox_interval.Items.AddRange(DIVE);
            comboBox_interval.SelectedIndex = 0;
            INTVAL = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SETVISA();
            SETVELO();
            DIVIDE();

        }

        //---------------------------------------------------------------------------

        private void button_VNASET_Click(object sender, EventArgs e)
        {
            string FRQ = ":SENS1:FREQ:CENT " + VFRQ + "E6";
            string SPN = ":SENS1:FREQ:SPAN " + VSPN + "E6";
            string POINT = ":SENS1:SWE:POIN " + VPOINT;
            string POW = ":SOUR1:VPOW " + VPOW;

            try
            {
                var session = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(E5071C);

                session.FormattedIO.WriteLine(FRQ);
                session.FormattedIO.WriteLine(SPN);
                session.FormattedIO.WriteLine(POINT);
                session.FormattedIO.WriteLine(POW);
                session.Dispose();
                session = null;
            }
            catch
            {

            }

        }

        private void button_Move_Click(object sender, EventArgs e)
        {
            double POSITION;
            double DISTINATION = double.Parse(textBox_Distination.Text);
            double SPAN;
            string DISTSTR, POSISTR;

            try
            {
                var session = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(Table);


                session.FormattedIO.WriteLine("SPD00002.00");
                session.FormattedIO.WriteLine("CP");
                POSISTR = session.FormattedIO.ReadLine();
                POSITION = double.Parse(POSISTR);

                SPAN = DISTINATION - POSITION;
                if (SPAN < 0)
                {
                    SPAN = SPAN + 360;
                }

                DISTSTR = "CWP" + String.Format("00000.00", SPAN);
                session.FormattedIO.WriteLine(DISTSTR);

                while (POSISTR == textBox_Distination.Text)
                {
                    session.FormattedIO.WriteLine("CP");
                    POSISTR = session.FormattedIO.ReadLine();
                    textBox_Angle.Text = POSISTR;
                }

                session.FormattedIO.WriteLine("ST");
                session.FormattedIO.WriteLine(SPD);
                session.Dispose();
                session = null;
            }
            catch
            {

            }

        }

        private void button_SETVISA_Click(object sender, EventArgs e)
        {
            comboBox_visaList.Items.Clear();
            SETVISA();
        }

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

        private void button_SetTable_Click(object sender, EventArgs e)
        {
            SPD = "SPD00000.00";
            try
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

                var session = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(Table);

                session.FormattedIO.WriteLine(SPD);

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


        private void TEST()
        {
            double CPOS;
            
            try
            {   //偏波
                var session_POL = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(Tower);
                session_POL.FormattedIO.WriteLine(POL);
                session_POL.Dispose();
                session_POL = null;

                string VSPN = ":SENS1:FREQ:SPAN 0E6";
                string MK = ":CALC1:MARK1 ON";


                //VNAセット
                var session_VNA = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(E5071C);
                session_VNA.FormattedIO.WriteLine(VSPN);
                session_VNA.FormattedIO.WriteLine(MK);

                session_VNA.Dispose();
                session_VNA = null;


                //回転台初期値セット
                this.button_SetTable.PerformClick();

                //回転台接続開始
                var session_turn = (Ivi.Visa.IMessageBasedSession)
                Ivi.Visa.GlobalResourceManager.Open(Table);
                session_turn.FormattedIO.WriteLine("DL0");
                session_turn.FormattedIO.WriteLine("S1");
                session_turn.FormattedIO.WriteLine("HD0");
                session_turn.FormattedIO.WriteLine("VL1");
                session_turn.FormattedIO.WriteLine("M0");
                session_turn.FormattedIO.WriteLine("CP");

                //0位置移動
                string AG = session_turn.FormattedIO.ReadLine();

                if (AG != "0")
                {
                    textBox_Distination.Text = "0";
                    this.button_Move.PerformClick();

                }

                //逆回転
                session_turn.FormattedIO.WriteLine("CCP00010.00");
                do
                {
                    session_turn.FormattedIO.WriteLine("CP");
                    AG = session_turn.FormattedIO.ReadLine();
                    textBox_Angle.Text = AG;

                } while (AG != "350");
                session_turn.FormattedIO.WriteLine("ST");

                //回転開始
                session_turn.FormattedIO.WriteLine("CWP00370.00");
                do
                {
                    session_turn.FormattedIO.WriteLine("CP");
                    AG = session_turn.FormattedIO.ReadLine();
                    textBox_Angle.Text = AG;
                    CPOS = double.Parse(AG);

                } while (CPOS < 1.0 || CPOS > 359.0);

                //測定開始
                for(int i = 0; i < MESPOINT; i++)
                {
                    do
                    {
                        session_turn.FormattedIO.WriteLine("CP");
                        AG = session_turn.FormattedIO.ReadLine();
                        textBox_Angle.Text = AG;
                        CPOS = double.Parse(AG);

                        if (Math.Abs(i * INTVAL - CPOS) <= 0.2)
                        {
                            VNAMEAS();
                        }

                        if(Math.Abs(i * INTVAL - CPOS) > 2)
                        {
                            break;
                        }
                    }
                    while (CPOS > i * INTVAL);
                }

                session_turn.Dispose();
                session_turn = null;

            }
            catch
            {

            }


        }

        private void VNAMEAS()
        {
            string resultMK1 = null;
            string resultMK2 = null;
            string resultMK3 = null;
            string resultMK4 = null;
            string resultMK5 = null;
            string resultMK6 = null;
            string TEXTDATA = null;
            string[] ReadResults;
            double num;

            try
            {


                for (int i = 1; i <7; i++)
                {

                    string loop = i.ToString();

                    INST.IO = (IMessage)RM.Open(E5071C , AccessMode.NO_LOCK, 2000, "");
                    INST.IO.Timeout = 5000;

                    INST.IO.Clear();
                    INST.WriteString(":SENS1:FREQ:SPAN " + "0E6", true);

                    switch (i)
                    {
                        case 1:
                            if (checkBox1.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST.WriteString(":SENS1:FREQ:CENT " + textBox_MK1.Text + "E6", true);
                                INST.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK1 = num.ToString();
                            }

                            break;

                        case 2:
                            if (checkBox2.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST.WriteString(":SENS1:FREQ:CENT " + textBox_MK2.Text + "E6", true);
                                INST.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK2 = num.ToString();
                            }

                            break;

                        case 3:
                            if (checkBox3.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST.WriteString(":SENS1:FREQ:CENT " + textBox_MK3.Text + "E6", true);
                                INST.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK3 = num.ToString();
                            }

                            break;

                        case 4:
                            if (checkBox4.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST.WriteString(":SENS1:FREQ:CENT " + textBox_MK4.Text + "E6", true);
                                INST.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK4 = num.ToString();
                            }

                            break;

                        case 5:
                            if (checkBox5.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST.WriteString(":SENS1:FREQ:CENT " + textBox_MK5.Text + "E6", true);
                                INST.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK5 = num.ToString();
                            }

                            break;

                        case 6:
                            if (checkBox6.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST.WriteString(":SENS1:FREQ:CENT " + textBox_MK6.Text + "E6", true);
                                INST.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK6 = num.ToString();
                            }

                            break;



                    }

                }
            }

            catch
            {

            }
            finally
            {
                INST.IO.Close();
            }

            

            string[] results = { textBox_MK1.Text, resultMK1, textBox_MK2.Text, resultMK2, textBox_MK3.Text, resultMK3,
            textBox_MK4.Text, resultMK4, textBox_MK5.Text, resultMK5, textBox_MK6.Text, resultMK6};

            StreamWriter prow = new StreamWriter(FileName, true, Encoding.Default);
            for (int k = 0; k < results.Length; k++)
            {
                if (k == 0)
                {
                    TEXTDATA = results[k] + ",";
                }
                else
                {
                    TEXTDATA = TEXTDATA + results[k] + ",";
                }

            }
            prow.WriteLine(TEXTDATA);
            prow.Close();


        }

        private void button_startMeas_Click(object sender, EventArgs e)
        {

            //回転回数　1回か2回かを判定
            if (checkBox_Hol.Checked == true)
            {
                POL = "PH";
                FileName = "C:\\TEST\\patternH.csv";
                TEST();
            }

            if (checkBox_Vel.Checked == true)
            {
                POL = "PB";
                FileName = "C:\\TEST\\patternV.csv";
                TEST();
            }

  
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
