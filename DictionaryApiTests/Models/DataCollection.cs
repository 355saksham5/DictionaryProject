using DictionaryApi.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DictionaryApiTests.Models
{
    internal class DataCollection<T>
    {
        [XmlArray("Data")]
        public T[] data { get; set; }
    }
}
