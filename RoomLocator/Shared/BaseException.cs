﻿using System;

namespace Shared
{
    public class BaseException : Exception
    {
        public string Title { get; set; }

        public BaseException(string title, string message) : base(message)
        {
            Title = title;
        }
    }
}
