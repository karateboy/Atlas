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

        public void ReadBytes(uint start, uint len)
        {
            FinsHead sendHead = new FinsHead();
            FinsCommandMessage message = new FinsCommandMessage("0101820000000064");

            sendHead.Compose(peerAddr, -1);
            port.Send(sendHead, message.GetBytes(), message.Length);

            FinsHead rcvHead;
            Byte[] rcvData;
            port.Receive(out rcvHead, out rcvData, 1000);

            FinsResponseMessage rcvMessage = new FinsResponseMessage(rcvData);

            Byte[] memoryData = rcvMessage.GetBytes(4, rcvMessage.Length - 4);
            int[] wordData = PLCDataConverter.PLC2ByteAsBINToSystemInteger(memoryData, 0, memoryData.Length / 2);
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
