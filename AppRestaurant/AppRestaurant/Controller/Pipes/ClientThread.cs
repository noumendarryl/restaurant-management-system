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

        public static void WriteFromClient(object parentHandle)
        {
            Console.WriteLine("Kitchen write pipe : " + parentHandle.ToString());
            pipeWrite = new AnonymousPipeClientStream(PipeDirection.Out, parentHandle.ToString());
           
            using (pipeWrite)
            {
                try
                {
                    writeString(pipeWrite, "Hello from Kitchen !");
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

        public static void ReadFromClient(object parentHandle)
        {
            Console.WriteLine("Kitchen read pipe : " + parentHandle.ToString());
            pipeRead = new AnonymousPipeClientStream(PipeDirection.In, parentHandle.ToString());
            
            using (pipeRead)
            {
                try
                {
                    values = readString(pipeRead);
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

                if (values.Count > 0)
                    Console.WriteLine("Received message from Dining room : " + values[0]);

                Console.ReadLine();
            }
        }
    }
}
