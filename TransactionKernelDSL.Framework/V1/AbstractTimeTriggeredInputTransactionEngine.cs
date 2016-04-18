using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TransactionKernelDSL.Framework.V1
{
    public abstract class AbstractTimeTriggeredInputTransactionEngine : AbstractInputTransactionEngine
    {
        protected List<TimeTriggeredTransactionEngineContext> _TriggerTimers = null;
        protected bool _IsTriggering = false;

        protected Timer this[string name]
        {
            get
            {
                return _TriggerTimers.Find(t => t.TriggerName == name).TriggerTimer;
            }
            set
            {
                _TriggerTimers.Find(t => t.TriggerName == name).TriggerTimer = value;
            }
        }

        public override bool IsEngineOn
        {
            get { return _IsTriggering; }
        }



        #region Constructor
        protected AbstractTimeTriggeredInputTransactionEngine()
            : base()
        {
            _TriggerTimers = new List<TimeTriggeredTransactionEngineContext>();
        }
        #endregion

        protected virtual bool Add(string triggerName, int triggerDueTimeCheck, int triggerIntervalTimeCheck)
        {
            if (_TriggerTimers.Find(t => t.TriggerName == triggerName) != null)
            {
                _Log.Error("Timer '" + triggerName + "' already exists. Cannot add any with this name");
                return false;
            }
            else
            {
                _TriggerTimers.Add(new TimeTriggeredTransactionEngineContext(triggerName, TimeTriggeredTransactionEngineCallback, triggerDueTimeCheck, triggerIntervalTimeCheck));
                _Log.Info("Timer '" + triggerName + "' added OK!");
                return true;
            }
        }
        protected virtual bool Remove(string triggerName)
        {
            if (_TriggerTimers.Find(t => t.TriggerName == triggerName) == null)
            {
                _Log.Error("Timer '" + triggerName + "' doesn't exist. Cannot remove any with this name");
                return false;
            }
            else
            {
                _TriggerTimers.Find(t => t.TriggerName == triggerName).Dispose();
                _TriggerTimers.RemoveAt(_TriggerTimers.FindIndex(t => t.TriggerName == triggerName));
                _Log.Info("Timer '" + triggerName + "' removed OK!");
                return true;
            }
        }

        public override bool Start()
        {
            foreach (TimeTriggeredTransactionEngineContext t in _TriggerTimers)
            {
                try
                {
                    t.StartTimer();
                    _Log.Info("Timer '" + t.ToString() + "' started OK!");
                }
                catch (Exception ex)
                {
                    _Log.Fatal("Exception found starting Timer '" + t.ToString() + "': " + ex.Message + " - SS:" + ex.StackTrace);
                }

            }

            return true;
        }

        public override bool Stop()
        {
            foreach (TimeTriggeredTransactionEngineContext t in _TriggerTimers)
            {
                try
                {
                    t.Dispose();
                    _Log.Info("Timer '" + t.ToString() + "' disposed OK!");
                }
                catch (Exception ex)
                {
                    _Log.Fatal("Exception found disposing Timer '" + t.ToString() + "': " + ex.Message + " - SS:" + ex.StackTrace);
                }
            }
            return true;
        }

        public virtual bool Reset(string triggerName, int? dueTime = null, int? intervalTime = null)
        {
            if (_TriggerTimers.Find(t => t.TriggerName == triggerName) == null)
            {
                _Log.Error("Timer '" + triggerName + "' doesn't exist. Cannot reset any with this name");
                return false;
            }
            else
            {
                if (dueTime.HasValue) _TriggerTimers.Find(t => t.TriggerName == triggerName).TriggerDueTimeCheck = dueTime.Value;
                if (intervalTime.HasValue) _TriggerTimers.Find(t => t.TriggerName == triggerName).TriggerIntervalTimeCheck = intervalTime.Value;

                _TriggerTimers.Find(t => t.TriggerName == triggerName).StartTimer();
                _Log.Info("Timer '" + triggerName + "' reset OK!");
                return true;
            }
        }

        public virtual bool Pause(string triggerName)
        {
            if (_TriggerTimers.Find(t => t.TriggerName == triggerName) == null)
            {
                _Log.Error("Timer '" + triggerName + "' doesn't exist. Cannot pause any with this name");
                return false;
            }
            else
            {
                _TriggerTimers.Find(t => t.TriggerName == triggerName).StopTimer();
                _Log.Info("Timer '" + triggerName + "' paused OK!");
                return true;
            }
        }

        protected virtual void TimeTriggeredTransactionEngineCallback(object callbackParam)
        {
            AbstractTransactionParser parser = ParserFactory(callbackParam);
            AbstractTransactionHandler transaction = TransactionHandlerFactory(parser.RequestStructure.GetOperationId());
            transaction.Client = this;
            transaction.Parser = parser;
            transaction.DoTransaction();
        }

        public class TimeTriggeredTransactionEngineContext : IDisposable
        {
            private Timer _TriggerTimer = null;
            private string _TriggerName = null;
            private int _TriggerIntervalTimeCheck = Timeout.Infinite;
            private int _TriggerDueTimeCheck = Timeout.Infinite;

            public Timer TriggerTimer
            {
                get
                {
                    return _TriggerTimer;
                }
                set
                {
                    _TriggerTimer = value;
                }
            }
            public int TriggerIntervalTimeCheck
            {
                get
                {
                    return _TriggerIntervalTimeCheck;
                }
                set
                {
                    _TriggerIntervalTimeCheck = value;
                }
            }
            public int TriggerDueTimeCheck
            {
                get
                {
                    return _TriggerDueTimeCheck;
                }
                set
                {
                    _TriggerDueTimeCheck = value;
                }
            }
            public string TriggerName
            {
                get
                {
                    return _TriggerName;
                }
            }

            public TimeTriggeredTransactionEngineContext(string triggerName, TimerCallback triggerDelegate, int triggerDueTimeCheck, int triggerIntervalTimeCheck)
            {
                _TriggerName = triggerName;
                _TriggerDueTimeCheck = triggerDueTimeCheck;
                _TriggerIntervalTimeCheck = triggerIntervalTimeCheck;
                _TriggerTimer = new Timer(new TimerCallback(triggerDelegate), this, Timeout.Infinite, Timeout.Infinite);
            }

            public void StartTimer()
            {
                _TriggerTimer.Change(((_TriggerDueTimeCheck == Timeout.Infinite) ? _TriggerDueTimeCheck : _TriggerDueTimeCheck * 1000), ((_TriggerIntervalTimeCheck == Timeout.Infinite) ? _TriggerIntervalTimeCheck : _TriggerIntervalTimeCheck * 1000));
            }

            public void StopTimer()
            {
                _TriggerTimer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            public override string ToString()
            {
                return _TriggerName + "(" + _TriggerDueTimeCheck.ToString() + "/" + _TriggerIntervalTimeCheck.ToString() + ")";
            }

            #region IDisposable Members

            public void Dispose()
            {
                _TriggerTimer.Dispose();
            }

            #endregion
        }
    }
}
