
using System;
namespace FlexUI
{
    public interface IDocParser
    {
        Element Parse(string source, Element parent, Document document);
    }
}

