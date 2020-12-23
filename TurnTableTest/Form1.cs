﻿using System;
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
        private FormattedIO488 INST_Table = new FormattedIO488();
        


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

        //結果のファイル名
        string FileName = "C:\\TEST\\test.csv";

        StreamWriter prow = new StreamWriter("C:\\TEST\\test.csv", true, Encoding.Default);

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

            DISTSTR = String.Format("{0:00000.00}", double.Parse(textBox_Distination.Text));

        }
        private void SETVISA()
        {
            string[] VISA = { Table, Tower, E5071C };
            comboBox_visaList.Items.AddRange(VISA);
            comboBox_visaList.SelectedIndex = 0;
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

                    INST_Table.IO = (IMessage)RM.Open(E5071C , AccessMode.NO_LOCK, 2000, "");
                    INST_Table.IO.Timeout = 5000;

                    INST_Table.IO.Clear();
                    INST_Table.WriteString(":SENS1:FREQ:SPAN " + "0E6", true);

                    switch (i)
                    {
                        case 1:
                            if (checkBox1.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK1.Text + "E6", true);
                                INST_Table.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST_Table.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK1 = num.ToString();
                            }

                            break;

                        case 2:
                            if (checkBox2.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK2.Text + "E6", true);
                                INST_Table.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST_Table.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK2 = num.ToString();
                            }

                            break;

                        case 3:
                            if (checkBox3.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK3.Text + "E6", true);
                                INST_Table.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST_Table.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK3 = num.ToString();
                            }

                            break;

                        case 4:
                            if (checkBox4.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK4.Text + "E6", true);
                                INST_Table.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST_Table.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK4 = num.ToString();
                            }

                            break;

                        case 5:
                            if (checkBox5.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK5.Text + "E6", true);
                                INST_Table.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST_Table.ReadString().Split(',');
                                num = double.Parse(ReadResults[0], NumberStyles.Float);
                                resultMK5 = num.ToString();
                            }

                            break;

                        case 6:
                            if (checkBox6.Checked == true)
                            {
                                System.Threading.Thread.Sleep(100);
                                INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK6.Text + "E6", true);
                                INST_Table.WriteString(":CALC1:MARK1:Y?");
                                ReadResults = INST_Table.ReadString().Split(',');
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
                INST_Table.IO.Close();
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

             TEST();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(prow != null)
            {
                prow.Close();
            }
            Application.Exit();
        }

        private async void button_startTest_Click(object sender, EventArgs e)
        {

            try
            {
                INST_Table.WriteString("DL0");
                INST_Table.WriteString("S1");
                INST_Table.WriteString("HD0");
                INST_Table.WriteString("VL1");
                INST_Table.WriteString("M0");
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

                    if(temp > 1)
                    {
                        MESPOS = MESPOINT;
                        return;
                    }

                } while (temp > 0.1);

                prow.WriteLine(CPOS);
                
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
                INST_Table.WriteString("DL0");
                INST_Table.WriteString("S1");
                INST_Table.WriteString("HD0");
                INST_Table.WriteString("VL1");
                INST_Table.WriteString("M0");
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
            INST_Table.WriteString(":SENS1:FREQ:CENT " + textBox_MK1.Text + "E6", true);
            INST_Table.WriteString(":CALC1:MARK1:Y?");
            String[] ReadResults = INST_Table.ReadString().Split(',');
            double num = double.Parse(ReadResults[0], NumberStyles.Float);
            string resultMK1 = num.ToString();
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

            prow.WriteLine(CPOS);
            return CPOS;

        }

        private void button_Table_Click(object sender, EventArgs e)
        {
            if(INST_Table.IO == null)
            {
                INST_Table.IO = (IMessage)RM.Open(Table, AccessMode.NO_LOCK, 5000, "");
                INST_Table.IO.Timeout = 5000;
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
    }
}
