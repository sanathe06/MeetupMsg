using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MeetupMsg
    {
    public static class Log
        {

        private static string folderPath =Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"/Log") ;
        public static void D(string message)
            {
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);

            FileStream fs = new FileStream(folderPath + "\\log.txt",
                                FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(message);
            m_streamWriter.Flush();
            m_streamWriter.Close();
            }

        }
    }