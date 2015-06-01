using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TextMenu
{
    public class MenuItem
    {
        internal char Selector;
        internal string Description;
        internal Action Runner;

        public MenuItem(char Command, string Descrip, Action Response)
        {
            Selector = Command;
            Description = Descrip;
            Runner = Response;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(typeof(MenuItem)))
                return char.ToLower(((MenuItem)obj).Selector) == this.Selector;
            else if (obj.GetType().Equals(typeof(char)))
                return char.ToLower((char)obj) == this.Selector;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class MenuLabel : MenuItem
    {
        public MenuLabel(string Descrip)
            : base('\x00', Descrip, new Action(() => {}))
        {

        }
    }
    
    public class ItemAlreadyExistsException : InvalidOperationException
    { }

    public class Menu
    {
        protected TextWriter OutStream;
        protected TextReader InStream;
        protected bool ClearConsole;

        protected List<MenuItem> mItems = new List<MenuItem>();
        public readonly Action CloseSelf;
        private bool IsRunning = false, CaseSensitive;
        protected bool tempNoClear = false;

        public Menu(TextWriter Out = null, TextReader In = null, bool CaseSensitive = true, bool ClearConsole = false)
        {
            OutStream = Out ?? StreamWriter.Null;
            InStream = In ?? StreamReader.Null;
            this.CaseSensitive = CaseSensitive;
            this.ClearConsole = ClearConsole;
            CloseSelf = new Action(() => { IsRunning = false; });
        }

        public void AddItem(char Command, string Description, Action Response)
        {
            AddItem(new MenuItem(Command, Description, Response));
        }

        public void AddItem(MenuItem m)
        {
            if (!mItems.Any(q => q.Selector == m.Selector))
                mItems.Add(m);
            else
                throw new ItemAlreadyExistsException();
        }

        public void Remove(MenuItem m)
        {
            mItems.Remove(m);
        }

        public void Remove(char c)
        {
            mItems.RemoveAll(m => m.Selector == c);
        }

        public void Clear()
        {
            mItems.Clear();
        }

        public void Print(string s = "", params object[] FormatArgs)
        {
            if (ClearConsole && !tempNoClear) Console.Clear();
            tempNoClear = true;
            OutStream.Write(string.Format(s, FormatArgs));
        }

        public void PrintLine(string s = "", params object[] FormatArgs)
        {
            Print(s + OutStream.NewLine, FormatArgs);
        }

        public void PrintMenu(string InitialMessage = "Please choose an option:")
        {
            if (ClearConsole && !tempNoClear) Console.Clear();
            tempNoClear = false;
            OutStream.WriteLine(InitialMessage);
            foreach (MenuItem m in mItems)
            {
                OutStream.WriteLine(m.GetType() == typeof(MenuLabel) ?
                    m.Description :
                    String.Format("{0} - {1}", m.Selector, m.Description));
            }
        }

        public bool HandleResponse(string FailMessage = "Invalid command", bool ClearInputLine = true, bool Retry = false)
        {
            string input_str = "";
            MenuItem found = null;
            while (string.IsNullOrEmpty(input_str) || found == null)
            {
                char input;
                if (ClearInputLine)
                {
                    input_str = InStream.ReadLine();
                    if (!string.IsNullOrEmpty(input_str))
                        input = input_str[0];
                    else
                    {
                        if(FailMessage != null)
                            this.PrintLine(FailMessage);
                        if(Retry)
                            continue;
                        else
                            return false;
                    }
                }
                else
                    input = (char) InStream.Read();
                input = CaseSensitive ? input : char.ToLower(input);

                found = mItems.Find(m => (CaseSensitive ? m.Selector : char.ToLower(m.Selector)) == input);
                if (found == null)
                {
                    if (FailMessage != null)
                        this.PrintLine(FailMessage);
                    if (Retry)
                        continue;
                    else
                        return false;
                }
            }

            found.Runner();
            return true;
        }

        public double? RequestDouble(string Message = "Please input decimal number:", bool Retry = false, double? Default = null)
        {
                OutStream.WriteLine(string.Format(Message, Default));

            double? Response = null;
            double r;
            bool Success;
            do
            {
                string resp = InStream.ReadLine();
                if (!string.IsNullOrEmpty(resp))
                {
                    Success = double.TryParse(resp, out r);
                    if (Success)
                        return r;
                }
                else if (!Retry)
                {
                    return Default;
                }
                else
                    Success = false;
                if (OutStream != null)
                    OutStream.WriteLine("Input error: input not an integer, please try again");
            } while (Retry && !Success);
            return Response; // Always null if you got here
        }

        public int? RequestInt(string Message = "Please input integer number:", bool Retry = false, int? Default = null)
        {
                OutStream.WriteLine(string.Format(Message, Default));

            int? Response = null;
            int r;
            bool Success;
            do
            {
                string resp = InStream.ReadLine();
                if (!string.IsNullOrEmpty(resp))
                {
                    Success = int.TryParse(resp, out r);
                    if(Success)
                        return r;
                }
                else if (!Retry)
                {
                    return Default;
                }
                else
                    Success = false;
                if (OutStream != null)
                    OutStream.WriteLine("Input error: input not an integer, please try again");
            } while (Retry && !Success);
            return Response; // Always null if you got here
        }

        public bool RequestYN(string Message = "Please input Y or N:", bool Retry = false)
        {
                OutStream.WriteLine(Message);

            string Temp;
            bool Response = false;
            bool Success;
            if (!Retry)
            {
                Response = (Temp = InStream.ReadLine()).Length > 0 && char.ToLower(Temp[0]) == 'y';
            }
            else
            {
                do
                {
                    Success = (Temp = InStream.ReadLine()).Length > 0;
                    if (Success)
                    {
                        Response = char.ToLower(Temp[0]) == 'y';
                        break;
                    }
                    if (OutStream != null)
                        OutStream.WriteLine("Input error: input not valid, please try again (Y/N)");
                } while (!Success);
            }
            return Response;
        }

        public string RequestString(string Message = "Please input a string:")
        {
                OutStream.WriteLine(Message);

            return Console.ReadLine();
        }

        public static T GetValue<T>(StreamReader InStream, Func<string, T> Converter, Func<string, bool> ValidChecker, string PromptMessage = null, System.IO.StreamWriter OutStream = null, bool Reprompt = false)
        {
            if (!string.IsNullOrEmpty(PromptMessage))
                (OutStream ?? StreamWriter.Null).WriteLine(PromptMessage);
            string input = InStream.ReadLine();
            bool correct = ValidChecker(input);
            if (correct)
                return Converter(input);
            else if (Reprompt)
                return GetValue<T>(InStream, Converter, ValidChecker, PromptMessage, OutStream, Reprompt);
            else
                return default(T);
        }

        public void Run(string InitialMessage = null, string FailMessage = "Invalid command", bool ReprintOnFail = false)
        {
            var ShowMenu = InitialMessage == null ? new Action(() => PrintMenu(null)) : new Action(() => PrintMenu(InitialMessage: InitialMessage));
            IsRunning = true;
            while (IsRunning)
            {
                ShowMenu();
                while (!this.HandleResponse())
                {
                    if (FailMessage != null && FailMessage.Length > 0)
                        this.PrintLine(FailMessage);
                    if (ReprintOnFail)
                        ShowMenu();
                }
            }
        }

        public int? RequestIntInRange(int MinInc, int MaxExc, int? Cancel = null, int? Default = null, TextReader InStream = null, TextWriter OutStream = null, string Message = "Please input number between {0} and {1}:")
        {
            int? g;
            while (((g = RequestInt(string.Format(Message, MinInc, MaxExc, Default), Retry: true, Default: Default)) < MinInc ||
                g >= MaxExc ||
                g == null) &&
                g != Cancel)
            {
                // Do nothing, just loop again
            }
            return g ?? Default;
        }
    }

}
