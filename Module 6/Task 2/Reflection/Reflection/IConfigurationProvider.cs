﻿namespace Reflection;

public interface IConfigurationProvider
{
    void Load(object obj);
    void Save(object obj);
}
