using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Vector2 mouse;
	public int w  = 32;
	public int h = 32;
	public Texture2D cursorTexture;

	void Start()
	{

		Cursor.visible = false;
	}

	void Update()
	{
		mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(mouse.x - (w / 2), mouse.y - (h / 2), w, h), cursorTexture);
	}
}
