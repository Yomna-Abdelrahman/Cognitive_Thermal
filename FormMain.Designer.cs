namespace IPC2
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonFlagRenew = new System.Windows.Forms.Button();
            this.labelFlag = new System.Windows.Forms.Label();
            this.labelFlag1 = new System.Windows.Forms.Label();
            this.labelPIF = new System.Windows.Forms.Label();
            this.labelFrameCounter = new System.Windows.Forms.Label();
            this.labelTempTarget = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.labelInstanceName = new System.Windows.Forms.Label();
            this.textBoxInstanceName = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.loadChartButton = new System.Windows.Forms.Button();
            this.saveTask = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newParticipantButton = new System.Windows.Forms.Button();
            this.saveParticipant = new System.Windows.Forms.Button();
            this.participantNumberTextbox = new System.Windows.Forms.TextBox();
            this.refreshChartButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(460, 11);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(382, 288);
            this.pictureBox.TabIndex = 84;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            // 
            // buttonFlagRenew
            // 
            this.buttonFlagRenew.Location = new System.Drawing.Point(118, 125);
            this.buttonFlagRenew.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonFlagRenew.Name = "buttonFlagRenew";
            this.buttonFlagRenew.Size = new System.Drawing.Size(60, 23);
            this.buttonFlagRenew.TabIndex = 86;
            this.buttonFlagRenew.Text = "Flag";
            this.buttonFlagRenew.UseVisualStyleBackColor = true;
            this.buttonFlagRenew.Click += new System.EventHandler(this.buttonFlagRenew_Click);
            // 
            // labelFlag
            // 
            this.labelFlag.AutoSize = true;
            this.labelFlag.Location = new System.Drawing.Point(50, 130);
            this.labelFlag.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFlag.Name = "labelFlag";
            this.labelFlag.Size = new System.Drawing.Size(31, 13);
            this.labelFlag.TabIndex = 92;
            this.labelFlag.Text = "open";
            // 
            // labelFlag1
            // 
            this.labelFlag1.AutoSize = true;
            this.labelFlag1.Location = new System.Drawing.Point(10, 130);
            this.labelFlag1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFlag1.Name = "labelFlag1";
            this.labelFlag1.Size = new System.Drawing.Size(30, 13);
            this.labelFlag1.TabIndex = 91;
            this.labelFlag1.Text = "Flag:";
            // 
            // labelPIF
            // 
            this.labelPIF.AutoSize = true;
            this.labelPIF.Location = new System.Drawing.Point(10, 108);
            this.labelPIF.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPIF.Name = "labelPIF";
            this.labelPIF.Size = new System.Drawing.Size(26, 13);
            this.labelPIF.TabIndex = 90;
            this.labelPIF.Text = "PIF:";
            // 
            // labelFrameCounter
            // 
            this.labelFrameCounter.AutoSize = true;
            this.labelFrameCounter.Location = new System.Drawing.Point(10, 86);
            this.labelFrameCounter.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelFrameCounter.Name = "labelFrameCounter";
            this.labelFrameCounter.Size = new System.Drawing.Size(123, 13);
            this.labelFrameCounter.TabIndex = 89;
            this.labelFrameCounter.Text = "Frame counter HW/SW:";
            // 
            // labelTempTarget
            // 
            this.labelTempTarget.AutoSize = true;
            this.labelTempTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTempTarget.Location = new System.Drawing.Point(10, 63);
            this.labelTempTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTempTarget.Name = "labelTempTarget";
            this.labelTempTarget.Size = new System.Drawing.Size(71, 13);
            this.labelTempTarget.TabIndex = 85;
            this.labelTempTarget.Text = "Target-Temp:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "---";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 200;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 93;
            this.label2.Text = "Forehead-Temp:";
            // 
            // timer3
            // 
            this.timer3.Interval = 13;
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 63);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 95;
            this.label4.Text = "Nasal-Temp:";
            // 
            // labelInstanceName
            // 
            this.labelInstanceName.AutoSize = true;
            this.labelInstanceName.Location = new System.Drawing.Point(10, 15);
            this.labelInstanceName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInstanceName.Name = "labelInstanceName";
            this.labelInstanceName.Size = new System.Drawing.Size(80, 13);
            this.labelInstanceName.TabIndex = 87;
            this.labelInstanceName.Text = "Instance name:";
            // 
            // textBoxInstanceName
            // 
            this.textBoxInstanceName.Location = new System.Drawing.Point(94, 11);
            this.textBoxInstanceName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textBoxInstanceName.Name = "textBoxInstanceName";
            this.textBoxInstanceName.Size = new System.Drawing.Size(178, 20);
            this.textBoxInstanceName.TabIndex = 88;
            // 
            // chart1
            // 
            chartArea1.CursorX.Interval = 5D;
            chartArea1.CursorX.IntervalOffset = 5D;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorX.LineColor = System.Drawing.Color.LightCoral;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 171);
            this.chart1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.chart1.Name = "chart1";
            this.chart1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series1.BorderColor = System.Drawing.Color.Red;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.DarkBlue;
            series1.Legend = "Legend1";
            series1.Name = "Forehead";
            series1.YValuesPerPoint = 2;
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.Crimson;
            series2.Legend = "Legend1";
            series2.Name = "Nasal-Area";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(436, 211);
            this.chart1.TabIndex = 98;
            this.chart1.Text = "chart1";
            // 
            // loadChartButton
            // 
            this.loadChartButton.Location = new System.Drawing.Point(240, 479);
            this.loadChartButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.loadChartButton.Name = "loadChartButton";
            this.loadChartButton.Size = new System.Drawing.Size(118, 41);
            this.loadChartButton.TabIndex = 101;
            this.loadChartButton.Text = "Start Task";
            this.loadChartButton.UseVisualStyleBackColor = true;
            this.loadChartButton.Click += new System.EventHandler(this.loadChartButton_Click);
            // 
            // saveTask
            // 
            this.saveTask.Location = new System.Drawing.Point(555, 479);
            this.saveTask.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.saveTask.Name = "saveTask";
            this.saveTask.Size = new System.Drawing.Size(118, 39);
            this.saveTask.TabIndex = 102;
            this.saveTask.Text = "save Task";
            this.saveTask.UseVisualStyleBackColor = true;
            this.saveTask.Click += new System.EventHandler(this.saveTask_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});

            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(456, 336);
            this.listView1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(330, 137);
            this.listView1.TabIndex = 105;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;


            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Participant ID";
            this.columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Condition";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Task";
            this.columnHeader3.Width = 123;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Forehead";
            this.columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Orbital";
            this.columnHeader5.Width = 150;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Nasal";
            this.columnHeader6.Width = 150;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Cheek";
            this.columnHeader7.Width = 150;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Maxillary";
            this.columnHeader8.Width = 150;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Chin";
            this.columnHeader9.Width = 150;
            // 
            // newParticipantButton
            // 
            this.newParticipantButton.Location = new System.Drawing.Point(107, 416);
            this.newParticipantButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.newParticipantButton.Name = "newParticipantButton";
            this.newParticipantButton.Size = new System.Drawing.Size(139, 41);
            this.newParticipantButton.TabIndex = 106;
            this.newParticipantButton.Text = "Add New Participant";
            this.newParticipantButton.UseVisualStyleBackColor = true;
            this.newParticipantButton.Click += new System.EventHandler(this.newParticipantButton_Click);
            // 
            // saveParticipant
            // 
            this.saveParticipant.Location = new System.Drawing.Point(240, 523);
            this.saveParticipant.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.saveParticipant.Name = "saveParticipant";
            this.saveParticipant.Size = new System.Drawing.Size(433, 41);
            this.saveParticipant.TabIndex = 107;
            this.saveParticipant.Text = "Save Participant and Exit";
            this.saveParticipant.UseVisualStyleBackColor = true;
            this.saveParticipant.Click += new System.EventHandler(this.saveParticipant_Click);
            // 
            // participantNumberTextbox
            // 
            this.participantNumberTextbox.Location = new System.Drawing.Point(33, 416);
            this.participantNumberTextbox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.participantNumberTextbox.Name = "participantNumberTextbox";
            this.participantNumberTextbox.Size = new System.Drawing.Size(70, 20);
            this.participantNumberTextbox.TabIndex = 109;
            // 
            // refreshChartButton
            // 
            this.refreshChartButton.Location = new System.Drawing.Point(396, 479);
            this.refreshChartButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.refreshChartButton.Name = "refreshChartButton";
            this.refreshChartButton.Size = new System.Drawing.Size(102, 41);
            this.refreshChartButton.TabIndex = 110;
            this.refreshChartButton.Text = "Rest";
            this.refreshChartButton.UseVisualStyleBackColor = true;
            this.refreshChartButton.Click += new System.EventHandler(this.refreshChartButton_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(284, 426);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(326, 426);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 31);
            this.label5.TabIndex = 112;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(354, 426);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 31);
            this.label6.TabIndex = 113;
            this.label6.Text = "0";
            // 
            // timer4
            // 
            this.timer4.Interval = 1000;
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader22});
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(798, 336);
            this.listView2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.listView2.MultiSelect = false;
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(328, 137);
            this.listView2.TabIndex = 114;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "participant ID";
            this.columnHeader12.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "task";
            this.columnHeader14.Width = 150;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "forehead temperatures";
            this.columnHeader15.Width = 150;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "nasal temperatures";
            this.columnHeader16.Width = 150;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "flag";
            this.columnHeader22.Width = 100;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 567);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.refreshChartButton);
            this.Controls.Add(this.participantNumberTextbox);
            this.Controls.Add(this.saveParticipant);
            this.Controls.Add(this.newParticipantButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.saveTask);
            this.Controls.Add(this.loadChartButton);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.buttonFlagRenew);
            this.Controls.Add(this.labelFlag);
            this.Controls.Add(this.labelFlag1);
            this.Controls.Add(this.labelPIF);
            this.Controls.Add(this.labelFrameCounter);
            this.Controls.Add(this.textBoxInstanceName);
            this.Controls.Add(this.labelInstanceName);
            this.Controls.Add(this.labelTempTarget);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "FormMain";
            this.Text = "Stress Detector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button buttonFlagRenew;
        private System.Windows.Forms.Label labelFlag;
        private System.Windows.Forms.Label labelFlag1;
        private System.Windows.Forms.Label labelPIF;
        private System.Windows.Forms.Label labelFrameCounter;
        private System.Windows.Forms.Label labelTempTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelInstanceName;
        private System.Windows.Forms.TextBox textBoxInstanceName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button loadChartButton;
        private System.Windows.Forms.Button saveTask;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button newParticipantButton;
        private System.Windows.Forms.Button saveParticipant;
        private System.Windows.Forms.TextBox participantNumberTextbox;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.Button refreshChartButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader22;
    }
}

