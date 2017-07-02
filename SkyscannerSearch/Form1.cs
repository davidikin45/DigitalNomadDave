using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using DND.Services.Factories;
using DND.Services;
using Solution.Base.Implementation.UnitOfWork;
using Solution.Base.Implementation.Repository;
using AutoMapper;
using Solution.Base.Implementation.Logging;
using DND.EFPersistance;
using DND.Core.DTOs;
using System.Threading;

namespace SkyscannerSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            radioReturn.Checked = true;
        }

        private async void btnGet1_Click(object sender, EventArgs e)
        {
            var sw = new Stopwatch();
            sw.Start();

            IMapper _mapper;
            var config = new MapperConfiguration(cfg => {

            });
            _mapper = config.CreateMapper();

            var searchEngine = new FlightSearchService(new BaseUnitOfWorkScopeFactory(new DbContextFactory(), new BaseAmbientDbContextLocator(), new BaseRepositoryFactory()), _mapper);

            if (radioReturn.Checked)
            {
                var request = new FlightSearchRequestDTO();
                request.Markets = new string[] { txtMarket.Text };
                request.Currency = txtCurrency.Text;
                request.Locale = txtLocale.Text;
                request.OriginLocation = drpOutboundLocation.SelectedItem.ToString();
                request.DestinationLocation = drpInboundLocation.SelectedItem.ToString();
                request.OutboundDate = DateTime.Parse(txtOutboundArrivalDate.Text);
                request.InboundDate = DateTime.Parse(txtOutboundArrivalDate.Text);
                request.Adults = DND.Core.Enums.Adult.One;
                request.Children = DND.Core.Enums.Children.Zero;
                request.Infants = DND.Core.Enums.Infant.Zero;
                request.PriceMaxFilter = Convert.ToDouble(txtBudget.Text);
                request.FlightClass = DND.Core.Enums.FlightClass.Economy;
                request.MaxStopsFilter = Convert.ToInt32(txtStops.Text);
                request.Skip = 0;
                request.Take = 10;
                request.OutboundArrivalTimeFromFilter = DateTime.Parse(txtOutboundArrivalDate.Text + " " + txtOutboundDepartureTimeFrom.Text);
                request.OutboundArrivalTimeToFilter = DateTime.Parse(txtOutboundArrivalDate.Text + " " + txtOutboundDepartureTimeTo.Text);
                request.InboundArrivalTimeFromFilter = DateTime.Parse(txtInboundArrivalDate.Text + " " + txtInboundArrivalTimeFrom.Text);
                request.InboundArrivalTimeToFilter = DateTime.Parse(txtInboundArrivalDate.Text + " " + txtInboundArrivalTimeTo.Text);

                var result = await searchEngine.SearchAsync(request, default(CancellationToken));

                var ts2 = sw.Elapsed;
            }
            else{

                var request = new FlightSearchRequestDTO();
                request.Markets = new string[] { txtMarket.Text };
                request.Currency = txtCurrency.Text;
                request.Locale = txtLocale.Text;
                request.OriginLocation = drpOutboundLocation.SelectedItem.ToString();
                request.DestinationLocation = drpInboundLocation.SelectedItem.ToString();
                request.OutboundDate = DateTime.Parse(txtOutboundArrivalDate.Text);
                request.InboundDate = null;
                request.Adults = DND.Core.Enums.Adult.One;
                request.Children = DND.Core.Enums.Children.Zero;
                request.Infants = DND.Core.Enums.Infant.Zero;
                request.PriceMaxFilter = Convert.ToDouble(txtBudget.Text);
                request.FlightClass = DND.Core.Enums.FlightClass.Economy;
                request.MaxStopsFilter = Convert.ToInt32(txtStops.Text);
                request.Skip = 0;
                request.Take = 10;
                request.OutboundArrivalTimeFromFilter = DateTime.Parse(txtOutboundArrivalDate.Text + " " + txtOutboundDepartureTimeFrom.Text);
                request.OutboundArrivalTimeToFilter = DateTime.Parse(txtOutboundArrivalDate.Text + " " + txtOutboundDepartureTimeTo.Text);
                request.InboundArrivalTimeFromFilter = null;
                request.InboundArrivalTimeToFilter = null;

                var result = await searchEngine.SearchAsync(request, default(CancellationToken));

                var ts2 = sw.Elapsed;
            }
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
//var collection = new AutoCompleteStringCollection();
//            if (drpCabinClass.Text.Length > 1)
//            {
//                var results = new LocationService().SearchByQuery("prtl6749387986743898559646983194", txtMarket.Text, txtCurrency.Text, txtLocale.Text, drpCabinClass.Text);
//                foreach (Location.Place place in results.Places)
//                {
//                    collection.Add(place.PlaceId);
//                }

//            }
            
//            drpCabinClass.AutoCompleteCustomSource = collection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            drpCabinClass.SelectedIndex = drpCabinClass.FindStringExact("Economy");
            drpOutboundLocation.SelectedIndex = drpOutboundLocation.FindStringExact("Lond-sky");
            drpInboundLocation.SelectedIndex = drpInboundLocation.FindStringExact("KEF-sky");
        }    
    }
}
