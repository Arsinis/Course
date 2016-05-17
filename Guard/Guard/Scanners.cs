using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using PacketDotNet;
using SharpPcap;
using System.Timers;
using System.Security.Permissions;

namespace Guard
{
    public class Scanners
    {
        public SortedSet<string> ports = new SortedSet<string>();

        public SortedSet<string> ips = new SortedSet<string>();

        RegistryKey hklm = Registry.LocalMachine;

        public long Count = 0;

        public long RegChan = 0;

        public System.Timers.Timer myTimer = new System.Timers.Timer();

        public CaptureDeviceList deviceList;

        public ICaptureDevice captureDevice;

        public FileSystemWatcher watcher;

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            RegChan++;
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            RegChan++;
        }

        private void Program_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            Count++;
            // парсинг всего пакета
            Packet packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            
            // получение только TCP пакета из всего фрейма
            var tcpPacket = TcpPacket.GetEncapsulated(packet);
            // получение только IP пакета из всего фрейма
            var ipPacket = IpPacket.GetEncapsulated(packet);
            if (tcpPacket != null && ipPacket != null)
            {
                DateTime time = e.Packet.Timeval.Date;
                int len = e.Packet.Data.Length;

                // IP адрес отправителя
                var srcIp = ipPacket.SourceAddress.ToString();
                ips.Add(srcIp);
                // IP адрес получателя
                //var dstIp = ipPacket.DestinationAddress.ToString();

                // порт отправителя
                //var srcPort = tcpPacket.SourcePort.ToString();
                // порт получателя
                var dstPort = tcpPacket.DestinationPort.ToString();
                ports.Add(dstPort);
                // данные пакета
                //var data = tcpPacket.PayloadPacket;
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            captureDevice.OnPacketArrival -=Program_OnPacketArrival;
            watcher.Changed -= OnChanged;
            watcher.Created -= OnChanged;
            watcher.Deleted -= OnChanged;
            watcher.Renamed -= OnRenamed;
            captureDevice.StopCapture();
        }

        public void Scan()
        {
            myTimer = new System.Timers.Timer(4100);
            myTimer.Elapsed += Timer_Elapsed;
            myTimer.AutoReset = false;
            myTimer.Start();

            watcher = new FileSystemWatcher();
            watcher.Path = "C:/Windows";
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnRenamed;

            watcher.EnableRaisingEvents = true;

            deviceList = CaptureDeviceList.Instance;
            captureDevice = deviceList[1];
            captureDevice.OnPacketArrival += Program_OnPacketArrival;
            captureDevice.Open(DeviceMode.Promiscuous, 1000);
            captureDevice.StartCapture();

        }

        public void GenerateAttack()
        {
            string attack = "";
            #region traffic
            if (Count == 0)
                attack += "traffic=none and ";
            else if (Count > 50)
                attack += "traffic=small and ";
            else if (Count > 100)
                attack += "traffic=medium and ";
            else if (Count > 500)
                attack += "traffic=large and ";
            #endregion

            #region portimpact
            if (ports.Count() == Count)
                attack += "portimpact=little and";
            else if (ports.Count() < Count / 10)
                attack += "portimpact=medium and";
            else if (ports.Count() < Count / 100)
                attack += "portimpact=big and";
            #endregion

            #region workwithregistry
            RegistryKey hklmnow = Registry.LocalMachine;
            if (hklmnow == hklm)
                attack += "workwithregistry=no and ";
            else
                attack += "workwithregistry=yes and ";
            #endregion

            #region elevation

            if (RegChan == 0)
                attack += "elevation=no and ";
            else
                attack += "elevation=yes and";
            #endregion

            #region CanalsOpen
            if (ips.Count() == Count)
                attack += "CanalsOpen=little and";
            else if (ips.Count() < Count / 10)
                attack += "CanalsOpen=average and";
            else if (ips.Count() < Count / 100)
                attack += "CanalsOpen=much and";
            #endregion

            #region identificate
            if (ips.Contains("192.168.100.1"))
                attack += "identificate=no";
            else
                attack += "identificate=yes";
            #endregion
            StreamWriter sw = new StreamWriter("e:\\1.txt");
            sw.Write(attack);
            sw.Close();
        }
    }
}
