namespace TurnTableTest
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_visaTable = new System.Windows.Forms.TextBox();
            this.textBox_visaPol = new System.Windows.Forms.TextBox();
            this.textBox_visaVNA = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_SENDCMD = new System.Windows.Forms.Button();
            this.comboBox_visaList = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.button_SETVISA = new System.Windows.Forms.Button();
            this.textBox_CMD = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_VNASET = new System.Windows.Forms.Button();
            this.textBox_Power = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_FreqCenter = new System.Windows.Forms.TextBox();
            this.textBox_FreqBandwidth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_PointNum = new System.Windows.Forms.TextBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.textBox_MK5 = new System.Windows.Forms.TextBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox_MK4 = new System.Windows.Forms.TextBox();
            this.textBox_MK1 = new System.Windows.Forms.TextBox();
            this.textBox_MK2 = new System.Windows.Forms.TextBox();
            this.textBox_MK3 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Move2 = new System.Windows.Forms.Button();
            this.textBox_Angle = new System.Windows.Forms.TextBox();
            this.comboBox_interval = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox_velo = new System.Windows.Forms.ComboBox();
            this.button_SetTable = new System.Windows.Forms.Button();
            this.button_Move = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_Distination = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_VALUE = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_Vel = new System.Windows.Forms.CheckBox();
            this.checkBox_Hol = new System.Windows.Forms.CheckBox();
            this.button_SetPol = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button_startMeas = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.textBox_MK6 = new System.Windows.Forms.TextBox();
            this.button_startTest = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button_Table = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Polarity";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "VNA";
            // 
            // textBox_visaTable
            // 
            this.textBox_visaTable.Location = new System.Drawing.Point(72, 18);
            this.textBox_visaTable.Name = "textBox_visaTable";
            this.textBox_visaTable.Size = new System.Drawing.Size(223, 19);
            this.textBox_visaTable.TabIndex = 3;
            this.textBox_visaTable.Text = "GPIB0::2::INSTR";
            // 
            // textBox_visaPol
            // 
            this.textBox_visaPol.Location = new System.Drawing.Point(72, 43);
            this.textBox_visaPol.Name = "textBox_visaPol";
            this.textBox_visaPol.Size = new System.Drawing.Size(223, 19);
            this.textBox_visaPol.TabIndex = 4;
            this.textBox_visaPol.Text = "GPIB0::5::INSTR";
            // 
            // textBox_visaVNA
            // 
            this.textBox_visaVNA.Location = new System.Drawing.Point(72, 68);
            this.textBox_visaVNA.Name = "textBox_visaVNA";
            this.textBox_visaVNA.Size = new System.Drawing.Size(224, 19);
            this.textBox_visaVNA.TabIndex = 5;
            this.textBox_visaVNA.Text = "USB0::0x0957::0x0D09::MY46101208::0::INSTR";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_SENDCMD);
            this.groupBox1.Controls.Add(this.comboBox_visaList);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.button_SETVISA);
            this.groupBox1.Controls.Add(this.textBox_CMD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.textBox_visaTable);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_visaPol);
            this.groupBox1.Controls.Add(this.textBox_visaVNA);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 174);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "VISA";
            // 
            // button_SENDCMD
            // 
            this.button_SENDCMD.Location = new System.Drawing.Point(196, 144);
            this.button_SENDCMD.Name = "button_SENDCMD";
            this.button_SENDCMD.Size = new System.Drawing.Size(100, 24);
            this.button_SENDCMD.TabIndex = 8;
            this.button_SENDCMD.Text = "SEND";
            this.button_SENDCMD.UseVisualStyleBackColor = true;
            this.button_SENDCMD.Click += new System.EventHandler(this.button_SENDCMD_Click);
            // 
            // comboBox_visaList
            // 
            this.comboBox_visaList.FormattingEnabled = true;
            this.comboBox_visaList.Location = new System.Drawing.Point(72, 93);
            this.comboBox_visaList.Name = "comboBox_visaList";
            this.comboBox_visaList.Size = new System.Drawing.Size(223, 20);
            this.comboBox_visaList.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 12);
            this.label13.TabIndex = 9;
            this.label13.Text = "COMMAND";
            // 
            // button_SETVISA
            // 
            this.button_SETVISA.Location = new System.Drawing.Point(72, 144);
            this.button_SETVISA.Name = "button_SETVISA";
            this.button_SETVISA.Size = new System.Drawing.Size(100, 24);
            this.button_SETVISA.TabIndex = 29;
            this.button_SETVISA.Text = "SET";
            this.button_SETVISA.UseVisualStyleBackColor = true;
            this.button_SETVISA.Click += new System.EventHandler(this.button_SETVISA_Click);
            // 
            // textBox_CMD
            // 
            this.textBox_CMD.Location = new System.Drawing.Point(72, 119);
            this.textBox_CMD.Name = "textBox_CMD";
            this.textBox_CMD.Size = new System.Drawing.Size(223, 19);
            this.textBox_CMD.TabIndex = 7;
            this.textBox_CMD.Text = "*IDN?";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 96);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(31, 12);
            this.label14.TabIndex = 6;
            this.label14.Text = "VISA";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_VNASET);
            this.groupBox2.Controls.Add(this.textBox_Power);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_FreqCenter);
            this.groupBox2.Controls.Add(this.textBox_FreqBandwidth);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_PointNum);
            this.groupBox2.Location = new System.Drawing.Point(320, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 93);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VNA";
            // 
            // button_VNASET
            // 
            this.button_VNASET.Location = new System.Drawing.Point(183, 62);
            this.button_VNASET.Name = "button_VNASET";
            this.button_VNASET.Size = new System.Drawing.Size(109, 24);
            this.button_VNASET.TabIndex = 8;
            this.button_VNASET.Text = "SET";
            this.button_VNASET.UseVisualStyleBackColor = true;
            this.button_VNASET.Click += new System.EventHandler(this.button_VNASET_Click);
            // 
            // textBox_Power
            // 
            this.textBox_Power.Location = new System.Drawing.Point(249, 37);
            this.textBox_Power.Name = "textBox_Power";
            this.textBox_Power.Size = new System.Drawing.Size(40, 19);
            this.textBox_Power.TabIndex = 12;
            this.textBox_Power.Text = "0";
            this.textBox_Power.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(160, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "Powe[dBm]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(160, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "Point";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "Bandwidth[MHz]";
            // 
            // textBox_FreqCenter
            // 
            this.textBox_FreqCenter.Location = new System.Drawing.Point(100, 12);
            this.textBox_FreqCenter.Name = "textBox_FreqCenter";
            this.textBox_FreqCenter.Size = new System.Drawing.Size(40, 19);
            this.textBox_FreqCenter.TabIndex = 6;
            this.textBox_FreqCenter.Text = "4600";
            this.textBox_FreqCenter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_FreqBandwidth
            // 
            this.textBox_FreqBandwidth.Location = new System.Drawing.Point(100, 38);
            this.textBox_FreqBandwidth.Name = "textBox_FreqBandwidth";
            this.textBox_FreqBandwidth.Size = new System.Drawing.Size(40, 19);
            this.textBox_FreqBandwidth.TabIndex = 7;
            this.textBox_FreqBandwidth.Text = "100";
            this.textBox_FreqBandwidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Frequency[MHz]";
            // 
            // textBox_PointNum
            // 
            this.textBox_PointNum.Location = new System.Drawing.Point(249, 12);
            this.textBox_PointNum.Name = "textBox_PointNum";
            this.textBox_PointNum.Size = new System.Drawing.Size(40, 19);
            this.textBox_PointNum.TabIndex = 8;
            this.textBox_PointNum.Text = "801";
            this.textBox_PointNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(406, 18);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(46, 16);
            this.checkBox5.TabIndex = 26;
            this.checkBox5.Text = "MK5";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // textBox_MK5
            // 
            this.textBox_MK5.Location = new System.Drawing.Point(458, 16);
            this.textBox_MK5.Name = "textBox_MK5";
            this.textBox_MK5.Size = new System.Drawing.Size(40, 19);
            this.textBox_MK5.TabIndex = 25;
            this.textBox_MK5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(306, 17);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(46, 16);
            this.checkBox4.TabIndex = 24;
            this.checkBox4.Text = "MK4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(206, 17);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(46, 16);
            this.checkBox3.TabIndex = 23;
            this.checkBox3.Text = "MK3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(106, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(46, 16);
            this.checkBox2.TabIndex = 22;
            this.checkBox2.Text = "MK2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(8, 18);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(46, 16);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "MK1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox_MK4
            // 
            this.textBox_MK4.Location = new System.Drawing.Point(358, 16);
            this.textBox_MK4.Name = "textBox_MK4";
            this.textBox_MK4.Size = new System.Drawing.Size(40, 19);
            this.textBox_MK4.TabIndex = 20;
            this.textBox_MK4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_MK1
            // 
            this.textBox_MK1.Location = new System.Drawing.Point(58, 16);
            this.textBox_MK1.Name = "textBox_MK1";
            this.textBox_MK1.Size = new System.Drawing.Size(40, 19);
            this.textBox_MK1.TabIndex = 13;
            this.textBox_MK1.Text = "4600";
            this.textBox_MK1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_MK2
            // 
            this.textBox_MK2.Location = new System.Drawing.Point(158, 16);
            this.textBox_MK2.Name = "textBox_MK2";
            this.textBox_MK2.Size = new System.Drawing.Size(40, 19);
            this.textBox_MK2.TabIndex = 15;
            this.textBox_MK2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_MK3
            // 
            this.textBox_MK3.Location = new System.Drawing.Point(258, 16);
            this.textBox_MK3.Name = "textBox_MK3";
            this.textBox_MK3.Size = new System.Drawing.Size(40, 19);
            this.textBox_MK3.TabIndex = 16;
            this.textBox_MK3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button_Move2);
            this.groupBox3.Controls.Add(this.textBox_Angle);
            this.groupBox3.Controls.Add(this.comboBox_interval);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.comboBox_velo);
            this.groupBox3.Controls.Add(this.button_SetTable);
            this.groupBox3.Controls.Add(this.button_Move);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.textBox_Distination);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Location = new System.Drawing.Point(320, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 127);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Table";
            // 
            // button_Move2
            // 
            this.button_Move2.Location = new System.Drawing.Point(190, 57);
            this.button_Move2.Name = "button_Move2";
            this.button_Move2.Size = new System.Drawing.Size(100, 26);
            this.button_Move2.TabIndex = 30;
            this.button_Move2.Text = "GO";
            this.button_Move2.UseVisualStyleBackColor = true;
            this.button_Move2.Click += new System.EventHandler(this.button_Move2_Click);
            // 
            // textBox_Angle
            // 
            this.textBox_Angle.Location = new System.Drawing.Point(72, 18);
            this.textBox_Angle.Name = "textBox_Angle";
            this.textBox_Angle.Size = new System.Drawing.Size(112, 19);
            this.textBox_Angle.TabIndex = 8;
            // 
            // comboBox_interval
            // 
            this.comboBox_interval.FormattingEnabled = true;
            this.comboBox_interval.Location = new System.Drawing.Point(72, 94);
            this.comboBox_interval.Name = "comboBox_interval";
            this.comboBox_interval.Size = new System.Drawing.Size(112, 20);
            this.comboBox_interval.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "Angle";
            // 
            // comboBox_velo
            // 
            this.comboBox_velo.FormattingEnabled = true;
            this.comboBox_velo.Location = new System.Drawing.Point(72, 68);
            this.comboBox_velo.Name = "comboBox_velo";
            this.comboBox_velo.Size = new System.Drawing.Size(112, 20);
            this.comboBox_velo.TabIndex = 11;
            // 
            // button_SetTable
            // 
            this.button_SetTable.Location = new System.Drawing.Point(190, 93);
            this.button_SetTable.Name = "button_SetTable";
            this.button_SetTable.Size = new System.Drawing.Size(100, 26);
            this.button_SetTable.TabIndex = 28;
            this.button_SetTable.Text = "SET";
            this.button_SetTable.UseVisualStyleBackColor = true;
            this.button_SetTable.Click += new System.EventHandler(this.button_SetTable_Click);
            // 
            // button_Move
            // 
            this.button_Move.Location = new System.Drawing.Point(190, 18);
            this.button_Move.Name = "button_Move";
            this.button_Move.Size = new System.Drawing.Size(100, 26);
            this.button_Move.TabIndex = 27;
            this.button_Move.Text = "GO";
            this.button_Move.UseVisualStyleBackColor = true;
            this.button_Move.Click += new System.EventHandler(this.button_Move_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Distination";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "Interval";
            // 
            // textBox_Distination
            // 
            this.textBox_Distination.Location = new System.Drawing.Point(72, 43);
            this.textBox_Distination.Name = "textBox_Distination";
            this.textBox_Distination.Size = new System.Drawing.Size(112, 19);
            this.textBox_Distination.TabIndex = 3;
            this.textBox_Distination.Text = "0";
            this.textBox_Distination.TextChanged += new System.EventHandler(this.textBox_Distination_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "Velocity";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 12);
            this.label12.TabIndex = 7;
            this.label12.Text = "Value";
            // 
            // textBox_VALUE
            // 
            this.textBox_VALUE.Location = new System.Drawing.Point(73, 18);
            this.textBox_VALUE.Name = "textBox_VALUE";
            this.textBox_VALUE.Size = new System.Drawing.Size(525, 19);
            this.textBox_VALUE.TabIndex = 9;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox_Vel);
            this.groupBox5.Controls.Add(this.checkBox_Hol);
            this.groupBox5.Controls.Add(this.button_SetPol);
            this.groupBox5.Location = new System.Drawing.Point(12, 192);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(302, 46);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Polarity";
            // 
            // checkBox_Vel
            // 
            this.checkBox_Vel.AutoSize = true;
            this.checkBox_Vel.Location = new System.Drawing.Point(87, 21);
            this.checkBox_Vel.Name = "checkBox_Vel";
            this.checkBox_Vel.Size = new System.Drawing.Size(64, 16);
            this.checkBox_Vel.TabIndex = 28;
            this.checkBox_Vel.Text = "Vertical";
            this.checkBox_Vel.UseVisualStyleBackColor = true;
            // 
            // checkBox_Hol
            // 
            this.checkBox_Hol.AutoSize = true;
            this.checkBox_Hol.Location = new System.Drawing.Point(6, 20);
            this.checkBox_Hol.Name = "checkBox_Hol";
            this.checkBox_Hol.Size = new System.Drawing.Size(75, 16);
            this.checkBox_Hol.TabIndex = 27;
            this.checkBox_Hol.Text = "Horizontal";
            this.checkBox_Hol.UseVisualStyleBackColor = true;
            // 
            // button_SetPol
            // 
            this.button_SetPol.Location = new System.Drawing.Point(195, 15);
            this.button_SetPol.Name = "button_SetPol";
            this.button_SetPol.Size = new System.Drawing.Size(100, 24);
            this.button_SetPol.TabIndex = 8;
            this.button_SetPol.Text = "Polarity";
            this.button_SetPol.UseVisualStyleBackColor = true;
            this.button_SetPol.Click += new System.EventHandler(this.button_SetPol_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox_VALUE);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Location = new System.Drawing.Point(11, 299);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(609, 50);
            this.groupBox6.TabIndex = 28;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Status";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(512, 355);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(100, 45);
            this.button6.TabIndex = 10;
            this.button6.Text = "END";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button_startMeas
            // 
            this.button_startMeas.Location = new System.Drawing.Point(406, 355);
            this.button_startMeas.Name = "button_startMeas";
            this.button_startMeas.Size = new System.Drawing.Size(100, 45);
            this.button_startMeas.TabIndex = 30;
            this.button_startMeas.Text = "Mesurement";
            this.button_startMeas.UseVisualStyleBackColor = true;
            this.button_startMeas.Click += new System.EventHandler(this.button_startMeas_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.checkBox6);
            this.groupBox7.Controls.Add(this.textBox_MK6);
            this.groupBox7.Controls.Add(this.textBox_MK1);
            this.groupBox7.Controls.Add(this.checkBox5);
            this.groupBox7.Controls.Add(this.checkBox1);
            this.groupBox7.Controls.Add(this.textBox_MK5);
            this.groupBox7.Controls.Add(this.checkBox2);
            this.groupBox7.Controls.Add(this.checkBox4);
            this.groupBox7.Controls.Add(this.textBox_MK4);
            this.groupBox7.Controls.Add(this.textBox_MK2);
            this.groupBox7.Controls.Add(this.checkBox3);
            this.groupBox7.Controls.Add(this.textBox_MK3);
            this.groupBox7.Location = new System.Drawing.Point(11, 244);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(610, 49);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "MEASUREMENTS";
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(506, 18);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(46, 16);
            this.checkBox6.TabIndex = 28;
            this.checkBox6.Text = "MK6";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // textBox_MK6
            // 
            this.textBox_MK6.Location = new System.Drawing.Point(558, 16);
            this.textBox_MK6.Name = "textBox_MK6";
            this.textBox_MK6.Size = new System.Drawing.Size(40, 19);
            this.textBox_MK6.TabIndex = 27;
            this.textBox_MK6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_startTest
            // 
            this.button_startTest.Location = new System.Drawing.Point(300, 355);
            this.button_startTest.Name = "button_startTest";
            this.button_startTest.Size = new System.Drawing.Size(100, 45);
            this.button_startTest.TabIndex = 32;
            this.button_startTest.Text = "TEST";
            this.button_startTest.UseVisualStyleBackColor = true;
            this.button_startTest.Click += new System.EventHandler(this.button_startTest_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 33;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(92, 377);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 34;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button_Table
            // 
            this.button_Table.Location = new System.Drawing.Point(173, 377);
            this.button_Table.Name = "button_Table";
            this.button_Table.Size = new System.Drawing.Size(75, 23);
            this.button_Table.TabIndex = 35;
            this.button_Table.Text = "Table";
            this.button_Table.UseVisualStyleBackColor = true;
            this.button_Table.Click += new System.EventHandler(this.button_Table_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 413);
            this.Controls.Add(this.button_Table);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_startTest);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.button_startMeas);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_visaTable;
        private System.Windows.Forms.TextBox textBox_visaPol;
        private System.Windows.Forms.TextBox textBox_visaVNA;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_Power;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_FreqCenter;
        private System.Windows.Forms.TextBox textBox_FreqBandwidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_PointNum;
        private System.Windows.Forms.Button button_VNASET;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.TextBox textBox_MK5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox_MK4;
        private System.Windows.Forms.TextBox textBox_MK1;
        private System.Windows.Forms.TextBox textBox_MK2;
        private System.Windows.Forms.TextBox textBox_MK3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_SetTable;
        private System.Windows.Forms.Button button_Move;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_Distination;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBox_visaList;
        private System.Windows.Forms.Button button_SENDCMD;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox_CMD;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_Angle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_VALUE;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox checkBox_Vel;
        private System.Windows.Forms.CheckBox checkBox_Hol;
        private System.Windows.Forms.Button button_SetPol;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button_startMeas;
        private System.Windows.Forms.ComboBox comboBox_velo;
        private System.Windows.Forms.Button button_SETVISA;
        private System.Windows.Forms.ComboBox comboBox_interval;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.TextBox textBox_MK6;
        private System.Windows.Forms.Button button_startTest;
        private System.Windows.Forms.Button button_Move2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button_Table;
    }
}

