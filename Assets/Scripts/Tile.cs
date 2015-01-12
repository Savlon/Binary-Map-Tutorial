using UnityEngine;
using System.Collections;

//Place different tile types here
public enum TileType
{
	Grass,
	Dirt
}

//Direction for each neighbour check. Can be changed to utilize 8 directions instead of 4
public enum Direction
{
	Up = 0,
	Right = 1,
	Down = 2,
	Left = 3
}

public class Tile : MonoBehaviour 
{
	public TileType type;
	public Sprite[] directionalSprites;
	public bool[] neighbourTiles;

	private SpriteRenderer spriteRenderer;

	void Start () 
	{
		spriteRenderer = GetComponent <SpriteRenderer> ();
	}

	public void SetTileSprite (int index)
	{
		if (index >= 0 && index < directionalSprites.Length)
			spriteRenderer.sprite = directionalSprites[index];
		else
			Debug.LogWarning ("Could NOT locate sprite at index: " + index);
	}
	
	public bool[] GetNeighbouringTiles (Map map)
	{
		neighbourTiles = new bool[4];

		Tile up = map.GetTile (XPosition, YPosition + 1);
		Tile down = map.GetTile (XPosition, YPosition - 1);
		Tile left = map.GetTile (XPosition - 1, YPosition);
		Tile right = map.GetTile (XPosition + 1, YPosition);

		neighbourTiles[(int)Direction.Up] = (up && up.type == type) ? true : false;
		neighbourTiles[(int)Direction.Right] = (right && right.type == type) ? true : false;
		neighbourTiles[(int)Direction.Down] = (down && down.type == type) ? true : false;
		neighbourTiles[(int)Direction.Left] = (left && left.type == type) ? true : false;

		if (neighbourTiles[(int)Direction.Up] || neighbourTiles[(int)Direction.Right] ||
		    neighbourTiles[(int)Direction.Down] || neighbourTiles[(int)Direction.Left])
			ConnectedToSameTileType = true;
		else
			ConnectedToSameTileType = false;

		return neighbourTiles;
	}

	public int XPosition { get { return Mathf.RoundToInt (transform.position.x);} set {}}
	public int YPosition { get { return Mathf.RoundToInt (transform.position.y);} set {}}
	public bool ConnectedToSameTileType {get; set;}
}











































