using UnityEngine;
using System.Collections;

using LitJson;

public class Panel {
	public string varname;
	public string name_txt;
	public int x;
	public int y;
	public int w;
	public int h;

	public string content;
	public Option[] options;

	public class Option {
		public string desc_txt;
		// pulblic int type; // use for different highlights
		// how to nest? option in option? ...might work
	}
}

