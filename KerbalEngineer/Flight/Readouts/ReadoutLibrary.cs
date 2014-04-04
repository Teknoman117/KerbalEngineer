﻿// Project:	KerbalEngineer
// Author:	CYBUTEK
// License:	Attribution-NonCommercial-ShareAlike 3.0 Unported

#region Using Directives

using System.Collections.Generic;
using System.Linq;

using KerbalEngineer.Flight.Readouts.Orbital;
using KerbalEngineer.Flight.Readouts.Surface;

#endregion

namespace KerbalEngineer.Flight.Readouts
{
    public class ReadoutLibrary
    {
        #region Instance

        private static readonly ReadoutLibrary instance = new ReadoutLibrary();

        /// <summary>
        ///     Gets the current instance of the readout library.
        /// </summary>
        public static ReadoutLibrary Instance
        {
            get { return instance; }
        }

        #endregion

        #region Fields

        private List<ReadoutModule> readoutModules = new List<ReadoutModule>();

        #endregion

        #region Constructors

        /// <summary>
        ///     Sets up and populates the readout library with the stock readouts.
        /// </summary>
        private ReadoutLibrary()
        {
            this.readoutModules.Add(new ApoapsisHeight());
            this.readoutModules.Add(new PeriapsisHeight());
            this.readoutModules.Add(new TimeToApoapsis());
            this.readoutModules.Add(new TimeToPeriapsis());
            this.readoutModules.Add(new Inclination());
            this.readoutModules.Add(new Eccentricity());
            this.readoutModules.Add(new OrbitalPeriod());
            this.readoutModules.Add(new LongitudeOfAscendingNode());
            this.readoutModules.Add(new LongitudeOfPeriapsis());
            this.readoutModules.Add(new SemiMajorAxis());
            this.readoutModules.Add(new SemiMinorAxis());

            this.readoutModules.Add(new AltitudeSeaLevel());
            this.readoutModules.Add(new AltitudeTerrain());
            this.readoutModules.Add(new VerticalSpeed());
            this.readoutModules.Add(new HorizontalSpeed());
            this.readoutModules.Add(new Longitude());
            this.readoutModules.Add(new Latitude());
            this.readoutModules.Add(new GeeForce());
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets and sets the available readout modules.
        /// </summary>
        public List<ReadoutModule> ReadoutModules
        {
            get { return this.readoutModules; }
            set { this.readoutModules = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets a readout module with the specified name or class name. (Returns null if not found.)
        /// </summary>
        public ReadoutModule GetReadoutModule(string name)
        {
            return this.readoutModules.FirstOrDefault(r => r.Name == name || r.GetType().Name == name);
        }

        /// <summary>
        ///     Gets a list of readout modules which are associated with the specified category.
        /// </summary>
        public List<ReadoutModule> GetCategory(ReadoutCategory category)
        {
            return this.readoutModules.Where(r => r.Category == category).ToList();
        }

        /// <summary>
        ///     Resets all the readout modules.
        /// </summary>
        public void Reset()
        {
            foreach (var readout in this.readoutModules)
            {
                readout.Reset();
            }
        }

        #endregion
    }
}