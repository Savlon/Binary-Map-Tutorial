using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour 
{
	public int width;
	public int height;
	public Tile[,] map;

	public Tile[] tilePrefabs;

	void Start () 
	{
		tilePrefabs = Resources.LoadAll <Tile> ("Prefabs");
		CreateMap ();
	}

	public void CreateMap ()
	{
		map = new Tile[width, height];
		for (int y = 0; y < map.GetLength (1); y++)
		{
			for (int x = 0; x < map.GetLength (0); x++)
			{
				Tile tile = Instantiate (tilePrefabs[Random.Range (0, tilePrefabs.Length)], new Vector3 (x, y, 0), Quaternion.identity) as Tile;
				tile.transform.parent = transform;
				map[x, y] = tile;
			}
		}
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
			StartCoroutine (CheckTiles ());
	}

	public IEnumerator CheckTiles ()
	{
		for (int y = 0; y < map.GetLength (1); y++)
		{
			for (int x = 0; x < map.GetLength (0); x++)
			{
				Tile tile = GetTile (x, y);
				bool[] neighbours = tile.GetNeighbouringTiles (this);

				bool up = neighbours[(int)Direction.Up];
				bool right = neighbours[(int)Direction.Right];
				bool down = neighbours[(int)Direction.Down];
				bool left = neighbours[(int)Direction.Left];

				int index = (up ? 0x1 : 0) | (right ? 0x2 : 0) | (down ? 0x4 : 0) | (left ? 0x8 : 0);

				tile.SetTileSprite (index);

			}
		}
		yield return null;
	}

	public Tile GetTile (int x, int y)
	{
		if (x >= 0 && y >= 0 && x < width && y < height)
			return map[x, y];
		return null;
	}
}














