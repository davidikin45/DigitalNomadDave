namespace SkyscannerSearch
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.drpCabinClass = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtInfants = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtChildren = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtAdults = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStops = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInboundArrivalTimeTo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtInboundArrivalTimeFrom = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOutboundDepartureTimeTo = new System.Windows.Forms.TextBox();
            this.drpInboundLocation = new System.Windows.Forms.ComboBox();
            this.drpOutboundLocation = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.panelOneWayReturn = new System.Windows.Forms.Panel();
            this.radioReturn = new System.Windows.Forms.RadioButton();
            this.radioOneWay = new System.Windows.Forms.RadioButton();
            this.label10 = new System.Windows.Forms.Label();
            this.txtLocale = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtMarket = new System.Windows.Forms.TextBox();
            this.txtInboundArrivalDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOutboundArrivalDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBudget = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOutboundDepartureTimeFrom = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnGet1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panelOneWayReturn.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(44, 79);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1938, 840);
            this.tabControl1.TabIndex = 20;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.drpCabinClass);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.txtInfants);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.txtChildren);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.txtAdults);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtStops);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.txtInboundArrivalTimeTo);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.txtInboundArrivalTimeFrom);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.txtOutboundDepartureTimeTo);
            this.tabPage1.Controls.Add(this.drpInboundLocation);
            this.tabPage1.Controls.Add(this.drpOutboundLocation);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.txtAPIKey);
            this.tabPage1.Controls.Add(this.panelOneWayReturn);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.txtLocale);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.txtCurrency);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.txtMarket);
            this.tabPage1.Controls.Add(this.txtInboundArrivalDate);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.txtOutboundArrivalDate);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.txtBudget);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtOutboundDepartureTimeFrom);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage1.Size = new System.Drawing.Size(1922, 793);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Parameters";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(38, 687);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 25);
            this.label18.TabIndex = 65;
            this.label18.Text = "Cabin Class";
            // 
            // drpCabinClass
            // 
            this.drpCabinClass.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.drpCabinClass.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.drpCabinClass.FormattingEnabled = true;
            this.drpCabinClass.Items.AddRange(new object[] {
            "Economy",
            "PremiumEconomy",
            "Business",
            "First"});
            this.drpCabinClass.Location = new System.Drawing.Point(44, 717);
            this.drpCabinClass.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.drpCabinClass.Name = "drpCabinClass";
            this.drpCabinClass.Size = new System.Drawing.Size(298, 33);
            this.drpCabinClass.TabIndex = 63;
            this.drpCabinClass.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(528, 600);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 25);
            this.label17.TabIndex = 62;
            this.label17.Text = "Infants";
            // 
            // txtInfants
            // 
            this.txtInfants.Location = new System.Drawing.Point(534, 631);
            this.txtInfants.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInfants.Name = "txtInfants";
            this.txtInfants.Size = new System.Drawing.Size(196, 31);
            this.txtInfants.TabIndex = 61;
            this.txtInfants.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(288, 600);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 25);
            this.label16.TabIndex = 60;
            this.label16.Text = "Children";
            // 
            // txtChildren
            // 
            this.txtChildren.Location = new System.Drawing.Point(294, 631);
            this.txtChildren.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtChildren.Name = "txtChildren";
            this.txtChildren.Size = new System.Drawing.Size(196, 31);
            this.txtChildren.TabIndex = 59;
            this.txtChildren.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(38, 600);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(72, 25);
            this.label15.TabIndex = 58;
            this.label15.Text = "Adults";
            // 
            // txtAdults
            // 
            this.txtAdults.Location = new System.Drawing.Point(44, 631);
            this.txtAdults.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtAdults.Name = "txtAdults";
            this.txtAdults.Size = new System.Drawing.Size(196, 31);
            this.txtAdults.TabIndex = 57;
            this.txtAdults.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(30, 521);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 25);
            this.label14.TabIndex = 56;
            this.label14.Text = "Stops";
            // 
            // txtStops
            // 
            this.txtStops.Location = new System.Drawing.Point(36, 552);
            this.txtStops.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtStops.Name = "txtStops";
            this.txtStops.Size = new System.Drawing.Size(196, 31);
            this.txtStops.TabIndex = 55;
            this.txtStops.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(626, 360);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 25);
            this.label6.TabIndex = 54;
            this.label6.Text = "Inbound Arrival Time To";
            // 
            // txtInboundArrivalTimeTo
            // 
            this.txtInboundArrivalTimeTo.Location = new System.Drawing.Point(632, 390);
            this.txtInboundArrivalTimeTo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInboundArrivalTimeTo.Name = "txtInboundArrivalTimeTo";
            this.txtInboundArrivalTimeTo.Size = new System.Drawing.Size(196, 31);
            this.txtInboundArrivalTimeTo.TabIndex = 53;
            this.txtInboundArrivalTimeTo.Text = "23:59";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(288, 360);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(264, 25);
            this.label13.TabIndex = 52;
            this.label13.Text = "Inbound Arrival Time From";
            // 
            // txtInboundArrivalTimeFrom
            // 
            this.txtInboundArrivalTimeFrom.Location = new System.Drawing.Point(294, 390);
            this.txtInboundArrivalTimeFrom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInboundArrivalTimeFrom.Name = "txtInboundArrivalTimeFrom";
            this.txtInboundArrivalTimeFrom.Size = new System.Drawing.Size(196, 31);
            this.txtInboundArrivalTimeFrom.TabIndex = 51;
            this.txtInboundArrivalTimeFrom.Text = "19:00";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(626, 183);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(291, 25);
            this.label12.TabIndex = 50;
            this.label12.Text = "Outbound Departure Time To";
            // 
            // txtOutboundDepartureTimeTo
            // 
            this.txtOutboundDepartureTimeTo.Location = new System.Drawing.Point(632, 213);
            this.txtOutboundDepartureTimeTo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutboundDepartureTimeTo.Name = "txtOutboundDepartureTimeTo";
            this.txtOutboundDepartureTimeTo.Size = new System.Drawing.Size(196, 31);
            this.txtOutboundDepartureTimeTo.TabIndex = 49;
            this.txtOutboundDepartureTimeTo.Text = "23:59";
            // 
            // drpInboundLocation
            // 
            this.drpInboundLocation.FormattingEnabled = true;
            this.drpInboundLocation.Items.AddRange(new object[] {
            "BGO-sky",
            "KEF-sky",
            "DUS-sky",
            "VLC-sky",
            "NYO-sky",
            "STOC-sky",
            "DUSS-sky",
            "SE-sky",
            "DE-sky",
            "NL-sky",
            "IE-sky",
            "UK-sky",
            "anywhere"});
            this.drpInboundLocation.Location = new System.Drawing.Point(30, 294);
            this.drpInboundLocation.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.drpInboundLocation.Name = "drpInboundLocation";
            this.drpInboundLocation.Size = new System.Drawing.Size(238, 33);
            this.drpInboundLocation.TabIndex = 48;
            // 
            // drpOutboundLocation
            // 
            this.drpOutboundLocation.FormattingEnabled = true;
            this.drpOutboundLocation.Items.AddRange(new object[] {
            "BGO-sky",
            "KEF-sky",
            "STN-sky",
            "LHR-sky",
            "LOND-sky",
            "UK",
            "anywhere"});
            this.drpOutboundLocation.Location = new System.Drawing.Point(26, 123);
            this.drpOutboundLocation.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.drpOutboundLocation.Name = "drpOutboundLocation";
            this.drpOutboundLocation.Size = new System.Drawing.Size(238, 33);
            this.drpOutboundLocation.TabIndex = 47;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1406, 94);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 25);
            this.label11.TabIndex = 44;
            this.label11.Text = "API Key";
            // 
            // txtAPIKey
            // 
            this.txtAPIKey.Location = new System.Drawing.Point(1412, 125);
            this.txtAPIKey.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.Size = new System.Drawing.Size(422, 31);
            this.txtAPIKey.TabIndex = 43;
            this.txtAPIKey.Text = "prtl6749387986743898559646983194";
            // 
            // panelOneWayReturn
            // 
            this.panelOneWayReturn.Controls.Add(this.radioReturn);
            this.panelOneWayReturn.Controls.Add(this.radioOneWay);
            this.panelOneWayReturn.Location = new System.Drawing.Point(26, 40);
            this.panelOneWayReturn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelOneWayReturn.Name = "panelOneWayReturn";
            this.panelOneWayReturn.Size = new System.Drawing.Size(290, 48);
            this.panelOneWayReturn.TabIndex = 42;
            // 
            // radioReturn
            // 
            this.radioReturn.AutoSize = true;
            this.radioReturn.Location = new System.Drawing.Point(170, 6);
            this.radioReturn.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.radioReturn.Name = "radioReturn";
            this.radioReturn.Size = new System.Drawing.Size(107, 29);
            this.radioReturn.TabIndex = 35;
            this.radioReturn.TabStop = true;
            this.radioReturn.Text = "Return";
            this.radioReturn.UseVisualStyleBackColor = true;
            // 
            // radioOneWay
            // 
            this.radioOneWay.AutoSize = true;
            this.radioOneWay.Location = new System.Drawing.Point(18, 6);
            this.radioOneWay.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.radioOneWay.Name = "radioOneWay";
            this.radioOneWay.Size = new System.Drawing.Size(132, 29);
            this.radioOneWay.TabIndex = 34;
            this.radioOneWay.TabStop = true;
            this.radioOneWay.Text = "One Way";
            this.radioOneWay.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1012, 263);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 25);
            this.label10.TabIndex = 41;
            this.label10.Text = "Locale";
            // 
            // txtLocale
            // 
            this.txtLocale.Location = new System.Drawing.Point(1014, 294);
            this.txtLocale.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtLocale.Name = "txtLocale";
            this.txtLocale.Size = new System.Drawing.Size(196, 31);
            this.txtLocale.TabIndex = 40;
            this.txtLocale.Text = "en-GB";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1012, 183);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 25);
            this.label9.TabIndex = 39;
            this.label9.Text = "Currency";
            // 
            // txtCurrency
            // 
            this.txtCurrency.Location = new System.Drawing.Point(1014, 213);
            this.txtCurrency.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(196, 31);
            this.txtCurrency.TabIndex = 38;
            this.txtCurrency.Text = "GBP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1012, 94);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 25);
            this.label8.TabIndex = 37;
            this.label8.Text = "Market";
            // 
            // txtMarket
            // 
            this.txtMarket.Location = new System.Drawing.Point(1014, 125);
            this.txtMarket.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtMarket.Name = "txtMarket";
            this.txtMarket.Size = new System.Drawing.Size(196, 31);
            this.txtMarket.TabIndex = 36;
            this.txtMarket.Text = "GB";
            // 
            // txtInboundArrivalDate
            // 
            this.txtInboundArrivalDate.Location = new System.Drawing.Point(30, 390);
            this.txtInboundArrivalDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtInboundArrivalDate.Name = "txtInboundArrivalDate";
            this.txtInboundArrivalDate.Size = new System.Drawing.Size(196, 31);
            this.txtInboundArrivalDate.TabIndex = 33;
            this.txtInboundArrivalDate.Text = "2017-09-18";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 360);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 25);
            this.label3.TabIndex = 32;
            this.label3.Text = "Inbound Arrival Date";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 263);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(177, 25);
            this.label7.TabIndex = 30;
            this.label7.Text = "Inbound Location";
            // 
            // txtOutboundArrivalDate
            // 
            this.txtOutboundArrivalDate.Location = new System.Drawing.Point(26, 213);
            this.txtOutboundArrivalDate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutboundArrivalDate.Name = "txtOutboundArrivalDate";
            this.txtOutboundArrivalDate.Size = new System.Drawing.Size(196, 31);
            this.txtOutboundArrivalDate.TabIndex = 27;
            this.txtOutboundArrivalDate.Text = "2017-09-16";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 183);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 25);
            this.label5.TabIndex = 26;
            this.label5.Text = "Outbound Arrival Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 438);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "Budget";
            // 
            // txtBudget
            // 
            this.txtBudget.Location = new System.Drawing.Point(30, 469);
            this.txtBudget.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtBudget.Name = "txtBudget";
            this.txtBudget.Size = new System.Drawing.Size(196, 31);
            this.txtBudget.TabIndex = 24;
            this.txtBudget.Text = "200.00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(315, 25);
            this.label2.TabIndex = 23;
            this.label2.Text = "Outbound Departure Time From";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 94);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 22;
            this.label1.Text = "Outbound Location";
            // 
            // txtOutboundDepartureTimeFrom
            // 
            this.txtOutboundDepartureTimeFrom.Location = new System.Drawing.Point(294, 213);
            this.txtOutboundDepartureTimeFrom.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtOutboundDepartureTimeFrom.Name = "txtOutboundDepartureTimeFrom";
            this.txtOutboundDepartureTimeFrom.Size = new System.Drawing.Size(196, 31);
            this.txtOutboundDepartureTimeFrom.TabIndex = 21;
            this.txtOutboundDepartureTimeFrom.Text = "20:00";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabPage2.Size = new System.Drawing.Size(1922, 793);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Results";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnGet1
            // 
            this.btnGet1.Location = new System.Drawing.Point(1832, 23);
            this.btnGet1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnGet1.Name = "btnGet1";
            this.btnGet1.Size = new System.Drawing.Size(150, 44);
            this.btnGet1.TabIndex = 24;
            this.btnGet1.Text = "Search";
            this.btnGet1.UseVisualStyleBackColor = true;
            this.btnGet1.Click += new System.EventHandler(this.btnGet1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2048, 956);
            this.Controls.Add(this.btnGet1);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Form1";
            this.Text = "Flight Search";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panelOneWayReturn.ResumeLayout(false);
            this.panelOneWayReturn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RadioButton radioReturn;
        private System.Windows.Forms.RadioButton radioOneWay;
        private System.Windows.Forms.TextBox txtInboundArrivalDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOutboundArrivalDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBudget;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOutboundDepartureTimeFrom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtLocale;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCurrency;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMarket;
        private System.Windows.Forms.Panel panelOneWayReturn;
        private System.Windows.Forms.Button btnGet1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.ComboBox drpOutboundLocation;
        private System.Windows.Forms.ComboBox drpInboundLocation;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOutboundDepartureTimeTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtInboundArrivalTimeTo;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtInboundArrivalTimeFrom;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtStops;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtAdults;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtInfants;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtChildren;
        private System.Windows.Forms.ComboBox drpCabinClass;
        private System.Windows.Forms.Label label18;
    }
}

