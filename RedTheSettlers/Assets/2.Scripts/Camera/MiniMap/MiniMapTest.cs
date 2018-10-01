using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedTheSettlers.Tiles;
using UnityEngine.UI;

namespace RedTheSettlers.GameSystem
{
    public class MiniMapTest : MonoBehaviour
    {
        //GameManager GameManager;
        //List<Tile> list;
        // Use this for initialization
        //List<BoardTile> list;
        [SerializeField]
        GameObject imagePrefab;

        public GameObject[,] TileImageGrid;
        //시작지점
         
        int count = 0;
        RectTransform rectTransform;
        GameObject tile;
        void Start()
        {
            TileImageGrid = new GameObject[GlobalVariables.BoardTileGridSize, GlobalVariables.BoardTileGridSize];
            //GameObject b = Instantiate(imagePrefab);

            rectTransform = imagePrefab.GetComponent<RectTransform>();
            //rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.position = gameObject.transform.position;
            //Debug.Log(rectTransform.rect.size);

            CreateTileImageGrid();
        }

        public void CreateTileImageGrid()
        {
            int count = 0;
            for (int z = 0; z < GlobalVariables.BoardTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BoardTileGridSize; x++)
                {
                    if (z > -x + GlobalVariables.BoardTileMinZIntercept && z < -x + GlobalVariables.BoardTileMaxZIntercept)
                    {
                        float xCoord = CalculateXcoord(x, z);
                        float zCoord = CalculateZcoord(z);
                        //TileImageGrid[x, z] = ObjectPoolManager.Instance.TileSet[count].gameObject;
                        // GameObject b = Instantiate(imagePrefab);
                        //imagePrefab = 
                        tile = TileImageGrid[x, z] = Instantiate(imagePrefab, rectTransform.position ,Quaternion.identity, gameObject.transform);
                        tile.transform.parent = gameObject.transform;
                        //rectTransform = b.GetComponent<RectTransform>();
                        rectTransform = TileImageGrid[x, z].GetComponent<RectTransform>();
                        rectTransform.sizeDelta = new Vector2(50, 50);
                        rectTransform.localScale = Vector3.one;
                        rectTransform.anchoredPosition = new Vector2(xCoord, zCoord);
                        //TileImageGrid[x, z].transform.position = new Vector3(xCoord, zCoord, 0.05f);
                        //TileImageGrid[x,z].transform.po
                        //TileImageGrid[x, z].transform.position = new Vector3(xCoord, 0.05f, zCoord);
                        //TileImageGrid[x, z].color = new Color(124, 124, 124);
                        count++;
                    }
                }
            }
        }
        public float CalculateXcoord(float x, float z)
        {
            return (x + z * 3f) * 1.74f;
        }

        public float CalculateZcoord(float z)
        {
            return z * 1.5f + 0.1f;
        }

        public void ShowBoardTile()
        {
            for (int z = 0; z < GlobalVariables.BoardTileGridSize; z++)
            {
                for (int x = 0; x < GlobalVariables.BoardTileGridSize; x++)
                {
                    if (z > -x + GlobalVariables.BoardTileMinZIntercept && z < -x + GlobalVariables.BoardTileMaxZIntercept)
                    {
                        TileImageGrid[x, z].gameObject.SetActive(true);
                    }
                }
            }
        }
        // Update is called once per frame
        void Update()
        {
            //TileImageGrid[0, 0].color = new Color(0, 0, 255 / 255);
        }
    } 
}
