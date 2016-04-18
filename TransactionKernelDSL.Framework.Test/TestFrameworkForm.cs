﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TransactionKernelDSL.Framework.Test
{
    public partial class TestFrameworkForm : Form
    {
        public TestFrameworkForm()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            TestEngine eng = (TestEngine.Instance as TestEngine);

            eng.Start();
			/// Kumar
			/// Lenin
            Thread.Sleep(2000);
            eng.Stop();
        }
    }
}
