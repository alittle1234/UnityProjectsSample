using UnityEngine;
using System.Collections;
using AssemblyCSharp.Level;


class cs_SceneLoader
{
	public Level loadScene (string jsonResource) {
		Level level = TextLoader.loadJson<Level>(jsonResource);

		return level;
	}
}
