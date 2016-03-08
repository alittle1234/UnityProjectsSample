using UnityEngine;
using System.Collections;

using LitJson;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class TextLoader {

	public static T loadJson<T>(string textAssetName) where T : class {
		TextAsset textAsset = Resources.Load(textAssetName) as TextAsset;
		
		using (var stream = new StringReader(textAsset.text)) {
			
			T codeObj = JsonMapper.ToObject<T>(textAsset.text);
			return codeObj;
		}
	}
	
	public static T loadXml<T>(string textAssetName) where T : class {
		TextAsset textAsset = Resources.Load(textAssetName) as TextAsset;
		
		using (StringReader stream = new StringReader(textAsset.text)) {
			XmlSerializer s = new XmlSerializer(typeof(T)); //typeof(T)
			//var s = new XmlSerializer(typeof(T));
			T deserializedXml = (T)s.Deserialize(stream);
			return deserializedXml;
		}
	}

    public static T loadXmlFile<T>(string fileName) where T : class {
        string fileCont = openFile(fileName);
        
        using (StringReader stream = new StringReader(fileCont)) {
            XmlSerializer s = new XmlSerializer(typeof(T)); //typeof(T)
            //var s = new XmlSerializer(typeof(T));
            T deserializedXml = (T)s.Deserialize(stream);
            return deserializedXml;
        }
    }

    public static string saveAsXml<T>(T xmlObject) where T : class {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        using (StringWriter writer = new StringWriter(sb)) {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            xmlSerializer.Serialize(writer, xmlObject);
            writer.Flush();
            writer.Close();
            return sb.ToString();
        }
    }
    public static string openFile(string file) {
        StreamReader reader = File.OpenText(file) as StreamReader;
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }

    public static void saveFile(string file, string content) {
        StreamWriter writer = File.CreateText(file) as StreamWriter;
        writer.WriteLine(content);
        writer.Close();
    }
}
