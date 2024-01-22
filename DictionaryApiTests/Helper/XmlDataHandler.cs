using DictionaryApi.Models.DTOs;
using DictionaryApiTests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DictionaryApiTests.Helper
{
    internal static class XmlDataHandler
    {
        public async static Task<IEnumerable<object[]>?> GetTestCasesAsync()
        {

            return new List<object[]>()
    {
        new object[]{ 1.2m, 2.3m,},
        new object[]{ 1.5m, 0.5m,  },
        new object[]{ 1.5m, 2.0m,  }
    };
            string? path = ConstantResources.AntonymCorrectFormatXmlPath;
            XmlSerializer serializer = new XmlSerializer(typeof(DataCollection<Antonyms>));
            StreamReader reader = new StreamReader(path);
            var dataCollection = (DataCollection<Antonyms>)serializer.Deserialize(reader);
            reader.Close();
            //return dataCollection?.data;
        }

        
    }
}
