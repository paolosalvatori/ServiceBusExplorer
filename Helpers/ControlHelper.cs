#region Copyright
//=======================================================================================
// Windows Azure Customer Advisory Team  
//
// This sample is supplemental to the technical guidance published on the community
// blog at http://www.appfabriccat.com/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright © 2011 Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER 
// EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE. YOU BEAR THE RISK OF USING IT.
//=======================================================================================
#endregion

#region Using Directives
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    public static class ControlHelper 
    {     
        #region Redraw Suspend/Resume
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]     
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);     
        private const int WmSetredraw = 0xB;      
        public static void SuspendDrawing(this Control target)     
        {         
            SendMessage(target.Handle, WmSetredraw, 0, 0);     
        }      
        
        public static void ResumeDrawing(this Control target) 
        { 
            ResumeDrawing(target, true); 
        }     

        public static void ResumeDrawing(this Control target, bool redraw)     
        {         
            SendMessage(target.Handle, WmSetredraw, 1, 0);          
            if (redraw)         
            {             
                target.Refresh();         
            }     
        }     
        #endregion 
    } 
}
