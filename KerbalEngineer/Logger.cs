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

#region Using Directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UnityEngine;

#endregion

namespace KerbalEngineer
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Logger : MonoBehaviour
    {
        #region Fields

        private static readonly string fileName;
        private static readonly AssemblyName assemblyName;
        private static readonly List<string> messages = new List<string>();

        #endregion

        #region Initialisation

        static Logger()
        {
            assemblyName = Assembly.GetExecutingAssembly().GetName();
            fileName = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "log");
            File.Delete(fileName);

            messages.Add("Version: " + assemblyName.Version);
            Blank();
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        #endregion

        #region Printing

        public static void Blank()
        {
            lock (messages)
            {
                messages.Add(string.Empty);
            }
        }

        public static void Log(object obj)
        {
            lock (messages)
            {
                messages.Add("[Log " + DateTime.Now.TimeOfDay + "]: " + obj);
            }
        }

        public static void Log(string message)
        {
            lock (messages)
            {
                messages.Add("[Log " + DateTime.Now.TimeOfDay + "]: " + message);
            }
        }

        public static void Warning(string message)
        {
            lock (messages)
            {
                messages.Add("[Warning " + DateTime.Now.TimeOfDay + "]: " + message);
            }
        }

        public static void Error(string message)
        {
            lock (messages)
            {
                messages.Add("[Error " + DateTime.Now.TimeOfDay + "]: " + message);
            }
        }

        public static void Exception(Exception ex)
        {
            lock (messages)
            {
                messages.Add("[Exception " + DateTime.Now.TimeOfDay + "]: " + ex.Message);
                messages.Add(ex.StackTrace);
                messages.Add(string.Empty);
            }
        }

        public static void Exception(Exception ex, string location)
        {
            lock (messages)
            {
                messages.Add("[Exception " + DateTime.Now.TimeOfDay + "]: " + location + " // " + ex.Message);
                messages.Add(ex.StackTrace);
                messages.Add(string.Empty);
            }
        }

        #endregion

        #region Flushing

        public static void Flush()
        {
            lock (messages)
            {
                if (messages.Count > 0)
                {
                    using (var file = File.AppendText(fileName))
                    {
                        foreach (var message in messages)
                        {
                            file.WriteLine(message);
                            print(assemblyName.Name + " -> " + message);
                        }
                    }
                    messages.Clear();
                }
            }
        }

        private void LateUpdate()
        {
            Flush();
        }

        #endregion
    }
}