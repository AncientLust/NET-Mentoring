﻿using OOP.Interfaces;

namespace OOP.InfoAttributes;

internal class Title : IInfo
{
    public string InfoName { get; set; }
    public object InfoValue { get; set; }
    public string InfoStringValue => (string)InfoValue;

    public Title() {}

    public Title(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty or white space only");

        InfoValue = title;
        InfoName = GetType().Name;
    }
}
