using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace AppRestaurant.Controller.Pipes
{
    public class ServerThread : StreamString
    {
        private static AnonymousPipeServerStream pipeRead;
        private static AnonymousPipeServerStream pipeWrite;
        private static List<string> result;

        public ServerThread()
        {
            pipeWrite = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            pipeRead = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
        }

        public void WriteFromServer()
        {
            using (pipeWrite)
            {
                Thread writeThread = new Thread(new ParameterizedThreadStart(ClientThread.ReadFromClient));
                writeThread.Start(pipeWrite.GetClientHandleAsString());
                Console.WriteLine("Dining room write pipe : " + pipeWrite.GetClientHandleAsString());

                try
                {
                    writeString(pipeWrite, "Hello from Dining room !");
                }
                catch (IOException ex)
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

        public List<string> ReadFromServer()
        {
            using (pipeRead)
            {
                Thread readThread = new Thread(new ParameterizedThreadStart(ClientThread.WriteFromClient));
                result = new List<string>();
                readThread.Start(pipeRead.GetClientHandleAsString());
                Console.WriteLine("Dining room read pipe : " + pipeRead.GetClientHandleAsString());

                try
                {
                    result = readString(pipeRead);
                }
                catch (IOException ex)
                {
                    //TODO Exception handling/logging
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    pipeRead.Close();
                }

                if (result.Count > 0)
                    Console.WriteLine("Received message from Kitchen : " + result[0]);

                Console.ReadLine();
                return result;
            }
        }
    }
}
