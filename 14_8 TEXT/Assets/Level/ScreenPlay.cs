using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace AssemblyCSharp.Level {

    [XmlRoot("ScreenPlay")]
public class ScreenPlay {
    [XmlElement("D")]
	public Dialog[] dialog;
	
    public class Dialog {
        [XmlAttribute ("actor")]
		public string text;
        [XmlAttribute ("when")]
        public string when;
        [XmlAttribute ("target")]
        public string target;
        [XmlAttribute ("set")]
        public string set;
        [XmlAttribute ("value")]
        public string value;

        
        [XmlText]
        public string line;

        [XmlElement("D")]
        public Dialog[] dialog;
	}

}

}






