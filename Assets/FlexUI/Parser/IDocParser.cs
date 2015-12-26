
using System;
namespace catwins.flexui
{
    public interface IDocParser
    {
        Element Parse(string source, Element parent, Document document);
    }
}

