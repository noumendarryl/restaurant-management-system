using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppRestaurant.Controller.Pipes
{
    public abstract class StreamString
    {
        /*
		* Character set
		*/
        private static UnicodeEncoding streamEncoding = new UnicodeEncoding();

        /*
		* Write a string using the character set and the writing pipe
		*/
        protected static void writeString(Stream pipeWrite, string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);

            int len = outBuffer.Length;
            if (len > ushort.MaxValue)
            {
                len = ushort.MaxValue;
            }

            pipeWrite.WriteByte((byte)(len / 256));
            pipeWrite.WriteByte((byte)(len & 255));
            pipeWrite.Write(outBuffer, 0, len);
            pipeWrite.Flush();
        }

        /*
		* Read a string using the character set and the reading pipe
		*/
        protected static string readString(Stream pipeRead)
        {
            int len;
            string str;
            len = pipeRead.ReadByte() * 256;
            len += pipeRead.ReadByte();
            var inBuffer = new byte[len];
            pipeRead.Read(inBuffer, 0, len);
            str = streamEncoding.GetString(inBuffer);
            return str;
        }
    }
}
