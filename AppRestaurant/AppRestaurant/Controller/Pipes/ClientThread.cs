using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Controller.Pipes
{
    public class ClientThread : StreamString
    {
        private static AnonymousPipeClientStream pipeRead;
        private static AnonymousPipeClientStream pipeWrite;
        private static List<string> values;

        public ClientThread()
        {
            values = new List<string>();
        }

        /*
		* Send a message to the server
		*/
        public void WriteFromClient(string content)
        {
            Console.WriteLine("Client write pipe : " + ServerThread.pipeRead.GetClientHandleAsString());
            pipeWrite = new AnonymousPipeClientStream(PipeDirection.Out, ServerThread.pipeRead.GetClientHandleAsString());
           
            using (pipeWrite)
            {
                try
                {
                    writeString(pipeWrite, content);
                }
                catch (Exception ex)
                {
                    //TODO Exception handling/logging
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    pipeWrite.Close();
                }
            }
        }

        /*
		* Read a message sent from the server
		*/
        public List<string> ReadFromClient()
        {
            Console.WriteLine("Client read pipe : " + ServerThread.pipeWrite.GetClientHandleAsString());
            pipeRead = new AnonymousPipeClientStream(PipeDirection.In, ServerThread.pipeWrite.GetClientHandleAsString());
            
            using (pipeRead)
            {
                try
                {
                    string temp = readString(pipeRead);
                    for (int i = 0; i < temp.Split(';').Length; i++)
                        values.Add(temp.Split(';')[i]);
                }
                catch (Exception ex)
                {
                    //TODO Exception handling/logging
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    pipeRead.Close();
                }

                for (int i = 0; i < values.Count - 1; i++)
                    Console.WriteLine("Received message from Server : " + values[i]);

                //Console.ReadLine();
                return values;
            }
        }
    }
}
