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

        public void WriteFromServer(string content)
        {
            using (pipeWrite)
            {
                Console.WriteLine("Dining room write pipe : " + pipeWrite.GetClientHandleAsString());

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

        public List<string> ReadFromServer()
        {
            using (pipeRead)
            {
                Console.WriteLine("Dining room read pipe : " + pipeRead.GetClientHandleAsString());

                try
                {
                    result = readString(pipeRead);
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

                if (result.Count > 0)
                    Console.WriteLine("Received message from Kitchen : " + result[0]);

                //Console.ReadLine();
                return result;
            }
        }
    }
}
