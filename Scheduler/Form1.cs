using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentScheduler;

namespace Scheduler3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            JobManager.Initialize();

            JobManager.AddJob(
                 () => message = "5",
                 s => s.ToRunEvery(5).Seconds()
                );

            JobManager.AddJob(
                  () => message = "3",
                 s => s.ToRunEvery(3).Seconds()
                );
            timer1.Start();

            Action action1 = new Action(()=> textUpdateInvoke(label2,DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")));

            JobManager.AddJob(
                 action1,
                 s => s.ToRunEvery(5).Seconds()
                );

        }

        string message = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = message;
            label1.Refresh();
        }

        public delegate void delegate_textUpdate(Control targetControl, string Text);
        public void textUpdateInvoke(Control targetControl, string Text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new delegate_textUpdate(this.textUpdate), targetControl,Text);
                return;
            }
            this.textUpdate(targetControl, Text);
        }

        public void textUpdate(Control targetControl, string Text)
        {
            if (targetControl is Label) { ((Label)targetControl).Text = Text; }
            if (targetControl is Button) { ((Button)targetControl).Text = Text; }
        }

    }

}
