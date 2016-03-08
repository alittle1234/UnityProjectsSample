using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Library")]
public class TextLibrary {
	[XmlElement("Text")]
	public List<Text> texts;
	
	public class Text {
        public Text(){}
        public Text(string key, string value){
            text = key;
            content = value;
        }
		[XmlAttribute ("k")]
		public string text;
		[XmlAttribute ("v")]
		public string content;
	}

}






