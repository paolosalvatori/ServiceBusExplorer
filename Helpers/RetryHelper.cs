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
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
#endregion

namespace Microsoft.WindowsAzure.CAT.ServiceBusExplorer
{
    /// <summary>
    /// This class contains a methods to implement retry logic
    /// </summary>
    public static class RetryHelper
    {
        #region Private Constants
        //***************************
        // Constants
        //***************************
        private const string ActionCannotBeNull = "Action argument cannot be null.";
        private const string FuncCannotBeNull = "Func argument cannot be null.";
        private const string RetryingMethod = "Exception: {0}. Method {1}: retry {2} of {3}.";
        private const int DefaultRetryCount = 10;
        private const int DefaultRetryTimeout = 100;
        #endregion

        #region Private Static Fields
        private static bool traceEnabled = false;
        private static int retryCount = DefaultRetryCount;
        private static int retryTimeout = DefaultRetryTimeout;
        #endregion

        #region Public Static Fields
        public static void RetryAction(Action action, WriteToLogDelegate writeToLog)
        {
            var numRetries = retryCount;

            if (action == null)
            {
                throw new ArgumentNullException(ActionCannotBeNull); 
            }
            do
            {
                try
                {
                    action(); 
                    return;
                }
                catch (MessagingEntityAlreadyExistsException)
                {
                    throw;
                }
                catch (CommunicationException)
                {
                    throw;
                }
                catch (MessagingException ex)
                {
                    if (numRetries == 0 || (!ex.IsTransient))
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 action.Method.Name, 
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
                catch (TimeoutException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 action.Method.Name, 
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
                catch (Exception ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 action.Method.Name, 
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
            } while (numRetries-- > 0);
        }

        public static T RetryFunc<T>(Func<T> func, WriteToLogDelegate writeToLog)
        {
            var numRetries = retryCount;

            if (func == null)
            {
                throw new ArgumentNullException(FuncCannotBeNull);
            }
            do
            {
                try
                {
                    return func();
                }
                catch (MessagingEntityAlreadyExistsException)
                {
                    throw;
                }
                catch (CommunicationException)
                {
                    throw;
                }
                catch (ServerBusyException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 func.Method.Name, 
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
                catch (MessagingCommunicationException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 func.Method.Name, 
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
                catch (TimeoutException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 func.Method.Name, 
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
                catch (Exception ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    if (traceEnabled)
                    {
                        writeToLog(string.Format(RetryingMethod, 
                                                 ex.Message, 
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1, 
                                                 retryCount), false);
                    }
                    Thread.Sleep(retryTimeout);
                }
            } while (numRetries-- > 0);
            return default(T);
        }
        #endregion

        #region Public Static Properties
        /// <summary>
        /// Gets or sets the debug flag.
        /// </summary>
        public static bool TraceEnabled
        {
            get
            {
                lock (typeof(RetryHelper))
                {
                    return traceEnabled;
                }
            }
            set
            {
                lock (typeof(RetryHelper))
                {
                    traceEnabled = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        public static int RetryCount
        {
            get
            {
                lock (typeof(RetryHelper))
                {
                    return retryCount;
                }
            }
            set
            {
                lock (typeof(RetryHelper))
                {
                    retryCount = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the retry timeout.
        /// </summary>
        public static int RetryTimeout
        {
            get
            {
                lock (typeof(RetryHelper))
                {
                    return retryTimeout;
                }
            }
            set
            {
                lock (typeof(RetryHelper))
                {
                    retryTimeout = value;
                }
            }
        }
        #endregion
    }
}
