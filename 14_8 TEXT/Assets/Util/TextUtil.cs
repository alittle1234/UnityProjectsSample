using UnityEngine;
using System.Collections;

using LitJson;
using System.Collections.Generic;

public class TextUtil {
	/// 
	/// The text dictionary. store text varnames, with translations.
	/// 
    private static Dictionary<string, string> td = new Dictionary<string, string>();

	static void initialize() {
		if(td == null) {
			td = new Dictionary<string, string>();
		}
	}

	/// 
	/// Add the specified textLibrary to the Text Dictionary.
	/// 
	/// <param name="textLibrary">Text library.</param>
	public static void add(TextLibrary textLibrary) {
		initialize();
		if(textLibrary != null && textLibrary.texts != null) {
			foreach(TextLibrary.Text text in textLibrary.texts) {
				set(text.text, text.content);
			}
		}
	}
    /// <summary>
    /// returns the text library.
    /// </summary>
    /// <returns>The text library.</returns>
    public static TextLibrary toTextLibrary(){
        TextLibrary tl =  new TextLibrary();
        tl.texts = new List<TextLibrary.Text>();

        initialize();

        foreach(string key in td.Keys) {
            tl.texts.Add(new TextLibrary.Text(key, td[key]));
        }
        return tl;
    }

    public static void set(string text, string content) {
		td[text] = content;
	}

	/// 
	/// Get the specified text translation.
	/// 
	/// <param name="text">Text.</param>
	public static string get(string text) {
        try{
            if(td.ContainsKey(text)) {
                return td[text];
            } else {
                return text;
            }
        }catch (KeyNotFoundException e){
            return text;
        }
	}
}

