using Shapes2D;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
 public enum PanelType
  {
    Circle,
    Diamond,
    Heart,
    Star,
    Triangle
  }
public class blocks:MonoBehaviour
{
   
  public GameObject[] PanelPrefabs;

[System.NonSerialized]
  public GameObject[,] board = new GameObject[6,8];
  
  void Start()
  {
    for(int x = 0;x <= 5; x++)
    {
   for(int y = 0;y <= 7; y++)
      {
        int randomBlocks = UnityEngine.Random.Range(0,PanelPrefabs.Length);
          GameObject newBlock = Instantiate(PanelPrefabs[randomBlocks],new Vector3(x + 0.5f ,y + 0.5f),quaternion.identity);  
          board[x,y] = newBlock;

      newBlock.transform.SetParent(this.transform);
      }
    }
  }

   public void SwapBlocks(int cursorX,int cursorY)
  {
   int leftX = cursorX;
   int rightX = cursorX + 1;
   int y = cursorY;

   if(rightX >= 6) return;

   GameObject leftBlock = board[leftX,y];
   GameObject rightBlock = board[rightX,y];

   board[leftX,y] = rightBlock;
   board[rightX,y] = leftBlock;

   if(leftBlock != null)
    {
      leftBlock.transform.position = new Vector3(rightX + 0.5f, y+ 0.5f , 0f);
    }
    if(rightBlock != null)
    {
      rightBlock.transform.position = new Vector3(leftX +0.5f , y+ 0.5f , 0f);
    }
Debug.Log($"({cursorX},{cursorY})とのその右のブロックの入れ替えが完了");
CheckMatches();

  }
  void CheckMatches()
  {
    // PanelType type = board[x,y].GetComponent<Panel>().myType;
    for(int x = 0;x <= 3; x++)
    {
      for(int y = 0;y <= 7; y++)
      {
        if(board[x,y] != null && board[x + 1,y] != null && board[x + 2,y] != null)
        {
          PanelType type1 = board[x,y].GetComponent<Panel>().myType;
          PanelType type2 = board[x + 1,y].GetComponent<Panel>().myType;
          PanelType type3 = board[x + 2,y].GetComponent<Panel>().myType;

          if(type1 == type2 && type2 == type3)
          {
            Debug.Log($"横で三つそろった！種類:{type1}場所:({x},{y})");
          }
        }
      }
    }
  }
  void Update()
  {
   
  }

 
}
