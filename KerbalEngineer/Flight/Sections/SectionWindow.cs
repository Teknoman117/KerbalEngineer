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

using UnityEngine;

#endregion

namespace KerbalEngineer.Flight.Sections
{
    public class SectionWindow : MonoBehaviour
    {
        #region Fields

        private bool resizeRequested;
        private int windowId;
        private Rect windowPosition;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initialises the object's state on creation.
        /// </summary>
        private void Start()
        {
            this.windowId = this.GetHashCode();
            this.InitialiseStyles();
            RenderingManager.AddToPostDrawQueue(0, this.Draw);
            GuiDisplaySize.OnSizeChanged += this.OnSizeChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets and sets the parent section for the floating section window.
        /// </summary>
        public SectionModule ParentSection { get; set; }

        /// <summary>
        ///     Gets and sets the window position.
        /// </summary>
        public Rect WindowPosition
        {
            get { return this.windowPosition; }
            set { this.windowPosition = value; }
        }

        #endregion

        #region GUIStyles

        private GUIStyle windowStyle;

        /// <summary>
        ///     Initialises all the styles required for this object.
        /// </summary>
        private void InitialiseStyles()
        {
            this.windowStyle = new GUIStyle(HighLogic.Skin.window)
            {
                margin = new RectOffset(),
                padding = new RectOffset(5, 5, 0, 5),
            };
        }

        private void OnSizeChanged()
        {
            this.InitialiseStyles();
            this.RequestResize();
        }

        #endregion

        #region Drawing

        /// <summary>
        ///     Called to draw the floating section window when the UI is enabled.
        /// </summary>
        private void Draw()
        {
            if (!DisplayStack.Instance.Hidden && (this.ParentSection != null && this.ParentSection.IsVisible))
            {
                if (this.resizeRequested)
                {
                    this.windowPosition.width = 0;
                    this.windowPosition.height = 0;
                    this.resizeRequested = false;
                }
                GUI.skin = null;
                this.windowPosition = GUILayout.Window(this.windowId, this.windowPosition, this.Window, string.Empty, this.windowStyle).ClampToScreen();
                this.ParentSection.FloatingPositionX = this.windowPosition.x;
                this.ParentSection.FloatingPositionY = this.windowPosition.y;
            }
        }

        /// <summary>
        ///     Draws the floating section window.
        /// </summary>
        private void Window(int windowId)
        {
            this.ParentSection.Draw();

            GUI.DragWindow();
        }

        #endregion

        #region Destruction

        /// <summary>
        ///     Runs when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            RenderingManager.RemoveFromPostDrawQueue(0, this.Draw);
            GuiDisplaySize.OnSizeChanged -= this.OnSizeChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Request that the floating section window's size is reset in the next draw call.
        /// </summary>
        public void RequestResize()
        {
            this.resizeRequested = true;
        }

        #endregion
    }
}