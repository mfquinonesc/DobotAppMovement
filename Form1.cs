﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dobot.CPlusDll;




namespace Dobot
{
    public partial class Form1 : Form
    {
        private Timer myTimer;
        private bool estado = true;
        private Timer timeout;
        private int time;


        public Form1()
        {
            InitializeComponent();

          

            this.time = 0;

            this.timeout = new Timer();
            timeout.Tick += new EventHandler(timeout_tick);
            timeout.Interval = 1000;
            

            myTimer = new Timer();
            myTimer.Tick += new EventHandler(timer_Tick);
            myTimer.Interval = 2000;

        }

        private void timeout_tick(object sender, EventArgs e)
        {
            time = time + 1000;
            this.label3.Text= time.ToString();
        }


       private void timer_Tick(object sender, EventArgs e)
        {
            this.estado = !this.estado;
            if (this.estado)
            {
                this.moverXadelante();
            }
            else
            {
                this.moverXatras();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.conectar();

        }

        //Metodo para conectar el dobot
        private void conectar()
        {
            StringBuilder fwType = new StringBuilder(60);
            StringBuilder version = new StringBuilder(60);
            
            int ret = DobotDll.ConnectDobot("", 115200, fwType, version);
            if (ret != (int)DobotConnect.DobotConnect_NoError)
            {
                this.label1.Text = "no coneccion Error: "+ret.ToString() ;
                // El error 2  quiere decir que ya se esta ejecutando
            }
            else
            { 
                this.setHomeParams();
                this.label1.Text = "coneccion: "+ret.ToString();               
            }

        }


        //metodo creado para mover el dobot hacia adelante CMD 1
       
        private void moverXadelante()
        {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)1;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }

        //metodo creado para mover el dobot hacia adelante CMD 2

        private void moverXatras()
        {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)2;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }


        //Metodo para mover Y hacia der CDM 3
        private void moverYder() {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)3;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }

        //Metodo para mover Y hacia der CDM 4
        private void moverYizq()
        {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)4;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }


        //Metodo para mover Z hacia arriba CDM 5
        private void moverZarriba() {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)5;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }


        //Metodo para mover Z hacia abajo CDM 6
        private void moverZabajo()
        {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)6;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }


        //Metodo para mover Z hacia abajo CDM 7
        private void rotarDer()
        {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)7;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }


        //Metodo para mover Z hacia abajo CDM 8
        private void rotarIzq()
        {
            UInt64 cmdIndex = 0;
            JogCmd currentCmd;
            currentCmd.cmd = (byte)8;
            currentCmd.isJoint = (byte)0;
            DobotDll.SetJOGCmd(ref currentCmd, false, ref cmdIndex);
        }

        private void SetParam()
        {            
            UInt64 cmdIndex = 0;
            JOGJointParams jsParam;
            jsParam.velocity = new float[] { 200, 200, 200, 200 };
            jsParam.acceleration = new float[] { 200, 200, 200, 200 };
            DobotDll.SetJOGJointParams(ref jsParam, false, ref cmdIndex);

            JOGCommonParams jdParam;
            jdParam.velocityRatio = 100;
            jdParam.accelerationRatio = 100;
            DobotDll.SetJOGCommonParams(ref jdParam, false, ref cmdIndex);
        }

        private void SetHome() {
            // [DllImport("DobotDll.dll", EntryPoint = "SetHOMECmd", CallingConvention = CallingConvention.Cdecl)]
            // public static extern int SetHOMECmd(ref HOMECmd homeCmd, bool isQueued, ref UInt64 queuedCmdIndex);
            HOMECmd homeCmd;
            homeCmd.temp = 0;
            bool isQueued = false;
            UInt64 cmdIndex = 0;
            this.label2.Text= DobotDll.SetHOMECmd(ref homeCmd, isQueued, ref cmdIndex).ToString();
        }


        private void getHomeParams() {
            HOMEParams homeparams;
            homeparams.r = 0;
            homeparams.z = 0;
            homeparams.y = 0;
            homeparams.x = 0;
            this.label2.Text = DobotDll.GetHOMEParams(ref homeparams).ToString();
            this.label1.Text = "[" + homeparams.r.ToString()+ "," + homeparams.x.ToString() + "," + homeparams.y.ToString() + "," + homeparams.z.ToString() + "]";
             
        }

        private void setHomeParams() {

            //x = 250
            //y = 0
            //z = 50
            //r = 0

            UInt64 cmdIndex = 0;
            HOMEParams homeparams;
            homeparams.r = 0;
            homeparams.z = 50;
            homeparams.y = 0;
            homeparams.x = 250;
            this.label2.Text = DobotDll.SetHOMEParams(ref homeparams,false,ref cmdIndex).ToString();
            this.label1.Text = "[" + homeparams.r.ToString() + "," + homeparams.x.ToString() + "," + homeparams.y.ToString() + "," + homeparams.z.ToString() + "]";
        }


        private void Disconnect() {
            DobotDll.DisconnectDobot();        
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.moverXadelante();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.moverXatras();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myTimer.Start();           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            myTimer.Stop(); 
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.moverYder();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.moverYizq();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.moverZarriba();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.moverZabajo();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.rotarDer();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.rotarIzq();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            UInt64 cmdIndex = 0;
            DobotDll.SetEndEffectorSuctionCup(true, true, false, ref cmdIndex);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            this.SetParam();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.SetHome();
            this.label2.Text = "end";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            this.getHomeParams();
            this.label2.Text = "end!";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            this.setHomeParams();
            this.label2.Text = "end!!";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            this.Disconnect();
            this.label1.Text = "Disconect";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            

            
        }
    }




}
