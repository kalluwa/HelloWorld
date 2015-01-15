using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptIn14Days.src
{
    class LineNumberReader
    {
        string Source;
        int CurrentLineNumber;
        StringBuilder stringToBeBuilt;

        int CurrentPosInSource;
        /// <summary>
        /// TODO:source.Replace("\n\r","\n");
        /// </summary>
        /// <param name="source"></param>
        public LineNumberReader(string source)
        {
            Source = source;
            CurrentLineNumber = 0;
            CurrentPosInSource = 0;
            stringToBeBuilt = new StringBuilder();
        }   

        public int GetCurrentLine()
        {
            return CurrentLineNumber;
        }

        public string ReadLine()
        {
            //in .Net 3.5
            //stringToBeBuilt.Length=0;
            //in .Net4.0 
            stringToBeBuilt.Clear();
            while (Source.Length > CurrentPosInSource)
            {
                if (Source[CurrentPosInSource] == '\n')
                {
                    CurrentPosInSource++;
                    break;
                }
                stringToBeBuilt.Append(Source[CurrentPosInSource]);
                CurrentPosInSource++;
                if (Source.Length == CurrentPosInSource)
                {
                    CurrentLineNumber++;
                    return stringToBeBuilt.ToString();
                }
            }
            
            CurrentLineNumber++;

            if (Source.Length == CurrentPosInSource)
                return "eof";

            return stringToBeBuilt.ToString();
        }

        
    }
}
