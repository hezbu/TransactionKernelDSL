using log4net;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract partial class AbstractTcpTriggeredMultiThreadedPushPullInputTransactionEngine : AbstractThreadedInputTransactionEngine
    {
        protected void TrackThreadWorker()
        {
            try
            {
                _Log.InfoFormat("TrackThreadWorker on..");
                while (_IsListening)
                {
                    var trackerInfo = DoTrackerWork();
                    if (trackerInfo != null)
                    {
                        if (_OnTrackSuccessEvent != null) _OnTrackSuccessEvent(this, trackerInfo);
                    }
                    Thread.Sleep(_TrackerSleep);
                }
            }
            catch (Exception ex)
            {
                _Log.FatalFormat("Exception found at TrackThreadWorker: {0}", ex);

            }
            finally
            {
                _Log.InfoFormat("TrackThreadWorker off..");
            }
        }

        protected abstract OnTrackArgs DoTrackerWork();        

    }
}
