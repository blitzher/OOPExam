using System;
using System.Collections.Generic;
using System.IO;

namespace Stregsystem
{
    class Logger
    {

        /* Store loggers in dictionary, such that no two loggers
         * are trying to access the same filestream, and any object  
         * can write to the logger it is suited for. */
        private static Dictionary<string, Logger> ExistingLoggers = new Dictionary<string, Logger>();

        private StreamWriter stream;

        private FileStream fileStream;

        /* Private constructor, since it should be created with the 
         * static method, GetLogger, see below. */
        private Logger(string Name)
        {
            if (!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }
            fileStream = File.Create(Path.Combine("Logs", Name + ".log"));
            stream = new StreamWriter(fileStream);
            ExistingLoggers.Add(Name, this);
        }

        public static void DisposeAll()
        {
            foreach(Logger logger in ExistingLoggers.Values)
            {
                logger.Dispose();
            }
        }

        private void Dispose()
        {
            stream.Close();
            fileStream.Close();
        }

        private void Write(string symbol, string Text)
        {
            string output = string.Format("{0} ({1}) {2}", symbol, DateTime.Now, Text);
            stream.WriteLine(output);
        }

        public void Log(string Text)
        {
            Write("[?]", Text);
        }

        public void Error(string Text)
        {
            Write("[X]", Text);
        }

        public void Warn(string Text)
        {
            Write("[!]", Text);
        }

        /* Replaces the constructor, which returns the existing logger,
         * if it already exists, or create a new if it doesn't. */
        public static Logger GetLogger(string Name)
        {
            if (ExistingLoggers.ContainsKey(Name))
            {
                return ExistingLoggers[Name];
            }

            return new Logger(Name);
        }
    }
}