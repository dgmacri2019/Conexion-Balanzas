using Common.Responses;
using Program.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Program.Services.Implementations
{
    public class TcpClientHelper : ITcpClientHelper
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Contructor

        public TcpClientHelper()
        {
        }

        #endregion

        #region Public Methods

        public async Task<TcpClientResponse> ConnectAsync(string server, int port, byte[] bufferEscritura, CancellationToken cancellationToken = default)
        {
            TcpClientResponse result = new TcpClientResponse { Success = false, Message = "Fallo de inicialización" };
            List<byte> buffer = new List<byte>();
            using (Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                try
                {
                    await client.ConnectAsync(server, port, cancellationToken);
                    if (!client.Connected)
                    {
                        result.Message = "Socket no conectado";
                        return result;
                    }

                Line0: int resultSend = client.Send(bufferEscritura, SocketFlags.None);
                    bufferEscritura = null;
                    byte[] bufferLectura = null;
                    buffer.Clear();
                    client.ReceiveTimeout = 800;

                    while (client.Available > 0)
                    {
                        var currByte = new byte[1];
                        var byteCounter = client.Receive(currByte, currByte.Length, SocketFlags.None);

                        if (byteCounter.Equals(1))
                        {
                            buffer.Add(currByte[0]);
                        }
                    }

                   // result.Respuesta = Encoding.ASCII.GetString(buffer, 0, bytesRead);


                    result.Success = true;
                    return result;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    return result;
                }
                finally
                {

                }

        }

        #endregion


        #region Private Methods

        #endregion





    }
}
