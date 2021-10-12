using System;

namespace com.vreshly.Service
{
    public interface ILogger
    {
        void Error(Exception ex);
        void Info(string info);
    }
}