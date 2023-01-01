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

        public void WriteFromClient(string content)
        {
            Console.WriteLine("Kitchen write pipe : " + ServerThread.pipeRead.GetClientHandleAsString());
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

        public List<string> ReadFromClient()
        {
            Console.WriteLine("Kitchen read pipe : " + ServerThread.pipeWrite.GetClientHandleAsString());
            pipeRead = new AnonymousPipeClientStream(PipeDirection.In, ServerThread.pipeWrite.GetClientHandleAsString());
            
            using (pipeRead)
            {
                try
                {
                    values = readString(pipeRead);
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

                if (values.Count > 0)
                    Console.WriteLine("Received message from Dining room : " + values[0]);

                //Console.ReadLine();
                return values;
            }
        }
    }
}
