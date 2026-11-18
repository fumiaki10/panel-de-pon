using System.Collections.Generic;

using System.Collections;
using Unity.Mathematics;
using UnityEngine;

 public enum PanelType
  {
    Circle,
    Diamond,
    Triangle,
    Star,
    Heart
  }
 
 
public class blocks:MonoBehaviour
{
 
 public GameObject[] PanelPrefabs;
 [System.NonSerialized]
public GameObject[,] board = new GameObject[6,9];


 
  
  void Start()
  {
    for(int x = 0;x <= 5; x++)
    {
      for(int y = 0;y <= 7; y++)
      {
        int randomPanels = UnityEngine.Random.Range(0,PanelPrefabs.Length);

       while(x >= 2 && (PanelPrefabs[randomPanels].GetComponent<Panel>().myType == board[x-1,y].GetComponent<Panel>().myType && PanelPrefabs[randomPanels].GetComponent<Panel>().myType == board[x-2,y].GetComponent<Panel>().myType)
       ||
       y >= 2 && (PanelPrefabs[randomPanels].GetComponent<Panel>().myType == board[x,y-1].GetComponent<Panel>().myType && PanelPrefabs[randomPanels].GetComponent<Panel>().myType == board[x,y-2].GetComponent<Panel>().myType)
       )
        {
          randomPanels = UnityEngine.Random.Range(0,PanelPrefabs.Length);
        }
        GameObject newPanel = Instantiate(PanelPrefabs[randomPanels],new Vector3(x + 0.5f,y + 0.5f),quaternion.identity);

        board[x,y] = newPanel;
        newPanel.transform.SetParent(this.transform);
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
      leftBlock.transform.position = new Vector3(rightX + 0.5f, y + 0.5f , 0f);
    }
    if(rightBlock != null)
    {
      rightBlock.transform.position = new Vector3(leftX + 0.5f, y + 0.5f , 0f);
    }
  StartCoroutine(CheckMatches());
  }

 IEnumerator CheckMatches()
  {
    List<Vector2Int> destroyPanel = new List<Vector2Int>();
 for(int x = 0;x <= 5; x++)
    {
      for(int y = 0;y <= 8; y++)
      {
        if(board[x,y] == null)
        {
          for(int aboveY = y + 1;aboveY <= 8; aboveY++)
          {
            if(board[x,aboveY] != null)
            {
              board[x,y] = board[x,aboveY];
              board[x,aboveY] = null;

              board[x,y].transform.position = new Vector3(x + 0.5f, y+ 0.5f , 0f);
              break;
            }
          }
        }
      }
    }
  yield return new WaitForSeconds(0.3f);


    for(int x = 0;x <= 3; x++)
    {
      for(int y = 0;y <= 8; y++)
      {
        if(board[x,y] != null && board[x+1,y] != null && board[x + 2,y] != null)
        {
          PanelType type1 = board[x,y].GetComponent<Panel>().myType;
          PanelType type2 = board[x+1,y].GetComponent<Panel>().myType;
          PanelType type3 = board[x+2,y].GetComponent<Panel>().myType;

          if(type1 == type2 && type2 == type3)
          {
            destroyPanel.Add(new Vector2Int(x,y));
            destroyPanel.Add(new Vector2Int(x+1,y));
            destroyPanel.Add(new Vector2Int(x+2,y));
          }
        }
      }
    }
    for(int x = 0;x <= 5; x++)
    {
      for(int y = 0;y <= 6; y++)
      {
        if(board[x,y] != null && board[x,y+1] != null && board[x,y+2] != null)
        {
          PanelType type1 = board[x,y].GetComponent<Panel>().myType;
          PanelType type2 = board[x,y+1].GetComponent<Panel>().myType;
          PanelType type3 = board[x,y+2].GetComponent<Panel>().myType;

          if(type1 == type2 && type2 == type3)
          {
         
            destroyPanel.Add(new Vector2Int(x,y));
            destroyPanel.Add(new Vector2Int(x,y+1));
            destroyPanel.Add(new Vector2Int(x,y+2));

          
          }
        }
      }
    }
     
   
    if(destroyPanel.Count > 0)
    {
     
       foreach(var item in destroyPanel)
    {
      if(board[item.x,item.y] != null)
      {
        Destroy(board[item.x,item.y]);
        
        board[item.x,item.y] = null;

      }
    }

  
    yield return new WaitForSeconds(0.3f);
     StartCoroutine(CheckMatches());
    }
  }

  void Update()
  {
   
  }

 
}
