using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScriptWords : MonoBehaviour {
	// params
	private string[] objectKeys = new string[]{
		"player",
		"world"
		//"obj123123" -- will be some kind of identifier, known by scripter
		//"a sword?" -- how will scripter get Item Type object? todo latter...
	};

	// key words used in scripts

//	public bool isEqual(Object obj1, Object obj2){
//		return false;
//	}
//
//	public bool isLessThan(Object obj1, Object obj2){
//		return false;
//	}
//
//	public bool isGreaterThan(Object obj1, Object obj2){
//		return false;
//	}

	public static bool movePlayer(string room){
		s_Scene.player.roomVarName = room;
		s_Scene.refreshUI();
		return true;
	}

    public static bool giveToPlayer(object obj1){
		return false;
	}

    public static bool takeFromPlayer(object obj1){
		return false;
	}
//
//	public bool playerHas(Object obj1, Object obj2){
//		return false;
//	}
//
//	public bool objectContainsItem(Object obj1, Object obj2){
//		return false;
//	}

	/// <summary>
	/// Should return an item carried by an object. like a sword or armor
	/// 
	/// To return a general item, first param should be "world"
	/// </summary>
	/// <returns><c>true</c>, if item was gotten, <c>false</c> otherwise.</returns>
	/// <param name="obj1">Obj1.</param>
	/// <param name="obj2">Obj2.</param>
//	public bool getItem(Object obj1, Object obj2){
//		return false;
//	}

	/// <summary>
	/// Should return an attribute of an item like health or power.
	/// </summary>
	/// <returns><c>true</c>, if attribute was gotten, <c>false</c> otherwise.</returns>
	/// <param name="obj1">Obj1.</param>
	/// <param name="obj2">Obj2.</param>
//	public bool getAttribute(Object obj1, Object obj2){
//		return false;
//	}

    public enum ScriptWordEnum{
        movePlayer, giveToPlayer, takeFromPlayer
    }
    private static Dictionary<string, ScriptWordEnum> valMap;
    public static Dictionary<string, ScriptWordEnum> getSWEValues(){
        if(valMap == null){
            valMap = new Dictionary<string, ScriptWordEnum>();
            foreach (ScriptWordEnum scr in (ScriptWordEnum[]) Enum.GetValues(typeof(ScriptWordEnum))){
                valMap.Add(Enum.GetName(typeof(ScriptWordEnum),scr), scr);
            }
        }
        return valMap;
    }

}
