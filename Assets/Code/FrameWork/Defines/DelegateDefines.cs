using UnityEngine;

public delegate void VoidDelegate();
public delegate void VoidDelegate_Col2d(Collider2D collider2d);
public delegate void VoidDelegate_GObj_Col2d(GameObject obj, Collision2D col);
public delegate bool BoolDelegate_GObj_Col2d(GameObject obj, Collision2D col);
public delegate bool BoolDelegate_GObj_GObj(GameObject obj, GameObject target);
public delegate bool BoolDelegate_E_Args(GameEvent eEventType, params object[] args);