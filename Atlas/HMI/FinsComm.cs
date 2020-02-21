using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OMRON.FinsGateway;
using OMRON.FinsGateway.Messaging;
using OMRON.FinsGateway.Messaging.FINS;
using OMRON.FinsGateway.EventMemory;
using OMRON.FinsGateway.Service;
using System.Diagnostics;


namespace HMI
{
    class FinsComm : IDisposable
    {
        public enum MemAreaBitCode
        {
            CIO_Bit = 0x30,
            WR_Bit = 0x31,
            HR_Bit = 0x32,
            AR_Bit = 0x33,
            DM_Bit = 0x02
        }

        public enum MemAreaWordCode
        {
            CIO_Word = 0xB0,
            WR_Word = 0xB1,
            HR_Word = 0xB2,
            AR_Word = 0xB3,
            DM_Word = 0x82
        }
        private static FinsComm instance;

        public static FinsComm Instance { get => instance; set => instance = value; }

        private FinsAddress peerAddr;
        private readonly Port port;

        private FinsComm(string addr)
        {
            port = new Port();
            port.Open("HMI");
            this.peerAddr = FinsAddress.Parse(addr);
        }

        public static void Init()
        {
            Debug.WriteLine(message: $"FinsAddr {HmiConfig.Instance.FinsAddr}");
            Instance = new FinsComm(HmiConfig.Instance.FinsAddr);
        }

        public void Dispose()
        {
            port.Close();
        }

        public bool ReadFloat(MemAreaWordCode memCode, uint addr, uint len, out float[] floatArray)
        {
            if (ReadWord(memCode, addr, len * 2, out byte[] byteArray))
            {
                Debug.WriteLine($"byteArray len={byteArray.Length}");
                floatArray = PLCDataConverter.PLC4ByteToSystemFloat(byteArray, 0, byteArray.Length / 4);
                return true;
            }
            else
            {
                floatArray = Array.Empty<float>();
                return false;
            }
        }

        public bool ReadInt(MemAreaWordCode memCode, uint addr, uint len, out int[] intArray)
        {
            if (ReadWord(memCode, addr, len, out byte[] byteArray))
            {
                Debug.WriteLine($"FinsComm.ReadInt out len={byteArray.Length}");
                intArray = PLCDataConverter.PLC2ByteAsBINToSystemInteger(byteArray, 0, byteArray.Length / 2);
                return true;
            }
            else
            {
                intArray = Array.Empty<int>();
                return false;
            }
        }
        public bool ReadWord(MemAreaWordCode memCode, uint addr, uint len, out byte[] memory)
        {
            Debug.Assert(len <= byte.MaxValue);
            Debug.Assert(addr <= ushort.MaxValue);
            string cmd = $"0101{((uint)memCode).ToString("X2")}{addr.ToString("X4")}00{len.ToString("X4")}";
            Debug.WriteLine($"FINS cmd={cmd}");
            FinsCommandMessage message = new FinsCommandMessage(cmd);

            FinsHead sendHead = new FinsHead();
            sendHead.Compose(peerAddr, -1);
            port.Send(sendHead, message.GetBytes(), message.Length);
            Byte[] rcvData;
            port.Receive(out _, out rcvData, 1000);

            FinsResponseMessage rcvMessage = new FinsResponseMessage(rcvData);

            if (rcvMessage.MRES != 0)
            {
                memory = Array.Empty<byte>();
                return false;
            }
            else
            {
                memory = rcvMessage.GetBytes(4, rcvMessage.Length - 4);
                return true;
            }
        }

        public bool ReadBit(MemAreaBitCode memCode, uint addr, uint bitAddr, uint len, out bool[] boolArray)
        {
            string cmd = $"0101{((uint)memCode).ToString("X2")}{(addr + bitAddr).ToString("X6")}{len.ToString("X4")}";
            Debug.WriteLine($"FINS cmd={cmd}");
            FinsCommandMessage message = new FinsCommandMessage(cmd);

            FinsHead sendHead = new FinsHead();
            sendHead.Compose(peerAddr, -1);
            port.Send(sendHead, message.GetBytes(), message.Length);
            Byte[] rcvData;
            port.Receive(out _, out rcvData, 1000);

            FinsResponseMessage rcvMessage = new FinsResponseMessage(rcvData);

            if (rcvMessage.MRES != 0)
            {
                boolArray = Array.Empty<bool>();
                return false;
            }
            else
            {
                byte[] memory = rcvMessage.GetBytes(4, rcvMessage.Length - 4);
                boolArray = PLCDataConverter.PLC1ByteAsBITToSystemBoolean(memory, 0, memory.Length);
                return true;
            }
        }
        public bool WriteWord(MemAreaWordCode mem_code, uint addr, in byte[] sendData)
        {
            int len = sendData.Length / 2;
            string cmd = $"0102{((uint)mem_code).ToString("X2")}{addr.ToString("X6")}{len.ToString("X4")}";
            Debug.WriteLine($"FINS cmd={cmd}");
            FinsCommandMessage message = new FinsCommandMessage(cmd);
            message.SetBytes(sendData, message.Length);

            FinsHead sendHead = new FinsHead();
            sendHead.Compose(peerAddr, -1);
            port.Send(sendHead, message.GetBytes(), message.Length);


            FinsHead rcvHead;
            Byte[] rcvData;
            port.Receive(out rcvHead, out rcvData, 1000);
            FinsResponseMessage rcvMessage = new FinsResponseMessage(rcvData);
            if (rcvMessage.MRES != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool WriteBit(MemAreaBitCode memCode, uint addr, uint bitAddr, in bool[] boolArray)
        {
            byte[] data = new byte[boolArray.Length];
            PLCDataConverter.SystemBooleanToPLC1ByteAsBIT(boolArray, ref data, 0);
            int len = data.Length;
            string cmd = $"0102{((uint)memCode).ToString("X2")}{(addr + bitAddr).ToString("X6")}{len.ToString("X4")}";
            Debug.WriteLine($"FINS cmd={cmd}");
            FinsCommandMessage message = new FinsCommandMessage(cmd);
            message.SetBytes(data, message.Length);

            FinsHead sendHead = new FinsHead();
            sendHead.Compose(peerAddr, -1);
            port.Send(sendHead, message.GetBytes(), message.Length);


            FinsHead rcvHead;
            Byte[] rcvData;
            port.Receive(out rcvHead, out rcvData, 1000);
            FinsResponseMessage rcvMessage = new FinsResponseMessage(rcvData);
            if (rcvMessage.MRES != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool TestCommunication()
        {
            FinsMessage sendMessage = new FinsCommandMessage("0501");

            // create FINS header
            FinsHead sendhead = new FinsHead();
            sendhead.Compose(peerAddr, -1);

            try
            {
                int sendsize = port.Send(sendhead, sendMessage.GetBytes(), sendMessage.Length);

                if (sendsize != sendMessage.Length)
                    return false;

                FinsHead receiveHead;
                Byte[] receiveData;
                int receivesize = port.Receive(out receiveHead, out receiveData, 1000);

                FinsResponseMessage responseMessage = new FinsResponseMessage(receiveData);

                return responseMessage.EndCode == 0;
            }
            catch (FgwMsgException msgexp)
            {
                Debug.WriteLine(msgexp.Message + "(" + msgexp.ErrorCode.ToString() + ")");
            }
            catch (Exception exp)
            {
                Debug.WriteLine(exp.Message);
            }

            return false;
        }
    }
}
