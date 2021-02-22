#region Copyright
//=======================================================================================
// Microsoft Azure Customer Advisory Team 
//
// This sample is supplemental to the technical guidance published on my personal
// blog at http://blogs.msdn.com/b/paolos/. 
// 
// Author: Paolo Salvatori
//=======================================================================================
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// LICENSED UNDER THE APACHE LICENSE, VERSION 2.0 (THE "LICENSE"); YOU MAY NOT USE THESE 
// FILES EXCEPT IN COMPLIANCE WITH THE LICENSE. YOU MAY OBTAIN A COPY OF THE LICENSE AT 
// http://www.apache.org/licenses/LICENSE-2.0
// UNLESS REQUIRED BY APPLICABLE LAW OR AGREED TO IN WRITING, SOFTWARE DISTRIBUTED UNDER THE 
// LICENSE IS DISTRIBUTED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY 
// KIND, EITHER EXPRESS OR IMPLIED. SEE THE LICENSE FOR THE SPECIFIC LANGUAGE GOVERNING 
// PERMISSIONS AND LIMITATIONS UNDER THE LICENSE.
//=======================================================================================
#endregion

#region Using Directives

using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using ServiceBusExplorer.Utilities.Helpers;
using Microsoft.ServiceBus.Messaging;

#endregion

namespace ServiceBusExplorer.Helpers
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
        private const string CaughtGenericException = "Exception type was: {0}.";
        private const int DefaultRetryCount = 10;
        private const int DefaultRetryTimeout = 100;
        #endregion

        #region Private Static Fields
        private static bool traceEnabled;
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
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 action.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    Thread.Sleep(retryTimeout);
                }
                catch (TimeoutException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 action.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    Thread.Sleep(retryTimeout);
                }
                catch (Exception ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 action.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    writeToLog(string.Format(CaughtGenericException, ex.GetType()));
                    Thread.Sleep(retryTimeout);
                }
            } while (numRetries-- > 0);
        }

        public static async Task RetryActionAsync(Func<Task> action, WriteToLogDelegate writeToLog)
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
                    await action();
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
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 action.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    
                    await Task.Delay(retryTimeout);
                }
                catch (TimeoutException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 action.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    
                    await Task.Delay(retryTimeout);
                }
                catch (Exception ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 action.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    writeToLog(string.Format(CaughtGenericException, ex.GetType()));
                    
                    await Task.Delay(retryTimeout);
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
                                                 retryCount));
                    }
                    Thread.Sleep(retryTimeout);
                }
                catch (MessagingCommunicationException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    Thread.Sleep(retryTimeout);
                }
                catch (MessagingException ex)
                {
                    if (numRetries == 0 || (!ex.IsTransient))
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    Thread.Sleep(retryTimeout);
                }
                catch (TimeoutException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    Thread.Sleep(retryTimeout);
                }
                catch (Exception ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    writeToLog(string.Format(CaughtGenericException, ex.GetType()));
                    Thread.Sleep(retryTimeout);
                }
            } while (numRetries-- > 0);
            return default(T);
        }

        public static async Task<T> RetryFuncAsync<T>(Func<Task<T>> func, WriteToLogDelegate writeToLog)
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
                    return await func();
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
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    
                    await Task.Delay(retryTimeout);
                }
                catch (MessagingCommunicationException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    
                    await Task.Delay(retryTimeout);
                }
                catch (MessagingException ex)
                {
                    if (numRetries == 0 || (!ex.IsTransient))
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    
                    await Task.Delay(retryTimeout);
                }
                catch (TimeoutException ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    
                    await Task.Delay(retryTimeout);
                }
                catch (Exception ex)
                {
                    if (numRetries == 0)
                    {
                        throw;
                    }
                    writeToLog(string.Format(RetryingMethod,
                                                 ex.Message,
                                                 func.Method.Name,
                                                 retryCount - numRetries + 1,
                                                 retryCount));
                    writeToLog(string.Format(CaughtGenericException, ex.GetType()));
                    
                    await Task.Delay(retryTimeout);
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
