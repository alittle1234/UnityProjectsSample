// C# editor script
using UnityEngine;
using UnityEditor;

static class UsefulShortcuts {
	
	// CMD + SHIFT + C
	[MenuItem ("Tools/Clear Console %#c")]
	static void ClearConsole() {
		consoleClear();
	}

	public static void consoleClear() {
		
		// This simply does "LogEntries.Clear()" the long way:
		var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
		var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		clearMethod.Invoke(null, null);
	}
	
}