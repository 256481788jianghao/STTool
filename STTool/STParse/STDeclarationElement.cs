﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static STTool.STFile.STFileBase;
using static STTool.STParse.STElement;

namespace STTool.STParse
{
    class STDeclarationElement : STElement
    {
        public string Name;
        public ElementType ElType = ElementType.UNKNOWN;

        List<STLine> m_Lines = new List<STLine>();
        public List<STVARElement> VARElements = new List<STVARElement>();

        public STDeclarationElement(STFileType fileType):base(fileType)
        {
        }

        public void Parse(string content)
        {
            //FIXME:
            if (FileType == STFileType.INTERFACE)
            {
                ElType = ElementType.INTERFACE;
            }
            if (!string.IsNullOrEmpty(content))
            {
                string[] temp_lines = content.Split('\n');
                bool is_can_input = true;
                foreach(string item in temp_lines)
                {
                    string temp = item.Replace("\r", "").Trim();
                    temp = temp.Replace("\t", "");
                    if (!string.IsNullOrEmpty(temp) && !temp.StartsWith("//"))
                    {
                        if (temp.StartsWith("(*"))
                        {
                            is_can_input = false;
                        }
                        if (temp.Contains("*)"))
                        {
                            is_can_input = true;
                            continue;
                        }
                        if (is_can_input)
                        {
                            m_Lines.Add(new STLine(temp));
                        }
                        
                    }
                }
                ParseLines();
            }
        }

        private void ParseLines()
        {
            if(m_Lines.Count <= 0)
            {
                return;
            }
            else
            {
                if(FileType == STFileType.GVL)
                {
                    ElType = ElementType.GVL;
                    bool start_input_var = false;
                    STVARElement varElement = new STVARElement(STVARElement.STVARType.UNKNOWN);
                    for (int i = 0; i < m_Lines.Count; i++)
                    {
                        if (m_Lines[i].Content.StartsWith("{"))
                        {
                            continue;
                        }
                        if (m_Lines[i].Words[0] == "VAR_GLOBAL" && !start_input_var)
                        {
                            start_input_var = true;
                            varElement = new STVARElement(STVARElement.STVARType.GLOBAL);
                            continue;
                        }
                        if (m_Lines[i].Words[0] == "END_VAR" && start_input_var)
                        {
                            start_input_var = false;
                            VARElements.Add(varElement);
                        }
                        if (start_input_var)
                        {
                            varElement.Lines.Add(m_Lines[i]);
                        }
                    }
                    foreach (STVARElement item in VARElements)
                    {
                        item.Parse();
                    }
                }
                if(FileType == STFileType.INTERFACE)
                {
                    ElType = ElementType.INTERFACE;
                }
                if(FileType == STFileType.DUT)
                {

                }
                if(FileType == STFileType.POU)
                {
                    ParseNameAndTypePOU(m_Lines[0]);
                    bool start_input_var = false;
                    STVARElement varElement = new STVARElement(STVARElement.STVARType.UNKNOWN);
                    for (int i = 1; i < m_Lines.Count; i++)
                    {
                        if (m_Lines[i].Words[0] == "VAR" && !start_input_var)
                        {
                            start_input_var = true;
                            varElement = new STVARElement(STVARElement.STVARType.VAL);
                            continue;
                        }
                        if (m_Lines[i].Words[0] == "VAR_INPUT" && !start_input_var)
                        {
                            start_input_var = true;
                            varElement = new STVARElement(STVARElement.STVARType.INPUT);
                            continue;
                        }
                        if (m_Lines[i].Words[0] == "VAR_OUTPUT" && !start_input_var)
                        {
                            start_input_var = true;
                            varElement = new STVARElement(STVARElement.STVARType.OUTPUT);
                            continue;
                        }
                        if (m_Lines[i].Words[0] == "VAR_GLOBAL" && !start_input_var)
                        {
                            start_input_var = true;
                            varElement = new STVARElement(STVARElement.STVARType.GLOBAL);
                            continue;
                        }
                        if (m_Lines[i].Words[0] == "END_VAR" && start_input_var)
                        {
                            start_input_var = false;
                            VARElements.Add(varElement);
                        }
                        if (start_input_var)
                        {
                            varElement.Lines.Add(m_Lines[i]);
                        }
                    }
                }  
            }

            foreach(STVARElement item in VARElements)
            {
                item.Parse();
            }
        }

        private void ParseNameAndTypePOU(STLine line)
        {
            if(line.Words.Count >= 2)
            {
                Name = line.Words[1];
                ElType = ParseType(line.Words[0]);
                
            }
        }

        private ElementType ParseType(string typestr)
        {
            switch (typestr)
            {
                case "FUNCTION_BLOCK": { return ElementType.FUNCTIONBLOCK; }
                case "FUNCTION": { return ElementType.FUNCTION; }
                case "PROGRAM": { return ElementType.PROGRAM; }
                case "INTERFACE": { return ElementType.INTERFACE; }
                case "METHOD": { return ElementType.METHOD; }
                default:return ElementType.UNKNOWN;
            }
        }
    }
}
