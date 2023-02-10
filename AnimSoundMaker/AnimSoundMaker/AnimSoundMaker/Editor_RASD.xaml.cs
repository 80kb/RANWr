﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static lib_RASD;

namespace AnimSoundMaker
{   
    public partial class Editor_RASD : Page
    {

        public List<Event> events { get; set; }
        public EventTypes type { get; set; }


        public Editor_RASD()
        {
            InitializeComponent();

        }

        public void LoadData(RASD rasd)
        {
            events = rasd.AnimSound.Events;
            DataGrid.ItemsSource = events;
        }

        private void AdjustColumns()
        {
            DataGrid.Columns.First().Header = "";
            DataGrid.Columns.First().Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            DataGrid.Columns.Last().Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            DataGrid.Columns[5].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
            DataGrid.Columns[3].Width = new DataGridLength(1.5, DataGridLengthUnitType.Star);
            DataGrid.Columns[3].Header = "Playback Interval";

            DataGrid.Columns[0].CellStyle = FindResource("MyCellStyle") as Style;
            DataGrid.Columns[1].CellStyle = FindResource("MyCellStyle") as Style;
            DataGrid.Columns[2].CellStyle = FindResource("MyEndFrameStyle") as Style;
            DataGrid.Columns[6].CellStyle = FindResource("MyCellStyle") as Style;
            DataGrid.Columns[7].CellStyle = FindResource("MyCellStyle") as Style;
            DataGrid.Columns[8].CellStyle = FindResource("MyCellStyle") as Style;
        }

        public enum EventTypes
        {
            Range = 0,
            Trigger = 1
        }

        public static IEnumerable<EventTypes> GetEnumTypes => Enum.GetValues(typeof(EventTypes)).Cast<EventTypes>();

        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            
        }

        private void DataGrid_AutoGeneratedColumns(object sender, EventArgs e)
        {
            AdjustColumns();
        }


    }
}
