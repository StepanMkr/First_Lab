using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

[Serializable]
[XmlRoot("SortingTasks")]
public class SortingConfig
{
    public Settings Settings { get; set; }
    public Arrays Arrays { get; set; }
}

[Serializable]
public class Settings
{
    public string LogFilePath { get; set; }
    public bool GenerateRandomArrays { get; set; }
    public bool TestPresorted { get; set; }
}

[Serializable]
public class Arrays
{
    [XmlElement("Array")]
    public ArrayConfig[] StaticArrays { get; set; }
    
    public RandomArraysConfig RandomArrays { get; set; }
}

[Serializable]
public class ArrayConfig
{
    [XmlElement("Values")]
    public string Values { get; set; }
    
    public int[] GetIntArray()
    {
        string[] parts = Values.Split(',');
        int[] result = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            result[i] = int.Parse(parts[i].Trim());
        }
        return result;
    }
}

[Serializable]
public class RandomArraysConfig
{
    [XmlElement("Size")]
    public int[] Sizes { get; set; }
    
    public int MinValue { get; set; }
    public int MaxValue { get; set; }
}