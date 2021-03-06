﻿// 
//     Kerbal Engineer Redux
// 
//     Copyright (C) 2014 CYBUTEK
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 

#region

using KerbalEngineer.Extensions;
using KerbalEngineer.VesselSimulator;

#endregion

namespace KerbalEngineer.Flight.Readouts.Vessel
{
    public class DeltaVTotal : ReadoutModule
    {
        private bool showing;

        public DeltaVTotal()
        {
            this.Name = "DeltaV Total";
            this.Category = ReadoutCategory.Vessel;
            this.HelpString = "Shows the vessel's total delta velocity.";
            this.IsDefault = true;
        }

        public override void Update()
        {
            SimulationProcessor.RequestUpdate();
        }

        public override void Draw()
        {
            if (SimulationProcessor.ShowDetails)
            {
                this.showing = true;
                this.DrawLine(SimulationProcessor.LastStage.totalDeltaV.ToString("N0") + "m/s (" + SimulationProcessor.LastStage.totalTime.ToTime() + ")");
            } else if (this.showing)
            {
                this.showing = false;
                this.ResizeRequested = true;
            }   
        }

        public override void Reset()
        {
            FlightEngineerCore.Instance.AddUpdatable(SimulationProcessor.Instance);
        }
    }
}