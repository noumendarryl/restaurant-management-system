using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace AppRestaurant.Controller.Pipes
{
    public class ServerThread : StreamString
    {
        public static AnonymousPipeServerStream pipeWrite;
        public static AnonymousPipeServerStream pipeRead;
        private static List<string> result;

        public ServerThread()
        {
            pipeWrite = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
            pipeRead = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
            result = new List<string>();
        }

        /*
		* Send a message to the client
		*/
        public void WriteFromServer(string content)
        {
            using (pipeWrite)
            {
                Console.WriteLine("Server write pipe : " + pipeWrite.GetClientHandleAsString());
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
		* Read a message sent from the client
		*/
        public List<string> ReadFromServer()
        {
            using (pipeRead)
            {
                Console.WriteLine("Server read pipe : " + pipeRead.GetClientHandleAsString());
                try
                {
                    string temp = readString(pipeRead);
                    for (int i = 0; i < temp.Split(';').Length; i++)
                        result.Add(temp.Split(';')[i]);
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

                for (int i = 0; i < result.Count - 1; i++)
                    Console.WriteLine("Received message from Client : " + result[i]);

                Console.ReadLine();
                return result;
            }
        }
    }
}
